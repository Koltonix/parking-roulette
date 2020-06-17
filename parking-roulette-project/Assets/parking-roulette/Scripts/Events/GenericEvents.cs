﻿//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System;
using UnityEngine;
using UnityEngine.Events;
using ParkingRoulette.Placing;

namespace ParkingRoulette.Events
{
    [Serializable]
    public class RaycastHitEvent : UnityEvent<RaycastHit> { };

    [Serializable]
    public class StringEvent : UnityEvent<string> { };

    [Serializable]
    public class IntEvent : UnityEvent<int> { };

    [Serializable]
    public class PlacementEvent : UnityEvent<Placement> { };
}