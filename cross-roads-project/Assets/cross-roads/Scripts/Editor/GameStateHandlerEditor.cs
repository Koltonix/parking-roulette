//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using UnityEditor;
using ParkingRoulette.GameHandler;

namespace ParkingRoulette.Tools
{
    [CustomEditor(typeof(GameStateHandler))]
    public class GameStateHandlerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameStateHandler gameHandler = (GameStateHandler)target;

            if (GUILayout.Button("Check For Overlap"))
                gameHandler.CheckOverlap();
        }
    }
}
