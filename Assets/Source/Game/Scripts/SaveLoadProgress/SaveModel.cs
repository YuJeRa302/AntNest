using System.Collections.Generic;

[System.Serializable]
public class SaveModel
{
    public string Language;
    public int PlayerLevel;
    public int PlayerCoins;
    public int PlayerExperience;
    public Dictionary<int, bool> PlayerCompleteLevels;
}