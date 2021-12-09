using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertState : State
{
    public bool detectsPlayer;
    public bool losePlayer;

    public bool stopMovement;

    public int detectionGrade = 0;
    public int timeInAlert = 800;

    public PatrolState patrol;
    public AtackState attack;
    public DeadState dead;

    public FOV fov;
    public Animator anim;
    public GameObject enemigo;

    public EnemyStats enemyStats;

    public IABasic ia;

    public LayerMask detectionLayer; //Field of view

    public NavMeshAgent nav;
    public GameObject[] destinos;
    public int destinoActual = 0;

    public void Update()
    {
        if (fov.canSeePlayer)
        {
            detectionGrade++;
            timeInAlert ++;
            stopMovement = true;
            

        }
        else
        {
            if(!(detectionGrade <= 0))
            {
                detectionGrade--;
            }
            
        }
        if (detectionGrade <= 20)
        {
            stopMovement = false;
           
        }
        if (detectionGrade >= 200)
        {
            detectsPlayer = true;
        }
        if (timeInAlert > 0) { timeInAlert--; }
        

        if(timeInAlert <= 1)
        {
            losePlayer = true;
        }
    }

    public override State RunCurrentState()
    {  
        
        anim = enemigo.GetComponent<Animator>();
        if (stopMovement)
        {
            anim.SetFloat("speed", 0.0f);
        }
        else {
            anim.SetFloat("speed", 0.75f);
        }
        fov.setRadius(30);
        fov.setAngle(120);

        nav.speed = 7;
      
        
        nav.destination = destinos[destinoActual].transform.position;
        if (destinoActual < destinos.Length - 1)
        {
            nav.isStopped = stopMovement;
            if (this.transform.position.x == destinos[destinoActual].transform.position.x && this.transform.position.z == destinos[destinoActual].transform.position.z)
            {
                destinoActual++;
                nav.isStopped = stopMovement;
                nav.destination = destinos[destinoActual].transform.position; // set next target
            }
        }
        else
        {
            nav.isStopped = stopMovement;
            destinoActual = 0;
            nav.destination = destinos[destinoActual].transform.position;
        }



        State aux = changeState();
        return aux;
    }

    private State changeState()
    {
        if (losePlayer)
        {
            fov.setRadius(8);
            fov.setAngle(80);
            //cambiar color y tamaño de luz
            losePlayer = false;
            return patrol;
        }
        else if (enemyStats.isDead)
        {
            return dead;
        }
        else if (enemyStats.hurt || detectsPlayer)
        {
            detectsPlayer = false;
            return attack;
        }
        else
        {
            return this;
        }
    }

    public void LosePlayerAction()
    {
        losePlayer = true;
    }
}

