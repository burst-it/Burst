using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour {

	public float g_healthProvided;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, 100.0f * Time.deltaTime, Space.World);
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			other.gameObject.GetComponent<PlayerController> ().getHeal (g_healthProvided);
			Destroy(this.gameObject.transform.parent.gameObject);
		}
	}
}
