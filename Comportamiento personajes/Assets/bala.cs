using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    void OnCollisionEnter(Collision colision)
    {
        Destroy(gameObject);
    }
}
