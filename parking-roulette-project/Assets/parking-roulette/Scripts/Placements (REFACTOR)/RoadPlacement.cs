using System.Collections.Generic;
using UnityEngine;
using ParkingRoulette.Enums;
using ParkingRoulette.Boards;
using ParkingRoulette.Roads;

namespace ParkingRoulette.Placing
{
    public class RoadPlacement : Placement
    {
        [SerializeField]
        private GameObject roadPrefab;
        [SerializeField]
        private Vector3 spawnOffset = Vector3.zero;
        private Dictionary<Tile, Road> instancedRoads = new Dictionary<Tile, Road>();

        private void Start()
        {
            type = PlacementType.ROAD;
        }

        public override void PlaceItem(RaycastHit hit)
        {
            //Converting this to a ScriptableObject... 
            //Takes in a position, returns a Tile?
            //Store the Board Data in a ScriptableObject???
            //For now a singleton will do...
            Tile tile = BoardManager.Instance.WorldToTile(hit.point);

            //Tile exists, does not have a road, and a road can be placed
            if (tile != null && !tile.hasRoad && tile.canPlaceRoad)
            {
                SpawnRoad(tile.GO.transform.position);
                tile.hasRoad = true;
            }
                
        }

        private void SpawnRoad(Vector3 position)
        {
            Road road = Instantiate(roadPrefab, position + spawnOffset, Quaternion.identity).GetComponent<Road>();
            instancedRoads.Add(BoardManager.Instance.WorldToTile(position), road);

            road.UpdateRoad();
            UpdateAdjacentRoads(position);
        }

        public override void RemoveItem(RaycastHit hit)
        {
            
        }

        private void DestroyRoad()
        {

        }

        private void UpdateAdjacentRoads(Vector3 centrePosition)
        {
            Tile centreTile = BoardManager.Instance.WorldToTile(centrePosition);
            Tile[] adjacentTiles = BoardManager.Instance.GetAdjacentTiles(centreTile);
            foreach (Tile tile in adjacentTiles)
            {
                if (tile.hasRoad && tile.canPlaceRoad)
                    instancedRoads[tile].UpdateRoad();
            }
        }
    }
}
