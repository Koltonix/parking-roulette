using UnityEngine;

namespace Roads.Placement
{
    public class PlacementHandler : MonoBehaviour
    {
        [Header("Input")]
        public RaycastHit hit;
        [Space]

        [Header("Placement")]
        private Placement currentPlacement;
        [SerializeField]
        private Placement roads;
        [SerializeField]
        private Placement path;

        private void Update()
        {
            
        }

        public void PlaceItem()
        {
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
    }
}