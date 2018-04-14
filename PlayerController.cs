﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float walkSpeed = 1.0f;

	Animator g_anim;                      // Reference to the animator component.
	int g_groundMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
	float g_camRayLength = 100f;          // The length of the ray from the camera into the scene.



    void Start () {
		// Create a layer mask for the floor layer.
		g_groundMask = LayerMask.GetMask ("Ground");

		// Set up references.
		//anim = GetComponent <Animator> ();
		
	}



	void Update () {
		
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");

		// Move the player around the scene.
		Move (h, v);

		// Turn the player to face the mouse cursor.
		Turning ();
    }



	/*************************************************************************************************************/
	/*												FONCTIONS		  											 */
	/*************************************************************************************************************/


	void Move (float p_h, float p_v)
	{
		var x = p_h * walkSpeed;
		var z = p_v * walkSpeed;

		var move = new Vector3(x, 0f, z);
		transform.position += Vector3.ClampMagnitude(move, walkSpeed) * Time.deltaTime;
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


}
