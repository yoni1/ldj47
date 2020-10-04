using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishProperties : MonoBehaviour
{
    public Sprite NW;
    public Sprite N;
    public Sprite NE;
    public Sprite E;
    public Sprite SE;
    public Sprite S;
    public Sprite SW;
    public Sprite W;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
