//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using ParkingRoulette.Roads;

namespace ParkingRoulette.Boards
{
    public class Tile : MonoBehaviour
    {
        [Header("World Details")]
        public GameObject GO;
        public Color32 selectedColour;
        public Color32 defaultColour;
        [Space]

        [Header("Board Details")]
        public int x;
        public int y;
        [Space]

        [Header("Attributes")]
        public bool parkingSlot;

        [Header("Road Attributes")]
        [SerializeField]
        private LayerMask roadMask;
        public Road road;
        public bool canHaveRoad = true;
        public bool hasRoad = false;

        public bool canHavePath = false;

        private void Start()
        {
            defaultColour = this.GetComponent<Renderer>().material.color;
            CheckForRoad();
        }

        private void CheckForRoad()
        {
            RaycastHit hit;
            Physics.Raycast(GO.transform.position, Vector3.up, out hit, 1.0f, roadMask);

            if (hit.collider)
                road = hit.collider.GetComponent<Road>();

            hasRoad = (road == null) ? false : true;
            if (road)
                road.tile = this;
        }
    }
}