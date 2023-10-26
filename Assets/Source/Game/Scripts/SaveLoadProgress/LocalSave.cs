using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LocalSave : MonoBehaviour
{
    [Header("[LoadConfig]")]
    [SerializeField] private LoadConfig _loadConfig;

    private readonly string _fileName = "PlayerProgress.dat";

    private string _savePath;

    private void Awake()
    {
        _savePath = $"{Application.persistentDataPath}/Saves/";
    }

    public void SavePlayerProgress(string language, int playerCoins, int playerLevel, int playerExperience, int levelId, bool levelState)
    {
        var dir = new DirectoryInfo(_savePath);

        if (!dir.Exists) dir.Create();

        BinaryFormatter bf = new();
        SaveModel data = null;
        FileStream file;

        if (File.Exists(_savePath + _fileName))
        {
            file = File.Open(_savePath + _fileName, FileMode.Open);
            data = file.Length > 0 ? (SaveModel)bf.Deserialize(file) : null;
            file.Close();
            file = File.Open(_savePath + _fileName, FileMode.Truncate);
        }
        else file = File.Create(_savePath + _fileName);

        SaveModel newData = new()
        {
            Language = language,
            PlayerCoins = playerCoins,
            PlayerLevel = playerLevel,
            PlayerExperience = playerExperience,
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

        bf.Serialize(file, newData);
        file.Close();
    }

    public void LoadPlayerProgress()
    {
        if (File.Exists(_savePath + _fileName))
        {
            BinaryFormatter bf = new();
            FileStream file = File.Open(_savePath + _fileName, FileMode.Open);
            SaveModel data = file.Length > 0 ? (SaveModel)bf.Deserialize(file) : null;
            _loadConfig.UpdateListPlayerLevels(data.PlayerCompleteLevels);
            SetConfigParameters(data.Language, data.PlayerCoins, data.PlayerLevel, data.PlayerExperience);
            file.Close();
        }
        else return;
    }

    private void SetConfigParameters(string language, int playerCoins, int playerLevel, int playerExperience)
    {
        _loadConfig.SetCurrentLanguage(language);
        _loadConfig.SetPlayerParameters(playerCoins, playerLevel, playerExperience);
    }
}
