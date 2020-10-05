using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int enemiesDeadCount = 0;
    public int enemiesGonePastYouCount = 0;
    public int enemiesTotal;

    public Text masterHPText;
    public Text enemiesDeadText;
    public GameObject alert;

    public AudioSource enemyPassedSound;

    // Start is called before the first frame update
    void Start()
    {
        masterHPText.text = "Dead agents: " + enemiesDeadCount + " / " + enemiesTotal;
        enemiesDeadText.text = "Agents escaped: " + enemiesGonePastYouCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void killEnemy()
    {
        enemiesDeadCount++;
        masterHPText.text = "Dead agents: " + enemiesDeadCount + " / " + enemiesTotal;
    }

    public void enemyGonePastYou(Collider2D collision)
    {
        enemiesGonePastYouCount++;
        Vector3 pos = collision.transform.position;
        Debug.Log("pos " + pos);
        _ = Instantiate(alert, pos, Quaternion.identity);
        
        enemiesDeadText.text = "Agents escaped: " + enemiesGonePastYouCount;

        enemyPassedSound.Play();
    }
}
