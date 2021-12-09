using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertState : State
{
    public GameObject objetivo;
    public NavMeshAgent agente;
    public float dist;
    private Animator anim;
    public GameObject enemigo;


    public override State RunCurrentState()
    {
        agente.speed = 10;
        anim = enemigo.GetComponent<Animator>();
        anim.SetFloat("speed", 0.75f);
      agente.destination = objetivo.transform.position;
      dist = Vector3.Distance(transform.position, objetivo.transform.position);
        Debug.Log(dist);
        if (dist < 15.0f)
        {
            agente.isStopped=true;
        }
        if (dist > 15.0f)
        {
            agente.isStopped = false;
        }


        return this;
    }
}
