//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using ParkingRoulette.Roads;
using UnityEditor;
using UnityEngine;

namespace ParkingRoulette.Boards
{
    [CreateAssetMenu(fileName = "Board", menuName = "ScriptableObjects/Board")]
    public class Board : ScriptableObject
    {
        [SerializeField]
        private string filePath = "/parking-roulette/Scripts/Editor/Board Creation/";

        public int height;
        public int width;
        public int tileSize;

        public bool spawnParkingSpots = true;
        public bool spawnCorners = false;
        public GameObject[] parkingSpots;
        public Vector3 roadOffset = new Vector3(0.0f, 0.75f, 0.0f);

        public GameObject tilePrefab;
        public GameObject parkingTilePrefab;

        [HideInInspector]
        public GameObject prefabReference;

        private GameObject worldReference;

        public void CreateBoard()
        {
            if (!worldReference) DestroyImmediate(worldReference);
            worldReference = new GameObject("Board");

            Tile[,] tiles = new Tile[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Vector3 spawnPosition = new Vector3(x * tileSize - ((width - 1) * tileSize * .5f), 0.0f,
                                                        y * tileSize - ((height - 1) * tileSize * .5f));

                    tiles[x, y] = SpawnTile(spawnPosition, x, y);
                    tiles[x, y].GO.transform.SetParent(worldReference.transform);
                }
            }

            string boardName = string.Format("{0}x{1} Board.prefab", width, height);
            prefabReference = PrefabUtility.SaveAsPrefabAsset(worldReference, Application.dataPath + filePath + boardName);

            AssignPrefabToValues(tiles, prefabReference);
            DestroyImmediate(worldReference);
        }

        private void AssignPrefabToValues(Tile[,] tiles, GameObject prefab)
        {
            for (int i = 0; i < prefab.transform.childCount; i++)
            {
                Tile tile = prefab.transform.GetChild(i).GetComponent<Tile>();
                tiles[tile.x, tile.y] = tile;
            }
        }

        private Tile SpawnTile(Vector3 worldPosition, int x, int y)
        {
            bool parkingSlot = (x == 0 || y == 0 || x == width - 1|| y == height - 1);
            bool cornerPiece = ((x == 0 && y == 0) || (x == 0 && y == height - 1) || (x == width - 1 && y == height - 1) || (x == width - 1 && y == 0));

            //If you can spawn corners then ensure all corners are not considered a corner
            cornerPiece = spawnCorners ? false : cornerPiece;

            GameObject prefab = parkingSlot ? parkingTilePrefab : tilePrefab;

            Tile tile = Instantiate(prefab, worldPosition, Quaternion.identity).GetComponent<Tile>();

            tile.GO = tile.gameObject;

            tile.x = x;
            tile.y = y;

            tile.parkingSpace = parkingSlot;

            tile.canPlaceRoad = !cornerPiece;
            tile.GO.SetActive(!cornerPiece);

            if (tile != null && tile.parkingSpace && spawnParkingSpots && !cornerPiece)
                SpawnRoad(tile);

                

            return tile;
        }

        private void SpawnRoad(Tile tile)
        {
            GameObject roadPrefab = parkingSpots[Random.Range(0, parkingSpots.Length)];

            GameObject roadGO = Instantiate(roadPrefab, tile.GO.transform.position + roadOffset, Quaternion.identity, tile.GO.transform);
            Road road = roadGO.GetComponent<Road>();

            tile.hasRoad = true;
            tile.canPlaceRoad = false;

            roadGO.transform.forward = GetDirectionToCentre(tile.x, tile.y);
            roadGO.transform.localScale = new Vector3(1.0f, roadGO.transform.localScale.y, 1.0f);

            DestroyImmediate(road);
        }

        private Vector3 GetDirectionToCentre(int x, int y)
        {
            if (x == 0) return Vector3.right;
            if (y == 0) return Vector3.forward;
            if (x == width - 1) return -Vector3.right;
            if (y == height - 1) return -Vector3.forward;

            return Vector3.zero;
        }
    }
}
