using UnityEngine;

public class Rune : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] Particles;
    [SerializeField] private Runes rune;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;
    [SerializeField] private int _healing = 20;
    [SerializeField] private int _coins = 50;

    enum Runes
    {
        Healing = 1,
        Gold
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.PlayerStats.PlayerHealth.CurrentHealth < player.PlayerStats.PlayerHealth.MaxHealth || rune != Runes.Healing)
            {
                TakeRune(rune, player);
                Destroy(gameObject, 0.38f);
            }
            else return;
        }
    }

    private void TakeRune(Runes rune, Player player)
    {
        _audioSource.PlayOneShot(_audioClip);

        foreach (var particle in Particles)
        {
            particle.Play();
        }

        switch (rune)
        {
            case Runes.Healing:
                player.PlayerStats.PlayerHealth.TakeHealRune(_healing);
                break;
            case Runes.Gold:
                player.Wallet.TakeCoins(_coins);
                break;
        }
    }
}
