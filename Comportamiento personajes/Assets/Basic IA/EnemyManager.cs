using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager 
{
    EnemyMovementManager enemyMovManager;
    EnemyAnimatorManager enemyAnimManager;

    public bool isPerformingAction;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    [Header("A.I. Properties")]
    public float detectionRadius;

    //The higher or lower the angles are, the greater detection
    public float maxDetectionAngle = 50;
    public float minDetectionAngle = -50;

    public float currentRecoveryTime = 0;

    private void Awake()
    {
        enemyMovManager = GetComponent<EnemyMovementManager>();
        enemyAnimManager = getComponentInChildren<EnemyAnimatorManager>();
    }

    private void Update()
    {
        HandleRecoveryTimer();
    }

    private void FixedUpdate()
    {
        HandleCurrentAction();
    }

    private void HandleCurrentAction()
    {
        if(enemyMovManager.currentTarget == null)
        {
            enemyMovManager.HandleDetection();
        }
        else if (enemyMovManager.distanceFromTarget > enemyMovManager.stoppingDistance)
        {
            enemyMovManager.HandleMoveToTarget();
        }
        else if (enemyMovManager.distanceFromTarget <= enemyMovManager.stoppingDistance)
        { 
            //Handle Attack
        }
    }

    private void HandleRecoveryTimer()
    {
        if(currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }

    //Into other script
    #region Attack

    private void AttackTarget()
    {
        if (isPerformingAction) return;

        if(currentAttack == null)
        {
            GetTypeOfAttack();
        }
        else
        {
            isPerformingAction = true;
            currentRecoveryTime = currentAttack.recoveyTime;
            enemyAnimManager.PlaytTargetAnimation(currentAttack.actionAnimation, true);
        }
    }

    private void GetTypeOfAttack()
    {
        Vector3 targetDirection = enemyMovManager.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transfom.forward);
        enemyMovManager.distanceFromTarget = Vector3.Distance(enemyMovManager.currentTarget.transform.position, transform.position);

        int maxScore = 0;

        //Choose between melee and distant attack
        for(int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if(enemyMovManager.distanceFromTarget <= enemyAttackAction.maxDistanceToAttack && enemyMovManager.distanceFromTarget >= enemyAttackAction.minDistanceToAttack)
            {
                if(enemyMovManager.distanceFromTarget <= enemyAttackAction.maxDistanceToMelee && enemyMovManager.distanceFromTarget >= enemyAttackAction.minDistanceToMelee)
                {
                    maxScore = enemyAttackAction.attackScore;
                }
            }
        }

        int random = Random.Range(0, maxScore);
        int auxScore = 0;

        for(int j = 0; j < enemyAttacks.Length; j++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemyMovManager.distanceFromTarget <= enemyAttackAction.maxDistanceToAttack && enemyMovManager.distanceFromTarget >= enemyAttackAction.minDistanceToAttack)
            {
                if (enemyMovManager.distanceFromTarget <= enemyAttackAction.maxDistanceToMelee && enemyMovManager.distanceFromTarget >= enemyAttackAction.minDistanceToMelee)
                {
                    if(currentAttack != null) return;

                    auxScore += enemyAttackAction.attackScore;

                    if (auxScore > random) currentAttack = enemyAttackAction;
                }
            }
        }
    }

    #endregion

}
