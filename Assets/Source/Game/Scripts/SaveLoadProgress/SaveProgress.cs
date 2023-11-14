using UnityEngine;
using Agava.YandexGames;

public class SaveProgress : MonoBehaviour
{
    private string _key = "antHill";
    private SaveModel data = null;

    public void Save(string language, int playerCoins, int playerLevel, int playerExperience, int score, bool isFirstSession, int levelId, bool levelState)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
if (PlayerAccount.IsAuthorized == true) PlayerAccount.GetCloudSaveData(OnSuccessLoad, OnErrorLoad);
#endif

        SaveModel newData = new()
        {
            Language = language,
            PlayerCoins = playerCoins,
            PlayerLevel = playerLevel,
            PlayerExperience = playerExperience,
            IsFirstSession = isFirstSession,
            PlayerScore = score,
            PlayerCompleteLevels = new(),
        };

        newData.PlayerCompleteLevels.Add(levelId, levelState);

        if (data != null)
        {
            var keys = data.PlayerCompleteLevels.Keys;

            foreach (int key in keys)
            {
                if (!newData.PlayerCompleteLevels.ContainsKey(key))
                {
                    data.PlayerCompleteLevels.TryGetValue(key, out bool value);
                    newData.PlayerCompleteLevels.Add(key, value);
                }
            }
        }

        string json = JsonUtility.ToJson(newData);

        if (PlayerAccount.IsAuthorized == false)
        {
            UnityEngine.PlayerPrefs.SetString(_key, json);
            UnityEngine.PlayerPrefs.Save();
        }
        else PlayerAccount.SetCloudSaveData(json);
    }

    public void GetLoad(LoadConfig loadConfig)
    {
        SaveModel data = null;
#if UNITY_WEBGL && !UNITY_EDITOR
if (PlayerAccount.IsAuthorized == true) PlayerAccount.GetCloudSaveData(OnSuccessLoad, OnErrorLoad);
#endif

        if (data != null)
        {
            if (data.PlayerCompleteLevels != null) loadConfig.UpdateListPlayerLevels(data.PlayerCompleteLevels);

            SetConfigParameters(data.Language, data.PlayerCoins, data.PlayerLevel, data.PlayerExperience, data.PlayerScore, data.IsFirstSession, loadConfig);
        }
        else return;
    }

    private void SetConfigParameters(string language, int playerCoins, int playerLevel, int playerExperience, int score, bool isFirstSession, LoadConfig loadConfig)
    {
        loadConfig.SetCurrentLanguage(language);
        loadConfig.SetPlayerParameters(playerCoins, playerLevel, playerExperience, score, isFirstSession);
    }

    private void OnSuccessLoad(string json)
    {
        data = JsonUtility.FromJson<SaveModel>(json);
    }

    private void OnErrorLoad(string message)
    {
        if (UnityEngine.PlayerPrefs.HasKey(_key))
        {
            string _hashKey = UnityEngine.PlayerPrefs.GetString(_key);
            data = JsonUtility.FromJson<SaveModel>(_hashKey);
        }
        else return;
    }
}