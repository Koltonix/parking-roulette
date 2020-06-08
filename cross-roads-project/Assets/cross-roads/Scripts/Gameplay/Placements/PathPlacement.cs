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

            //Trying to leave a parking spot that isn't the original
            if (tile != null && selectedVehicle.previousTile.parkingSlot && !tile.parkingSlot && selectedVehicle.previousTile != selectedVehicle.originalTile)
                return;

            if (tile != null && tile.hasRoad && TileIsAdjacent(selectedVehicle.previousTile, tile))
            {
                //Started in parking -> Can Place
                //Last Tile was a parking space -> Can't Place

                //If the tile is not a parking slot or it is the original tile
                if (!tile.parkingSlot || tile.parkingSlot && !selectedVehicle.previousTile.parkingSlot)
                    selectedVehicle.AddPathPoint(tile);

                
            }

            else if (tile == selectedVehicle.originalTile || tile == selectedVehicle.previousTile && !tile.parkingSlot)
                selectedVehicle.AddPathPoint(tile);
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