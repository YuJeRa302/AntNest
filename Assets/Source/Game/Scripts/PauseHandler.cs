using UnityEngine;

namespace Source.Game.Scripts
{
    public class PauseHandler : MonoBehaviour
    {
        private static float s_pauseValue = 0;
        private static float s_resumeValue = 1f;

        [SerializeField] private LoadConfig _config;

        private readonly int _valueWithGamePause = 1;
        private readonly int _valueWithoutGamePause = 0;

        private int _sourceCounter;

        public void PauseGame()
        {
            if (_sourceCounter == _valueWithoutGamePause)
            {
                if (_config.IsGamePause == false)
                {
                    AudioListener.pause = true;
                    AudioListener.volume = s_pauseValue;
                }

                Time.timeScale = s_pauseValue;
            }

            if (_sourceCounter == _valueWithGamePause)
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

            if (_sourceCounter == _valueWithoutGamePause)
            {
                SetSoundState();
                Time.timeScale = s_resumeValue;
            }

            if (_sourceCounter == _valueWithGamePause)
                SetSoundState();
        }

        private void SetSoundState()
        {
            if (_config.IsSoundOn == false)
            {
                AudioListener.pause = true;
                AudioListener.volume = s_pauseValue;
            }
            else
            {
                AudioListener.pause = false;
                AudioListener.volume = s_resumeValue;
            }
        }
    }
}