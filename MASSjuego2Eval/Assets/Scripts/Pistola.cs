using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    public GameObject disparoPrefab;
    public GameObject disparo;

    public Transform puntoDisparo;

    private AudioSource audioBala;

    

    void Awake()
    {
        puntoDisparo = transform.Find("PuntoDisparo");//asignamos un punto de dispar hijo para devolver la posicion y lance la bala desde el punto asignado
        audioBala = GetComponent<AudioSource>();

        
    }
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("Disparar", 1f);//invokamos la funcion interpolada para hacer vario disparos
        //Invoke("Disparar", 2f);
        //Invoke("Disparar", 3f);
        //Disparar();
        //Instantiate(disparoPrefab, transform.position, Quaternion.identity);//queter produce un desplazamiento suave
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Disparar();
        }
    }

    public void Disparar()
    {
       /* if (disparoPrefab != null)
        {

        Debug.Log("disparoPrefab", disparoPrefab);
        }
        if (puntoDisparo != null)
        {

        Debug.Log("puntoDisparo", puntoDisparo);
        }
        if (disparo != null)
        {

        Debug.Log("disparo", disparo);
        }*/

        if (disparoPrefab != null && puntoDisparo != null && disparo != null)
        {
            GameObject misDisparos = Instantiate(disparoPrefab, puntoDisparo.position, Quaternion.identity) as GameObject;
            Disparos disparosComponent = misDisparos.GetComponent<Disparos>();

            if(disparo.transform.localScale.x < 0f)
            {
                disparosComponent.direction = Vector2.left;//instanciamos nuevo vector para que marque la dirección del disparo
                audioBala.Play();
                
            }
            else
            {
                disparosComponent.direction = Vector2.right;
                audioBala.Play();

            }

        }
    }
}
