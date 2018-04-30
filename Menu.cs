using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	// Use this for initialization
	public void LoadScene()
    {
        SceneManager.LoadScene("scene_test");
    }

    public void Parameters()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
