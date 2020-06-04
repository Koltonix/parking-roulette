using UnityEditor;
using UnityEngine;

namespace Roads.Boards
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

        private void Start()
        {
            Tile[] tiles = GetTilesFromBoard(board);

            width = GetLength(tiles, 0);
            height = GetLength(tiles, 1);

            boardInstance = AssignTilesToGrid(tiles);
            tileGap = GetTileGap(boardInstance);
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
                float distance = Vector3.Distance(position, tile.GO.transform.position);
                if (distance < shortestDistance)
                    closestTile = tile;
            }

            return closestTile;
        }

        public void WorldToTile(Vector3 worldPostion)
        {
            //float offset = -12.5f;
            //int x = 
        }

        //public void TileToWorld(Tile tile)
        //{
        //    float offset = -12.5f;

        //    Vector3 worldPosition = Vector3.zero;
        //    worldPosition.x = (((tile.x + 1) + offset) - boardInstance.GetLength(0)) * boardReference.tileSize;
        //    worldPosition.z = (((tile.y + 1) + offset) - boardInstance.GetLength(1)) * boardReference.tileSize;

        //    Debug.Log(string.Format("({0} : {1}) : {2}", tile.x, tile.y, worldPosition));
        //}
    }
}