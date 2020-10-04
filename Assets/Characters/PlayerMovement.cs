using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rigidBody;
    public Camera camera;

    public Sprite N;
    public Sprite S;

    Vector2 movement;
    Vector2 mousePosition;

    public GameObject currentFirePoint;

    private Dictionary<SpriteDirectionResolver.Direction, GameObject> dirToFirePoint;

    void Start()
    {
        dirToFirePoint = new Dictionary<SpriteDirectionResolver.Direction, GameObject> {
            {SpriteDirectionResolver.Direction.SW, this.gameObject.transform.GetChild(0).gameObject},
            {SpriteDirectionResolver.Direction.NE, this.gameObject.transform.GetChild(1).gameObject},
            {SpriteDirectionResolver.Direction.NW, this.gameObject.transform.GetChild(2).gameObject},
            {SpriteDirectionResolver.Direction.SE, this.gameObject.transform.GetChild(3).gameObject}
        };

        currentFirePoint = dirToFirePoint[SpriteDirectionResolver.Direction.SW];
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        
    //}

    //private void FixedUpdate()
    //{
        rigidBody.MovePosition(rigidBody.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - rigidBody.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 180f;

        //rigidBody.rotation = angle;
        //this.gameObject.transform.GetChild(0).transform.Rotate(new Vector3(0, 0, angle));
        // TODO(geran): This can be made faster if we give the firepoint a rigidbody and use rigidbody.rotation
        //this.gameObject.transform.GetChild(0).transform.rotation = Quaternion.Euler(0, 0, angle-180);

        SpriteDirectionResolver.Direction direction = SpriteDirectionResolver.ResolveDirection(-angle);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        switch (direction)
        {
            case SpriteDirectionResolver.Direction.W:
                break;
            case SpriteDirectionResolver.Direction.NW:
                renderer.sprite = N;
                currentFirePoint = dirToFirePoint[SpriteDirectionResolver.Direction.NW];
                Debug.Log($"Sprite is now N, angle is {angle}");
                renderer.flipX = false;
                break;
            case SpriteDirectionResolver.Direction.N:
                break;
            case SpriteDirectionResolver.Direction.NE:
                renderer.sprite = N;
                currentFirePoint = dirToFirePoint[SpriteDirectionResolver.Direction.NE];
                Debug.Log($"Sprite is now N flip, angle is {angle}");
                renderer.flipX = true;
                break;
            case SpriteDirectionResolver.Direction.E:
                break;
            case SpriteDirectionResolver.Direction.SE:
                renderer.sprite = S;
                currentFirePoint = dirToFirePoint[SpriteDirectionResolver.Direction.SE];
                Debug.Log($"Sprite is now S flip, angle is {angle}");
                renderer.flipX = true;
                break;
            case SpriteDirectionResolver.Direction.S:
                break;
            case SpriteDirectionResolver.Direction.SW:
                renderer.sprite = S;
                currentFirePoint = dirToFirePoint[SpriteDirectionResolver.Direction.SW];
                Debug.Log($"Sprite is now S, angle is {angle}");
                renderer.flipX = false;
                break;
        }

        Vector2 shootDirection = mousePosition - (Vector2)currentFirePoint.transform.position;
        // TODO: This might be an issue with the -180
        float shootAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;

        currentFirePoint.transform.rotation = Quaternion.Euler(0, 0, shootAngle);

    }
}
 