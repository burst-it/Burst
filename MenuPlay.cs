﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPlay : MonoBehaviour {

	// Use this for initialization
	public void LoadScene()
    {
        SceneManager.LoadScene("scene_test");
    }
}
