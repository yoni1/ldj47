using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform firePoint;
    public GameObject fishPrefab;

    public float fishForce = 5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject fish = Instantiate(fishPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rigidBody = fish.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(firePoint.right * fishForce, ForceMode2D.Impulse);
    }
}
