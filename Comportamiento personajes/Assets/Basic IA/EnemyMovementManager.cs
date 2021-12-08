using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovementManager : MonoBehaviour
{
    EnemyManager enemyManager;
    EnemyAnimatorManager enemyAnimationManager;

    NavMeshAgent navMeshAgent;

    public Rigidbody enemyRigidBody;

    public SoldierStats currentTarget;

    public LayerMask detectionLayer; //Field of view

    public float distanceFromTarget;
    public float stoppingDistance = 20f;

    public float rotationSpeed = 15;

    public void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        enemyAnimationManager = GetComponentInChildren<EnemyAnimatorManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidBody = GetComponent<Rigidbody>();
    }

    public void Start()
    {
        navMeshAgent.enabled = false;
        enemyRigidBody.isKinematic = false;
    }

    public void HandleDetection()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            SoldierStats soldierStats = colliders[i].transform.GetComponent<SoldierStats>();

            if (soldierStats != null)
            {
                Vector3 targetDirection = soldierStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > enemyManager.minDetectionAngle && viewableAngle < enemyManager.maxDetectionAngle)
                {
                    currentTarget = soldierStats;
                }
            }
        }
    }

    public void HandleMoveToTarget()
    {
        if (enemyManager.isPerformingAction) return;

        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        //If we are performing an animation, stop the movement
        if (enemyManager.isPerformingAction)
        {
            enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            navMeshAgent.enabled = false;
        }
        else
        {
            if(distanceFromTarget > stoppingDistance)
            {
                enemyAnimationManager.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }
            else if (distanceFromTarget <= stoppingDistance)
            {
                enemyAnimationManager.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
            }
        }

        HandleRotateTowardsTarget();

        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    private void HandleRotateTowardsTarget()
    {
        //Rotate manually
        if (enemyManager.isPerformingAction)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if(direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
        }

        //Rotate with pathfinding (navmesh)
        else
        {
            Vector3 relativeDiection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyRigidBody.velocity;

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(currentTarget.transform.position);

            enemyRigidBody.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }
    }
}
