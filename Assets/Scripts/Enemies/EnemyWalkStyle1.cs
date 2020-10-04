using UnityEngine;
using System.Collections;

public class EnemyWalkStyle1 : EnemyWalkController
{
    const float c_speedMultiplier = 3.0f;
    const float c_xMultiplier = c_speedMultiplier * 0.7071067811865476f;    // cos(30) = sqrt(2) / 2
    const float c_yMultiplier = c_speedMultiplier * 0.5f;                   // sin(30) = 1 / 2

    private Vector2 newVelocity = new Vector2();
    private int xDirection;
    const int c_yDirection = -1;

    public EnemyWalkStyle1(int initialXDirection)
    {
        xDirection = initialXDirection;
    }

    public override void UpdateWalkingState()
    {
        newVelocity.Set(c_xMultiplier * xDirection * enemyBaseSpeed, c_yMultiplier * c_yDirection * enemyBaseSpeed);
        enemyRb.velocity = newVelocity;

        /* TODO: Flip on wall */

        /*
        int directionX;

        // wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);

        if (enemyRb.position.x > enemyRb.position.x)
        {
            directionX = 1;
        }
        else
        {
            directionX = -1;
        }

        newVelocity.Set(playerRb.position.x * directionX * c_movementSpeed, playerRb.position.y * c_movementSpeed);
        enemyRb.velocity = newVelocity;

        //transform.position = Vector3.MoveTowards(transform.position, playerRb.position, movementSpeed * Time.deltaTime);
        */
    }
}
