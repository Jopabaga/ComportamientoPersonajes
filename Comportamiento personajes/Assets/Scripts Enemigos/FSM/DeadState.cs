using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DeadState : State
{

    public Animator anim;
    public NavMeshAgent agente;
    public GameObject yo;
    public override State RunCurrentState()
    {

        yo.tag = "Dead";
        yo.layer = 8;
        agente.isStopped = true;
        anim.SetFloat("speed", 1.0f);
        
        return this;
    }
}
