using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class Spawner : MonoBehaviour
{
    public float elapsedTime = 0.0f;
    public GameObject objectPrefab;
    public bool isEnemy;

    public List<float> secondsBetweenSpawn;

    private new int i = 0;
    private System.Random rnd = new System.Random();

    void SpawnObject()
    {
        GameObject newObject = Instantiate(objectPrefab);
        newObject.transform.position = transform.position;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        
        if (elapsedTime > secondsBetweenSpawn[i])
        {
            SpawnObject();
            i++;

            if (i == secondsBetweenSpawn.Count)
            {
                Destroy(gameObject);
            }
        }

       
    }
}
