using UnityEditor;
using Roads.Board;
using UnityEngine;

namespace Roads.Tools
{
    [CustomEditor(typeof(BoardCreator))]
    public class BoardCreatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            BoardCreator boardCreator = (BoardCreator)target;
            if (GUILayout.Button("Create Board"))
            {
                boardCreator.CreateBoard(boardCreator.board);
            }
        }
    }
}

