using UnityEngine;
using ParkingRoulette.Enums;

namespace ParkingRoulette.Placing
{
    public class NullPlacement : Placement
    {
        private void Start()
        {
            type = PlacementType.UNSELECTED;
        }
    }
}
