using UnityEngine;

namespace Roads.Placement
{
    public abstract class Placement : MonoBehaviour
    {
        public virtual void PlaceItem(Vector3 position){ }
        public virtual void RemoveItem(Vector3 position) { }
    }
}

