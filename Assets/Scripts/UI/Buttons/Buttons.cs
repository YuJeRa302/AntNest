using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Buttons : MonoBehaviour
{
    [Header("[Name Level]")]
    [SerializeField] private TMP_Text _nameLevel;
    [Header("[Image]")]
    [SerializeField] private Image _image;
    [Header("[Sprite]")]
    [SerializeField] private Sprite _acceptSprite;
    [SerializeField] private Sprite _cancelSprite;
    [Header("[Level Prefab]")]
    [SerializeField] private Levels _loadLevel;
    [Header("[Button]")]
    [SerializeField] private Button _button;
    [Header("[Config]")]
    [SerializeField] private LoadConfig _loadConfig;

    public bool IsLevelComplete => _loadLevel.IsComplete;

    public void SetImage()
    {
        if (_loadLevel.IsComplete == true)
        {
            _image.sprite = _acceptSprite;
        }
        else
        {
            _image.sprite = _cancelSprite;
        }
    }

    public void UnlockButton()
    {
        _button.interactable = true;
    }

    public void GetConfig(LoadConfig config)
    {
        _loadConfig = config;
    }

    public void LoadLevel()
    {
        _loadConfig.SetLevelParameters(_loadLevel);
        SceneManager.LoadScene(_loadLevel.NameScene);
    }

    private void Start()
    {
        _nameLevel.text = _loadLevel.NameLocation;
    }
}