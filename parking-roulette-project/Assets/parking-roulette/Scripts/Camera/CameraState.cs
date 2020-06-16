//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;

namespace ParkingRoulette.Cameras
{
    public abstract class CameraState : MonoBehaviour
    {
        public virtual void OnEnter() { }
        public virtual void OnStay() { }
        public virtual void OnExit() { }
    }
}

