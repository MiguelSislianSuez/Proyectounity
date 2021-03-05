using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparos : MonoBehaviour
{
    //††††††††††VARIABLES††††††††††††
    //velocidad de la bala
    public float velocidad = 2;
    public Vector2 direction;
    //tiempo en scena de bala
    public float tiempoBala = 3f;
    public int danho = 1;

    public Color colorInicial;
    public Color colorFinal;

    private SpriteRenderer render;
    private float tiempoInicial;
    private Rigidbody2D rigidbody;
    //†††††††††††††††††††††††††††††††

    void Awake()
    {
        render = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {

        tiempoInicial = Time.time;

        //Fun destruye bala de la escena
        Destroy(this.gameObject, tiempoBala);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movimiento = direction.normalized * velocidad * Time.deltaTime;
        //transform.position = new Vector2(transform.position.x + movimiento.x, transform.position.y + movimiento.y);//indicamos la posicion que debe tener
        transform.Translate(movimiento);

       // float espacioDeTiempo = Time.time - tiempoInicial;
        //float porcentajeInterpol = espacioDeTiempo / tiempoBala;

        //render.color = Color.Lerp(colorInicial, colorFinal, porcentajeInterpol);
    }

    private void FixedUpdate()
    {
        //movimiento bala con rigy
        Vector2 movimiento = direction.normalized * velocidad;
        rigidbody.velocity = movimiento;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("colisión");
        if (collision.CompareTag("Player")) { 
            //collision.gameObject.GetComponent<VidaProta>().Danho;
            collision.SendMessageUpwards("Danho", danho);
            Destroy(gameObject);
        //si hay colision se destruye bala
        
            Debug.Log("Hay jugador de colision");
        }
    }
}
