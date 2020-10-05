﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public float elapsedTime = 0.0f;
    public GameObject enemyObjectPrefab; 

    public List<float> secondsBetweenSpawn;
    public GameObject bulletFab;
    public EnemyBulletController.BulletStyle bulletStyle;

    private new int i = 0;

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyObjectPrefab);
        newEnemy.transform.position = transform.position;

        // TODO: Choose walking style
        BasicEnemyController newEnemyController = newEnemy.GetComponent<BasicEnemyController>();
        newEnemyController.walkingStyle = EnemyWalkController.WalkingStyle.Style2;
        newEnemyController.bulletStyle = bulletStyle;
        newEnemyController.bullet = bulletFab;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime > secondsBetweenSpawn[i])
        {
            SpawnEnemy();
            i++;

            if (i == secondsBetweenSpawn.Count)
            {
                Destroy(gameObject);
            }
        }

       
    }
}
