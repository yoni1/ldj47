using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyObject;
    public float elapsedTime = 0.0f;

    [AddComponentMenu]
    public float secondsBetweenSpawn;

    private void Start()
    {
        secondsBetweenSpawn = Random.Range(1f, 4f);
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime> secondsBetweenSpawn)
        {
            elapsedTime = 0;
            Instantiate(enemyObject, transform.position, transform.rotation);
        }
    }
}