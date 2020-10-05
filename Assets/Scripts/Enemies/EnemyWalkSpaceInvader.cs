using UnityEngine;
using System.Collections;

public class EnemyWalkSpaceInvader : EnemyWalkController
{
    private Vector2 newVelocity = new Vector2();
    private int xDirection;
    private bool goDown = true;
    private float stopGoingDownY = 1.0f;


    public EnemyWalkSpaceInvader(int initialXDirection)
    {
        xDirection = initialXDirection;
    }

    public override void UpdateWalkingState()
    {
        //newVelocity.Set(c_xMultiplier * xDirection * enemyBaseSpeed, c_yMultiplier * c_yDirection * enemyBaseSpeed);
        if (goDown)
        {
            if (enemyRb.position.y < stopGoingDownY)
                goDown = false;
            enemyRb.velocity = new Vector2(0, -enemyBaseSpeed);
        } else {
            enemyRb.velocity = new Vector2(xDirection * enemyBaseSpeed, 0);
        }
    }

    public override void OnCollide(Collider2D collision)
    {
        xDirection *= -1;
        goDown = true;
        stopGoingDownY = enemyRb.position.y - 0.3f;
    }
}