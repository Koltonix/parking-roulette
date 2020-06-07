//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using ParkingRoulette.Roads;
using ParkingRoulette.Pathing;

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
        [Space]

        [Header("Pathing Attributes")]
        public List<PathPoint> points = new List<PathPoint>();

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