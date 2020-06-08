//////////////////////////////////////////////////
// Christopher Robertson 2020.
// https://github.com/Koltonix
// Copyright (c) 2020. All rights reserved.
//////////////////////////////////////////////////
using UnityEngine;
using TMPro;

namespace ParkingRoulette.GameHandler
{
    public class ScoreManager : MonoBehaviour
    {
        public int score;

        [SerializeField]
        private TextMeshProUGUI scoreUI;

        private void Start()
        {
            IncreaseScore(0);
        }

        public void IncreaseScore(int value)
        {
            score += value;
            scoreUI.text = score.ToString();

            SaveHighestScore(score);
        }

        public void ResetScore()
        {
            SaveHighestScore(score);

            score = 0;
            scoreUI.text = score.ToString();
        }

        private void SaveHighestScore(int score)
        {
            int highscore = PlayerPrefs.GetInt("HIGHSCORE");
            if (score > highscore)
                PlayerPrefs.SetInt("HIGHSCORE", score);

        }
    }
}

