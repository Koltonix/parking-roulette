using UnityEngine;

namespace ParkingRoulette.Roads
{
    [CreateAssetMenu(fileName = "Roads", menuName = "ScriptableObjects/Roads")]
    public class AllRoads : ScriptableObject
    {
        public RoadValue[] roads;
    }
}

