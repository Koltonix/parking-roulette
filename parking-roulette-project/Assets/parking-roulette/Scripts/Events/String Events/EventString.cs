using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Following this architecture: https://unity.com/how-to/architect-game-code-scriptable-objects
/// </summary>
namespace ParkingRoulette.Events
{
    [CreateAssetMenu(fileName = "RaycastEvent", menuName = "ScriptableObjects/Events/StringEvent")]
    public class EventString : ScriptableObject
    {
        private List<StringListener> listeners = new List<StringListener>();

        public void Raise(string value)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(value);
        }

        public void SubscribeListener(StringListener listener) { listeners.Add(listener); }
        public void UnsubscribeListener(StringListener listener) { listeners.Remove(listener); }
    }

}
