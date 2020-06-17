using UnityEngine;

/// <summary>
/// https://unity.com/how-to/architect-game-code-scriptable-objects
/// </summary>
namespace ParkingRoulette.Events
{
    public class StringListener : MonoBehaviour
    {
        public EventString Event;
        public StringEvent Reponse;

        private void OnEnable() { Event.SubscribeListener(this); }
        private void OnDisable() { Event.UnsubscribeListener(this); }
        public void OnEventRaised(string value) { Reponse.Invoke(value); }
    }
}
