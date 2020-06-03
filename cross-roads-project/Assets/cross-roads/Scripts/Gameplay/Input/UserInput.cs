using UnityEngine;
using UnityEngine.Events;
using Roads.Events;

namespace Roads.Controls
{
    public abstract class UserInput : MonoBehaviour
    {
        [Header("Keys")]
        [SerializeField]
        protected InputKeys keys;

        [Header("Raycast Settings")]
        public RaycastHit hit;
        public Ray ray;
        public LayerMask mask;
        public float distance;

        [Header("Camera Settings")]
        [SerializeField]
        protected Camera mainCamera;

        [Header("Debug Settings")]
        [SerializeField]
        protected float rayDistance = 100f;
        [SerializeField]
        protected Color32 rayColour = Color.red;

        [Header("Events")]
        public UnityEvent OnLeftClick;
        public UnityEvent OnRightClick;
        public RaycastHitEvent OnMouseHover;

        protected virtual void Start()
        {
            if (!mainCamera) mainCamera = Camera.main;
        }

        protected virtual void Update()
        {
            LeftClick();
            RightClick();
            DebugDrawDray();
        }

        protected virtual void LeftClick() { OnLeftClick.Invoke(); }
        protected virtual void RightClick() { OnRightClick.Invoke(); }

        private void DebugDrawDray()
        {
            if(hit.collider) 
                Debug.DrawLine(ray.origin, hit.point, rayColour);

            else 
                Debug.DrawLine(ray.origin, ray.direction.normalized * rayDistance, rayColour);
        }
    }
}
