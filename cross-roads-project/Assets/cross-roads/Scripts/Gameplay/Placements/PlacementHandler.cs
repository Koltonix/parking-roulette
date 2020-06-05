using UnityEngine;
using Roads.Enums;
using Roads.Events;
using UnityEngine.Events;
using Roads.Boards;

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

        [Header("Events")]
        [SerializeField]
        private StringEvent onPlacementChange;
        [SerializeField]
        private UnityEvent onPlacementDeselect;


        private void Start()
        {
            onPlacementChange?.Invoke(placement.ToString());
        }

        public void OnLeftClick()
        {
            if (!hit.collider)
            {
                Deselect();
                return;
            }

            Placement newPlacement = hit.collider.GetComponent<Placement>();

            if (newPlacement != null)
            {
                currentPlacement = hit.collider.GetComponent<Placement>();
                placement = (currentPlacement != null) ? currentPlacement.placement : PlacementType.NULL;

                onPlacementChange?.Invoke(placement.ToString());
                return;
            }

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

            if (currentPlacement == roads) BoardManager.Instance.SelectTile(hit);
        }

        private void Deselect()
        {
            currentPlacement = null;
            placement = PlacementType.NULL;

            onPlacementDeselect?.Invoke();
            onPlacementChange?.Invoke(placement.ToString());
        }
    }
}