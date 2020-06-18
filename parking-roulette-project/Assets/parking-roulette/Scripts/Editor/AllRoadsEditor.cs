using UnityEngine;
using UnityEditor;
using ParkingRoulette.Roads;

namespace ParkingRoulette.Tools
{
    [CustomEditor(typeof(AllRoads))]
    public class AllRoadsEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            AllRoads allRoads = (AllRoads)target;
            if (GUILayout.Button("Auto-Set Values"))
                SetValuesLinearly(allRoads);
        }

        private void SetValuesLinearly(AllRoads allRoads)
        {
            for (int i = 0; i < allRoads.roads.Length; i++)
                allRoads.roads[i].value = i;
        }
    }
}