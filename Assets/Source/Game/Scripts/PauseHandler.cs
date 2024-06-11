using UnityEngine;

namespace Source.Game.Scripts
{
    public class PauseHandler : MonoBehaviour
    {
        private static float s_pauseValue = 0;
        private static float s_resumeValue = 1f;

        private int _sourceCounter;

        public void PauseGame()
        {
            if (_sourceCounter == 0)
            {
                AudioListener.pause = true;
                AudioListener.volume = s_pauseValue;
                Time.timeScale = s_pauseValue;
            }

            _sourceCounter++;
        }

        public void ResumeGame()
        {
            _sourceCounter--;

            if (_sourceCounter == 0)
            {
                AudioListener.pause = false;
                AudioListener.volume = s_resumeValue;
                Time.timeScale = s_resumeValue;
            }
        }
    }
}