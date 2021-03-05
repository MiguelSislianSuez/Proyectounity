using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformaTrampa1 : MonoBehaviour
{

    public float caidaDelay = 1f;//retardo de trampa
    public float reapareceDelay = 5f;

    private Rigidbody2D rigidbody;
    private PolygonCollider2D polygonC;
    private Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        polygonC = GetComponent<PolygonCollider2D>();
        start = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Caida", caidaDelay);
            Invoke("Reaparece", caidaDelay + reapareceDelay);
        }
    }

    void Caida()
    {
        Debug.Log("gay");
        rigidbody.isKinematic = false;
        polygonC.isTrigger = true;
    }

    void Reaparece()
    {
        transform.position = start;
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;
        polygonC.isTrigger = false;
    }
}
