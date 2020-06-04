using UnityEngine;
using UnityEditor;

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
        [SerializeField]
        private GameObject parkingSpotPrefab;

        public void CreateBoard(Board board)
        {
            if (!boardParent) DestroyImmediate(boardParent);
            boardParent = new GameObject("Board");

            board.tiles = new Tile[board.width + 1, board.height + 1];
            board.debugTiles = new System.Collections.Generic.List<Tile>();
            
            for (int x = 0; x < board.width + 1; x++)
            {
                for (int y = 0; y < board.height + 1; y++)
                {
                    #region Removing Corners
                    if (x == 0 && y == 0) continue;
                    if (x == board.width && y == board.height) continue;

                    if (x == 0 && y == board.height) continue;
                    if (x == board.width && y == 0) continue;
                    #endregion

                    Vector3 spawnPosition = new Vector3(x * board.tileSize - ((board.width - 1 + 1) * board.tileSize * .5f), 0.0f,
                                                        y * board.tileSize - ((board.height - 1 + 1) * board.tileSize * .5f));

                    board.tiles[x, y] = SpawnTile(spawnPosition, x, y);
                    board.tiles[x, y].GO.transform.SetParent(boardParent.transform);
                }
            }
            
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(boardParent, Application.dataPath + "/cross-roads/Scripts/Gameplay/Board/ScriptableObjects/board.prefab");
            board.prefab = prefab;

            for (int i = 0; i < board.prefab.transform.childCount; i++)
            {
                Tile tile = board.prefab.transform.GetChild(i).GetComponent<Tile>();
                board.tiles[tile.x, tile.y] = tile;
                board.debugTiles.Add(tile);
            }

            DestroyImmediate(boardParent)
        }

        private Tile SpawnTile(Vector3 worldPosition, int x, int y)
        {
            bool parkingSlot = (x == 0 || y == 0 || x == board.width || y == board.height);
            GameObject prefab = parkingSlot ? parkingSpotPrefab : tilePrefab;

            Tile tile = Instantiate(prefab, worldPosition, Quaternion.identity).AddComponent<Tile>();

            tile.GO = tile.gameObject;
            tile.position = worldPosition;

            tile.x = x;
            tile.y = y;

            tile.parkingSlot = parkingSlot;

            return tile;
        }
    }
}