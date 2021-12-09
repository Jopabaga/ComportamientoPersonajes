using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State
{
    public IABasic ia;

    public LayerMask detectionLayer; //Field of view

    public NavMeshAgent nav;
    public GameObject[] destinos;
    public int destinoActual = 0;

    public AlertState alert;

    public override State RunCurrentState()
    {
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, ia.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            SoldierStats soldierStats = colliders[i].transform.GetComponent<SoldierStats>();

            if (soldierStats != null)
            {
                Vector3 targetDirection = soldierStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > ia.minDetectionAngle && viewableAngle < ia.maxDetectionAngle)
                {
                    ia.currentTarget = soldierStats;
                }
            }
        }

        if (ia.currentTarget != null)
        {
            return alert;
        }
        else
        {
            return this;
        }
    }
}