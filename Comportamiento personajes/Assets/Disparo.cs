using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    public Transform arma;
    public GameObject balaPrefab;
    public float fuerzaBala = 20f;
    public int balas=100;

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButton("Fire1"))
        {
            Shoot();
            
        }
    }
    void Shoot()
    {
        if(balas>0)
        {
            GameObject bala = Instantiate(balaPrefab, arma.position, arma.rotation);
            Rigidbody rb = bala.GetComponent<Rigidbody>();
            rb.AddForce(arma.forward * fuerzaBala, ForceMode.Impulse);
            balas--;
        }
    }
}
