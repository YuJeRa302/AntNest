using UnityEngine;
using Agava.YandexGames;

public class SaveProgress : MonoBehaviour
{
    private string _key = "antHill";

    public void Save(string language, int playerCoins, int playerLevel, int playerExperience, int score, bool isFirstSession, int levelId, bool levelState)
    {
        string _hashKey = null;
        SaveModel data = null;

        if (PlayerAccount.IsAuthorized == true)
        {
            PlayerAccount.GetCloudSaveData((data) => _key = data);
            data = JsonUtility.FromJson<SaveModel>(_key);
        }
        else
        {
            if (UnityEngine.PlayerPrefs.HasKey(_key))
            {
                _hashKey = UnityEngine.PlayerPrefs.GetString(_key);
                data = JsonUtility.FromJson<SaveModel>(_hashKey);
            }
            else return;
        }

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
        string _hashKey = null;
        SaveModel data = null;

        if (PlayerAccount.IsAuthorized == true)
        {
            PlayerAccount.GetCloudSaveData((data) => _key = data);

            if (_key != null) data = JsonUtility.FromJson<SaveModel>(_key);
            else return;
        }
        else
        {
            if (UnityEngine.PlayerPrefs.HasKey(_key))
            {
                _hashKey = UnityEngine.PlayerPrefs.GetString(_key);
                data = JsonUtility.FromJson<SaveModel>(_hashKey);
            }
            else return;
        }

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
}