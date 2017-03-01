﻿
using UnityEngine;
using System.Collections;
/// <summary>
/// This class is a simple example of how to build a controller that interacts with PlatformerMotor2D.
/// </summary>
[RequireComponent(typeof(PlatformerMotor2D))]
public class PlayerController2D : MonoBehaviour
{
    private PlatformerMotor2D _motor;
    private BoxCollider2D colisionador;
    private SpriteRenderer sprite;
    private Animator animador;
    private LayerMask capaEnemigos;

    private bool invencible;
    private int direccion;

    // Use this for initialization
    void Start()
    {
        _motor = GetComponent<PlatformerMotor2D>();
        colisionador = GetComponent<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animador = GetComponentInChildren<Animator>();
        capaEnemigos = LayerMask.GetMask("Enemigo");

        invencible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(Input.GetAxis(PC2D.Input.HORIZONTAL)) > PC2D.Globals.INPUT_THRESHOLD)
        {
            _motor.normalizedXMovement = Input.GetAxis(PC2D.Input.HORIZONTAL);
            if (_motor.normalizedXMovement > 0)
            {
                direccion = 1;
            }
            else
            {
                direccion = -1;
            }
                
        }
        else
        {
            _motor.normalizedXMovement = 0;
        }

        // Jump?
        if (Input.GetButtonDown(PC2D.Input.JUMP))
        {
            _motor.Jump();
        }

        _motor.jumpingHeld = Input.GetButton(PC2D.Input.JUMP);

        if (Input.GetAxis(PC2D.Input.VERTICAL) < -PC2D.Globals.FAST_FALL_THRESHOLD)
        {
            _motor.fallFast = true;
        }
        else
        {
            _motor.fallFast = false;
        }

        if (Input.GetButtonDown(PC2D.Input.DASH))
        {
            _motor.Dash();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            
            Vector2 centro = colisionador.bounds.center;
            Vector2 origen = new Vector2(centro.x + (colisionador.bounds.extents.x * direccion), centro.y);
            
            RaycastHit2D ataque = Physics2D.Raycast(origen, Vector2.right * direccion, 1, capaEnemigos);
            Vector2 destino = origen + (Vector2.right * direccion);
            Debug.DrawLine(origen, destino, Color.cyan, 2, false);
            animador.Play("Attack");
           
            if (ataque.collider != null)
            {
                
                ataque.collider.gameObject.GetComponent<CompEnemigo>().herir();
            }
        }

        
        
    }

   

    public void resetear()
    {
        
        GameObject reinicio = GameObject.FindGameObjectWithTag("Respawn");

        transform.position = reinicio.transform.position;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "Enemigo"  && !invencible)
        {
            //resetear();
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
}