//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System.Collections.Generic;
using UnityEngine;
using ParkingRoulette.Boards;

namespace ParkingRoulette.Vehicles
{
    public class Vehicle : MonoBehaviour
    {
        private List<Tile> path = new List<Tile>();
        public Tile previousTile = null;

        public void AddPathPoint(Tile tile)
        {
            path.Add(tile);
            previousTile = tile;
            //Update the Path visuals here
        }

        public void RemovePathsUntil(Tile tile)
        {
            for (int i = path.Count - 1; i >= 0; i--)
            {
                if (path[i] == tile)
                {
                    previousTile = path[i];
                    break;
                }

                path.RemoveAt(i);
            }
            
            //Update the Path Visuals here
        }

        public void ResetPath()
        {
            path = new List<Tile>();
            
            //Update the Path Visuals here
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (path != null)
            {
                foreach (Tile tile in path)
                    Gizmos.DrawSphere(tile.GO.transform.position, 2.5f);
            }
        }
    }
}
