using UnityEngine;
using System.Collections;

public class EnemyWalkStyle2 : EnemyWalkController
{
    const float c_speedMultiplier = 1.0f;
    const float c_xVelocity = 0.0f;
    const int c_yDirection = -1;

    private Vector2 newVelocity = new Vector2();

    public override void UpdateWalkingState()
    {
        newVelocity.Set(c_xVelocity, c_yDirection * c_speedMultiplier * enemyBaseSpeed);
        enemyRb.velocity = newVelocity;
    }
}
