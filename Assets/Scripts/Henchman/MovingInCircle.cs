using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInCircle : MonoBehaviour
{
    [SerializeField]
    public float speed = 0.03f;
    // the radius of the player compared to the fishtank
    [SerializeField]
    public float radius;
    private float angle;

    public float dragging_speed = 0.01f;

    // the player's rigidbody
    private Rigidbody2D henchman;

    [SerializeField]
    private GameObject fishtank;
    [SerializeField]
    private float dragProximity = 0.03f;

    public GameObject line;

    //meant for isometric calc
    private float squishFactor = 2f;

    // remove me at the end
    [SerializeField]
    private bool bool_debug = false;
    [SerializeField]
    private float float_debug1 = 0f;
    [SerializeField]
    private float float_debug2 = 0f;
    [SerializeField]
    private Vector2 vector2_debug1;
    [SerializeField]
    private Vector2 vector2_debug2;
    [SerializeField]
    private Vector2 vector2_debug3;

    public GameObject currentFirePoint;

    private Dictionary<SpriteDirectionResolver.Direction, GameObject> dirToFirePoint;

    public Sprite S;
    public Sprite N;

    // Start is called before the first frame update
    void Start()
    {
        henchman = GetComponent<Rigidbody2D>();
        // NOTE: hencman must start with same y as fishtank, to make it easier for the isometric transforms.

        Vector3 radiusVector = (fishtank.transform.position - henchman.transform.position);
        radius = radiusVector.magnitude;
        angle = Mathf.Atan(radiusVector.y / radiusVector.x);

        dirToFirePoint = new Dictionary<SpriteDirectionResolver.Direction, GameObject> {
                {SpriteDirectionResolver.Direction.SW, this.gameObject.transform.GetChild(0).gameObject},
                {SpriteDirectionResolver.Direction.NE, this.gameObject.transform.GetChild(1).gameObject},
                {SpriteDirectionResolver.Direction.NW, this.gameObject.transform.GetChild(2).gameObject},
                {SpriteDirectionResolver.Direction.SE, this.gameObject.transform.GetChild(3).gameObject}
            };

        currentFirePoint = dirToFirePoint[SpriteDirectionResolver.Direction.SW];
    }

    private Vector2 get_to_position()
    {
        Vector2 movement = new Vector2(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"));

        if (movement.magnitude < 0.001)
        {
            return default;
        }

        Vector2 fishtankPos = (Vector2)fishtank.transform.position;

        // convert keys to place on circle
        movement /= movement.magnitude;
        movement *= radius;
        movement += fishtankPos;

        // correct for isometric 
        movement.y = fishtankPos.y + ((movement.y - fishtankPos.y) / squishFactor);
        return movement;
    }

    private void MoveLine()
    {
        //line.transform.position = henchman.transform.position;
        line.transform.position = (henchman.transform.position - fishtank.transform.position) / 1.2f + fishtank.transform.position;
        float pi = Mathf.PI;
        if (((angle < 2 * pi) && (angle > pi)) || ((angle > -pi) && (angle < 0)))
        {
            henchman.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            line.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            fishtank.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            henchman.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
            line.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            fishtank.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
        line.transform.eulerAngles = new Vector3(0, 0, angle * 180 / (Mathf.PI));
    }

    private bool GoClockwise(Vector2 currentPos, Vector2 endPos)
    {
        Vector2 currentFix = currentPos - (Vector2)fishtank.transform.position;
        Vector2 endFix = endPos - (Vector2)fishtank.transform.position;
        currentFix.y *= squishFactor;
        endFix.y *= squishFactor;
        float diff = Mathf.Atan2(endFix.y, endFix.x) -
            Mathf.Atan2(currentFix.y, currentFix.x);

        if (diff < 0) { diff += 2 * Mathf.PI; }
        return diff < (Mathf.PI);
    }

    private void RotateSprite(Vector2 newPosition)
    {
        Debug.Log("Entering RotateSprite");
        Vector2 fishTankPos = (Vector2)fishtank.transform.position;
        Vector2 lookDirection = newPosition - fishTankPos;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 180f;

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

        currentFirePoint.transform.rotation = Quaternion.Euler(0, 0, angle - 180);

    }

    private bool MovePlayer(Vector2 endPos)
    {
        Debug.Log("Entering MovePlayer");
        Vector3 henchmanPos = henchman.transform.position;
        Vector3 fishtankPos = fishtank.transform.position;

        if ((endPos - (Vector2)henchmanPos).magnitude < dragProximity)
            return true;

        float_debug1 = (endPos - (Vector2)henchmanPos).magnitude;

        float clockwise = -1f;
        if (GoClockwise((Vector2)henchmanPos, endPos))
        {
            clockwise = 1;
        }
        float angularSpeed = clockwise * speed;
        angle += angularSpeed;//* Time.deltaTime;;
        if (angle > 2 * Mathf.PI)
            angle -= 2 * Mathf.PI;
        if (angle < -2 - Mathf.PI)
            angle += 2 * Mathf.PI;

        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

        Vector3 newPos = new Vector3(
            fishtankPos.x + offset.x,
            fishtankPos.y + (offset.y / squishFactor),
            fishtankPos.z);

        henchman.transform.position = newPos;
        RotateSprite((Vector2)newPos);
        return false;
    }

    void DragFishtank()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        movement *= dragging_speed;
        fishtank.transform.position += movement;
        henchman.transform.position += movement;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 endPos = get_to_position();
        if (endPos != default)
        {
            bool dragNow = MovePlayer(endPos);
            if (dragNow)
            {
                Debug.Log("Start dragging here");
                DragFishtank();
            }
        }
        MoveLine();
    }
}