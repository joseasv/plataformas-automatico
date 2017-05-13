using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class ManejadorBarra : MonoBehaviour {
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
    private Vidas manVidas;

	// Use this for initialization
	void Start () {
        vida.fillAmount = vidaLlena;
        puntosVidaActual = puntosDeVidaMaximos;
        manVidas = FindObjectOfType<Vidas>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void bajarVida()
    {
        puntosVidaActual--;
        vida.fillAmount = (puntosVidaActual * vidaLlena)/puntosDeVidaMaximos;
        Debug.Log(puntosVidaActual);
        if (puntosVidaActual == 0)
        {
            //StartCoroutine(recargarEscena(0.5f));
            manVidas.disminuirVida();
        }
      
    }

   

    

    public void modVida(int cantidad)
    {
        puntosVidaActual += cantidad;

        if (puntosVidaActual < 0)
        {
            puntosVidaActual = 0;
        }
        else
        {
            if(puntosVidaActual >= puntosDeVidaMaximos)
            {
                puntosVidaActual = puntosDeVidaMaximos;
            }
        }

        vida.fillAmount = (puntosVidaActual * vidaLlena) / puntosDeVidaMaximos;
        Debug.Log(puntosVidaActual);
        if (puntosVidaActual == 0)
        {
            //StartCoroutine(recargarEscena(0.5f));
            manVidas.disminuirVida();
        }
    }

    public void bajarVidaTotal()
    {
        puntosVidaActual = 0;
        vida.fillAmount = 0;
        //StartCoroutine(recargarEscena(3f));
    }
}
