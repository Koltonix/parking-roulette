//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System;
using System.Collections.Generic;
using ParkingRoulette.Boards;
using ParkingRoulette.Pathing;
using UnityEngine;

namespace ParkingRoulette.GameHandler
{
    public class GameStateHandler : MonoBehaviour
    {
        #region Singleton
        public static GameStateHandler Instance;
        private void Awake()
        {
            if (!Instance) Instance = this;
            else Destroy(this);
        }
        #endregion

        [Header("Lose Conditions")]

        [Header("Car Spawning")]
        [SerializeField]
        private int amountToSpawn = 4;
        [SerializeField]
        private GameObject[] carPrefabs;
        [SerializeField]
        private Vehicle[] vehicles;
        [SerializeField]
        private Vector3 spawnOffset = Vector3.up * 1.5f;

        private void Start()
        {
            SpawnCars(amountToSpawn);
        }

        public void CheckOverlap()
        {
            CheckForLoss(vehicles);
        }

        private void CheckForLoss(Vehicle[] vehicles)
        {
            int checkSize = GetLongestPath(vehicles);
            for (int i = 0; i < checkSize; i++)
            {
                CheckForDuplicateTile(vehicles, i);
            }
        }

        private void CheckForDuplicateTile(Vehicle[] vehicles, int index)
        {
            List<Tile> currentTiles = GetAllCurrentTiles(vehicles, index);
            foreach (Tile tile in currentTiles)
            {
                int numOnTile = 0;
                foreach(Vehicle vehicle in vehicles)
                {
                    if (index <= vehicle.path.Count - 1  && vehicle.path[index].tile == tile)
                        numOnTile++;

                    if (numOnTile > 1)
                        Debug.Log("LOST");
                }
            }
        }

        private List<Tile> GetAllCurrentTiles(Vehicle[] vehicles, int index)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Vehicle vehicle in vehicles)
                if (index <= vehicle.path.Count - 1)
                    tiles.Add(vehicle.path[index].tile);

            return tiles;
        }

        private int GetLongestPath(Vehicle[] vehicles)
        {
            int longest = Int32.MaxValue;
            foreach (Vehicle vehicle in vehicles)
            {
                int pathLength = vehicle.path.Count;
                if (pathLength < longest)
                    longest = pathLength;
            }

            return longest;
        }

        #region Spawning Cars
        private void SpawnCars(int amount)
        {
            //Sanity check
            int numParkingSpaces = Mathf.FloorToInt(BoardManager.Instance.parkingSpots.Length *.5f);
            amount = (amount < numParkingSpaces) ? amount : numParkingSpaces;

            vehicles = new Vehicle[amount];
            List<Tile> parkingSpots = new List<Tile>(BoardManager.Instance.parkingSpots);
            
            for (int i = 0; i < amount; i++)
            {
                Tile randomTile = parkingSpots[UnityEngine.Random.Range(0, parkingSpots.Count)];
                vehicles[i] = CreateCar(randomTile).GetComponent<Vehicle>();

                parkingSpots.Remove(randomTile);
            }
        }

        private GameObject CreateCar(Tile tile)
        {
            Vector3 spawnPoint = tile.GO.transform.position + spawnOffset;

            GameObject carClone = Instantiate(carPrefabs[UnityEngine.Random.Range(0, carPrefabs.Length)], spawnPoint, Quaternion.identity);
            carClone.transform.forward = GetDirection(tile.x, tile.y);

            return carClone;
        }

        private Vector3 GetDirection(int x, int y)
        {
            //Left hand side
            if (x == 0) return Vector3.right;
            if (y == 0) return Vector3.forward;
            if (x == BoardManager.Instance.width - 1) return -Vector3.right;
            if (y == BoardManager.Instance.height - 1) return -Vector3.forward;

            return Vector3.zero;
        }
        #endregion
    }
}
