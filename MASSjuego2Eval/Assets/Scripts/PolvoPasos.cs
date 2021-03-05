using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolvoPasos : MonoBehaviour
{

    public GameObject prefab;
    public Transform puntoParticula;
    public float tiempoDeVida;

    // Start is called before the first frame update
   public void Instantiate()
    {
        GameObject objeto = Instantiate(prefab, puntoParticula.position, Quaternion.identity) as GameObject;
        if (tiempoDeVida > 0f)
        {
            Destroy(objeto, tiempoDeVida);
        }
    }
}
