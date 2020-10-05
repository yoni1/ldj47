using UnityEngine;
using System;
using System.Collections;

public abstract class EnemyWalkController
{
    private static System.Random rnd = new System.Random();

    protected float enemyBaseSpeed;
    protected Rigidbody2D playerRb;
    protected Rigidbody2D enemyRb;

    public enum WalkingStyle
    {
        ZigZagWideStartLeft,
        ZigZagWideStartRight,
        JustDown,
    }

    public virtual void BeginWalk()
    {
    }

    public abstract void UpdateWalkingState();

    public virtual void EndWalk()
    {
        enemyRb.velocity = new Vector2(0, 0);
    }

    public virtual void OnCollide()
    {
    }

    public static EnemyWalkController Create(WalkingStyle walkingStyle, float enemyBaseSpeed, Rigidbody2D playerRb, Rigidbody2D enemyRb)
    {
        EnemyWalkController obj = null;

        switch (walkingStyle)
        {
            case WalkingStyle.ZigZagWideStartLeft:
                obj = new EnemyWalkStyleZigZagWide(-1);
                break;

            case WalkingStyle.ZigZagWideStartRight:
                obj = new EnemyWalkStyleZigZagWide(1);
                break;

            case WalkingStyle.JustDown:
                obj = new EnemyWalkStyleJustDown();
                break;

            default:
                return null;
        }

        obj.enemyBaseSpeed = enemyBaseSpeed;
        obj.playerRb = playerRb;
        obj.enemyRb = enemyRb;
        return obj;
    }
}
