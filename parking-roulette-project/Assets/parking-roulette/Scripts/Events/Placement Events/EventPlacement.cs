using System.Collections.Generic;
using UnityEngine;
using ParkingRoulette.Placing;

/// <summary>
/// Following this architecture: https://unity.com/how-to/architect-game-code-scriptable-objects
/// </summary>
namespace ParkingRoulette.Events
{
    [CreateAssetMenu(fileName = "RaycastEvent", menuName = "ScriptableObjects/Events/PlacementEvent")]
    public class EventPlacement : ScriptableObject
    {
        private List<PlacementListener> listeners = new List<PlacementListener>();

        public void Raise(Placement placement)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(placement);
        }

        public void SubscribeListener(PlacementListener listener) { listeners.Add(listener); }
        public void UnsubscribeListener(PlacementListener listener) { listeners.Remove(listener); }
    }

}
