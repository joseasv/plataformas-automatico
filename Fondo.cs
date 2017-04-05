using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fondo : MonoBehaviour
{

    ///<summary>
    /// Repite una imagen (Contenida en un gameobject) para usarla como fondo. Crea un gameobject
    /// con el mismo fondo y los va alternando entre el original y el creado a medida que avanza la cámara.
    /// 
    /// José Sánchez
    ///</summary>
    public GameObject fondo;

    private Transform camara;
    private float anchoFondo;

    public GameObject fondoDuplicado;
    public GameObject fondoActivo;

    private Vector3 posInicial;

    // Use this for initialization
    void Start()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera").transform;
        anchoFondo = fondo.GetComponent<SpriteRenderer>().sprite.bounds.size.x * fondo.transform.localScale.x;

        posInicial = fondo.transform.position;
       

       
        fondoDuplicado = Instantiate(fondo, posInicial, Quaternion.identity) as GameObject;

        fondoActivo = fondo;
        
    }

    public void resetear()
    {
        fondo.transform.position = posInicial;
  
        fondoDuplicado.transform.position = posInicial;
        fondoActivo = fondoDuplicado;
    }

    // Update is called once per frame
    void Update()
    {
        if (camara.position.x > fondoActivo.GetComponent<Renderer>().bounds.center.x)
        {
            
            if (fondoActivo == fondoDuplicado)
            {

                fondo.transform.position += Vector3.right * anchoFondo;
                fondoActivo = fondo;
                
            }
            else
            {
                fondoDuplicado.transform.position += Vector3.right * anchoFondo;
                fondoActivo = fondoDuplicado;
                
            }
        }
       
    }
}
