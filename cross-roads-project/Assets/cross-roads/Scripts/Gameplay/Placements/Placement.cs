﻿//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using ParkingRoulette.Enums;
using UnityEngine;

namespace ParkingRoulette.Placement
{
    public abstract class Placement : MonoBehaviour
    {
        [HideInInspector]
        public PlacementType placement = PlacementType.NULL;

        public virtual void PlaceItem(Vector3 position){ }
        public virtual void RemoveItem(Vector3 position) { }
    }
}
