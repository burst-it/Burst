using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private bool isPaused = false;


	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {

		//Echap = menu
		if (Input.GetKeyDown(KeyCode.Escape))
			isPaused = !isPaused;
		if (isPaused)
			Time.timeScale = 0f;
		else
			Time.timeScale = 1.0f;	
	}
}
