using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/EnemyAction/AttackAction")]
public class EnemyAttackAction : EnemyAction
{
    public int attackScore = 3;
    public float recoveyTime = 0.2f;

    public float maxDistanceToMelee = 1;
    public float minDistanceToMelee = 0;

    public float maxDistanceToAttack = 20;
    public float minDistanceToAttack = 1;
}
