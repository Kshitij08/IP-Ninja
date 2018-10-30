using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour {

    [SerializeField]
    private string targetTag;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == targetTag)
        {
            GetComponent<Collider2D>().enabled = false;
        }
    }

}
