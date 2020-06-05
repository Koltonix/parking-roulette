//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;

namespace ParkingRoulette.Boards
{
    public class BoardManager : MonoBehaviour
    {
        #region Singleton
        public static BoardManager Instance;
        private void Awake()
        {
            if (!Instance) Instance = this;
            else Destroy(this);
        }
        #endregion

        [Header("Board Assignment")]
        [SerializeField]
        private GameObject board;
        [Space]

        [Header("Board Settings")]
        private int width = 0;
        private int height = 0;
        private Vector2 tileGap = Vector2.zero;
        [Space]

        [Header("Board References")]
        private Tile[,] boardInstance;
        private Tile[] tileDebug;

        private void Start()
        {
            tileDebug = GetTilesFromBoard(board);

            width = GetLength(tileDebug, 0);
            height = GetLength(tileDebug, 1);

            boardInstance = AssignTilesToGrid(tileDebug);
            
            tileGap = GetTileGap(boardInstance);

            foreach (Tile tile in boardInstance)
            {
                TileToWorld(tile);
            }
        }

        #region Calculating Board Values
        private Tile[,] AssignTilesToGrid(Tile[] tiles)
        {
            Tile[,] gridTiles = new Tile[width, height];

            foreach (Tile tile in tiles)
            {
                gridTiles[tile.x, tile.y] = tile;
            }

            return gridTiles;
        }

        private Tile[] GetTilesFromBoard(GameObject board)
        {
            Tile[] tiles = new Tile[board.transform.childCount];
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i] = board.transform.GetChild(i).GetComponent<Tile>();
            }

            return tiles;
        }

        private int GetLength(Tile[] tiles, int index)
        {
            Tile furthestTile = null;
            int largestSize = 0;

            foreach (Tile tile in tiles)
            {
                int currentSize = (index == 0) ? tile.x : tile.y;
                if (currentSize > largestSize)
                {
                    furthestTile = tile;
                    largestSize = currentSize;
                }
            }

            return (index == 0) ? furthestTile.x + 1 : furthestTile.y + 1;
        }

        private Vector2 GetTileGap(Tile[,] tiles)
        {
            if (tiles.GetLength(0) < 3 || tiles.GetLength(1) < 3) return Vector2.zero;

            float x = Mathf.Abs(tiles[1, 1].transform.position.x - tiles[2, 1].transform.position.x);
            float y = Mathf.Abs(tiles[1, 1].transform.position.z - tiles[1, 2].transform.position.z);

            return new Vector2(x, y);
        }
        #endregion

        public Tile GetClosestTile(Vector3 position)
        {
            float shortestDistance = float.MaxValue;
            Tile closestTile = null;

            foreach (Tile tile in boardInstance)
            {
                if (tile == null) continue;

                float distance = Vector3.Distance(position, tile.GO.transform.position);
                if (distance < shortestDistance)
                {
                    closestTile = tile;
                    shortestDistance = distance;
                }
            }

            return closestTile;
        }

        public Vector3 TileToWorld(Tile tile)
        {
            if (tile == null) return Vector3.zero;

            Vector2 offset = GetOffset(width, height, tileGap);
            Vector3 worldPosition = Vector3.zero;

            worldPosition.x = (((tile.x + 1) + (offset.x / tileGap.x)) - width) * tileGap.x;
            worldPosition.z = (((tile.y + 1) + (offset.y / tileGap.y)) - width) * tileGap.y;

            return worldPosition;
        }

        public Tile WorldToTile(Vector3 position)
        {
            Vector2 offset = GetOffset(width, height, tileGap);
            int x = Mathf.RoundToInt((((position.x / tileGap.x) + width) - (offset.x / tileGap.x)) - 1);
            int y = Mathf.RoundToInt((((position.z / tileGap.y) + height) - (offset.y / tileGap.y)) - 1);

            x = Mathf.Clamp(x, 0, width - 1);
            y = Mathf.Clamp(y, 0, height - 1);

            return boardInstance[x, y];
        }

        private Vector2 GetOffset(int width, int height, Vector2 tileGap)
        {
            Vector2 offset;
            offset.x = ((width * tileGap.x) - tileGap.x) * .5f;
            offset.y = ((height * tileGap.y) - tileGap.y) * .5f;

            return offset;
        }

        public void SelectTile(RaycastHit hit)
        {
            if (!hit.collider) return;

            ResetTiles();

            Tile hitTile = WorldToTile(hit.point);
            if (hitTile)
                hitTile.GO.GetComponent<Renderer>().material.color = hitTile.selectedColour;
        }

        private void ColourTiles(Tile[,] tiles, Color32 colour)
        {
            foreach (Tile tile in tiles)
            {
                if (tile != null)
                    tile.GO.GetComponent<Renderer>().material.color = colour;
            }
        }

        public void ResetTiles()
        {
            foreach (Tile tile in boardInstance)
            {
                if (tile != null)
                    tile.GO.GetComponent<Renderer>().material.color = tile.defaultColour;
            }
        }
    }
}