using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private TypeRune _typeRune;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private int _healing = 20;
    [SerializeField] private int _coins = 50;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.PlayerStats.PlayerHealth.CurrentHealth < player.PlayerStats.PlayerHealth.MaxHealth || _typeRune != TypeRune.Healing)
            {
                TakeRune(_typeRune, player);
                Destroy(gameObject, 0.38f);
            }
            else
                return;
        }
    }

    private void TakeRune(TypeRune typeRune, Player player)
    {
        _audioSource.PlayOneShot(_audioClip);

        foreach (var particle in _particles)
            particle.Play();

        switch (typeRune)
        {
            case TypeRune.Healing:
                player.PlayerStats.PlayerHealth.TakeHealRune(_healing);
                break;
            case TypeRune.Gold:
                player.Wallet.TakeGoldenRune(_coins);
                break;
        }
    }
}
