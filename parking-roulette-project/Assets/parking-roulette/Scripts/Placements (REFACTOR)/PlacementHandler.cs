//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using ParkingRoulette.Enums;
using ParkingRoulette.Events;
using UnityEngine.Events;

namespace ParkingRoulette.Placing
{
    public class PlacementHandler : MonoBehaviour
    {
        [Header("Input")]
        public RaycastHit hit;
        [Space]

        [Header("Placement")]
        [SerializeField]
        private PlacementType type;
        private Placement currentPlacement;
        [SerializeField]
        private Placement roads;
        [SerializeField]
        private Placement path;

        [Header("Events")]
        [SerializeField]
        private EventString onPlacementNameChange;


        private void Start()
        {
            onPlacementNameChange.Raise(type.ToString());
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

                type = (currentPlacement != null) ? currentPlacement.type : PlacementType.UNSELECTED;

                onPlacementNameChange.Raise(type.ToString());
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

        public void Deselect()
        {
            currentPlacement?.OnExit();

            currentPlacement = null;
            type = PlacementType.UNSELECTED;

            onPlacementNameChange.Raise(type.ToString());
        }

        public void SelectPlacement(Placement placement)
        {
            currentPlacement = placement;
            type = placement.type;

            onPlacementNameChange.Raise(type.ToString());
        }

        public void ReceiveRaycastHit(RaycastHit hit) { this.hit = hit; }
    }
}