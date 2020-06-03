using UnityEngine;

namespace Roads.Board
{
    [CreateAssetMenu(fileName = "Board", menuName = "ScriptableObjects/Board")]
    public class Board : ScriptableObject
    {
        public int height;
        public int width;
        public int tileSize;

        public Tile[,] tiles;
    }
}
