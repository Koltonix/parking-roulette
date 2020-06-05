using UnityEngine;
using Roads.Enums;

namespace Roads.Placement
{
    public class PlacementHandler : MonoBehaviour
    {
        [Header("Input")]
        public RaycastHit hit;
        [Space]

        [Header("Placement")]
        [SerializeField]
        private PlacementType placement;
        private Placement currentPlacement;
        [SerializeField]
        private Placement roads;
        [SerializeField]
        private Placement path;

        private void Update()
        {
            
        }

        public void OnLeftClick()
        {
            if (!hit.collider)
            {
                Deselect();
                return;
            }

            currentPlacement = hit.collider.GetComponent<Placement>();
            placement = (currentPlacement != null) ? currentPlacement.placement : PlacementType.NULL;

            PlaceItem();
        }

        public void OnRightClick()
        {
            if (currentPlacement)
                DestroyItem();
        }

        public void PlaceItem()
        {
            if (currentPlacement)
                currentPlacement.PlaceItem(hit.point);
        }

        public void DestroyItem()
        {
            currentPlacement.RemoveItem(hit.point);
        }

        public void ReceiveRaycastHit(RaycastHit hit)
        {
            this.hit = hit;
        }

        private void Deselect()
        {
            currentPlacement = null;
            placement = PlacementType.NULL;
        }
    }
}