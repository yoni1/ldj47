using UnityEngine;
using System.Collections;

public class EnemyWalkStyleZigZagWide : EnemyWalkController
{
    const float c_speedMultiplier = 3.0f;
    const float c_xMultiplier = c_speedMultiplier * 0.7071067811865476f;    // cos(30) = sqrt(2) / 2
    const float c_yMultiplier = c_speedMultiplier * 0.5f;                   // sin(30) = 1 / 2

    private Vector2 newVelocity = new Vector2();
    private int xDirection;
    const int c_yDirection = -1;

    public EnemyWalkStyleZigZagWide(int initialXDirection)
    {
        xDirection = initialXDirection;
    }

    public override void UpdateWalkingState()
    {
        newVelocity.Set(c_xMultiplier * xDirection * enemyBaseSpeed, c_yMultiplier * c_yDirection * enemyBaseSpeed);
        enemyRb.velocity = newVelocity;
    }

    public override void OnCollide()
    {
        xDirection *= -1;
    }
}
