using UnityEngine;
using ParkingRoulette.Enums;

namespace ParkingRoulette.Placing
{
    public class RoadPlacement : Placement
    {
        private void Start()
        {
            type = PlacementType.ROAD;
        }
    }
}
