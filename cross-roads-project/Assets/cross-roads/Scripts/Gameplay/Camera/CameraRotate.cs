//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System.Collections;
using UnityEngine;

namespace ParkingRoulette.Cameras
{
    public class CameraRotate : CameraState
    {
        [Header("Speeds")]
        [SerializeField]
        private float moveSpeed = 1.25f;
        [SerializeField]
        private float rotateSpeed = 1.25f;
        [Space]

        [Header("Points")]
        [SerializeField]
        private Transform[] points;
        private int pointIndex = 0;
        [Space]

        [Header("Coroutines")]
        private Coroutine moving;
        private Coroutine rotating;
        [Space]

        [Header("Debug Tools")]
        [SerializeField]
        private Color32 gizmoSphereColour = Color.yellow;
        [SerializeField]
        private Color32 gizmoLineColour = Color.yellow;
        [SerializeField]
        private float gizmoRadius = 1.0f;

        public override void OnEnter()
        {
            CyclePoints(0);
        }

        public void CyclePoints(int direction)
        {
            //Only can cycle once the previous move has ended
            if (moving != null || rotating != null || points == null) return;

            //Sanity check ensuring it is either 1, 0, or -1
            direction = (direction > 0) ? 1 : direction;
            direction = (direction < 0) ? -1 : direction;

            pointIndex += direction;

            //Larger or smaller than the points array size
            pointIndex = (pointIndex > points.Length - 1) ? 0 : pointIndex;
            pointIndex = (pointIndex < 0) ? points.Length - 1 : pointIndex;

            moving = StartCoroutine(MoveToPoint(points[pointIndex], moveSpeed));
            rotating = StartCoroutine(RotateToPoint(points[pointIndex], rotateSpeed));
        }

        #region Coroutines
        private IEnumerator MoveToPoint(Transform point, float speed)
        {
            Vector3 originalPosition = this.transform.position;
            float t = 0.0f;

            while (t < 1.0f)
            {
                t += speed * Time.deltaTime;
                this.transform.position = Vector3.Slerp(originalPosition, point.position, t);
                yield return new WaitForEndOfFrame();
            }

            moving = null;
        }

        private IEnumerator RotateToPoint(Transform point, float speed)
        {
            Quaternion originalRotation = this.transform.rotation;
            float t = 0.0f;

            while (t < 1.0f)
            {
                t += speed * Time.deltaTime;
                this.transform.rotation = Quaternion.Slerp(originalRotation, point.rotation, t);
                yield return new WaitForEndOfFrame(); ;
            }

            rotating = null;
        }
        #endregion

        private void OnDrawGizmos()
        {
            if (points != null)
            {
                for (int i = 0; i < points.Length; i++)
                {
                    Gizmos.color = gizmoSphereColour;
                    Gizmos.DrawSphere(points[i].position, gizmoRadius);

                    if (i == points.Length - 1) Gizmos.DrawLine(points[i].position, points[0].position);
                    else if (points.Length > 1) Gizmos.DrawLine(points[i].position, points[i + 1].position);
                }
            }
        }
    }
}

