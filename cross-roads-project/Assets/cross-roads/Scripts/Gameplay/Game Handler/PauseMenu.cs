//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using UnityEngine.Events;

namespace ParkingRoulette.GameHandler
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private KeyCode pauseKey = KeyCode.Escape;
        public bool isPaused;

        [SerializeField]
        private UnityEvent onPause;
        [SerializeField]
        private UnityEvent onResume;


        private void Update()
        {
            if (Input.GetKeyDown(pauseKey))
                PauseGame(!isPaused);
        }

        public void PauseGame(bool value)
        {
            Time.timeScale = value ? 0.0f : 1.0f;
            isPaused = value;

            if (isPaused)
                onPause?.Invoke();

            if (!isPaused)
                onResume?.Invoke();
        }

        private void OnDestroy()
        {
            Time.timeScale = 1.0f;
        }
    }
}
