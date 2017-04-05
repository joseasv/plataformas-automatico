using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Meta : MonoBehaviour {
    /// <summary>
    /// Al colisionar con un GameObject con este script la escena
    /// termina. Por ejemplo, al llegar al final de un nivel
    /// 
    /// José Sánchez
    /// </summary>

    public string proxEscena;
  

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController2D personaje = collision.gameObject.GetComponent<PlayerController2D>();
            personaje.detener();
            StartCoroutine(terminarEscena());
        }
    }

    IEnumerator terminarEscena()
    {
        yield return new WaitForSeconds(3);
        GameObject.FindObjectOfType<ManejadorTransicion>().iniciarTransProxEscena(proxEscena);
    }

  }
