using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara : MonoBehaviour {

    /// <summary>
    /// Este script está pensando para ser utilizado en una cámara. Mueve la cámara siguiendo el 
    /// movimiento de otro gameobject, como un personaje. No tiene movimiento vertical.
    /// La cámara se puede centrar en el personaje o moverse desde la posición que se deja
    /// en la escena.
    /// 
    /// José Sánchez
    /// </summary>
    public Transform objetivo;
    public bool centrada;

    private Vector3 posInicial;

	// Use this for initialization
	void Start () {
        posInicial = transform.position;
  
	}
	
	// Update is called once per frame
	void Update () {

        float nuevaPosX = objetivo.position.x;

        if (!centrada)
        {
            nuevaPosX += posInicial.x;
        }
        
        transform.position = new Vector3(nuevaPosX, posInicial.y, posInicial.z);
    }
}
