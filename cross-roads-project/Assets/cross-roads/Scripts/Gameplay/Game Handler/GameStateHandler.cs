//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System.Collections.Generic;
using ParkingRoulette.Boards;
using ParkingRoulette.Pathing;
using UnityEngine;

namespace ParkingRoulette.GameHander
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

        [Header("Car Prefabs")]
        [SerializeField]
        private GameObject[] carPrefabs;
        [SerializeField]
        private Vehicle[] vehicles;
        [SerializeField]
        private Vector3 spawnOffset = Vector3.up * 1.5f;

        private void Start()
        {
            SpawnCars(2);
        }

        private void SpawnCars(int amount)
        {
            //Sanity check
            int numParkingSpaces = Mathf.FloorToInt(BoardManager.Instance.parkingSpots.Length *.5f);
            amount = (amount < numParkingSpaces) ? amount : numParkingSpaces;

            vehicles = new Vehicle[amount];
            List<Tile> parkingSpots = new List<Tile>(BoardManager.Instance.parkingSpots);
            
            for (int i = 0; i < amount; i++)
            {
                Tile randomTile = parkingSpots[Random.Range(0, parkingSpots.Count)];
                vehicles[i] = CreateCar(randomTile).GetComponent<Vehicle>();

                parkingSpots.Remove(randomTile);
            }
        }

        private GameObject CreateCar(Tile tile)
        {
            Vector3 spawnPoint = tile.GO.transform.position + spawnOffset;

            GameObject carClone = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPoint, Quaternion.identity);
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
    }
}
