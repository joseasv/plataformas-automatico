using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompEnemigo : MonoBehaviour
{

    
    public float fuerzaMov = 2;
    public bool evitaCaerse;
    public bool perseguir;
    public int vida = 1;
    public LayerMask capaPlataforma;

    private int direccion = 1;
    private SpriteRenderer sprite;
    private Transform personaje;
    private Rigidbody2D cuerpo;

    // Use this for initialization
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        cuerpo.bodyType = RigidbodyType2D.Kinematic;

        // Desactivando friccion
        PhysicsMaterial2D sinFriccion = new PhysicsMaterial2D("sinFriccion");
        sinFriccion.friction = 0;
        gameObject.GetComponent<Collider2D>().sharedMaterial = sinFriccion;

        if (perseguir)
        {
            print(GameObject.FindGameObjectWithTag("Player"));
            personaje = GameObject.FindGameObjectWithTag("Player").transform;
        }

        cuerpo.velocity = new Vector2(fuerzaMov, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {

        float posRayX = (sprite.bounds.extents.x + 0.05f) * direccion;
        Vector2 centro = sprite.bounds.center;

        Vector2 posIniRay = new Vector2(posRayX + centro.x , centro.y - sprite.bounds.extents.y);
        RaycastHit2D objeto = Physics2D.Raycast(posIniRay, Vector2.down,  0.5f, capaPlataforma);
        
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
            
            if (personaje.position.x < transform.position.x && direccion != -1)
            {
                voltear();
            }

            if (personaje.position.x > transform.position.x && direccion != 1)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController2D>().herir();
        }
    }

    public void herir()
    {
        vida--;
        if (vida == 0)
        {
            Debug.Log("SE MURIO");
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
