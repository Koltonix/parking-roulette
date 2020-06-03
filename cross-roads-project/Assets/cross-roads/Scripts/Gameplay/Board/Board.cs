using UnityEngine;

namespace Roads.Board
{
    [CreateAssetMenu(fileName = "Board", menuName = "ScriptableObjects/Board")]
    public class Board : ScriptableObject
    {
        public Tile[,] tiles;
    }
}
