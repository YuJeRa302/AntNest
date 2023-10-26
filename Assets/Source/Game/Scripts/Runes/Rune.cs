using UnityEngine;

public class Rune : MonoBehaviour
{
    [Header("[Containers]")]
    [SerializeField] private ParticleSystem[] Particles;
    [SerializeField] private Runes rune;
    [Header("[Audio Source]")]
    [SerializeField] private AudioSource _audioSource;
    [Header("[Audio Clip]")]
    [SerializeField] private AudioClip _audioClip;

    private readonly int _coins = 50;
    private readonly int _healing = 20;

    enum Runes
    {
        Healing = 1,
        Gold
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            if (player.PlayerCurrentHealth < player.PlayerMaxHealth || rune != Runes.Healing)
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
                player.GetComponent<Player>().TakeHealRune(_healing);
                break;
            case Runes.Gold:
                player.GetComponent<Player>().Wallet.TakeCoin(_coins);
                break;
        }
    }
}
