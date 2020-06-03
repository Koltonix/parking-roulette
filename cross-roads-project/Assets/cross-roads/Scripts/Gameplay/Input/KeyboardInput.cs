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
                OnLeftClick.Invoke();
        }
        protected override void RightClick()
        {
            if (Input.GetKeyDown(keys.rightClick))
                OnRightClick.Invoke();
        }

        private void RaycastFromCamera()
        {
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out hit, distance, mask);

            OnMouseHover.Invoke(hit);
        }
    }
}