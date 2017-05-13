using UnityEngine;
using System.Collections;

public class Personaje : MonoBehaviour
{

    /// <summary>
    /// Permite mover un gameobject, como un personaje, constantemente a la derecha mediante un Rigid Body 2D
    /// y sin fricción. La fuerza utilizada para avanzar y saltar se pueden cambiar
    /// en el editor.
    /// 
    /// José Sánchez
    /// </summary>
	
    public float fuerzaMov;
    public float fuerzaSalto;

    private Rigidbody2D cuerpo;
    private BoxCollider2D colisionador;
    private SpriteRenderer sprite;
    private Animator animador;
    private LayerMask capaEnemigos;
    private bool sobrePlataforma;
    private Vector2 vectorSaltar;
    private float modificador;
    private bool sobrePlatPrimeraVez;

    private bool invencible;
    private bool controlActivo;

    public GameObject BarraDeVida;

    private ManejadorBarra manVida;

    // Use this for initialization
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        colisionador = GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animador = GetComponentInChildren<Animator>();
        capaEnemigos = LayerMask.GetMask("Enemigo");

        vectorSaltar = Vector2.up * fuerzaSalto;
        modificador = 1;

        fuerzaMov = 5;
        fuerzaSalto = 400;

        if (BarraDeVida != null)
        {
            manVida = BarraDeVida.GetComponent<ManejadorBarra>();
        }

        // Desactivando friccion
        PhysicsMaterial2D sinFriccion = new PhysicsMaterial2D("sinFriccion");
        sinFriccion.friction = 0;
        gameObject.GetComponent<Collider2D>().sharedMaterial = sinFriccion;

        controlActivo = true;
        invencible = false;

        resetear();
    }

    public void resetear()
    {
        modificador = 1;
        cuerpo.velocity = Vector2.zero;
        GameObject reinicio = GameObject.FindGameObjectWithTag("Respawn");

        transform.position = reinicio.transform.position;
       
        

        sobrePlatPrimeraVez = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (sobrePlatPrimeraVez)
        {
            if (!invencible)
            {
                cuerpo.velocity = new Vector2(fuerzaMov * modificador, cuerpo.velocity.y);

                if (Input.GetKey(KeyCode.Space) && cuerpo.velocity.y == 0 && controlActivo)
                {
                    print("saltando");
                    cuerpo.AddForce(vectorSaltar, ForceMode2D.Force);
                }
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
        //StopAllCoroutines();
        StartCoroutine(modificarVelocidad(2));
    }

    public void desacelerar()
    {
        //StopAllCoroutines();
        StartCoroutine(modificarVelocidad(0.5f));
    }

    IEnumerator modificarVelocidad(float valorModificador)
    {
        modificador = valorModificador;
        yield return new WaitForSeconds(2);
        modificador = 1;
    }

    public void modHPPorItem(int cantHP)
    {
        if (BarraDeVida != null)
        {
            BarraDeVida.GetComponent<ManejadorBarra>().modVida(cantHP);
        }

    }

    public void herir(Vector2 posEnemigo)
    {
        if (!invencible)
        {
            if (BarraDeVida != null)
            {
                manVida.bajarVida();
            }

            if (posEnemigo.x > transform.position.x)
            {
                print("impulso hacia atras");
                //_motor.GetComponent<Rigidbody2D>().AddForce(new Vector2(-5, -5), ForceMode2D.Impulse);
                cuerpo.AddForce(new Vector2(-200, 100), ForceMode2D.Force);
                
            }
            else
            {
                print("impulso hacia delante");
                //_motor.GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 5), ForceMode2D.Impulse);
                cuerpo.AddForce(new Vector2(200, 100), ForceMode2D.Force);
                //cuerpo.velocity = new Vector2(10, 5);
            }
            StartCoroutine(herido());
        }
    }

    private IEnumerator herido()
    {
        //colisionador.enabled = false;
        invencible = true;
        for (int i = 0; i < 4; i++)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        invencible = false;
        //colisionador.enabled = true;
    }

    public void detener()
    {

        controlActivo = false;
        cuerpo.velocity = new Vector2(0, 0);
        cuerpo.isKinematic = true;
    }

}
