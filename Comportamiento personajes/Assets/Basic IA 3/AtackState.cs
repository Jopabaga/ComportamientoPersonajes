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


    void Start()
    {
        tiempoDisparos = empezarTiempoDisparos;
    }
    public override State RunCurrentState()
    {

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
        else
        {
            return this;
        }
    }
}
