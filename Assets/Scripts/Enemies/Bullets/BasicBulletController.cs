using UnityEngine;
using System.Collections;

public abstract class EnemyBulletController
{
    protected float bulletSpeed;
    protected float minBulletMs;
    protected float maxBulletMs;
    public float bulletFrequency;

    protected Rigidbody2D playerRb;
    protected Rigidbody2D enemyRb;
    protected Rigidbody2D bulletRb;

    public enum BulletStyle
    {
        NoBullet,
        DirectDown,
        Back45Degrees,
        // Mitbayet Bullet
        Bullseye,
        // More than one
        BundleBullet,
        Flamer,
        AtomBomb,
        BenStealer       
    }

    public virtual void ShootBullet()
    {
    }

    public virtual void Collide()
    {
    }

    public static EnemyBulletController Create(BulletStyle bulletStyle, float enemyBaseSpeed, Rigidbody2D playerRb, Rigidbody2D enemyRb, Rigidbody2D bulletRb)
    {
        EnemyBulletController obj = null;

       
        switch (bulletStyle)
        {
            case BulletStyle.NoBullet:
                obj = new NoBullet();
                break;
            case BulletStyle.DirectDown:
                obj = new BulletDirectDown();
                break;

            /*          
             *             case BulletStyle.Bullseye:
                                obj = new BulletBullseye();
                                break;
                        case BulletStyle.Back45Degrees:
                            obj = new EnemyWalkStyle3();
                            break;

                        case BulletStyle.BundleBullet:
                            obj = new EnemyWalkStyle4();
                            break;

                        case WalkiBulletStylengStyle.Flamer:
                            obj = new EnemyWalkStyle5();
                            break;

                        case BulletStyle.AtomBomb:
                            obj = new EnemyWalkStyle6();
                            break;

                        case BulletStyle.BenStealer:
                            obj = new EnemyWalkStyle7();
                            break;
            */
            default:
                obj = new NoBullet();
                break;
        }

        obj.bulletSpeed = enemyBaseSpeed;
        obj.playerRb = playerRb;
        obj.enemyRb = enemyRb;
        obj.bulletRb = bulletRb;
        obj.bulletFrequency = Random.Range(obj.minBulletMs, obj.maxBulletMs);
        return obj;
    }
}
