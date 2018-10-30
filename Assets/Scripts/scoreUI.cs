using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreUI : MonoBehaviour {

    public Text scoreText;

    // Use this for initialization
    void Start () {
        scoreText.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "" + GameControl.control.coins;
        //scoreText.text = "" + Player.score;
    }
}
