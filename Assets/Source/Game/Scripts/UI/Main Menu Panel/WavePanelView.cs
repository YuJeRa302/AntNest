using UnityEngine;
using UnityEngine.UI;

public class WavePanelView : MonoBehaviour
{
    [Header("[Name Wave]")]
    [SerializeField] private Text _numberWave;
    [Header("[Animator]")]
    [SerializeField] private Animator _animatorTextWave;
    [SerializeField] private Animator _animatorNumberWave;

    enum TransitionParametr
    {
        Take
    }

    public void SetWaveName(int numberWave)
    {
        SetText(numberWave);
    }

    public void PlayAnimation()
    {
        _animatorTextWave.SetTrigger(TransitionParametr.Take.ToString());
        _animatorNumberWave.SetTrigger(TransitionParametr.Take.ToString());
    }

    private void SetText(int numberWave)
    {
        var wave = ++numberWave;
        _numberWave.text = wave.ToString();
        PlayAnimation();
    }
}