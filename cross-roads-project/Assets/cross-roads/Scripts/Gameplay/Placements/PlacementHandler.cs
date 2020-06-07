//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using ParkingRoulette.Enums;
using ParkingRoulette.Events;
using UnityEngine.Events;

namespace ParkingRoulette.Placement
{
    public class PlacementHandler : MonoBehaviour
    {
        [Header("Input")]
        public RaycastHit hit;
        public bool canSelect = true;
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
        private RaycastHitEvent OnSelectTile;
        [SerializeField]
        private StringEvent onPlacementChange;
        [SerializeField]
        private UnityEvent onPlacementDeselect;


        private void Start()
        {
            onPlacementChange?.Invoke(placement.ToString());
        }

        private void Update()
        {
            if (currentPlacement && canSelect)
                currentPlacement.OnUpdate(hit.point);
        }

        public void OnLeftClick()
        {
            if (!canSelect)
                return;

            if (!hit.collider)
            {
                Deselect();
                return;
            }

            Placement newPlacement = hit.collider.GetComponent<Placement>();

            if (newPlacement != null)
            {
                currentPlacement?.OnExit();
                currentPlacement = newPlacement;
                currentPlacement.OnEnter();

                placement = (currentPlacement != null) ? currentPlacement.placement : PlacementType.NULL;

                onPlacementChange?.Invoke(placement.ToString());
                return;
            }

            PlaceItem();
        }

        public void OnRightClick()
        {
            if (currentPlacement && canSelect)
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

            if (currentPlacement == roads) OnSelectTile?.Invoke(hit);
        }

        public void Deselect()
        {
            currentPlacement?.OnExit();

            currentPlacement = null;
            placement = PlacementType.NULL;

            onPlacementDeselect?.Invoke();
            onPlacementChange?.Invoke(placement.ToString());
        }

        public void CanSelect(bool value) { canSelect = value; }
    }
}