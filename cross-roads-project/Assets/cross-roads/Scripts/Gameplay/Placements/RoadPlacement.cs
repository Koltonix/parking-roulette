using System.Collections.Generic;
using UnityEngine;
using Roads.Roads;
using Roads.Boards;
using Roads.Enums;

namespace Roads.Placement
{
    public class RoadPlacement : Placement
    {
        [Header("Roads")]
        private Dictionary<Tile, Road> roads = new Dictionary<Tile, Road>();
        [SerializeField]
        private GameObject roadPrefab;

        private void Start()
        {
            placement = PlacementType.ROAD;   
        }

        public override void PlaceItem(Vector3 position)
        {
            GameObject road = Instantiate(roadPrefab, position, Quaternion.identity);
        }

        public override void RemoveItem(Vector3 position)
        {
            
        }
    }
}