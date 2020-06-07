//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using ParkingRoulette.Roads;
using ParkingRoulette.Boards;
using ParkingRoulette.Enums;
using ParkingRoulette.Pathing;

namespace ParkingRoulette.Placement
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
            if (tile == null || roads.ContainsKey(tile) || !tile.canHaveRoad) return;

            Road road = Instantiate(roadPrefab, tile.GO.transform.position + spawnOffset, Quaternion.identity).GetComponent<Road>();
            roads.Add(tile, road);

            tile.hasRoad = true;
            tile.road = road;

            road.tile = tile;
            road.UpdateRoad();
            UpdateAdjacentRoads(tile);
        }

        public override void RemoveItem(Vector3 position)
        {
            Tile tile = BoardManager.Instance.WorldToTile(position);
            if (tile == null || !roads.ContainsKey(tile)) return;

            Destroy(roads[tile].gameObject);
            tile.hasRoad = false;

            roads.Remove(tile);

            UpdateAdjacentRoads(tile);
            UpdateAllPathing(tile);
        }

        public override void OnExit()
        {
            BoardManager.Instance.ResetTiles();
        }

        private void UpdateAllPathing(Tile tile)
        {
            Vehicle[] vehicles = FindObjectsOfType<Vehicle>();
            foreach (Vehicle vehicle in vehicles)
                vehicle.RemovePathsUntilUpper(tile);
        }

        private void UpdateAdjacentRoads(Tile centreTile)
        {
            Tile[] adjacentTiles = BoardManager.Instance.GetAdjacentTiles(centreTile);
            foreach (Tile tile in adjacentTiles)
            {
                if (tile.road && tile.canHaveRoad)
                    tile.road.UpdateRoad();
            }
        }
    }
}