using UnityEngine;
using UnityEngine.UI;

public class WavePanelView : MonoBehaviour
{
    [Header("[Name Wave]")]
    [SerializeField] private Text _nameWave;
    [SerializeField] private Text _translateText;
    [Header("[Animator]")]
    [SerializeField] private Animator _animator;

    enum TransitionParametr
    {
        Take
    }

    public void SetWaveName(int numberWave)
    {
        SetText(_nameWave, _translateText, numberWave, _animator);
    }

    private void SetText(Text waveText, Text translateText, int numberWave, Animator animator)
    {
        waveText.text = translateText.text + " " + ++numberWave;
        animator.SetTrigger(TransitionParametr.Take.ToString());
    }
}