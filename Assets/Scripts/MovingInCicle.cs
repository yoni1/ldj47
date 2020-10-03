using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingInCicle : MonoBehaviour
{
    
    [SerializeField]
    public float speed = 0.1f;
    // the radius of the player compared to the fishtank
    [SerializeField]
    public float radius;
    private float angle;

    // the player's rigidbody
    private Rigidbody2D henchman;

    [SerializeField]
    private GameObject fishtank;

    //meant for isometric calc
    private float squish_factor = 2f;

    // remove me at the end
    [SerializeField]
    private bool bool_debug = false;
    [SerializeField]
    private float float_debug1 = 0f;
    [SerializeField]
    private float float_debug2 = 0f;
    [SerializeField]
    private Vector2 vector2_debug1;

    // Start is called before the first frame update
    void Start()
    {
        henchman = GetComponent<Rigidbody2D>();
        Vector3 radiusVector = (fishtank.transform.position - henchman.transform.position);
        radius = radiusVector.magnitude;
        angle = Mathf.Atan(radiusVector.y / radiusVector.x);
    }

    private Vector2 get_to_position()
    {
        Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * speed,
                                Input.GetAxis("Vertical") * speed);

        if (movement.magnitude < 0.001)
        {
            return default;
        }

        Vector2 fishtankPos = (Vector2)fishtank.transform.position;

        // convert keys to place on circle
        movement /= movement.magnitude;
        movement += fishtankPos;
        movement *= Mathf.Sqrt(radius);

        // curret for isometric 
        movement.y = fishtankPos.y + ((movement.y - fishtankPos.y) / squish_factor);
        return movement;
    }

    private bool GoClockwise(Vector2 current_pos, Vector2 end_pos)
    {
        Vector2 current_fix = current_pos - (Vector2)fishtank.transform.position;
        Vector2 end_fix = end_pos - (Vector2)fishtank.transform.position;
        current_fix.y *= squish_factor;
        end_fix.y *= squish_factor;
        float diff = Mathf.Atan2(end_fix.y, end_fix.x) - 
            Mathf.Atan2(current_fix.y, current_fix.x);
        if (diff < 0) { diff += 2 * Mathf.PI; }
        bool_debug = diff < (Mathf.PI);
        return bool_debug;
    }

    private void MovePlayer(Vector2 endPos)
    {
        Vector3 henchmanPos = henchman.transform.position;
        Vector2 fishtankPos = (Vector2)fishtank.transform.position;
        float clockwise = -1f;
        if (GoClockwise((Vector2)henchmanPos, endPos))
        {
            clockwise = 1;
        }
        float angularSpeed = clockwise * speed;
        angle += angularSpeed;// * Time.deltaTime;
        if (angle > 2 * Mathf.PI)
            angle -= 2 * Mathf.PI;

        var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        henchman.transform.position = new Vector3(
            fishtankPos.x + offset.x,
            fishtankPos.y + (offset.y / squish_factor),
            henchmanPos.z);
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
            Vector2 henchman2DPos = henchman.transform.position;
            if ((endPos - henchman2DPos).magnitude < 0.05)
            {
                Debug.Log("Start dragging here");
                DragFishtank();
            }

            MovePlayer(endPos);
        }


        //Vector2 movement = new Vector2(Input.GetAxis("Horizontal") * speed,
        //                        Input.GetAxis("Vertical") * speed);

        

        //float angularSpeed = -Input.GetAxis("Horizontal") * speed;

        //if (Mathf.Abs(angularSpeed) > 0.001)
        //{
        //    //Debug.Log("movement normed" + (movement / movement.magnitude));

        //    //Debug.Log("movement = " + movement.magnitude);


        //    angle += angularSpeed;// * Time.deltaTime;
        //    if (angle > 2 * Mathf.PI)
        //        angle -= 2 * Mathf.PI;


        //    var offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
        //    henchman.transform.position = new Vector3(
        //        fishtank.transform.position.x + offset.x,
        //        fishtank.transform.position.y + (offset.y / squish_factor),
        //        henchman.transform.position.z);
        //}

    }
}