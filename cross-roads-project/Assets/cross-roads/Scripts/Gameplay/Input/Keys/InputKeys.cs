//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;

namespace Roads.Controls
{
    [CreateAssetMenu(fileName = "Keys", menuName = "ScriptableObjects/Input/Keys")]
    public class InputKeys : ScriptableObject
    {
        public KeyCode leftClick = KeyCode.Mouse0;
        public KeyCode rightClick = KeyCode.Mouse1;

        public KeyCode positiveRotate = KeyCode.E;
        public KeyCode negativeRotate = KeyCode.Q;
    }
}