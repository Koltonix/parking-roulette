using UnityEditor;
using ParkingRoulette.Boards;
using UnityEngine;

namespace ParkingRoulette.Tools
{
    [CustomEditor(typeof(Board))]
    public class BoardEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            Board board = (Board)target;
            if (GUILayout.Button("Create Board"))
            {
                board.CreateBoard();
            }
        }
    }
}

