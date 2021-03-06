﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDirectDown : EnemyBulletController
{
    const float bulletForce = 3f;

    public override Vector3 CalcBulletFireDirection()
    {
        return new Vector3(0, -1, 0) * bulletForce;
    }
}
