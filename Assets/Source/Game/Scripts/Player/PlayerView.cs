using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    [Header("[Sliders]")]
    [SerializeField] private Slider _sliderHP;
    [SerializeField] private Slider _sliderXP;
    [Header("[Player]")]
    [SerializeField] private Player _player;
    [Header("[Level]")]
    [SerializeField] private Text _level;
    [Header("[Text]")]
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

    private void Awake()
    {
        _player.PlayerStats.PlayerHealth.ChangedHealth += OnChangeHealth;
        _player.PlayerStats.GoldValueChanged += OnChangeGold;
        _player.PlayerStats.ExperienceValueChanged += OnChangeExperience;
    }

    private void OnDestroy()
    {
        _player.PlayerStats.PlayerHealth.ChangedHealth -= OnChangeHealth;
        _player.PlayerStats.GoldValueChanged -= OnChangeGold;
        _player.PlayerStats.ExperienceValueChanged -= OnChangeExperience;
    }

    public void SetNewLevelValue(int value)
    {
        _level.text = value.ToString();
    }

    public void SetExperienceSliderValue(int value, int difference)
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
    }

    public void UpdatePlayerStats()
    {
        _playerDamage.text = _player.PlayerStats.Damage.ToString();
        _playerArmor.text = _player.PlayerStats.Armor.ToString();
    }

    private void OnChangeExperience(int target)
    {
        SetTextStats(_countExperienceUpdate, target.ToString(), _experienceUpdateAnimator);
        _sliderXP.value += target;
    }

    private void OnChangeGold(int target)
    {
        SetTextStats(_countGoldUpdate, target.ToString(), _goldUpdateAnimator);
    }

    private void OnChangeHealth(int target)
    {
        _sliderHP.value = target;
    }

    private void SetTextStats(Text template, string text, Animator animator)
    {
        animator.SetTrigger(TransitionParametr.Take.ToString());
        template.text = "+" + text;
    }
}