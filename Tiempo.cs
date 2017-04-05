using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiempo : MonoBehaviour {

    public Text texto;
    public int cantSegundos = 300;

	// Use this for initialization
	void Start () {
        texto.text = cantSegundos.ToString();
        StartCoroutine(segundero());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void actualizarContador()
    {
        
    
        texto.text = cantSegundos.ToString();
        if (cantSegundos == 0)
        {
            Debug.Log("se acabo el tiempo");
        }
    }

    IEnumerator segundero()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            cantSegundos--;
            actualizarContador();
        }
        
    }

    public void subirTiempo(int segundos)
    {
        cantSegundos += segundos;
    }
}
