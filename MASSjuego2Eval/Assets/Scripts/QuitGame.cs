using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnApplicationQuit()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego");
    }

}

