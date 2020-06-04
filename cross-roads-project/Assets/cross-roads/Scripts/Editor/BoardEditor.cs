using UnityEditor;
using Roads.Boards;
using UnityEngine;

namespace Roads.Tools
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

