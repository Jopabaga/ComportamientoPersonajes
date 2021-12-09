using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float radius;
    [Range(0, 360)]
    public float angle;

    public GameObject playerRef;
    public GameObject deadEnemy;

    public LayerMask playerMask;
    public LayerMask obstructionMask;
    public LayerMask deadMask;

    public bool canSeePlayer;
    public bool canSeeDeadBody;

    public void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Jugador");
        StartCoroutine(FOVRoutine());
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOVCheckPlayer();
            FOVCheckDeadEnemy();
        }
    }

    private void FOVCheckPlayer()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, playerMask);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);
                
                if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }
    private void FOVCheckDeadEnemy()
    {
        deadEnemy = GameObject.FindGameObjectWithTag("Dead");
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, deadMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask))
                {
                    canSeeDeadBody = true;
                }
                else
                {
                    canSeeDeadBody = false;
                }
            }
            else
            {
                canSeeDeadBody = false;
            }
        }
        else if (canSeeDeadBody)
        {
            canSeeDeadBody = false;
        }
    }

}
