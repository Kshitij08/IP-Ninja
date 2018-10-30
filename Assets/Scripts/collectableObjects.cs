using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectableObjects : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Player.score += 10;
            GameControl.control.coins += 10;
            Debug.Log(Player.score);
            Destroy(gameObject);
        }
    }
}
