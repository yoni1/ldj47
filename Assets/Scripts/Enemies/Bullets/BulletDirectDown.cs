using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirectDown : EnemyBulletController
{
    private Vector3 worldVector = new Vector3();

    float bulletForce = 12.4f;
   

    public override void ShootBullet()
    {
        Vector3 currentFirePoint = new Vector3(playerRb.transform.position.y, -1, 0);
        bulletRb.AddForce(currentFirePoint*bulletForce, ForceMode2D.Impulse);
    }
}
