using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Create Enemy", order = 51)]
    public class EnemyData : ScriptableObject
    {
        [Header("[Enemy Stats]")]
        [SerializeField] private int _level;
        [SerializeField] private int _damage;
        [SerializeField] private int _health;
        [SerializeField] private int _goldReward;
        [SerializeField] private int _experienceReward;
        [SerializeField] private int _score;
        [SerializeField] private string _name;
        [Header("[Ability Stats]")]
        [SerializeField] private float _abilityCoolDown;
        [SerializeField] private int _abilityDamage;
        [Header("[Sprites]")]
        [SerializeField] private Sprite _cancelAbilitySprite;
        [SerializeField] private Sprite _abilitySprite;
        [SerializeField] private Sprite _enemyIcon;
        [Header("[ParticleSystem]")]
        [SerializeField] private ParticleSystem _enemyHitParticleSystem;
        [SerializeField] private ParticleSystem _enemyDieParticleSystem;
        [SerializeField] private ParticleSystem _enemyAbilityParticleSystem;
        [Header("[Enemy Sound]")]
        [SerializeField] private AudioClip _audioClipDie;
        [SerializeField] private AudioClip _hitPlayer;
        [Header("[Enemy]")]
        [SerializeField] private Enemy _prefabEnemy;

        public int Level => _level;
        public int Damage => _damage;
        public int Health => _health;
        public int GoldReward => _goldReward;
        public int ExperienceReward => _experienceReward;
        public int Score => _score;
        public string Name => _name;
        public float AbilityCoolDown => _abilityCoolDown;
        public int AbilityDamage => _abilityDamage;
        public Sprite CancelAbilitySprite => _cancelAbilitySprite;
        public Sprite AbilitySprite => _abilitySprite;
        public Sprite EnemyIcon => _enemyIcon;
        public ParticleSystem EnemyHitParticleSystem => _enemyHitParticleSystem;
        public ParticleSystem EnemyDieParticleSystem => _enemyDieParticleSystem;
        public ParticleSystem EnemyAbilityParticleSystem => _enemyAbilityParticleSystem;
        public AudioClip AudioClipDie => _audioClipDie;
        public AudioClip HitPlayer => _hitPlayer;
        public Enemy PrefabEnemy => _prefabEnemy;
    }
}