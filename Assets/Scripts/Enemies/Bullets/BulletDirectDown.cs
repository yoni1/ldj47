using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirectDown : EnemyBulletController
{
    private Vector3 worldVector = new Vector3();

    const float bulletForce = 3f;

    public override void ShootBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, enemyRb.transform.position, Quaternion.identity);
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

        Vector3 currentFirePoint = new Vector3(0, -1, 0);
        bulletRb.AddForce(currentFirePoint*bulletForce, ForceMode2D.Impulse);
    }
}
