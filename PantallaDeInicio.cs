using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PantallaDeInicio : MonoBehaviour {

    public RawImage pantalla;
    public float velocTransicion = 1.5f;

    private bool botonesActivados;
	// Use this for initialization
	void Start () {
        
        pantalla.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        botonesActivados = true;
        pantalla.color = Color.clear;
        pantalla.raycastTarget = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void iniciarNivel1(string nombreEscena)
    {
        if (botonesActivados)
        {
            Debug.Log("iniciando transicion");
            StartCoroutine(cambioAEscena(nombreEscena));
            botonesActivados = false;
        }
        
    }

    IEnumerator cambioAEscena(string nombreEscena)
    {
        //pantalla.canvas.sortingLayerID = 1;
        while (pantalla.color != Color.black)
        {
            pantalla.color = Color.Lerp(pantalla.color, Color.black, velocTransicion * Time.deltaTime);
            yield return null;
        }
        SceneManager.LoadScene(nombreEscena);
        yield return null;
    }

   
}
