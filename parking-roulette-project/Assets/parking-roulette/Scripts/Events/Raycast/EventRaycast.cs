using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Following this architecture: https://unity.com/how-to/architect-game-code-scriptable-objects
/// </summary>
namespace ParkingRoulette.Events
{
    [CreateAssetMenu(fileName = "RaycastEvent", menuName = "ScriptableObjects/Events/RaycastEvent")]
    public class EventRaycast : ScriptableObject
    {
        private List<RaycastListener> listeners = new List<RaycastListener>();

        public void Raise(RaycastHit hit)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(hit);
        }

        public void SubscribeListener(RaycastListener listener) { listeners.Add(listener); }
        public void UnsubscribeListener(RaycastListener listener) { listeners.Remove(listener); }
    }

}
