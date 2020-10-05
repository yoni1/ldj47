using UnityEngine;
using System.Collections;

public class HenchmanCollisionHandler : MonoBehaviour
{
    public Sprite deadSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (LayerMask.LayerToName(collision.gameObject.layer)) {
            case ("EnemyBullet"):
                gameObject.GetComponent<MovingInCircle>().canMove = false;
                gameObject.GetComponent<Shooting>().canShoot = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = deadSprite;
                break;
         }
    }
}
