using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Walking,
        Knockback,
        Dead
    }

    private State currentState;

    private float elapsedTime = 0.0f;
    private GameObject player;
    private Rigidbody2D enemyRb, playerRb;
    public GameObject bulletPrefab;

    [SerializeField]
    private float enemyBaseSpeed;

    [SerializeField]
    private float bulletSpeed;

    public EnemyWalkController.WalkingStyle walkingStyle;
    private EnemyWalkController walkController;

    public EnemyBulletController.BulletStyle bulletStyle;
    private EnemyBulletController bulletController;

    private void Start()
    {
        player = GameObject.Find("Henchman").gameObject;
        enemyRb = GetComponent<Rigidbody2D>();
        playerRb = player.GetComponent<Rigidbody2D>();

        walkController = EnemyWalkController.Create(walkingStyle, enemyBaseSpeed, playerRb, enemyRb);
        walkController.BeginWalk();

        bulletController = EnemyBulletController.Create(bulletStyle, bulletSpeed, playerRb, enemyRb, bulletPrefab);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Walking:
                walkController.UpdateWalkingState();
                break;
        }

        spawnBullet();
    }
    
    private void spawnBullet()
    {
        if (bulletController == null)
        {
            return;
        }

        elapsedTime += Time.deltaTime;
        if (elapsedTime > bulletController.bulletFrequency)
        {
            GameObject bullet = Instantiate(bulletPrefab, enemyRb.transform.position, Quaternion.identity);
            Destroy(bullet, 5.0f);

            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.AddForce(bulletController.CalcBulletFireDirection(), ForceMode2D.Impulse);

            elapsedTime -= bulletController.bulletFrequency;
        }
    }

    private void SetState(State newState)
    {
        if (newState == currentState)
        {
            return;
        }

        switch (currentState)
        {
            case State.Walking:
                walkController.EndWalk();
                break;
        }

        currentState = newState;

        switch (currentState)
        {
            case State.Walking:
                walkController.BeginWalk();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        walkController.OnCollide();
    }
}
