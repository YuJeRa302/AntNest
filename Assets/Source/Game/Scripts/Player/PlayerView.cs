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
    [SerializeField] private Text _countHealthPotion;
    [SerializeField] private Text _countExperienceUpdate;
    [SerializeField] private Text _countGoldUpdate;
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
        _sliderHP.maxValue = _player.PlayerMaxHealth;
        _sliderHP.value = _player.PlayerMaxHealth;
        _sliderXP.maxValue = maxValueSlider;
        _sliderXP.value = experience;
        UpdatePlayerStats(_player.PlayerStats.PlayerArmor, _player.PlayerStats.PlayerDamage);
    }

    public void UpdatePlayerStats(int armor, int damage)
    {
        _playerArmor.text = armor.ToString();
        _playerDamage.text = damage.ToString();
    }

    private void SetTextStats(Text template, string text, string nameStats, Animator animator)
    {
        template.text = "+" + text + " " + nameStats;
        animator.SetTrigger(TransitionParametr.Take.ToString());
    }
}