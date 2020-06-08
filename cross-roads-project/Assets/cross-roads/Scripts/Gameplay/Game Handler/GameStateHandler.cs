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
        private float amountToSpawn = 1;
        [SerializeField]
        private float spawnIncrease = 0.5f;
        [SerializeField]
        private GameObject[] carPrefabs;
        [SerializeField]
        private Vehicle[] vehicles;
        [SerializeField]
        private Vector3 spawnOffset = Vector3.up * 1.5f;

        private Coroutine running;

        [Header("Events")]
        [SerializeField]
        private UnityEvent onRun;
        [SerializeField]
        private UnityEvent onWin;
        [SerializeField]
        private UnityEvent onLose;
        [SerializeField]
        private UnityEvent onReset;
        
        private void Start()
        {
            SpawnCars(Mathf.FloorToInt(amountToSpawn));
        }

        public void StartMovement()
        {
            onRun?.Invoke();

            foreach (Vehicle vehicle in vehicles)
                vehicle.EnablePath(true);
            
            if (running == null)
                running = StartCoroutine(MoveVehicles());   
        }

        private IEnumerator MoveVehicles()
        {
            Vehicle lastVehicle = GetLongestPath(vehicles);
            int checkAmount = lastVehicle.path.Count;

            //I want the lastVehicle to actually be last in the array...
            List<Vehicle> orderedVehicles = new List<Vehicle>(vehicles);
            orderedVehicles.Remove(lastVehicle);
            orderedVehicles.Add(lastVehicle);

            for (int i = 0; i < checkAmount; i++)
            {
                for (int j = 0; j < orderedVehicles.Count; j++)
               {
                    if (lastVehicle != orderedVehicles[j])
                        StartCoroutine(orderedVehicles[j].movement.MoveToPoint(i));

                    else if (lastVehicle == orderedVehicles[j])
                        yield return StartCoroutine(orderedVehicles[j].movement.MoveToPoint(i));
                    
                }
                    

                yield return new WaitForEndOfFrame();
            }

            CheckIfWon();
            running = null;
            yield return null;
        }

        private void CheckIfWon()
        {
            foreach (Tile tile in BoardManager.Instance.parkingSpots)
            {
                tile.CheckForVehicle();

                if (tile.currentVehicle != tile.expectedVehicle)
                {
                    LoseGame();
                    return;
                }
            }

            WinGame();
        }

        public void WinGame()
        { 
            onWin?.Invoke();
            Debug.Log("WON");

            ResetGame();
        }

        public void LoseGame()
        {
            if (running != null)
            {
                StopCoroutine(running);
                running = null;
                Debug.Log("STOP IT ALL");
            }

            onLose?.Invoke();
            //Reset the actual SCENE HERE
            Debug.Log("LOST");
        }

        public void ResetGame()
        {
            amountToSpawn += spawnIncrease;

            foreach (Vehicle vehicle in vehicles)
                Destroy(vehicle.gameObject);

            onReset?.Invoke();

            SpawnCars(Mathf.FloorToInt(amountToSpawn));
        }

        private Vehicle GetLongestPath(Vehicle[] vehicles)
        {
            Vehicle longestVehicle = null;
            int longest = 0;

            foreach (Vehicle vehicle in vehicles)
            {
                int pathLength = vehicle.path.Count;
                if (pathLength > longest)
                {
                    longest = pathLength;
                    longestVehicle = vehicle;
                }
                    
            }

            return longestVehicle;
        }

        #region Spawning Cars
        //I'm so sorry on how ugly this function is
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

            AssignEndPoints(parkingSpots.ToArray());
        }

        private void AssignEndPoints(Tile[] parkingSpots)
        {
            List<Tile> parking = new List<Tile>(parkingSpots);

            for (int i = 0; i < vehicles.Length; i++)
            {
                Tile endTile = parking[Random.Range(0, parking.Count)];
                endTile.expectedVehicle = vehicles[i];

                Vector3 spawnPosition = endTile.GO.transform.position;
                spawnPosition.y = vehicles[i].originalSpawnHeight;

                vehicles[i].carColour = vehicles[i].GetRandomColour();

                GameObject clone = Instantiate(vehicles[i].pathIconPrefab, spawnPosition, vehicles[i].pathIconPrefab.transform.rotation);
                clone.GetComponent<Renderer>().material.color = vehicles[i].carColour;
                clone.GetComponent<MeshRenderer>().enabled = false;

                vehicles[i].endPoint = clone;

                parking.Remove(endTile);
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
        #endregion
    }
}
