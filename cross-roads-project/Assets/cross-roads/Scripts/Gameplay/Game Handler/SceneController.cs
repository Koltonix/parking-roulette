using UnityEngine;
using UnityEngine.SceneManagement;

namespace ParkingRoulette.GameHandler
{
    public class SceneController : MonoBehaviour
    {
        public void ChangeScene(int buildIndex) { SceneManager.LoadScene(buildIndex); }
        public void ChangeScene(string levelName) { SceneManager.LoadScene(levelName); }
        public void RestartScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
        public void Quit() { Application.Quit(); }
    }
}
