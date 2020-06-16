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
        public Material defaultMaterial;
        public Material selectedMaterial;
        [Space]

        [Header("Board Details")]
        public int x;
        public int y;
        [Space]

        [Header("Attributes")]
        public bool parkingSpace;

        private void Start()
        {
            defaultMaterial = this.GetComponent<Renderer>().material;
        }

        public void ResetTileColour()
        {
            this.GetComponent<Renderer>().material = defaultMaterial;
        }

        public void SetTileMaterial(Material material)
        {
            this.GetComponent<Renderer>().material = selectedMaterial;
        }
    }
}