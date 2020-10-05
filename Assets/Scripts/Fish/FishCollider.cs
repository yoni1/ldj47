using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCollider : MonoBehaviour
{
    public AudioSource audioSrc;
    public AudioClip hitSound;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<BasicEnemyController>().GetState() != BasicEnemyController.State.Dead)
        {
            audioSrc.PlayOneShot(hitSound);
            Destroy(gameObject);
        }
    }
}