//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System;
using ParkingRoulette.Boards;
using UnityEngine;

namespace ParkingRoulette.Roads 
{
    public class Road : MonoBehaviour
    {
        [Header("Road Values")]
        [SerializeField]
        private RoadValue[] roadValues;
        [SerializeField]
        private GameObject childRoad;
        [Space]

        [Header("Tile Settings")]
        public Tile tile;

        private void Start()
        {
            if (!childRoad)
                childRoad = this.transform.GetChild(0).gameObject;
        }

        public void UpdateRoad()
        {
            int roadValue = Convert.ToInt32(GetBinaryRoadAdjacency(), 2);
            ReplaceGameObject(GetPrefabFromValue(roadValue));

            Debug.Log(roadValue);
        }

        private void ReplaceGameObject(GameObject prefab)
        {
            Destroy(childRoad);
            childRoad = Instantiate(prefab, this.transform.position, Quaternion.identity);
        }

        public string GetBinaryRoadAdjacency()
        {
            string adjacencyBinary = "";
            Tile[] adjacentTiles = BoardManager.Instance.GetAdjacentTiles(tile);

            for (int i = adjacentTiles.Length - 1; i >= 0; i--)
            {
                if (adjacentTiles[i] == null || !adjacentTiles[i].hasRoad)
                    adjacencyBinary += 0;

                else
                    adjacencyBinary += 1;
            }
           
            return adjacencyBinary;
        }

        private GameObject GetPrefabFromValue(int value)
        {
            foreach (RoadValue road in roadValues)
            {
                if (value == road.value)
                    return road.prefab;
            }

            return null;
        }
    }
}

