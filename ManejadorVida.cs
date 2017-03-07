using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class ManejadorVida : MonoBehaviour {

    public Image vida;
    public int puntosDeVidaMaximos;
    private float vidaActual;
    private int puntosVidaActual;
    private float vidaLlena = 1;

	// Use this for initialization
	void Start () {
        vida.fillAmount = vidaLlena;
        /*vida.type = Image.Type.Filled;
        vida.fillMethod = Image.FillMethod.Horizontal;*/
        print(vida.fillAmount);
        puntosVidaActual = puntosDeVidaMaximos;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    

    public void bajarVida()
    {
        puntosVidaActual--;
        vida.fillAmount = (puntosVidaActual * vidaLlena)/puntosDeVidaMaximos ;
      
    }
}
