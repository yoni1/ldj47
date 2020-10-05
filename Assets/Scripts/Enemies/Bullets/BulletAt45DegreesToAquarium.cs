using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAt45DegreesToAquarium : EnemyBulletController
{
    const float bulletForce = 3f;
    const float sqrtHalf = 0.7071067811865475f;

    public override void ShootBullet()
    {
        int xDirection;
        // TODO: compare to aquarium, not to player!
        if (enemyRb.transform.position.x > playerRb.transform.position.x)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }

        MakeBullet(new Vector3(xDirection, -1, 0) * sqrtHalf * bulletForce);
    }
}
