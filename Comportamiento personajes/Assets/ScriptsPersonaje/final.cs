using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class final : MonoBehaviour
{
    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag.Equals("Jugador") == true)
        {
            Debug.Log("Has llegado al destino");
            SceneManager.LoadScene(2);
        }


    }
}
