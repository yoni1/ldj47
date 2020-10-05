using UnityEngine;
using System.Collections;

public abstract class EnemyBulletController : MonoBehaviour
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
        Back45Degrees,
        // Mitbayet Bullet
        Bullseye,
        // More than one
        BundleBullet,
        Flamer,
        AtomBomb,
        BenStealer       
    }

    public abstract void ShootBullet();

    public static EnemyBulletController Create(BulletStyle bulletStyle, float bulletSpeed, Rigidbody2D playerRb, Rigidbody2D enemyRb,
        GameObject bulletPrefab)
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
            default:
                obj = new NoBullet();
                break;
        }

        obj.bulletSpeed = bulletSpeed;
        obj.playerRb = playerRb;
        obj.enemyRb = enemyRb;
        obj.bulletFrequency = Random.Range(c_minBulletSec, c_maxBulletSec);
        obj.bulletPrefab = bulletPrefab;
        return obj;
    }
}
