using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempWeaponCollider : MonoBehaviour
{
    public GameObject weaponType;
    public float weaponDuration;

    void OnTriggerEnter2D(Collider2D other)
    {
        Shooting otherShooting = other.gameObject.GetComponent<Shooting>();
        otherShooting.SetTemporaryWeapon(weaponType, weaponDuration);
        Destroy(gameObject);
    }
}
