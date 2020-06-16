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
        private StringEvent onPlacementNameChange;
        [SerializeField]
        private UnityEvent onPlacementDeselect;


        private void Start()
        {
            onPlacementNameChange?.Invoke(placement.ToString());
        }

        public void OnLeftClick()
        {
            if (!hit.collider)
            {
                Deselect();
                return;
            }

            Placement newPlacement = hit.collider.GetComponent<Placement>();

            if (newPlacement)
            {
                currentPlacement?.OnExit();
                currentPlacement = newPlacement;
                currentPlacement.OnEnter();

                placement = (currentPlacement != null) ? currentPlacement.placement : PlacementType.UNSELECTED;

                onPlacementNameChange?.Invoke(placement.ToString());
                return;
            }

            PlaceItem();
        }

        public void PlaceItem()
        {
            if (currentPlacement)
                currentPlacement.PlaceItem(hit);
        }

        public void DestroyItem()
        {
            if (currentPlacement)
                currentPlacement.RemoveItem(hit);
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
            placement = PlacementType.UNSELECTED;

            onPlacementDeselect?.Invoke();
            onPlacementNameChange?.Invoke(placement.ToString());
        }
    }
}