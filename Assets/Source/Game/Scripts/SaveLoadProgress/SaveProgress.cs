using UnityEngine;
using Agava.YandexGames;
using System.Collections;

public class SaveProgress : MonoBehaviour
{
    private readonly string _key = "antHill";
    private readonly int _nullLevel = 0;

    private bool _isGetLoadRespondRecive;
    private SaveModel _data = null;
    private LoadConfig _loadConfig;

    public void Save(int playerCoins, int playerLevel, int playerExperience, int score, LoadConfig loadConfig)
    {
        bool[] levelsComplete = new bool[loadConfig.CountLevels];

        if (loadConfig.LevelsComplete != null)
            levelsComplete = loadConfig.LevelsComplete;

        levelsComplete[loadConfig.LevelDataState.LevelData.LevelId] = loadConfig.LevelDataState.IsComplete;

        SaveModel newData = new()
        {
            PlayerCoins = playerCoins,
            PlayerLevel = playerLevel,
            PlayerExperience = playerExperience,
            PlayerScore = score,
            Language = loadConfig.Language,
            AmbientVolume = loadConfig.AmbientVolume,
            InterfaceVolume = loadConfig.InterfaceVolume,
            PlayerLevelComplete = levelsComplete
        };

#if UNITY_EDITOR
        loadConfig.UpdateListPlayerLevels(levelsComplete);
        loadConfig.SetAmbientVolume(loadConfig.AmbientVolume);
        loadConfig.SetIterfaceVolume(loadConfig.InterfaceVolume);
        SetConfigParameters(loadConfig.Language, playerCoins, playerLevel, playerExperience, score, loadConfig);
#else
        string json = JsonUtility.ToJson(newData);

        Debug.Log("Save progress" + json);

        if (PlayerAccount.IsAuthorized == false)
        {
            UnityEngine.PlayerPrefs.SetString(_key, json);
            UnityEngine.PlayerPrefs.Save();
        }
        else
            PlayerAccount.SetCloudSaveData(json);
#endif

    }

    public IEnumerator SaveApplicationParameters(LoadConfig loadConfig)
    {
        _loadConfig = loadConfig;
        float ambientVolume = loadConfig.AmbientVolume;
        float interfaceVolume = loadConfig.InterfaceVolume;
        string language = loadConfig.Language;

#if UNITY_EDITOR
        _isGetLoadRespondRecive = true;

#else
        if (PlayerAccount.IsAuthorized == true)
            PlayerAccount.GetCloudSaveData(OnSuccessLoad, OnErrorLoad);
#endif

        yield return new WaitUntil(() => _isGetLoadRespondRecive);

        SaveModel newData = new()
        {
            PlayerCoins = _loadConfig.PlayerCoins,
            PlayerLevel = _loadConfig.PlayerLevel,
            PlayerExperience = _loadConfig.PlayerExperience,
            PlayerScore = _loadConfig.PlayerScore,
            Language = language,
            AmbientVolume = ambientVolume,
            InterfaceVolume = interfaceVolume,
            PlayerLevelComplete = _loadConfig.LevelsComplete
        };

#if UNITY_EDITOR
        loadConfig.UpdateListPlayerLevels(_loadConfig.LevelsComplete);
        loadConfig.SetAmbientVolume(ambientVolume);
        loadConfig.SetIterfaceVolume(interfaceVolume);
        SetConfigParameters(language, _loadConfig.PlayerCoins, _loadConfig.PlayerLevel, _loadConfig.PlayerExperience, _loadConfig.PlayerScore, loadConfig);
#else
        string json = JsonUtility.ToJson(newData);

        if (PlayerAccount.IsAuthorized == false)
        {
            UnityEngine.PlayerPrefs.SetString(_key, json);
            UnityEngine.PlayerPrefs.Save();
        }
        else
            PlayerAccount.SetCloudSaveData(json);
#endif
    }

    public IEnumerator GetLoad(LoadConfig loadConfig)
    {
        _loadConfig = loadConfig;
        _isGetLoadRespondRecive = false;

#if UNITY_EDITOR
        _isGetLoadRespondRecive = true;
#else
        if (PlayerAccount.IsAuthorized == true)
            PlayerAccount.GetCloudSaveData(OnSuccessLoad, OnErrorLoad);
#endif
        yield return new WaitUntil(() => _isGetLoadRespondRecive);
    }

    private void UpdateConfig(SaveModel data, LoadConfig loadConfig)
    {
        if (data != null)
        {
            if (data.PlayerLevelComplete != null)
                loadConfig.UpdateListPlayerLevels(data.PlayerLevelComplete);

            var playerLevel = data.PlayerLevel == _nullLevel ? loadConfig.PlayerLevel : data.PlayerLevel;
            SetConfigParameters(data.Language, data.PlayerCoins, playerLevel, data.PlayerExperience, data.PlayerScore, loadConfig);
        }
        else
            return;
    }

    private void SetConfigParameters(string language, int playerCoins, int playerLevel, int playerExperience, int score, LoadConfig loadConfig)
    {
        loadConfig.SetCurrentLanguage(language);
        loadConfig.SetPlayerParameters(playerCoins, playerLevel, playerExperience, score);
    }

    private void OnSuccessLoad(string json)
    {
        _data = JsonUtility.FromJson<SaveModel>(json);
        UpdateConfig(_data, _loadConfig);
        _isGetLoadRespondRecive = true;
    }

    private void OnErrorLoad(string message)
    {
        if (UnityEngine.PlayerPrefs.HasKey(_key))
        {
            string _hashKey = UnityEngine.PlayerPrefs.GetString(_key);
            _data = JsonUtility.FromJson<SaveModel>(_hashKey);
        }

        _isGetLoadRespondRecive = true;
    }
}