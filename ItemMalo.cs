﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMalo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Personaje>().resetear();
        GameObject.FindGameObjectWithTag("ManejadorFondo").GetComponent<Fondo>().resetear();
        Destroy(gameObject);
    }
}
