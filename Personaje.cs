using UnityEngine;
using System.Collections;

public class Personaje : MonoBehaviour
{

    /// <summary>
    /// Permite mover un gameobject, como un personaje, constantemente a la derecha mediante un Rigid Body 2D
    /// y sin fricción. La fuerza utilizada para avanzar y saltar y la máxima velocidad se pueden cambiar
    /// en el editor.
    /// 
    /// José Sánchez
    /// </summary>
	private Rigidbody2D cuerpo;
    public float fuerzaMov;
    public float fuerzaSalto;
    public float maxVelocidad;
    private bool sobrePlataforma;
    private Vector2 vectorAceleracion;
    private Vector2 vectorSaltar;
    private float offset;

    private bool sobrePlatPrimeraVez;

    // Use this for initialization
    void Start()
    {
        cuerpo = GetComponent<Rigidbody2D>();
        vectorAceleracion = Vector2.right * fuerzaMov;
        vectorSaltar = Vector2.up * fuerzaSalto;
        offset = 0.25f;

        PhysicsMaterial2D sinFriccion = new PhysicsMaterial2D("sinFriccion");
        sinFriccion.friction = 0;

        gameObject.GetComponent<Collider2D>().sharedMaterial = sinFriccion;

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
            if (cuerpo.velocity.x < maxVelocidad)
            {
                cuerpo.AddForce(vectorAceleracion, ForceMode2D.Force);
            }

            if (Input.GetKey(KeyCode.Space) && sobrePlataforma)
            {
                cuerpo.AddForce(vectorSaltar, ForceMode2D.Force);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D colision)
    {

        float posInferiorPersonaje = transform.position.y - (gameObject.GetComponent<Collider2D>().bounds.size.y / 2);
        float posSuperiorPlataforma = colision.collider.transform.position.y + colision.collider.bounds.size.y / 2;

        print("personaje " + posInferiorPersonaje);
        print("plataforma " + posSuperiorPlataforma);
        if (posInferiorPersonaje + offset > posSuperiorPlataforma - offset)
        {
            sobrePlataforma = true;
            print("en plataforma");

            if (!sobrePlatPrimeraVez)
            {
                sobrePlatPrimeraVez = true;
            }
        }

    }

    void OnCollisionExit2D()
    {
        sobrePlataforma = false;
    }
}
