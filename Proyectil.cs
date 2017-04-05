using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController2D>().herir(transform.position);
            Destroy(gameObject);
        }


    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
