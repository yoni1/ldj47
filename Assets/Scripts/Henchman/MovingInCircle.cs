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

    [SerializeField]
    private GameObject pinkspot;

    //meant for isometric calc
    private float squishFactor = 2f;

    public Transform currentFirePoint;
    public Transform currentTetherPoint;

    private Dictionary<SpriteDirectionResolver.Direction, Transform> dirToFirePoint;
    private Dictionary<SpriteDirectionResolver.Direction, Transform> dirToTetherPoint;

    public Sprite S;
    public Sprite SW;
    public Sprite N;
    public Sprite NW;
    public Sprite W;

    public float pinkSpotFactor = 2.11f;
    public float pipeTextureFactor = 2f;

    public bool canMove = true;

    private LineRenderer lineRenderer;

    // Start is called before the first frame update
    void Start()
    {
        henchman = GetComponent<Rigidbody2D>();
        // NOTE: hencman must start with same y as fishtank, to make it easier for the isometric transforms.

        Vector3 radiusVector = (fishtank.transform.position - henchman.transform.position);
        radius = radiusVector.magnitude;
        angle = Mathf.Atan(radiusVector.y / radiusVector.x);

        dirToFirePoint = new Dictionary<SpriteDirectionResolver.Direction, Transform> {
                {SpriteDirectionResolver.Direction.E, this.gameObject.transform.Find("ActivePoints").Find("FirePointE")},
                {SpriteDirectionResolver.Direction.NE, this.gameObject.transform.Find("ActivePoints").Find("FirePointNE")},
                {SpriteDirectionResolver.Direction.N, this.gameObject.transform.Find("ActivePoints").Find("FirePointN")},
                {SpriteDirectionResolver.Direction.NW, this.gameObject.transform.Find("ActivePoints").Find("FirePointNW")},
                {SpriteDirectionResolver.Direction.W, this.gameObject.transform.Find("ActivePoints").Find("FirePointW")},
                {SpriteDirectionResolver.Direction.SW, this.gameObject.transform.Find("ActivePoints").Find("FirePointSW")},
                {SpriteDirectionResolver.Direction.S, this.gameObject.transform.Find("ActivePoints").Find("FirePointS")},
                {SpriteDirectionResolver.Direction.SE, this.gameObject.transform.Find("ActivePoints").Find("FirePointSE")}
            };

        dirToTetherPoint = new Dictionary<SpriteDirectionResolver.Direction, Transform> {
                {SpriteDirectionResolver.Direction.E, this.gameObject.transform.Find("ActivePoints").Find("TetherPointE")},
                {SpriteDirectionResolver.Direction.NE, this.gameObject.transform.Find("ActivePoints").Find("TetherPointNE")},
                {SpriteDirectionResolver.Direction.N, this.gameObject.transform.Find("ActivePoints").Find("TetherPointN")},
                {SpriteDirectionResolver.Direction.NW, this.gameObject.transform.Find("ActivePoints").Find("TetherPointNW")},
                {SpriteDirectionResolver.Direction.W, this.gameObject.transform.Find("ActivePoints").Find("TetherPointW")},
                {SpriteDirectionResolver.Direction.SW, this.gameObject.transform.Find("ActivePoints").Find("TetherPointSW")},
                {SpriteDirectionResolver.Direction.S, this.gameObject.transform.Find("ActivePoints").Find("TetherPointS")},
                {SpriteDirectionResolver.Direction.SE, this.gameObject.transform.Find("ActivePoints").Find("TetherPointSE")}
            };

        currentFirePoint = dirToFirePoint[SpriteDirectionResolver.Direction.E];
        currentTetherPoint = dirToTetherPoint[SpriteDirectionResolver.Direction.E];

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;

        UpdatePinkLine();

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
        Vector2 fishTankPos = (Vector2)fishtank.transform.position;
        Vector2 lookDirection = newPosition - fishTankPos;

        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 180f;

        SpriteDirectionResolver.Direction direction = SpriteDirectionResolver.ResolveDirection(-angle);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();


        currentFirePoint = dirToFirePoint[direction];
        currentTetherPoint = dirToTetherPoint[direction];

        switch (direction)
        {
            case SpriteDirectionResolver.Direction.W:
                renderer.sprite = W;
                renderer.flipX = false;
                break;
            case SpriteDirectionResolver.Direction.NW:
                renderer.sprite = NW;
                renderer.flipX = false;
                break;
            case SpriteDirectionResolver.Direction.N:
                renderer.sprite = N;
                renderer.flipX = false;
                break;
            case SpriteDirectionResolver.Direction.NE:
                renderer.sprite = NW;
                renderer.flipX = true;
                break;
            case SpriteDirectionResolver.Direction.E:
                renderer.sprite = W;
                renderer.flipX = true;
                break;
            case SpriteDirectionResolver.Direction.SE:
                renderer.sprite = SW;
                renderer.flipX = true;
                break;
            case SpriteDirectionResolver.Direction.S:
                renderer.sprite = S;
                renderer.flipX = false;
                break;
            case SpriteDirectionResolver.Direction.SW:
                renderer.sprite = SW;
                renderer.flipX = false;
                break;
        }

        currentFirePoint.transform.rotation = Quaternion.Euler(0, 0, angle - 180);

    }

    private bool MovePlayer(Vector2 endPos)
    {
        Vector3 henchmanPos = henchman.transform.position;
        Vector3 fishtankPos = fishtank.transform.position;

        if ((endPos - (Vector2)henchmanPos).magnitude < dragProximity)
            return true;

 
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
/*
        float pi = Mathf.PI;
        if (((angle < 2 * pi) && (angle > pi)) || ((angle > -pi) && (angle < 0)))
        {
            henchman.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
            fishtank.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        else
        {
            henchman.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
            fishtank.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 2;
        }
*/
        RotateSprite((Vector2)newPos);
        return false;
    }

    void UpdatePinkLine()
    {

        //GameObject lineConnector = this.gameObject.transform.GetChild(4).gameObject;
        //Vector3 lineVector = (lineConnector.transform.position - pinkspot.transform.position);

        //Vector3 middlePoint = pinkspot.transform.position + (lineVector / 2);

        //pinkLine.transform.position = middlePoint;
        //pinkLine.transform.localScale = new Vector3(1, 1, lineVector.magnitude);

        //float angle = Mathf.Atan2(lineVector.y, lineVector.x) * Mathf.Rad2Deg - 180f;
        //pinkLine.transform.rotation = Quaternion.Euler(0, 0, angle);
        pinkspot.transform.position = (henchman.transform.position - fishtank.transform.position) / pinkSpotFactor + fishtank.transform.position;

        float z = 0f;
        if (((angle < 2 * Mathf.PI) && (angle > Mathf.PI)) || ((angle > -Mathf.PI) && (angle < 0)))
        {
            z = 1f;
        }

        lineRenderer.SetPosition(0, new Vector3(pinkspot.transform.position.x, pinkspot.transform.position.y, z));
        lineRenderer.SetPosition(1, new Vector3(currentTetherPoint.position.x, currentTetherPoint.position.y, z));

        float lineLength = (currentTetherPoint.position - pinkspot.transform.position).magnitude;
        lineRenderer.material.mainTextureScale = new Vector2(lineLength / pipeTextureFactor, 1);
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
        if (!canMove)
            return;

        Vector2 endPos = get_to_position();
        if (endPos != default)
        {
            bool dragNow = MovePlayer(endPos);
            if (dragNow)
            {
                //Debug.Log("Start dragging here");
                DragFishtank();
            }

            UpdatePinkLine();
        }
    }
}