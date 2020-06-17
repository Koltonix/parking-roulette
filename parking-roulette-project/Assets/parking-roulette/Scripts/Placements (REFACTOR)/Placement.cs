//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using ParkingRoulette.Enums;
using UnityEngine;

namespace ParkingRoulette.Placing
{
    public abstract class Placement : MonoBehaviour
    {
        [HideInInspector]
        public PlacementType type = PlacementType.UNSELECTED;

        public virtual void PlaceItem(RaycastHit hit){ }
        public virtual void RemoveItem(RaycastHit hit) { }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}

