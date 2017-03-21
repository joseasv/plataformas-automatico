using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

    public int modHP;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (modHP != 0)
            {
                collision.GetComponent<PlayerController2D>().modHPPorItem(modHP);
            }
            Destroy(gameObject);
        }
        
    }
}
