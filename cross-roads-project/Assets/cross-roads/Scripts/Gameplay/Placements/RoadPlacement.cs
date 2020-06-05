//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using Roads.Roads;
using Roads.Boards;
using Roads.Enums;

namespace Roads.Placement
{
    public class RoadPlacement : Placement
    {
        [Header("Roads")]
        private Dictionary<Tile, Road> roads = new Dictionary<Tile, Road>();
        [SerializeField]
        private GameObject roadPrefab;

        [SerializeField]
        private Vector3 spawnOffset = Vector3.zero;

        private void Start()
        {
            placement = PlacementType.ROAD;   
        }

        public override void PlaceItem(Vector3 position)
        {
            Tile tile = BoardManager.Instance.WorldToTile(position);
            if (tile == null || roads.ContainsKey(tile)) return;

            Road road = Instantiate(roadPrefab, tile.GO.transform.position + spawnOffset, Quaternion.identity).GetComponent<Road>();
            roads.Add(tile, road);

            tile.hasRoad = true;
        }

        public override void RemoveItem(Vector3 position)
        {
            Tile tile = BoardManager.Instance.WorldToTile(position);
            if (tile == null || !roads.ContainsKey(tile)) return;

            Destroy(roads[tile].gameObject);
            tile.hasRoad = false;
            roads.Remove(tile);
        }
    }
}