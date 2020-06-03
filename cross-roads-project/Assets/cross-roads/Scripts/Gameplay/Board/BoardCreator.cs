using System.Security.Cryptography;
using UnityEngine;

namespace Roads.Board
{
    public class BoardCreator : MonoBehaviour
    {
        [Header("Board Settings")]
        public Board board;
        private GameObject boardParent;
        [Space]

        [Header("Tile Settings")]
        [SerializeField]
        private GameObject tilePrefab;

        public void CreateBoard(Board board)
        {
            if (boardParent) DestroyImmediate(boardParent);
            boardParent = new GameObject("Board");

            board.tiles = new Tile[board.width, board.height];

            for (int x = 0; x < board.width; x++)
            {
                for (int y = 0; y < board.height; y++)
                {
                    Vector3 spawnPosition = new Vector3(x * board.tileSize - ((board.width - 1) * board.tileSize * .5f), 0.0f,
                                                        y * board.tileSize - ((board.height - 1) * board.tileSize * .5f));

                    board.tiles[x, y] = SpawnTile(spawnPosition, x, y);
                    board.tiles[x, y].GO.transform.SetParent(boardParent.transform);
                }
            }
        }

        private Tile SpawnTile(Vector3 worldPosition, int x, int y)
        {
            Tile tile = Instantiate(tilePrefab, worldPosition, Quaternion.identity).AddComponent<Tile>();

            tile.GO = tile.gameObject;
            tile.position = worldPosition;

            tile.x = x;
            tile.y = y;

            //tile.GO.GetComponent<Renderer>().material.color = (x % 2 == 0) ? Color.black : Color.white;

            return tile;
        }
    }
}