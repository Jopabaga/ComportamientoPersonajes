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
    public DeadState dead;
    public AtackState attack;

    public EnemyStats enemyStats;
   

    public override State RunCurrentState()
    {
        destinoActual = 0;
        nav.destination = destinos[destinoActual].transform.position;
        nav.speed = 5;
        anim = enemigo.GetComponent<Animator>();
        anim.SetFloat("speed", 0.25f);
        if (enemigo.transform.position.x == destinos[destinoActual].transform.position.x && enemigo.transform.position.z == destinos[destinoActual].transform.position.z)
        {
            if (destinoActual < destinos.Length - 1)
            {


                destinoActual++;
                nav.destination = destinos[destinoActual].transform.position; // set next target

            } else
                 {
                destinoActual = 0;
                nav.destination = destinos[destinoActual].transform.position;
            
            }    
        }
       

        State aux = changeState();

        return aux;

    }
    

    private State changeState()
    {
        if (fov.canSeePlayer || fov.canSeeDeadBody)
        {
            return alert;
        }
        else if (enemyStats.isDead)
        {
            return dead;
        }
        else if (enemyStats.hurt)
        {
            return attack;
        }
        else
        {
            return this;
        }
    }
}