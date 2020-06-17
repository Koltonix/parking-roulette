using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Following this architecture: https://unity.com/how-to/architect-game-code-scriptable-objects
/// </summary>
namespace ParkingRoulette.Events
{
    [CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/Events/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        private List<GameEventListener> listeners = new List<GameEventListener>();

        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }

        public void SubscribeListener(GameEventListener listener) { listeners.Add(listener); }
        public void UnsubscribeListener(GameEventListener listener) { listeners.Remove(listener); }
    }

}
