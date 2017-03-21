using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompEnemigo : MonoBehaviour
{

    
    public float fuerzaMov = 2;
    public bool evitaCaerse;
    public bool perseguir;
    public bool miraALaDerecha;
    public bool inmovil;
    public int vida = 1;
    /*public bool lanzaProyectil;
    public GameObject proyectil;*/
    public LayerMask capaPlataforma;
    public float tiempoEsperaDisparo;

    private int direccion = 1;
    private SpriteRenderer sprite;
    private Transform personaje;
    private Rigidbody2D cuerpo;
    private Animator animador;
    private float desp = 1.2f;
    private bool moviendose;
    private float marcaTiempoDisparo;
    private static int estadoDisparo = Animator.StringToHash("Base Layer.Disparando");

    // Use this for initialization
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animador = GetComponent<Animator>();

        

        if (inmovil)
        {
            cuerpo.bodyType = RigidbodyType2D.Static;
            moviendose = false;
        }
        else
        {
            cuerpo.bodyType = RigidbodyType2D.Kinematic;

            // Desactivando friccion
            PhysicsMaterial2D sinFriccion = new PhysicsMaterial2D("sinFriccion");
            sinFriccion.friction = 0;
            gameObject.GetComponent<Collider2D>().sharedMaterial = sinFriccion;

            if (perseguir)
            {
                
                personaje = GameObject.FindGameObjectWithTag("Player").transform;
            }

            cuerpo.velocity = new Vector2(fuerzaMov, 0);

            if (!miraALaDerecha)
            {
                voltear();
            }

            moviendose = true;
        }

        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (!inmovil) {
            
            if (moviendose)
            {
                float posRayX = (sprite.bounds.extents.x + 0.05f) * direccion;
                Vector2 centro = sprite.bounds.center;

                Vector2 posIniRay = new Vector2(posRayX + centro.x, centro.y - sprite.bounds.extents.y);
                RaycastHit2D objeto = Physics2D.Raycast(posIniRay, Vector2.down, 0.5f, capaPlataforma);

                Vector2 posIniRayDetras = new Vector2((-posRayX) + centro.x, centro.y - sprite.bounds.extents.y);
                RaycastHit2D objetoDetras = Physics2D.Raycast(posIniRayDetras, Vector2.down, 0.5f, capaPlataforma);


                if (!perseguir)
                {
                    if (objeto.collider == null && evitaCaerse)
                    {
                        voltear();

                    }
                    else
                    {
                        chequeoCaidas(objetoDetras);
                    }
                }
                else
                {

                    if (personaje.position.x + desp < transform.position.x && direccion != -1)
                    {
                        voltear();
                    }

                    if (personaje.position.x - desp > transform.position.x && direccion != 1)
                    {
                        voltear();
                    }

                    if (objeto.collider == null && evitaCaerse)
                    {
                        cuerpo.velocity = new Vector2(0, 0);

                    }
                    else
                    {
                        if (objeto.collider != null && evitaCaerse)
                        {
                            cuerpo.velocity = new Vector2(fuerzaMov, 0);
                        }
                        else
                        {
                            chequeoCaidas(objetoDetras);
                        }

                    }
                }
            


            }
            else
            {
                Debug.Log("no hay movimiento");
                
            }
        }

        
        /*if (lanzaProyectil && marcaTiempoDisparo <= Time.time)
        {
            
            float posRayX = (sprite.bounds.extents.x + 0.05f) * direccion;
            Vector2 centro = sprite.bounds.center;
            Vector2 posIniRay = new Vector2(posRayX + centro.x, centro.y);
            RaycastHit2D objeto = Physics2D.Raycast(posIniRay, Vector2.right*direccion, 6f);

            if (objeto.transform != null && objeto.transform.tag=="Player")
            {
                
                Debug.Log("disparando");
                StartCoroutine(disparar());
            }

    
        }*/
        
    }

    IEnumerator disparar()
    {
        marcaTiempoDisparo = Time.time + tiempoEsperaDisparo;

        cuerpo.velocity = new Vector2(0, 0);
        moviendose = false;

        AnimatorStateInfo animActual = animador.GetCurrentAnimatorStateInfo(0);
        animador.SetTrigger("disparando");

        
        int hashAnim = animActual.shortNameHash;

   
        Debug.Log("Base Layer.Disparando : " + Animator.StringToHash("Base Layer.Disparando"));
        Debug.Log("Disparando : " + Animator.StringToHash("Disparando"));

        Debug.Log("shorthash: " + animActual.shortNameHash);
        Debug.Log("fullpathhash : " + animActual.fullPathHash);

        while (animActual.shortNameHash == animador.GetCurrentAnimatorStateInfo(0).shortNameHash)
        {
            Debug.Log("ANimando shrto " + animActual.shortNameHash);
            Debug.Log("ANimando full " + animActual.fullPathHash);
            yield return null;
        }
        
        Debug.Log("termiando animacion disparo");
        //animador.Play("Moving");
       
        moviendose = true;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController2D>().herir(transform.position);
            
        }
    }

    public void herir()
    {
        vida--;
        if (vida == 0)
        {
            DestroyImmediate(gameObject);
            
        }
    }

    private void chequeoCaidas(RaycastHit2D objetoDetras)
    {
        if (objetoDetras.collider == null && !evitaCaerse)
        {
            cuerpo.velocity = new Vector2(fuerzaMov * 0.5f, -Mathf.Abs(fuerzaMov * 4));
        }
        else
        {

            cuerpo.velocity = new Vector2(fuerzaMov, 0);

        }
    }

    private void voltear()
    {
        fuerzaMov = -fuerzaMov;
        direccion = -direccion;
        sprite.flipX = !sprite.flipX;
        cuerpo.velocity = new Vector2(fuerzaMov, 0);
    }
}
