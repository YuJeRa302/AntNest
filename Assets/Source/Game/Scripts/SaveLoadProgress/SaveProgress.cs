using UnityEngine;
using Agava.YandexGames;

public class SaveProgress : MonoBehaviour
{
    private readonly string _key = "antHill";
    private readonly int _nullLevel = 0;

    private SaveModel _data = null;
    private LoadConfig _loadConfig;

    public void Save(string language, int playerCoins, int playerLevel, int playerExperience, int score, bool isFirstSession, int levelId, bool levelState)
    {
        if (PlayerAccount.IsAuthorized == true) PlayerAccount.GetCloudSaveData(OnSuccessLoad, OnErrorLoad);

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

        if (_data != null)
        {
            var keys = _data.PlayerCompleteLevels.Keys;

            foreach (int key in keys)
            {
                if (!newData.PlayerCompleteLevels.ContainsKey(key))
                {
                    _data.PlayerCompleteLevels.TryGetValue(key, out bool value);
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
        _loadConfig = loadConfig;

        if (PlayerAccount.IsAuthorized == true) PlayerAccount.GetCloudSaveData(OnSuccessLoad, OnErrorLoad);
    }

    private void UpdateConfig(SaveModel data, LoadConfig loadConfig)
    {
        if (data != null)
        {
            if (data.PlayerCompleteLevels != null)
                loadConfig.UpdateListPlayerLevels(data.PlayerCompleteLevels);

            var playerLevel = data.PlayerLevel == _nullLevel ? loadConfig.PlayerLevel : data.PlayerLevel;
            SetConfigParameters(data.Language, data.PlayerCoins, playerLevel, data.PlayerExperience, data.PlayerScore, data.IsFirstSession, loadConfig);
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
        _data = JsonUtility.FromJson<SaveModel>(json);
        UpdateConfig(_data, _loadConfig);
    }

    private void OnErrorLoad(string message)
    {
        if (UnityEngine.PlayerPrefs.HasKey(_key))
        {
            string _hashKey = UnityEngine.PlayerPrefs.GetString(_key);
            _data = JsonUtility.FromJson<SaveModel>(_hashKey);
        }
        else return;
    }
}