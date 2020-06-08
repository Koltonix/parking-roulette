//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using TMPro;
using UnityEngine;

namespace ParkingRoulette.GameHandler
{
    public class Highscore : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI highscoreText;

        private void Start()
        {
            SetHighscore();
        }

        private void SetHighscore()
        {
            if (highscoreText)
                highscoreText.text = "HIGHSCORE\n" + PlayerPrefs.GetInt("HIGHSCORE").ToString();
        }

        public void ResetScore() 
        { 
            PlayerPrefs.DeleteKey("HIGHSCORE");
            SetHighscore();
        }
    }
}
