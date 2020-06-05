//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
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