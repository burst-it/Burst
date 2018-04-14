﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	NavMeshAgent g_agent;
	GameObject g_target;

	public int g_maxHealth;
	public int g_health;

	public bool g_isDead;

	public float g_attackRange;
	public int g_attackDamage;
	public float g_timeBetweenAttacks = 0.15f;        // The time between each shot.
	float g_timer;                                    // A timer to determine when to fire.


	// Use this for initialization
	void Start () {
		g_agent = GetComponent<NavMeshAgent>();
		g_target = GameObject.FindGameObjectWithTag ("Player");

		g_health = g_maxHealth;
		g_isDead = false;
	}



	// Update is called once per frame
	void Update () {
		g_timer += Time.deltaTime;

		if (!g_isDead) {
			g_agent.SetDestination (g_target.transform.position);
			if (g_agent.remainingDistance < g_attackRange) {
				g_agent.isStopped = true;
				if(g_timer >= g_timeBetweenAttacks){
					Attack ();
				}
			} else {
				g_agent.isStopped = false;
			}
		} else {
			this.enabled = false;
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
			g_isDead = true;
		}
	}



	void Attack(){
		g_timer = 0f;
		PlayerController playerCtrl = g_target.GetComponent <PlayerController> ();
		if(playerCtrl != null)
		{
			playerCtrl.getHit (g_attackDamage);
		}
	}



}
