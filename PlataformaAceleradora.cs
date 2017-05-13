using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaAceleradora : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
     
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("en plataforma aceleradora");
        if (collision.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController2D>().acelerar();
        }
    }

}
