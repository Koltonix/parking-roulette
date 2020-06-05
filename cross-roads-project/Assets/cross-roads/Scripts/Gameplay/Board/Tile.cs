//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;

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
        public bool hasRoad = false;

        private void Start()
        {
            defaultColour = this.GetComponent<Renderer>().material.color;
        }
    }
}