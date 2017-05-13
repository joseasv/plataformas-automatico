using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Historia : MonoBehaviour {
    /// <summary>
    /// Muestra una serie de imagenes almacenadas en una lista de GameObjects.
    /// Las imagenes van cambiando mediante un click. Al llegar a la ultima imagen
    /// la escena siguiente inicial luego de un retardo de 3 segundos
    /// 
    /// José Sánchez
    /// </summary>

    public List<GameObject> imagenes;
    public string nombreProxEscena;

    private int numImagenes;
    private int imagenActual;
    private bool ultimaImagen;
    

	// Use this for initialization
	void Start () {


        imagenActual = 0;
        numImagenes = imagenes.Count;
        ultimaImagen = false;
        foreach (GameObject item in imagenes)
        {
            item.GetComponent<SpriteRenderer>().enabled = false;
        }
        Debug.Log("imagenes.Count " + imagenes.Count);
        imagenes[imagenActual].GetComponent<SpriteRenderer>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
        
		if ((Input.GetMouseButtonDown(0) || (Input.GetButtonDown("Fire2"))) && (!ultimaImagen))
        {
            
            imagenes[imagenActual].GetComponent<SpriteRenderer>().enabled = false;
            imagenActual++;
            imagenes[imagenActual].GetComponent<SpriteRenderer>().enabled = true;
            if (imagenActual == numImagenes - 1)
            {
                ultimaImagen = true;
                StartCoroutine(proximaEscena());
            }
        }
        
	}

    IEnumerator proximaEscena()
    {
        
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(nombreProxEscena);
    }


}
