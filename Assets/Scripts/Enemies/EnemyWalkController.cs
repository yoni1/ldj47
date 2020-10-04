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
        RandomWalkingStyle,
        Style1GoLeft,
        Style1GoRight,
        Style2,
        /*
        Style3,
        Style4,
        Style5,
        Style6,
        Style7,
        */
    }

    public virtual void BeginWalk()
    {
    }

    public abstract void UpdateWalkingState();

    public virtual void EndWalk()
    {
    }

    public static EnemyWalkController Create(WalkingStyle walkingStyle, float enemyBaseSpeed, Rigidbody2D playerRb, Rigidbody2D enemyRb)
    {
        EnemyWalkController obj = null;

        if (walkingStyle == WalkingStyle.RandomWalkingStyle)
        {
            Array styles = Enum.GetValues(typeof(WalkingStyle));
            walkingStyle = (WalkingStyle)styles.GetValue(rnd.Next(1, styles.Length));
        }

        switch (walkingStyle)
        {
            case WalkingStyle.Style1GoLeft:
                obj = new EnemyWalkStyle1(-1);
                break;

            case WalkingStyle.Style1GoRight:
                obj = new EnemyWalkStyle1(1);
                break;

            case WalkingStyle.Style2:
                obj = new EnemyWalkStyle2();
                break;
/*
            case WalkingStyle.Style3:
                obj = new EnemyWalkStyle3();
                break;

            case WalkingStyle.Style4:
                obj = new EnemyWalkStyle4();
                break;

            case WalkingStyle.Style5:
                obj = new EnemyWalkStyle5();
                break;

            case WalkingStyle.Style6:
                obj = new EnemyWalkStyle6();
                break;

            case WalkingStyle.Style7:
                obj = new EnemyWalkStyle7();
                break;
*/
            default:
                return null;
        }

        obj.enemyBaseSpeed = enemyBaseSpeed;
        obj.playerRb = playerRb;
        obj.enemyRb = enemyRb;
        return obj;
    }
}
