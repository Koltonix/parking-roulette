using UnityEngine;

namespace Roads.Cameras
{
    public abstract class CameraState : MonoBehaviour
    {
        public virtual void OnEnter() { }
        public virtual void OnStay() { }
        public virtual void OnExit() { }
    }
}

