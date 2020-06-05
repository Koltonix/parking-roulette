//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;

namespace Roads.Controls
{
    public class KeyboardInput : UserInput
    {
        protected override void Update()
        {
            base.Update();
            RaycastFromCamera();
        }

        protected override void LeftClick()
        {
            if (Input.GetKeyDown(keys.leftClick))
                OnLeftClick?.Invoke();
        }
        protected override void RightClick()
        {
            if (Input.GetKeyDown(keys.rightClick))
                OnRightClick?.Invoke();
        }

        protected override void PositiveRotate()
        {
            if (Input.GetKeyDown(keys.positiveRotate))
                OnPositiveRotate?.Invoke();
        }

        protected override void NegativeRotate()
        {
            if (Input.GetKeyDown(keys.negativeRotate))
                OnNegativeRotate?.Invoke();
        }

        private void RaycastFromCamera()
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, distance, mask);

            OnMouseHover.Invoke(hit);
        }
    }
}