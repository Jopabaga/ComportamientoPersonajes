using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    EnemyMovementManager enemyMovManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyMovManager = GetComponentInParent<EnemyMovementManager>();
    }

    public void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemyMovManager.enemyRigidBody.drag = 0;

        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;

        Vector3 velocity = deltaPosition / delta;

        enemyMovManager.enemyRigidBody.velocity = velocity;
    }
}
