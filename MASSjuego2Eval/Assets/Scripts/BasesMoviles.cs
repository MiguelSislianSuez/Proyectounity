using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasesMoviles : MonoBehaviour
{
    public GameObject Player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == Player)
        {
            Player.transform.parent = transform;
            Debug.Log("HEMOS ENTRADO");
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == Player)
        {
            Player.transform.parent = null;
            Debug.Log("HEMOS Salido");

        }
    }

}
