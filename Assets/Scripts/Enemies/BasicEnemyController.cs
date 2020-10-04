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
    private GameObject enemy, player, bullet;
    private Rigidbody2D enemyRb, playerRb, bulletRb;

    [SerializeField]
    private Transform wallCheck;

    [SerializeField]
    private float wallCheckDistance;

    [SerializeField]
    private LayerMask whatIsWall;

    [SerializeField]
    private float enemyBaseSpeed;

    [SerializeField]
    private float bulletSpeed;

    public EnemyWalkController.WalkingStyle walkingStyle = EnemyWalkController.WalkingStyle.RandomWalkingStyle;
    private EnemyWalkController walkController;

    public EnemyBulletController.BulletStyle bulletStyle;
    private EnemyBulletController bulletController;

    private void Start()
    {
        enemy = transform.Find("JamesBond").gameObject;
        player = GameObject.Find("Henchman").gameObject;
        enemyRb = enemy.GetComponent<Rigidbody2D>();
        playerRb = player.GetComponent<Rigidbody2D>();

        walkController = EnemyWalkController.Create(walkingStyle, enemyBaseSpeed, playerRb, enemyRb);
        walkController.BeginWalk();

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

        elapsedTime += Time.deltaTime;
        if (elapsedTime > bulletController.bulletFrequency)
        {
            bullet = Instantiate(bullet);
            bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletController = EnemyBulletController.Create(bulletStyle, bulletSpeed, playerRb, enemyRb, bulletRb);
            bulletController.ShootBullet();
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
}
