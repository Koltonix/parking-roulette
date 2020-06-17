using UnityEngine;
using ParkingRoulette.Placing;

/// <summary>
/// https://unity.com/how-to/architect-game-code-scriptable-objects
/// </summary>
namespace ParkingRoulette.Events
{
    public class PlacementListener : MonoBehaviour
    {
        public EventPlacement Event;
        public PlacementEvent Reponse;

        private void OnEnable() { Event.SubscribeListener(this); }
        private void OnDisable() { Event.UnsubscribeListener(this); }
        public void OnEventRaised(Placement placement) { Reponse.Invoke(placement); }
    }
}
