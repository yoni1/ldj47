using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    public Sprite hitSprite;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collided with: {collision.gameObject.tag}");

        BasicEnemyController.State currentState = GetComponent<BasicEnemyController>().GetState();

        if (currentState == BasicEnemyController.State.Dead)
        {
            return;
        }

        if (collision.gameObject.tag == "Fish")
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = hitSprite;
            gameObject.GetComponent<BasicEnemyController>().SetState(BasicEnemyController.State.Dead);
        }
    }

}
