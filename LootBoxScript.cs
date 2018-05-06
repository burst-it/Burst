using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBoxScript : MonoBehaviour {

	public int g_lootPrice;

	public float g_damagePerShot;
	public float g_timeBetweenBullets;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			other.gameObject.GetComponent<PlayerController> ().setSelectedLootBox (this.gameObject);
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player"){
			other.gameObject.GetComponent<PlayerController> ().resetSelectedLootBox();
		}
	}
}
