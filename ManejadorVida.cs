using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class ManejadorVida : MonoBehaviour {
    /// <summary>
    /// Controla el relleno de la barra de vida como tal. Al vaciarse 
    /// la barra de vida se recarga la escena
    /// 
    /// José Sánchez
    /// </summary>

    public Image vida;
    public int puntosDeVidaMaximos;
    private float vidaActual;
    private int puntosVidaActual;
    private float vidaLlena = 1;

	// Use this for initialization
	void Start () {
        vida.fillAmount = vidaLlena;
        puntosVidaActual = puntosDeVidaMaximos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void bajarVida()
    {
        puntosVidaActual--;
        vida.fillAmount = (puntosVidaActual * vidaLlena)/puntosDeVidaMaximos;

        if (puntosVidaActual == 0)
        {
            StartCoroutine(recargarEscena());
        }
      
    }

   

    IEnumerator recargarEscena()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void modVida(int cantidad)
    {
        puntosVidaActual += cantidad;
        vida.fillAmount = (puntosVidaActual * vidaLlena) / puntosDeVidaMaximos;
    }

    public void bajarVidaTotal()
    {
        puntosVidaActual = 0;
        vida.fillAmount = 0;
        StartCoroutine(recargarEscena());
    }
}
