using UnityEngine;
using YG;

public class SaveProgress : MonoBehaviour
{
    public void Save(string language, int playerCoins, int playerLevel, int playerExperience, int levelId, bool levelState)
    {
        YandexGame.savesData.language = language;
        YandexGame.savesData.PlayerCoins = playerCoins;
        YandexGame.savesData.PlayerLevel = playerLevel;
        YandexGame.savesData.PlayerExperience = playerExperience;
        YandexGame.savesData.PlayerCompleteLevels = new();

        if (YandexGame.savesData.PlayerCompleteLevels != null)
        {
            var keys = YandexGame.savesData.PlayerCompleteLevels.Keys;

            foreach (int key in keys)
            {
                if (key != levelId)
                {
                    YandexGame.savesData.PlayerCompleteLevels.Add(levelId, levelState);
                }
                else
                {
                    if (levelState == true)
                    {
                        YandexGame.savesData.PlayerCompleteLevels[key] = levelState;
                    }
                }
            }
        }
        else YandexGame.savesData.PlayerCompleteLevels.Add(levelId, levelState);

        YandexGame.SaveProgress();
    }

    public void Load() => YandexGame.LoadProgress();

    public void GetLoad(LoadConfig loadConfig)
    {
        if (YandexGame.savesData.PlayerCompleteLevels != null) loadConfig.UpdateListPlayerLevels(YandexGame.savesData.PlayerCompleteLevels);
        SetConfigParameters(YandexGame.savesData.language, YandexGame.savesData.PlayerCoins,
            YandexGame.savesData.PlayerLevel, YandexGame.savesData.PlayerExperience, loadConfig);
    }

    private void SetConfigParameters(string language, int playerCoins, int playerLevel, int playerExperience, LoadConfig loadConfig)
    {
        loadConfig.SetCurrentLanguage(language);
        loadConfig.SetPlayerParameters(playerCoins, playerLevel, playerExperience);
    }
}