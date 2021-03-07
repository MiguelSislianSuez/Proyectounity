using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlProta : MonoBehaviour
{

    public float velocidad = 2.5f;
    public float potenciaSalto = 2.5f;//fuerza aplicada al saltar
    //suelo
    public Transform revisaSuelo;
    public LayerMask capaSuelo;
    public float radioSuelo;
    public int restaurarVida = 1;

    public GameObject vida1;
    public GameObject vida2;
    public GameObject vida3;
    //public GameObject puntoDisparo;

    //referencias
    private Rigidbody2D rigidBody;//mover
    private Animator animator;//animar
    //private VidaProta vida;

    private int vida = 3;

    //sonido

    //movimiento
    private Vector2 movimiento;//vector de movimiento x y
    private bool dirDerecha = true;
    private bool tocandoSuelo;

    //private SpriteRenderer render;
    public Collider2D colaider;
   
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //render = GetComponent<SpriteRenderer>();
        //colaider = GetComponent<Collider2D>();

    }
    void Start()
    {
        
    }

    void Update()
    {
        //movimiento
        float horizontal = Input.GetAxisRaw("Horizontal"); //con getaxis raw nos dara el valor final -1 y +1 para responder mejor al tecladito 
        movimiento = new Vector2(horizontal, 0f);

        //giro
        if(horizontal < 0f && dirDerecha == true){
            Giro();
            //puntoDisparo.transform.localScale.x = 1;
        }
        else if(horizontal > 0f && dirDerecha ==false)
        {
            Giro();
        }

        //verificar que toca el suelo
        tocandoSuelo = Physics2D.OverlapCircle(revisaSuelo.position, radioSuelo, capaSuelo);
        //verificar salto
        if (Input.GetKeyDown(KeyCode.UpArrow) && tocandoSuelo == true)
        {
            rigidBody.AddForce(Vector2.up * potenciaSalto, ForceMode2D.Impulse);
        }

    }

    private void FixedUpdate()//Cualquier objjeto fisico guardarlo en este meto
    {
        //convertimos los valores en vectores de movimiento
        float velocidad_H = movimiento.normalized.x * velocidad;
        
        rigidBody.velocity = new Vector2(velocidad_H, rigidBody.velocity.y);
    }

    private void LateUpdate()//
    {
        //seteamos el booleano y pasamos a estado quieto 
        animator.SetBool("Idle", movimiento == Vector2.zero);
        animator.SetBool("EstaEnSuelo", tocandoSuelo);
        animator.SetFloat("VelocidadVerticalSalto", rigidBody.velocity.y);
    }
    void Giro()
    {
        dirDerecha = !dirDerecha;
        float localSacaleX = transform.localScale.x;
        localSacaleX = localSacaleX * -1;
        transform.localScale = new Vector3(localSacaleX, transform.localScale.y, transform.localScale.z);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemigo"){
            vida--;

            if(vida == 2)
            {
                vida3.SetActive(false);
            }else if(vida == 1)
            {
                vida2.SetActive(false);
            }else if (vida == 0)
            {
                vida1.SetActive(false);
                SceneManager.LoadScene("Scene 1");
            }
            colaider.enabled = false;
            Invoke("ActivarColaider", 2f);

        }
        Debug.Log("Entramos en caida");
        if (collision.gameObject.tag == "Caida")
        {
            SceneManager.LoadScene("Scene 1");

        }
    }

 private void ActivarColaider()
    {
        colaider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ESTAMOS PILLANDO JABON");

        if (collision.gameObject.tag=="Vidas")
        {
            //nos curamos
            colaider.enabled = false;
            
            if(vida <= 3)
            {
                if (vida == 0)
                {
                    vida1.SetActive(true);
                }
                else if (vida == 1)
                {
                    vida2.SetActive(true);
                }
                else if (vida == 2)
                {
                    vida3.SetActive(true);
                }
                //render.enabled = false;
                Destroy(collision.gameObject);
                vida++;

            }

        }

    }
}
