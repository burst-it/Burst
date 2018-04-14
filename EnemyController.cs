using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	NavMeshAgent g_agent;
	GameObject g_target;

	public int g_maxHealth;
	public int g_health;

	public bool isDead;


	// Use this for initialization
	void Start () {
		g_agent = GetComponent<NavMeshAgent>();
		g_target = GameObject.FindGameObjectWithTag ("Player");

		g_health = g_maxHealth;
	}



	// Update is called once per frame
	void Update () {
		if (!isDead) {
			g_agent.SetDestination (g_target.transform.position);
		} else {
			GameObject.Destroy (this.gameObject);
		}
	}


	/*************************************************************************************************************/
	/*												FONCTIONS		  											 */
	/*************************************************************************************************************/



	public void getHit(int p_damage){
		g_health -= p_damage;
		if(g_health<=0){
			g_health = 0;
			isDead = true;
		}
	}


}
