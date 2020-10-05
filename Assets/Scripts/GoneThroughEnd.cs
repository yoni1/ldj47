using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoneThroughEnd : MonoBehaviour
{
    public LevelManager gameLevelManager;
    // Start is called before the first frame update
    void Start()
    {
        gameLevelManager = FindObjectOfType<LevelManager>();

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            gameLevelManager.enemyGonePastYou();
        }
    }
}
