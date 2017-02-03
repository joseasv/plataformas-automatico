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

    private GameObject fondoDuplicado;
    private GameObject fondoActivo;

    private Vector3 posInicial;

    // Use this for initialization
    void Start()
    {
        camara = Camera.main.transform;
        anchoFondo = fondo.GetComponent<SpriteRenderer>().sprite.bounds.size.x * fondo.transform.localScale.x;

        posInicial = fondo.transform.position;
        print("posInicial " + posInicial);

       
        fondoDuplicado = Instantiate(fondo, posInicial, Quaternion.identity) as GameObject;

        fondoActivo = fondo;
    }

    public void resetear()
    {
        fondo.transform.position = posInicial;
        print("posInicial " + posInicial);
        fondoDuplicado.transform.position = posInicial;
        fondoActivo = fondoDuplicado;
    }

    // Update is called once per frame
    void Update()
    {
        if (camara.position.x > fondoActivo.transform.position.x)
        {
            
            if (fondoActivo == fondoDuplicado)
            {

                fondo.transform.position += Vector3.right * anchoFondo;
                fondoActivo = fondo;
                print("moviendo fondo original");
            }
            else
            {
                fondoDuplicado.transform.position += Vector3.right * anchoFondo;
                fondoActivo = fondoDuplicado;
                print("moviendo fondo duplicado");
            }
        }
    }
}
