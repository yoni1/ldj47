using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalkToPlayer : EnemyWalkController
{
    public override void UpdateWalkingState()
    {
        Vector2 playerDirection = playerRb.position - enemyRb.position;
        enemyRb.velocity = playerDirection.normalized * enemyBaseSpeed;
    }
}
