using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}

    public void OnPlayClick()
    {
        SceneManager.LoadScene("Level1.1");
    }
    

    public void OnHelpClick()
    {
        SceneManager.LoadScene("Help");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
