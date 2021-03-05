using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoBasico : MonoBehaviour
{

    public float speed = 10;
    public float minX;
    public float maxX;
    public float tiempoDeEspera = 2f;
    public int vidaEnemy = 6;

    private GameObject target;
    private Animator animator;
    private Pistola pistola;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        pistola = GetComponentInChildren<Pistola>();
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateTarget();//inicializamos con el target
        StartCoroutine("PatrolToTarget");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTarget()
    {
        //la primera vez se crea un targ en escena y colocar en la izquierda
        if(target == null){
            target = new GameObject("Target");
            target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);

            return;//con esto conseguimos q no se ejecute el resto de cod si es la primera vez 
        }
        //si nostros estamos a la izquierda, cambiará a derecha
        if (target.transform.position.x == minX){
            target.transform.position = new Vector2(maxX, transform.position.y);
            transform.localScale = new Vector3(1, 1, 1);

        }else if (target.transform.position.x == maxX){
            target.transform.position = new Vector2(minX, transform.position.y);
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    //Creamos una corrutina del enemigo
    private IEnumerator PatrolToTarget()
    {
        //devuelve la distancia entre dos objetos, enemigo y yo
        while (Vector2.Distance(transform.position, target.transform.position) > 0.05f)
        {

            //actualiza el animator
            animator.SetBool("EnemigoIdle", false);
            // mueve direccion el targ
            Vector2 direction = target.transform.position - transform.position;
            float xDirection = direction.x;

            transform.Translate(direction.normalized * speed * Time.deltaTime);

            
            yield return null;//tipo especial con corrutina, el yield le dice que
            //no siga y que cuando termine vuelve al principio mientras estemos lejos del enemigo 
        }

        // Alcanzamos el targ, estableciendonuestra posicon en este
        Debug.Log("Target reached");
        transform.position = new Vector2(target.transform.position.x, transform.position.y);
        UpdateTarget();

        //actualiza el animator
        animator.SetBool("EnemigoIdle", true);

        //Disparos
        animator.SetTrigger("EnemigoDisparo");

        /*if(pistola != null)
        {
            pistola.Disparar();
        }*/

        // tiempo de espera cuando alcanza limite
        Debug.Log("Esperamos  " + tiempoDeEspera + " segundos");
        yield return new WaitForSeconds(tiempoDeEspera); // IMPORTANT

        // termina el tiempo y vuelve
        Debug.Log("Esperamos el tiempo, se actualiza el objeto y vuelve a moverse");
        
        StartCoroutine("PatrolToTarget");
    }

    //Agregamos funcion para los eventos de disparo y cuando llegue el evento que hemos creado en la anumacion disparamos arma
    public void PuedeDisparar()
    {
        if(pistola != null)
       {
           pistola.Disparar();
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("DAÑOS");

        if (collision.gameObject.tag == "Bala")
        {
            //Debug.Log("DAÑOS");
        }
    }
}