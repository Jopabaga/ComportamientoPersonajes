using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    public IABasic ia;

    public FOV fov;
    private Animator anim;
    public GameObject enemigo;

    

    public LayerMask detectionLayer; //Field of view

    public NavMeshAgent nav;
    public GameObject[] destinos;
    public int destinoActual = 0;

    public AlertState alert;

    public override State RunCurrentState()
    {
        nav.speed = 10;
        anim = enemigo.GetComponent<Animator>();
        anim.SetFloat("speed", 0.25f);
        nav.destination = destinos[destinoActual].transform.position;
        if (destinoActual < destinos.Length - 1)
        {
            if (this.transform.position.x == destinos[destinoActual].transform.position.x && this.transform.position.z == destinos[destinoActual].transform.position.z)
            {
                destinoActual++;
                nav.destination = destinos[destinoActual].transform.position; // set next target
            }
        }
        else
        {
            destinoActual = 0;
            nav.destination = destinos[destinoActual].transform.position;
        }

        State aux = detectarMov();

        return aux;

    }

    private State detectarMov()
    {
        if (fov.canSeePlayer)
        {
            return alert;
        }
        else
        {
            return this;
        }
    }
}