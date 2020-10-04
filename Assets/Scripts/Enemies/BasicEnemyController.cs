using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
  private enum State {
    Walking,
    Knockback,
    Dead
  }

  private State currentState;

  [SerializeField]
  private float movementSpeed;

  private int direction;

  private Vector2 movement;
  private GameObject jamesBond, player;
  private Rigidbody2D jamesBondRb, playerRb;

  private bool wallDetected;

  [SerializeField]
  private Transform wallCheck;

  [SerializeField]
  private float wallCheckDistance;

  [SerializeField]
  private LayerMask whatIsWall;

  private void Start() {
    jamesBond = transform.Find("JamesBond").gameObject;
    player = GameObject.Find("Hanchman").gameObject;
    jamesBondRb = jamesBond.GetComponent<Rigidbody2D>();
    playerRb = player.GetComponent<Rigidbody2D>();
  }

  private void Update() {
    switch (currentState) {
      case State.Walking:
        UpdateWalkingState();
        break;
      default:
        break;
    }
  }

  private void Flip() {

  }

  // WALKING STATE

  private void EnterWalkingState() {

  }

  private void UpdateWalkingState() {
    wallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);

    if (jamesBondRb.position.x > playerRb.position.x) {
      direction = 1;
    } else {
      direction = -1;
    }

    movement.Set(playerRb.position.x * direction * movementSpeed, playerRb.position.y * movementSpeed);
    jamesBondRb.velocity = movement;
    //transform.position = Vector3.MoveTowards(transform.position, playerRb.position, movementSpeed * Time.deltaTime);
  }

  private void ExitWalkingState() {

  }
}
