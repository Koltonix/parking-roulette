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
        private AllRoads allRoads;

        [SerializeField]
        private GameObject childRoad;
        [Space]

        [Header("Tile Settings")]
        public Tile tile;

        private void Start()
        {
            if (!childRoad)
                childRoad = this.transform.GetChild(0).gameObject;

            tile = BoardManager.Instance.WorldToTile(this.transform.position);
        }

        public void UpdateRoad()
        {
            int roadValue = Convert.ToInt32(GetBinaryRoadAdjacency(), 2);
            ReplaceGameObject(GetPrefabFromValue(roadValue));
        }

        private void ReplaceGameObject(GameObject prefab)
        {
            Destroy(childRoad);

            Transform childMesh = prefab.transform.GetChild(0).transform;
            childRoad = Instantiate(childMesh.gameObject, this.transform.position, childMesh.rotation, this.transform);
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
            foreach (RoadValue road in allRoads.roads)
            {
                if (value == road.value)
                    return road.prefab;
            }

            return null;
        }
    }
}

