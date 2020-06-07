//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using ParkingRoulette.Boards;
using ParkingRoulette.Pathing;
using UnityEngine;
using UnityEngine.Events;

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

        [Header("Events")]
        [SerializeField]
        private UnityEvent onRun;
        [SerializeField]
        private UnityEvent onWin;
        [SerializeField]
        private UnityEvent onLose;

        private void Start()
        {
            SpawnCars(amountToSpawn);
        }

        public void StartMovement()
        {
           
            onRun?.Invoke();

            foreach (Vehicle vehicle in vehicles)
                vehicle.EnablePath(true);

            StartCoroutine(MoveVehicles());   
        }

        private IEnumerator MoveVehicles()
        {
            int checkAmount = GetLongestPath(vehicles);

            for (int i = 0; i < checkAmount; i++)
            {
                for (int j = 0; j < vehicles.Length; j++)
                    yield return vehicles[j].movement.MoveToPoint(i);

                yield return new WaitForEndOfFrame();
            }
                

            yield return null;
        }

        public void WinGame()
        {
            onWin?.Invoke();
        }

        public void LoseGame()
        {
            onLose?.Invoke();
        }

        private int GetLongestPath(Vehicle[] vehicles)
        {
            int longest = 0;
            foreach (Vehicle vehicle in vehicles)
            {
                int pathLength = vehicle.path.Count;
                if (pathLength > longest)
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
