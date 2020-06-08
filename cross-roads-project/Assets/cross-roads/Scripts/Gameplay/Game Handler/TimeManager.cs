//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using ParkingRoulette.Events;

namespace ParkingRoulette.GameHandler 
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI timerUI;

        [SerializeField]
        private float defaultTime = 30.0f;
        [SerializeField]
        private float timeLeft = 0.0f;
        private bool timeStopped;

        [SerializeField]
        private IntEvent onReset;
        [SerializeField]
        private UnityEvent onTimeEnd;

        private void Start()
        {
            timeLeft = defaultTime;
            timeStopped = false;
        }


        private void Update()
        {
            if (!timeStopped)
                timeLeft -= Time.deltaTime;

            timerUI.text = Mathf.RoundToInt(timeLeft).ToString();

            if (timeLeft <= 0)
                onTimeEnd?.Invoke();
        }
        public void StopTime(bool value)
        {
            timeStopped = value;
        }

        public void ResetTime()
        {
            onReset?.Invoke(Mathf.RoundToInt(timeLeft));
            timeLeft = defaultTime;
        }
    }
}

