using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIUpdate : MonoBehaviour
{
    [Header("[Sliders]")]
    [SerializeField] private Slider _sliderHP;
    [SerializeField] private Slider _sliderXP;
    [Header("[Player]")]
    [SerializeField] private Player _player;
    [Header("[Level]")]
    [SerializeField] private TMP_Text _level;
    [Header("[Text]")]
    [SerializeField] private TMP_Text _countHealthPotion;
    [SerializeField] private TMP_Text _countExperienceUpdate;
    [SerializeField] private TMP_Text _countGoldUpdate;
    [Header("[Animator]")]
    [SerializeField] private Animator _experienceUpdateAnimator;
    [SerializeField] private Animator _goldUpdateAnimator;
    [Header("[Player Stats UI]")]
    [SerializeField] private Text _playerDamage;
    [SerializeField] private Text _playerArmor;

    private const string _nameExperienceParametr = "XP";
    private const string _nameGoldParametr = "G";

    enum TransitionParametr
    {
        Take
    }

    private void OnEnable()
    {
        _player.ChangedHealth += OnChangeHealth;
    }

    private void OnDisable()
    {
        _player.ChangedHealth -= OnChangeHealth;
    }

    public void SetDefaultParameters(int maxValueSlider, int experience)
    {
        _sliderHP.maxValue = _player.PlayerMaxHealth;
        _sliderHP.value = _player.PlayerMaxHealth;
        _sliderXP.maxValue = maxValueSlider;
        _sliderXP.value = experience;
        _playerDamage.text = _player.PlayerStats.PlayerDamage.ToString();
        _playerArmor.text = _player.PlayerStats.PlayerArmor.ToString();
    }

    public void ChangeLevel(int value)
    {
        _level.text = value.ToString();
    }

    public void SetNewValueSliderExperience(int value, int difference)
    {
        _sliderXP.maxValue = value;
        _sliderXP.value = difference;
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

    private void SetTextStats(TMP_Text template, string text, string nameStats, Animator animator)
    {
        template.text = "+" + text + " " + nameStats;
        animator.SetTrigger(TransitionParametr.Take.ToString());
    }
}