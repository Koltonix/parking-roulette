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

        [Header("Board")]
        [SerializeField]
        private Board boardReference;

        private Tile[,] boardInstance;

        private void Start()
        {
            
        }

        public Tile GetClosestTile(Vector3 position)
        {
            float shortestDistance = float.MaxValue;
            Tile closestTile = null;

            while (!closestTile && boardInstance.Length > 0)
            {
                foreach (Tile tile in boardInstance)
                {
                    float distance = Vector3.Distance(position, tile.GO.transform.position);
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
            worldPosition.x = (((tile.x + 1) + offset) - boardInstance.GetLength(0)) * boardReference.tileSize;
            worldPosition.z = (((tile.y + 1) + offset) - boardInstance.GetLength(1)) * boardReference.tileSize;

            Debug.Log(string.Format("({0} : {1}) : {2}", tile.x, tile.y, worldPosition));
        }
    }
}