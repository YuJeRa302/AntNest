[System.Serializable]
public class SaveModel
{
    public bool IsFirstSession = true;
    public bool IsSoundOn = true;
    public string Language;
    public float AmbientVolume;
    public float InterfaceVolume;
    public int PlayerLevel;
    public int PlayerCoins;
    public int PlayerScore;
    public int PlayerExperience;
    public bool[] PlayerLevelComplete;
}