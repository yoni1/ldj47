using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject fishPrefab;
    private GameObject originalFishPrefab;

    public float fishForce = 13f;

    private float remainingPowerupTime = 0f;

    private float remainingCooldown = 0f;
    public float shotCooldown = 0.25f;

    public bool canShoot = true;

    void Start()
    {
        originalFishPrefab = fishPrefab;
    }

    public void SetTemporaryWeapon(GameObject newFishPrefab, float powerupTime)
    {
        fishPrefab = newFishPrefab;
        remainingPowerupTime = powerupTime;
    }

    public void SetPermanentWeapon(GameObject newFishPrefab)
    {
        originalFishPrefab = newFishPrefab;
        fishPrefab = newFishPrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
            return;
        
        if (Input.GetButton("Fire1"))
        {
            if (0.0f != remainingCooldown)
            {
                remainingCooldown -= Time.deltaTime;
                if (0.0f > remainingCooldown)
                {
                    remainingCooldown = 0.0f;
                }
            }

            if (0.0f == remainingCooldown)
            {
                Shoot();
                remainingCooldown = shotCooldown;
            }
        }

        if (0.0f != remainingPowerupTime)
        {
            remainingPowerupTime -= Time.deltaTime;
            if (0.0f >= remainingPowerupTime)
            {
                remainingPowerupTime = 0.0f;
                fishPrefab = originalFishPrefab;
            }
        }
    }

    void Shoot()
    {
        Transform currentFirePoint = GetComponent<MovingInCircle>().currentFirePoint.transform;
        GameObject fish = Instantiate(fishPrefab, currentFirePoint.position, Quaternion.identity);
        FishProperties fishProperties = (FishProperties)fish.GetComponent(typeof(FishProperties));

        SpriteRenderer renderer = (SpriteRenderer)fish.GetComponent(typeof(SpriteRenderer));
        // TODO: Don't use firePoint.rotation, calculate the angle from the mouse like in PlayerMovement.cs
        float angle = Quaternion.Angle(currentFirePoint.rotation, Quaternion.identity);

        SpriteDirectionResolver.Direction direction = SpriteDirectionResolver.ResolveDirection(currentFirePoint.rotation);

        switch (direction)
        {
            case SpriteDirectionResolver.Direction.W:
                renderer.sprite = fishProperties.W;
                break;
            case SpriteDirectionResolver.Direction.NW:
                renderer.sprite = fishProperties.NW;
                break;
            case SpriteDirectionResolver.Direction.N:
                renderer.sprite = fishProperties.N;
                break;
            case SpriteDirectionResolver.Direction.NE:
                renderer.sprite = fishProperties.NE;
                break;
            case SpriteDirectionResolver.Direction.E:
                renderer.sprite = fishProperties.E;
                break;
            case SpriteDirectionResolver.Direction.SE:
                renderer.sprite = fishProperties.SE;
                break;
            case SpriteDirectionResolver.Direction.S:
                renderer.sprite = fishProperties.S;
                break;
            case SpriteDirectionResolver.Direction.SW:
                renderer.sprite = fishProperties.SW;
                break;
        }

        Rigidbody2D rigidBody = fish.GetComponent<Rigidbody2D>();
        rigidBody.AddForce(currentFirePoint.right * fishForce, ForceMode2D.Impulse);
        Destroy(fish, 5.0f);
    }
}
