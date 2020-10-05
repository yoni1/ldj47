using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int enemiesDeadCount = 0;
    public int enemiesGonePastYouCount = 0;

    public Text masterHPText;
    public Text enemiesDeadText;
    public GameObject alert;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void killEnemy()
    {
        enemiesDeadCount++;
        masterHPText.text = "Dead Enemies: " + enemiesDeadCount;
    }

    public void enemyGonePastYou(Collider2D collision)
    {
        enemiesGonePastYouCount++;
        Vector3 pos = collision.transform.position;
        Debug.Log("pos " + pos);
        _ = Instantiate(alert, pos, Quaternion.identity);
        
        enemiesDeadText.text = "Mastermind HP: " + enemiesGonePastYouCount;
        

    }
}
