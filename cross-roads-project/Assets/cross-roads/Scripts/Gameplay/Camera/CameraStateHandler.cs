using UnityEngine;

namespace Roads.Cameras
{
    public class CameraStateHandler : MonoBehaviour
    {
        [Header("States")]
        [SerializeField]
        private CameraState currentState;
        [SerializeField]
        private CameraState rotateState;

        private void Start()
        {
            currentState.OnEnter();
        }
    }
}

