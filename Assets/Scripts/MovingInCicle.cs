using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInCicle : MonoBehaviour
{
    
    [SerializeField]
    public float speed = 0.03f;
    // the radius of the player compared to the fishtank
    [SerializeField]
    public float radius;
    private float angle;

    // the player's rigidbody
    private Rigidbody2D henchman;

    [SerializeField]
    private GameObject fishtank;
    [SerializeField]
    private float dragProximity = 0.03f;

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


    // Start is called before the first frame update
    void Start()
    {
        henchman = GetComponent<Rigidbody2D>();
        // NOTE: hencman must start with same y as fishtank, to make it easier for the isometric transforms.

        Vector3 radiusVector = (fishtank.transform.position - henchman.transform.position);
        radius = radiusVector.magnitude;
        angle = Mathf.Atan(radiusVector.y / radiusVector.x);
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

    private bool MovePlayer(Vector2 endPos)
    {
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


        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        
        Vector3 newPos = new Vector3(
            fishtankPos.x + offset.x,
            fishtankPos.y + (offset.y / squishFactor),
            fishtankPos.z);

        henchman.transform.position = newPos;
        return false;
    }

    void DragFishtank()
    {

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
    }
}