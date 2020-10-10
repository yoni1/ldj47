using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class HenchmanCollisionHandler : MonoBehaviour
{
    public Sprite deadSprite;

    public float deadBeforeGameOverSec = 1.5f;
    public float gameOverBeforeRestartSec = 1.5f;//3.0f;

    private bool isDead = false;
    private float elapsedTime = -1f;

    private Text gameOverText;

    public AudioSource deathSound;
    public AudioSource lostStageSound;

    public Animator animator;

    private void Start()
    {
        gameOverText = GameObject.Find("GameOverText").GetComponent<Text>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Entered trigger for layer: ${LayerMask.LayerToName(collision.gameObject.layer)}");
        switch (LayerMask.LayerToName(collision.gameObject.layer)) {
            case ("EnemyBullet"):
            case ("Enemy"):
                if (isDead)
                    break;
                animator.SetTrigger("is_dead");
                gameObject.GetComponent<MovingInCircle>().canMove = false;
                gameObject.GetComponent<Shooting>().canShoot = false;
                gameObject.GetComponent<MovingInCircle>().dragSound.Stop();
                isDead = true;
                deathSound.Play();
                break;
         }
    }

    void Update()
    {
        if (!isDead)
            return;

        if (elapsedTime < 0f)
        {
            elapsedTime = 0f;
            return;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= deadBeforeGameOverSec && !gameOverText.enabled)
        {
            gameOverText.enabled = true;
            lostStageSound.Play();
            return;
        }

        if (elapsedTime >= deadBeforeGameOverSec + gameOverBeforeRestartSec)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
