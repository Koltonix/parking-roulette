using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// https://unity.com/how-to/architect-game-code-scriptable-objects
/// </summary>
namespace ParkingRoulette.Events
{
    public class RaycastListener : MonoBehaviour
    {
        public EventRaycast Event;
        public RaycastHitEvent Reponse;

        private void OnEnable() { Event.SubscribeListener(this); }
        private void OnDisable() { Event.UnsubscribeListener(this); }
        public void OnEventRaised(RaycastHit hit) { Reponse.Invoke(hit); }
    }
}
