using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puntaje : MonoBehaviour {

    public Text texto;
    private int puntajeActual = 0;

	// Use this for initialization
	void Start () {
		
	}
	
    private void actualizarPuntaje()
    {
        texto.text = puntajeActual.ToString();
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void subirPuntaje(int puntos)
    {
        puntajeActual += puntos;
        actualizarPuntaje();
    }
}
