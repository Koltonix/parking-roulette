//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using ParkingRoulette.Enums;
using ParkingRoulette.Pathing;
using ParkingRoulette.Boards;

namespace ParkingRoulette.Placement
{
    public class PathPlacement : Placement
    {
        [SerializeField]
        private Vehicle selectedVehicle;

        private void Start()
        {
            placement = PlacementType.PATHING;
            if (!selectedVehicle)
                selectedVehicle = this.GetComponent<Vehicle>();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            selectedVehicle.EnablePath(true);
        }

        public override void PlaceItem(Vector3 position)
        {
            base.PlaceItem(position);

            Tile tile = BoardManager.Instance.WorldToTile(position);
            if (tile != null && tile.hasRoad && TileIsAdjacent(selectedVehicle.previousTile, tile) && tile.vehicle == null)
            {
                //You can only go into a parking slot once and cannot leave once you have...
                if (!selectedVehicle.previousTile.parkingSlot || (selectedVehicle.path.Count == 1 && !tile.parkingSlot))
                    selectedVehicle.AddPathPoint(tile);
            }
        }

        public override void RemoveItem(Vector3 position)
        {
            base.RemoveItem(position);

            Tile tile = BoardManager.Instance.WorldToTile(position);
            if (tile.hasRoad)
                selectedVehicle.RemovePathsUntil(tile);

        }

        public override void OnUpdate(Vector3 position)
        {
            base.OnUpdate(position);
        }

        public override void OnExit()
        {
            base.OnExit();
            selectedVehicle.EnablePath(false);
        }

        private bool TileIsAdjacent(Tile centreTile, Tile newTile)
        {
            Tile[] adjacentTiles = BoardManager.Instance.GetAdjacentTiles(centreTile);
            foreach (Tile tile in adjacentTiles)
            {
                if (tile == newTile)
                    return true;
            }

            return false;
        }
    }
}