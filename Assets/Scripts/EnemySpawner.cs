﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public float elapsedTime = 0.0f;
    public GameObject enemyObjectPrefab; 

    [SerializeField]
    private List<float> secondsBetweenSpawn;

    private new int i = 0;

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyObjectPrefab);
        newEnemy.transform.position = transform.position;
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