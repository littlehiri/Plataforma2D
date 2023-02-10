using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //La velocidad del jugador
    public float moveSpeed;
    //El rigidbody del jugador
    private Rigidbody2D theRb;
    //Fuerza de salto del jugador
    public float jumpforce;

    //Variable para saber si el jugador está en el suelo
    private bool isGrounded;

    //Punto por debajo del jugador que tomamos como referencia para detectar el suelo
    public Transform groundCheckPoint;

    //variable para detectar el Layer de suelo
    public LayerMask whatIsGround;

    //Variable para saber si podemos hacer doble salto
    private bool canDoubleJump;

    //referencia al Animator del jugador
    private Animator anim;

    //Referencia al SpriteRenderer
    private SpriteRenderer theSr;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el RigidBody del jugador
        theRb = GetComponent<Rigidbody2D>();
        //Rellenamos la referencia del Animator del jugador
        anim = GetComponent<Animator>();
        //Rellenamos referencia
        theSr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        theRb.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatIsGround); //OverlapCircle( punto donde se genera el circulo, radio del circulo, layer se genera

        //Si se pulsa el botón de salto
        if (Input.GetButtonDown("Jump"))
        {
            //Si el jugador está en el suelo
            if (isGrounded)
            { 
                //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza 
                theRb.velocity = new Vector2(theRb.velocity.x, jumpforce);
                //Una vez en el suelo, reactivamos la posibilidad de doble salto
                canDoubleJump = true;
            }
            //Si el jugador no está en el suelo
            else
            {
                //si la variable booleana canDoubleJump es verdadera
                if (canDoubleJump)
                {
                    //El jugador salta, manteniendo su velocidad en X, y aplicamos la fuerza
                    theRb.velocity = new Vector2(theRb.velocity.x, jumpforce);
                    //Hacemos que no se pueda volver a saltar de nuevo
                    canDoubleJump = false;
                }
            }
        }
        //Girar el sprite del jugador
        if (theRb.velocity.x < 0)
        {
            theSr.flipX = false;
        }
        //Si el jugador por el contrario se está moviendo haci el lado izquierdo
        else if (theRb.velocity.x > 0)
        {
            //Cambiamnos la direccion del sprite
            theSr.flipX = true;
        }

        //Animaciones del jugador
        //Cambiamos le valor del parámetro del Animator "moveSpeed"
        anim.SetFloat("moveSpeed", Mathf.Abs(theRb.velocity.x)); //Mathf.Abs hace que un valor negativo sea positivo, lo que nos permite que al movernos a la izquierda tambien se anime esta acción
        //Cambiamos el valor del parametro del Animator "isGrounded"
        anim.SetBool("isGrounded", isGrounded);

    }


}
