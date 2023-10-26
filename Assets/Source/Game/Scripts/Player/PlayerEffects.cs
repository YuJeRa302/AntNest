using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    [Header("[Level Up]")]
    [SerializeField] private ParticleSystem _levelUpEffectLight;
    [SerializeField] private ParticleSystem _levelUpEffectStart;

    public ParticleSystem LevelUpEffectLight => _levelUpEffectLight;
    public ParticleSystem LevelUpEffectStart => _levelUpEffectStart;
}