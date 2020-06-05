using UnityEngine;

namespace Roads.Boards
{
    public class Tile : MonoBehaviour
    {
        [Header("World Details")]
        public GameObject GO;
        [Space]

        [Header("Board Details")]
        public int x;
        public int y;
        [Space]

        [Header("Attributes")]
        public bool parkingSlot;
        public bool hasRoad = false;
    }
}