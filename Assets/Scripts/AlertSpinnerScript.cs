using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertSpinnerScript : MonoBehaviour
{
    private float lifetime = 0.0f;
    private Transform child;

    // Start is called before the first frame update
    void Start()
    {
        lifetime = 0f;
        child = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        lifetime += Time.deltaTime;
        child.Rotate(new Vector3(0.0f, 0.0f, Time.deltaTime * 540.0f));
        if (2.0f <= lifetime)
        {
            Destroy(gameObject);  
        }
    }

    private void OnDestroy()
    {
        Debug.Log("destoyed");
    }
}
