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

    private GameObject enemy, player;
    private Rigidbody2D enemyRb, playerRb;

    [SerializeField]
    private Transform wallCheck;

    [SerializeField]
    private float wallCheckDistance;

    [SerializeField]
    private LayerMask whatIsWall;

    [SerializeField]
    private float enemyBaseSpeed;

    public EnemyWalkController.WalkingStyle walkingStyle = EnemyWalkController.WalkingStyle.RandomWalkingStyle;
    private EnemyWalkController walkController;

    private void Start()
    {
        enemy = transform.Find("JamesBond").gameObject;
        player = GameObject.Find("Hanchman").gameObject;
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
