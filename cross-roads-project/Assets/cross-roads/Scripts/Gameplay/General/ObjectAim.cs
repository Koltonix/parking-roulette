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
        private Vector3[] directions;
        private int index = 0;

        private Coroutine aiming;

        public void RotateObject(int direction)
        {
            if (aiming != null)
                return;

            //Sanity check ensuring it is either 1, 0, or -1
            direction = (direction > 0) ? 1 : direction;
            direction = (direction < 0) ? -1 : direction;

            index += direction;
            index = (index > directions.Length - 1) ? 0 : index;
            index = (index < 0) ? directions.Length - 1 : index;

            aiming = StartCoroutine(AimObject(directions[index], speed));
        }

        private IEnumerator AimObject(Vector3 direction, float speed)
        {
            Vector3 originalDirection = this.transform.forward;

            float t = 0.0f;
            while (t <= 1.0f)
            {
                t += Time.deltaTime * speed;
                this.transform.forward = Vector3.Lerp(originalDirection, direction, t);

                yield return new WaitForEndOfFrame();
            }

            aiming = null;
        }
    }
}
