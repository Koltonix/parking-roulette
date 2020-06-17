using UnityEngine;
using ParkingRoulette.Enums;

namespace ParkingRoulette.Placing
{
    public class PathPlacement : Placement
    {
        private void Start()
        {
            type = PlacementType.PATHING;
        }
    }
}
