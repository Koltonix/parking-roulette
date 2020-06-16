//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.Events;
using ParkingRoulette.Events;

namespace ParkingRoulette.Controls
{
    public abstract class UserInput : MonoBehaviour
    {
        [Header("Keys")]
        [SerializeField]
        protected InputKeys keys;

        [Header("Raycast Settings")]
        public RaycastHit hit;
        public Ray ray;
        public LayerMask mask;
        public float distance;

        [Header("Camera Settings")]
        [SerializeField]
        protected Camera mainCamera;

        [Header("Debug Settings")]
        [SerializeField]
        protected float rayDistance = 100f;
        [SerializeField]
        protected Color32 rayColour = Color.red;

        [Header("Events")]
        public UnityEvent OnLeftClick;
        public UnityEvent OnRightClick;
        [Space]
        public UnityEvent OnPositiveRotate;
        public UnityEvent OnNegativeRotate;
        [Space]

        public RaycastHitEvent OnMouseHover;

        protected virtual void Start()
        {
            if (!mainCamera) mainCamera = Camera.main;
        }

        protected virtual void Update()
        {
            LeftClick();
            RightClick();

            PositiveRotate();
            NegativeRotate();

            DebugDrawDray();
        }

        protected virtual void LeftClick() { }
        protected virtual void RightClick() { }

        protected virtual void PositiveRotate() { }
        protected virtual void NegativeRotate() { }

        private void DebugDrawDray()
        {
            if(hit.collider) 
                Debug.DrawLine(ray.origin, hit.point, rayColour);

            else 
                Debug.DrawLine(ray.origin, ray.direction.normalized * rayDistance, rayColour);
        }
    }
}
