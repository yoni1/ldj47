using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirectDown : EnemyBulletController
{
    const float bulletForce = 3f;

    public override void ShootBullet()
    {
        MakeBullet(new Vector3(0, -1, 0) * bulletForce);
    }
}
