using UnityEngine;
using System.Collections;

public abstract class EnemyBulletController
{
    protected float bulletSpeed;
    const float c_minBulletSec = 0.5f;
    const float c_maxBulletSec = 1.5f;
    public float bulletFrequency;

    protected Rigidbody2D playerRb;
    protected Rigidbody2D enemyRb;
    protected GameObject bulletPrefab;

    public enum BulletStyle
    {
        NoBullet,
        DirectDown,
        At45DegreesToAquarium,
        // Mitbayet Bullet
        Bullseye,
        // More than one
        BundleBullet,
        Flamer,
        AtomBomb,
        BenStealer       
    }

    public abstract Vector3 CalcBulletFireDirection();

    public static EnemyBulletController Create(BulletStyle bulletStyle, float bulletSpeed, Rigidbody2D playerRb, Rigidbody2D enemyRb,
        GameObject bulletPrefab, float minBulletSec, float maxBulletSec)
    {
        EnemyBulletController obj = null;

       
        switch (bulletStyle)
        {
            case BulletStyle.DirectDown:
                obj = new BulletDirectDown();
                break;
            case BulletStyle.At45DegreesToAquarium:
                obj = new BulletAt45DegreesToAquarium();
                break;
            default:
                return null;
        }

        obj.bulletSpeed = bulletSpeed;
        obj.playerRb = playerRb;
        obj.enemyRb = enemyRb;
        obj.bulletFrequency = Random.Range(minBulletSec, maxBulletSec);
        obj.bulletPrefab = bulletPrefab;
        return obj;
    }
}
