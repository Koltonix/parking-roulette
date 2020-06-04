using UnityEngine;

namespace Roads.Board
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

        [Header("Board")]
        [SerializeField]
        private Board board;

        private void Start()
        {
            foreach (Tile tile in board.tiles)
            {
                TileToWorld(tile);
            }
        }

        public Tile GetClosestTile(Vector3 position)
        {
            float shortestDistance = float.MaxValue;
            Tile closestTile = null;

            while (!closestTile && board.tiles.Length > 0)
            {
                foreach (Tile tile in board.tiles)
                {
                    float distance = Vector3.Distance(position, tile.position);
                    if (distance < shortestDistance) 
                        closestTile = tile;
                }
            }

            return closestTile;
        }

        public void WorldToTile(Vector3 worldPostion)
        {
            //float offset = -12.5f;
            //int x = 
        }

        public void TileToWorld(Tile tile)
        {
            float offset = -12.5f;

            Vector3 worldPosition = Vector3.zero;
            worldPosition.x = (((tile.x + 1) + offset) - board.tiles.GetLength(0)) * board.tileSize;
            worldPosition.z = (((tile.y + 1) + offset) - board.tiles.GetLength(1)) * board.tileSize;

            Debug.Log(string.Format("({0} : {1}) : {2}", tile.x, tile.y, worldPosition));
        }
    }
}