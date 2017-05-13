using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    /// <summary>
    /// El item a coleccionar en el juego. El item pueden modificar la vida del 
    /// personaje, el tiempo del contador y por ultimo otorgar un puntaje
    /// 
    /// José Sánchez
    /// </summary>

    public int modHP;
    public int modPuntaje;
    public int modTiempo;
    public bool especial;
    private Puntaje puntaje;
    private Tiempo tiempo;
    private ContadorItemEsp contEspecial;

	// Use this for initialization
	void Start () {
        puntaje = FindObjectOfType<Puntaje>();
        tiempo = FindObjectOfType<Tiempo>();
        contEspecial = FindObjectOfType<ContadorItemEsp>();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (modHP != 0)
            {
                collision.GetComponent<PlayerController2D>().modHPPorItem(modHP);
            }

            if (modPuntaje != 0 && puntaje != null)
            {
                puntaje.subirPuntaje(modPuntaje);
            }

            if (modTiempo !=0 && tiempo != null)
            {
                tiempo.subirTiempo(modTiempo);
            }

            if (especial && contEspecial != null)
            {
                contEspecial.activarItem();
            }

            Destroy(gameObject);
        }
        
    }
}
