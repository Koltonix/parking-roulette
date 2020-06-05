using System;
using UnityEngine;
using UnityEngine.Events;

namespace Roads.Events
{
    [Serializable]
    public class RaycastHitEvent : UnityEvent<RaycastHit> { };

    [Serializable]
    public class StringEvent : UnityEvent<string> { };
}
