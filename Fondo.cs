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


    // Use this for initialization
    void Start()
    {
        camara = Camera.main.transform;
        anchoFondo = fondo.GetComponent<SpriteRenderer>().sprite.bounds.size.x * fondo.transform.localScale.x;

        Vector3 posFondoDuplicado = new Vector3(fondo.transform.position.x + anchoFondo, fondo.transform.position.y, fondo.transform.position.z);
        fondoDuplicado = Instantiate(fondo, posFondoDuplicado, Quaternion.identity) as GameObject;

        fondoActivo = fondoDuplicado;
    }

    // Update is called once per frame
    void Update()
    {
        if (fondoActivo.transform.position.x < camara.position.x)
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
