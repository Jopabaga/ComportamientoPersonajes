using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtackState : State
{
    public GameObject objetivo;
    public NavMeshAgent agente;
    public float dist;
    public Animator anim;
    public GameObject enemigo;
    public float retirada;
    private float tiempoDisparos;
    public float empezarTiempoDisparos;
    public GameObject balaPrefab;
    public Transform arma;
    public float fuerzaBala = 20f;
    public EnemyStats enemyStats;
    public DeadState dead;
    public AlertState alert;
    public FOV fov;
    public bool losePlayer;
    public int timeNoSee=0;
    public Light linterna;


    void Start()
    {
        tiempoDisparos = empezarTiempoDisparos;
    }
    public void Update()
    {
       
        if(!fov.canSeePlayer)
        {
            timeNoSee++;
           
        }
        else
        {
            if (timeNoSee > 0)
            {
                timeNoSee--;
            }
        }

        if (timeNoSee >= 200)
        {
            losePlayer = true;
        }

        

        
    }
    public override State RunCurrentState()
    {
        linterna.color = Color.red;
        Vector3 direction = (objetivo.transform.position - enemigo.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        enemigo.transform.rotation = Quaternion.Slerp(enemigo.transform.rotation, lookRotation, Time.deltaTime * 10);
        agente.speed = 10;
        anim = enemigo.GetComponent<Animator>();
        anim.SetFloat("speed", 0.75f);
        agente.destination = objetivo.transform.position;
        dist = Vector3.Distance(transform.position, objetivo.transform.position);
        

        if (dist < 20.0f)
        {
            agente.isStopped = true;
            anim.SetFloat("speed", 0.0f);

            if (tiempoDisparos <= 0)
            {
                GameObject bala = Instantiate(balaPrefab, arma.position, arma.rotation);
                Rigidbody rb = bala.GetComponent<Rigidbody>();
                rb.AddForce(arma.forward * fuerzaBala, ForceMode.Impulse);
                tiempoDisparos = empezarTiempoDisparos;
            }
            else
            {
                tiempoDisparos -= Time.deltaTime;
            }

        }
        else if (dist > 20.0f)
        {
            anim.SetFloat("speed", 0.75f);
            agente.isStopped = false;
        }

        State aux = changeState();

        return aux;
    }

    private State changeState()
    {
        if (enemyStats.isDead)
        {
            return dead;
        }
        else if (losePlayer)
        {
            losePlayer = false;
            alert.timeInAlert = 800;
            alert.detectionGrade = 0;
            return alert;
        }
        else
        {
            return this;
        }
    }
}
