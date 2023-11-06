using System.Collections.Generic;

[System.Serializable]
public class SaveModel
{
    public bool IsFirstSession = true;
    public string Language;
    public int PlayerLevel;
    public int PlayerCoins;
    public int PlayerScore;
    public int PlayerExperience;
    public Dictionary<int, bool> PlayerCompleteLevels;
}