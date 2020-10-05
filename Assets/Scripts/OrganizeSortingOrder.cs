using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class OrganizeSortingOrder : MonoBehaviour
    {
        private SpriteRenderer sp;
        public int sortingLayerOffset = 0;

        // Use this for initialization
        void Start()
        {
            sp = GetComponent<SpriteRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            float bottom = sp.bounds.center.y + sp.bounds.extents.y;
            sp.sortingOrder = -(int)(bottom * 100.0f) + sortingLayerOffset;
        }
    }
}