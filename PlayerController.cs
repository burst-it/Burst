using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	GameObject g_shootingScriptObject;

    public Animator animator;

    public float g_walkSpeed = 1.0f;
    public GameObject life_bar;

	Animator g_anim;                      // Reference to the animator component.
	int g_groundMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float g_camRayLength = 100f;          // The length of the ray from the camera into the scene.


	public float g_maxHealth;
	public float g_health;

	public bool g_isDead;

	public int g_scorePoints;
	LootBoxScript g_selectLootBox;
	bool g_isLootBoxSelected; 


    void Start () {

		g_shootingScriptObject = transform.Find ("GunBarrelEnd").gameObject;

        // Create a layer mask for the floor layer.
        g_groundMask = LayerMask.GetMask ("Ground");

		// Set up references.
		//anim = GetComponent <Animator> ();

		g_health = g_maxHealth;
        life_bar.transform.Find("Text").GetComponent<Text>().text = g_health.ToString();
        life_bar.transform.Find("Life").GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);

        g_isDead = false;

		g_scorePoints = 0;
		g_isLootBoxSelected = false;
		g_selectLootBox = null;
    }



	void Update () {
		
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");
        if (h!=0 || v!=0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }


		if (!g_isDead) {

			if (g_isLootBoxSelected) {
				if(g_scorePoints>=g_selectLootBox.g_lootPrice){
					if(Input.GetKeyDown(KeyCode.E)){
						g_scorePoints -= g_selectLootBox.g_lootPrice;
						g_shootingScriptObject.GetComponent<ShootingController> ().g_damagePerShot = g_selectLootBox.g_damagePerShot;
						g_shootingScriptObject.GetComponent<ShootingController> ().g_timeBetweenBullets=g_selectLootBox.g_timeBetweenBullets;
					}
				}
			}

			// Move the player around the scene.
			Move (h, v);

			// Turn the player to face the mouse cursor.
			Turning ();
		}
    }



	/*************************************************************************************************************/
	/*												FONCTIONS		  											 */
	/*************************************************************************************************************/


	void Move (float p_h, float p_v)
	{
		var x = p_h * g_walkSpeed;
		var z = p_v * g_walkSpeed;

		var move = new Vector3(x, 0f, z);
		transform.position += Vector3.ClampMagnitude(move, g_walkSpeed) * Time.deltaTime;
	}



	void Turning ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, g_camRayLength, g_groundMask))
		{
			transform.LookAt (new Vector3(floorHit.point.x, transform.position.y, floorHit.point.z));
		}
	}



	void Animating (float p_h, float p_v)
	{
		// Create a boolean that is true if either of the input axes is non-zero.
		bool walking = p_h != 0f || p_v != 0f;

		// Tell the animator whether or not the player is walking.
		g_anim.SetBool ("IsWalking", walking);
	}



	public void getHit(float p_damage){
		g_health -= p_damage;
		if(g_health<=0f){
			g_health = 0f;
			g_isDead = true;
		}
        life_bar.transform.Find("Text").GetComponent<Text>().text = g_health.ToString();
        var lostHealth = g_maxHealth - g_health;
        life_bar.transform.Find("Life").GetComponent<RectTransform>().anchorMax = new Vector2(g_health / g_maxHealth, 1);
    }



	public void getHeal(float p_heal){
		g_health += p_heal;
		if(g_health>g_maxHealth){
			g_health = g_maxHealth;
		}
		life_bar.transform.Find("Text").GetComponent<Text>().text = g_health.ToString();
		life_bar.transform.Find("Life").GetComponent<RectTransform>().anchorMax = new Vector2(g_health / g_maxHealth, 1);
	}



	public void setSelectedLootBox(GameObject p_lootBox){
		g_isLootBoxSelected = true;
		g_selectLootBox = p_lootBox.GetComponent<LootBoxScript> ();
	}

	public void resetSelectedLootBox(){
		g_isLootBoxSelected = false;
		g_selectLootBox = null;
	}

}
