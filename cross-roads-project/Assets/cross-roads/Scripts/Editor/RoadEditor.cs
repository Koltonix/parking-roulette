//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using ParkingRoulette.Roads;

namespace ParkingRoulette.Tools
{
    [CustomEditor(typeof(Road))]
    public class RoadEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Road road = (Road)target;
            if (GUILayout.Button("DEBUG BINARY ROAD VALUE"))
                road.UpdateRoad();
        }
    }
}
