using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// https://unity.com/how-to/architect-game-code-scriptable-objects
/// </summary>
namespace ParkingRoulette.Events
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent Event;
        public UnityEvent Reponse;

        private void OnEnable() { Event.SubscribeListener(this); }
        private void OnDisable() { Event.UnsubscribeListener(this); }
        public void OnEventRaised() { Reponse.Invoke(); }
    }
}
