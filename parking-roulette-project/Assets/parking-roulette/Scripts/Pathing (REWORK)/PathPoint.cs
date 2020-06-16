using System;
using UnityEngine;
using ParkingRoulette.Boards;

namespace ParkingRoulette.Pathing
{
    [Serializable]
    public struct PathPoint
    {
        public Tile tile;
        public GameObject point;

        public PathPoint(Tile tile, GameObject point)
        {
            this.tile = tile;
            this.point = point;
        }
    }
}

