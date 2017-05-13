using UnityEngine;
using System.Collections;

public class PersonajeSimple : MonoBehaviour
{

    /// <summary>
    /// Permite mover un gameobject, como un personaje, constantemente a la derecha mediante un Rigid Body 2D
    /// y sin fricción. La fuerza utilizada para avanzar y saltar se pueden cambiar
    /// en el editor.
    /// 
    /// José Sánchez
    /// </summary>
	
    public float fuerzaMov = 5;
    public float fuerzaSalto = 400;

    private Rigidbody2D cuerpo;
    private bool sobrePlataforma;
    private Vector2 vectorSaltar;
    private float modificador;
    private bool sobrePlatPrimeraVez;

    // Use this for initialization
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        vectorSaltar = Vector2.up * fuerzaSalto;
        modificador = 1;

        

        // Desactivando friccion
        PhysicsMaterial2D sinFriccion = new PhysicsMaterial2D("sinFriccion");
        sinFriccion.friction = 0;
        gameObject.GetComponent<Collider2D>().sharedMaterial = sinFriccion;

        cuerpo.constraints = RigidbodyConstraints2D.FreezeRotation;
        //resetear();
    }

    /*public void resetear()
    {
        modificador = 1;
        cuerpo.velocity = Vector2.zero;
        GameObject reinicio = GameObject.FindGameObjectWithTag("Respawn");

        transform.position = reinicio.transform.position;
       
        

        sobrePlatPrimeraVez = false;
    }*/

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (sobrePlatPrimeraVez)
        {
            cuerpo.velocity = new Vector2(fuerzaMov * modificador, cuerpo.velocity.y);
            Debug.Log("y=" + cuerpo.velocity.y);
            if (Input.GetKey(KeyCode.Space) && cuerpo.velocity.y == 0)
            {
                print("saltando");
                cuerpo.AddForce(vectorSaltar, ForceMode2D.Force);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D colision)
    {
        if (!sobrePlatPrimeraVez)
        {
            sobrePlatPrimeraVez = true;
        }
    }

    public void acelerar()
    {
        StopAllCoroutines();
        StartCoroutine(modificarVelocidad(2));
    }

    public void desacelerar()
    {
        StopAllCoroutines();
        StartCoroutine(modificarVelocidad(0.5f));
    }

    IEnumerator modificarVelocidad(float valorModificador)
    {
        modificador = valorModificador;
        yield return new WaitForSeconds(2);
        modificador = 1;
    }

    
}
