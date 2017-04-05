using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BajarVidaTotal : MonoBehaviour {
    /// <summary>
    /// Todo lo que tenga este script le quitará
    /// toda la vida al personaje principal
    /// 
    /// José Sánchez
    /// </summary>

    public GameObject BarraDeVida;

    private ManejadorVida manVida;

    // Use this for initialization
    void Start () {
        manVida = BarraDeVida.GetComponent<ManejadorVida>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            manVida.bajarVidaTotal();
            collision.gameObject.GetComponent<PlayerController2D>().detener();
        }
        
    }
}
