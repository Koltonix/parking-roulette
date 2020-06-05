using UnityEngine;
using Roads.Enums;

namespace Roads.Placement
{
    public class PathPlacement : Placement
    {
        private void Start()
        {
            placement = PlacementType.PATHING;
        }
    }
}