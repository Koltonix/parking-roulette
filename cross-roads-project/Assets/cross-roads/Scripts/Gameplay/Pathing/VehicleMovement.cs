//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System.Collections;
using UnityEngine;

namespace ParkingRoulette.Pathing
{
    [RequireComponent(typeof(Vehicle))]
    public class VehicleMovement : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed = 1.25f;
        [SerializeField]
        private float rotateSpeed = 1.25f;
        private Vehicle vehicle;

        private void Start()
        {
            vehicle = this.GetComponent<Vehicle>();
        }

        public IEnumerator MoveToPoint(int pathIndex)
        {
            if (pathIndex < vehicle.path.Count)
            {
                Vector3 targetPosition = vehicle.path[pathIndex].tile.GO.transform.position;
                targetPosition.y = this.transform.position.y;

                Vector3 originalPosition = this.transform.position;

                float t = 0.0f;
                while (t < 1.0f)
                {
                    Vector3 direction = targetPosition - this.transform.position;
                    t += Time.deltaTime;

                    //Change to use direction properly later
                    if (direction != Vector3.zero)
                        this.transform.forward = Vector3.Lerp(this.transform.forward, direction, t * rotateSpeed);

                    this.transform.position = Vector3.Lerp(originalPosition, targetPosition, t * moveSpeed);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
}
