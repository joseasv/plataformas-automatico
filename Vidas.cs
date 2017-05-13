using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Vidas : MonoBehaviour {

    public int vidas;
    public string nombreEscenaFin;
    private Text texto;
    private ManejadorBarra manBarra;

	// Use this for initialization
	void Start () {
        texto = GetComponentInChildren<Text>();
        Debug.Log("vidas " + vidas);
        if (!PlayerPrefs.HasKey("vidas"))
        {
            Debug.Log("no existe vidas");
            PlayerPrefs.SetInt("vidas", vidas);
        }
        else
        {
            vidas = PlayerPrefs.GetInt("vidas");
            Debug.Log("si existe vidas y vale " + vidas);
        }
        actualizarTexto();

        manBarra = FindObjectOfType<ManejadorBarra>();
	}

    public void aumentarVida()
    {
        vidas++;
        actualizarTexto();
    }

    public void disminuirVida()
    {
        vidas--;
        actualizarTexto();
        manBarra.bajarVidaTotal();
        if (vidas == 0)
        {
            StartCoroutine(juegoTerminado(3f));
        }
        else
        {
            StartCoroutine(recargarEscena(2f));
        }
    }

    private void actualizarTexto()
    {
        texto.text = vidas.ToString();
        PlayerPrefs.SetInt("vidas", vidas);
    }

    IEnumerator juegoTerminado(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene(nombreEscenaFin);


    }

    IEnumerator recargarEscena(float segundos)
    {
        
        yield return new WaitForSeconds(segundos);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }

    // Update is called once per frame
    void Update () {
		
	}
}
