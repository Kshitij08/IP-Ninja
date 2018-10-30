using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portions : MonoBehaviour {

    public GameObject player;

    public Text portionText;
    public Button fullHealth;
    public Button halfHealth;
    public Button dontRevive;

    public Canvas portionCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //GameControl.control.coins
	}

    public void OnFullHealth()
    {
        if(GameControl.control.coins >= 50)
        {
            
        }
    }

    public void OnHalfHealth()
    {

    }


    public void OnDontRevive()
    {

    }
}
