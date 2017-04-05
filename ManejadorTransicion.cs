using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManejadorTransicion : MonoBehaviour {
    /// <summary>
    /// Controla la transicion inicial de la escena
    /// y la transicion para pasar a otra escena
    /// 
    /// José Sánchez
    /// </summary>

    public RawImage pantalla;
    public float velocTransicion = 1.5f;
    

    // Use this for initialization
    void Start () {
        pantalla.color = Color.black;
        pantalla.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        pantalla.raycastTarget = false;
        StartCoroutine(cambioAEscena());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void iniciarTransProxEscena(string nombreProxEscena)
    {
        StartCoroutine(proximaEscena(nombreProxEscena));
    }

    IEnumerator cambioAEscena()
    {

        while (pantalla.color != Color.clear)
        {
            pantalla.color = Color.Lerp(pantalla.color, Color.clear, velocTransicion * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }

    IEnumerator proximaEscena(string nombreProxEscena)
    {
        while (pantalla.color != Color.black)
        {
            pantalla.color = Color.Lerp(pantalla.color, Color.black, velocTransicion * Time.deltaTime);
            yield return null;
        }
        SceneManager.LoadScene(nombreProxEscena);
    }
}
