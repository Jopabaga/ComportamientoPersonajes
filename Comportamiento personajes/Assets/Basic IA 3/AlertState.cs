using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertState : State
{
    public bool detectsPlayer;
    public bool losePlayer;

    public bool stopMovement;

    public int detectionGrade ;
    public int timeInAlert;

    public PatrolState patrol;
    public AtackState attack;
    public DeadState dead;

    public FOV fov;
    public Animator anim;
    public GameObject enemigo;

    public EnemyStats enemyStats;
    public Light linterna;

    public IABasic ia;

    public LayerMask detectionLayer; //Field of view

    public NavMeshAgent nav;
    public GameObject objetivo;
    public int destinoActual = 0;
    
    public void Start()
    {
        detectionGrade = 0;
        timeInAlert = 800;
    }
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
        if (detectionGrade >= 50)
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

        linterna.color = Color.yellow;
        anim = enemigo.GetComponent<Animator>();
        if (stopMovement)
        {
            anim.SetFloat("speed", 0.0f);
        }
        else {
            anim.SetFloat("speed", 0.75f);
        }
        fov.setRadius(45);
        fov.setAngle(150);

        nav.speed = 7;
      
        nav.isStopped = stopMovement;
        nav.destination = objetivo.transform.position;



        State aux = changeState();
        return aux;
    }

    private State changeState()
    {
        if (losePlayer)
        {
            fov.setRadius(25);
            fov.setAngle(150);
            //cambiar color y tamaño de luz
            losePlayer = false;
            timeInAlert = 800;
            patrol.destinoActual = 0;
            return patrol;
        }
        else if (enemyStats.isDead)
        {
            return dead;
        }
        else if (enemyStats.hurt || detectsPlayer)
        {
            enemyStats.hurt = false;
            detectsPlayer = false;
            detectionGrade = 0;
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

