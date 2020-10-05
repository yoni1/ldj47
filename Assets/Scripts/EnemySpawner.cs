using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public float elapsedTime = 0.0f;
    public GameObject enemyObjectPrefab; 

    public List<float> secondsBetweenSpawn;

    private new int i = 0;
    private System.Random rnd = new System.Random();

    void SpawnEnemy()
    {
        GameObject newEnemy = Instantiate(enemyObjectPrefab);
        newEnemy.transform.position = transform.position;

        // TODO: Choose walking style
        BasicEnemyController newEnemyController = newEnemy.GetComponent<BasicEnemyController>();
        switch (rnd.Next(3))
        {
            case 0:
                newEnemyController.walkingStyle = EnemyWalkController.WalkingStyle.JustDown;
                break;
            case 1:
                newEnemyController.walkingStyle = EnemyWalkController.WalkingStyle.ZigZagWideStartRight;
                break;
            case 2:
                newEnemyController.walkingStyle = EnemyWalkController.WalkingStyle.ZigZagWideStartLeft;
                break;
        }

        // TODO: Choose bullet style
        switch (rnd.Next(2))
        {
            case 0:
                newEnemyController.bulletStyle = EnemyBulletController.BulletStyle.DirectDown;
                break;

            case 1:
                newEnemyController.bulletStyle = EnemyBulletController.BulletStyle.At45DegreesToAquarium;
                break;
        }
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
