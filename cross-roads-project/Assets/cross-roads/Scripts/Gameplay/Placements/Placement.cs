using Roads.Enums;
using UnityEngine;

namespace Roads.Placement
{
    public abstract class Placement : MonoBehaviour
    {
        [HideInInspector]
        public PlacementType placement = PlacementType.NULL;

        public virtual void PlaceItem(Vector3 position){ }
        public virtual void RemoveItem(Vector3 position) { }
    }
}

