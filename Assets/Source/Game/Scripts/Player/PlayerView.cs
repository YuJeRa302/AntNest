using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    private readonly string _nameExperienceParametr = "XP";
    private readonly string _nameGoldParametr = "G";

    [Header("[Sliders]")]
    [SerializeField] private Slider _sliderHP;
    [SerializeField] private Slider _sliderXP;
    [Header("[Player]")]
    [SerializeField] private Player _player;
    [Header("[Level]")]
    [SerializeField] private Text _level;
    [Header("[Text]")]
    [SerializeField] private Text _countHealthPotion;
    [SerializeField] private Text _countExperienceUpdate;
    [SerializeField] private Text _countGoldUpdate;
    [Header("[Animator]")]
    [SerializeField] private Animator _experienceUpdateAnimator;
    [SerializeField] private Animator _goldUpdateAnimator;
    [Header("[Player Stats UI]")]
    [SerializeField] private Text _playerDamage;
    [SerializeField] private Text _playerArmor;

    enum TransitionParametr
    {
        Take
    }

    public void ChangeLevel(int value)
    {
        _level.text = value.ToString();
    }

    public void ChangeCountPotion(int value)
    {
        _countHealthPotion.text = value.ToString();
    }

    public void OnChangeHealth(int target)
    {
        _sliderHP.value = target;
    }

    public void OnChangeExperience(int target)
    {
        SetTextStats(_countExperienceUpdate, target.ToString(), _nameExperienceParametr, _experienceUpdateAnimator);
        _sliderXP.value += target;
    }

    public void OnChangeGold(int target)
    {
        SetTextStats(_countGoldUpdate, target.ToString(), _nameGoldParametr, _goldUpdateAnimator);
    }

    public void SetNewValueSliderExperience(int value, int difference)
    {
        _sliderXP.maxValue = value;
        _sliderXP.value = difference;
    }

    public void SetDefaultParameters(int maxValueSlider, int experience)
    {
        _sliderHP.maxValue = _player.PlayerStats.PlayerHealth.MaxHealth;
        _sliderHP.value = _player.PlayerStats.PlayerHealth.MaxHealth;
        _sliderXP.maxValue = maxValueSlider;
        _sliderXP.value = experience;
        UpdatePlayerStats();
    }

    public void UpdatePlayerStats()
    {
        _playerArmor.text = _player.PlayerStats.Armor.ToString();
        _playerDamage.text = _player.PlayerStats.Daamge.ToString();
    }

    private void OnEnable()
    {
        _player.PlayerStats.PlayerHealth.ChangedHealth += OnChangeHealth;
    }

    private void OnDisable()
    {
        _player.PlayerStats.PlayerHealth.ChangedHealth -= OnChangeHealth;
    }

    private void SetTextStats(Text template, string text, string nameStats, Animator animator)
    {
        template.text = "+" + text + " " + nameStats;
        animator.SetTrigger(TransitionParametr.Take.ToString());
    }
}