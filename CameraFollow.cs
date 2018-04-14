using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	// The target we are following
	public Transform g_target;
	// The distance in the x-z plane to the target
	public float g_distance = 10.0f;
	// the height we want the camera to be above the target
	public float g_height = 5.0f;
	// How much we 
	public float g_heightDamping = 2.0f;

	void Start(){
	}

	void  LateUpdate ()
	{
		// Early out if we don't have a target
		if (!g_target) {
			g_target = GameObject.FindGameObjectWithTag ("Player").transform;
			return;
		}

		// Calculate the current rotation angles
		float wantedHeight = g_target.position.y + g_height;
		float currentHeight = transform.position.y;

		// Damp the height
		currentHeight = Mathf.Lerp (currentHeight, wantedHeight, g_heightDamping * Time.deltaTime);

		// Set the position of the camera on the x-z plane to:
		// distance meters behind the target
		transform.position = g_target.position;
		transform.position -= Vector3.forward * g_distance;

		// Set the height of the camera
		transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

		// Always look at the target
		transform.LookAt (g_target);
	}

}
