//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System.Collections;
using UnityEngine;

namespace ParkingRoulette.Utilities
{
    public class ObjectAim : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1.25f;
        [SerializeField]
        private Transform objectToRotate;
        [SerializeField]
        private Vector3[] directions;

        private float timeLeft = 5.0f;

        [SerializeField]
        private float maxTime = 15.0f;
        [SerializeField]
        private float minTime = 3.0f;

        private Coroutine aiming;

        private void Start()
        {
            if (!objectToRotate)
                objectToRotate = this.transform;
        }

        private void Update()
        {
            if (CanRotate())
                RotateObject();
        }

        private bool CanRotate()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                timeLeft = Random.Range(minTime, maxTime);
                return true;
            }

            return false;
        }

        public void RotateObject()
        {
            Vector3 randomDirection = directions[Random.Range(0, directions.Length)];

            if (aiming == null)
                aiming = StartCoroutine(AimObject(randomDirection, speed));
        }

        private IEnumerator AimObject(Vector3 direction, float speed)
        {
            Vector3 originalDirection = this.transform.forward;

            float t = 0.0f;
            while (t <= 1.0f)
            {
                t += Time.deltaTime * speed;
                this.transform.forward = Vector3.Slerp(originalDirection, direction, t);

                yield return new WaitForEndOfFrame();
            }

            aiming = null;
        }
    }
}
