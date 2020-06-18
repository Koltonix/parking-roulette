//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using ParkingRoulette.Enums;
using ParkingRoulette.Events;

namespace ParkingRoulette.Placing
{
    public class PlacementHandler : MonoBehaviour
    {
        [Header("Input")]
        private RaycastHit hit;
        [Space]

        [Header("Placement")]
        [SerializeField]
        private PlacementType type;
        private Placement currentPlacement;

        [Header("Events")]
        [SerializeField]
        private EventString onPlacementNameChange;


        private void Start()
        {
            onPlacementNameChange.Raise(type.ToString());
        }

        public void PlaceItem()
        {
            if (hit.collider)
            {
                Placement placementHit = hit.collider.GetComponent<Placement>();

                //Typically used when a vehicle has been selected...
                if (placementHit)
                {
                    currentPlacement?.OnExit();
                    currentPlacement = placementHit;
                    currentPlacement.OnEnter();

                    type = (currentPlacement != null) ? currentPlacement.type : PlacementType.UNSELECTED;
                    onPlacementNameChange.Raise(type.ToString());

                    return;
                }

                else if (currentPlacement)
                    currentPlacement.PlaceItem(hit);
            }

            else
                Deselect();
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