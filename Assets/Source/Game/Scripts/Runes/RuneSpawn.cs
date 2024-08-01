using UnityEngine;

namespace Assets.Source.Game.Scripts
{
    public class RuneSpawn : MonoBehaviour
    {
        private readonly System.Random rnd = new ();
        private readonly Vector3 _fixRotation = new (-90, 0, 0);

        [SerializeField] private Transform[] _position;
        [SerializeField] private Rune[] _listRunes;

        public void Initialize()
        {
            var childrenTransform = gameObject.GetComponentInChildren<Transform>();
            _position = new Transform[childrenTransform.childCount];

            for (int i = 0; i < childrenTransform.childCount; i++)
            {
                _position[i] = childrenTransform.GetChild(i);
            }

            Spawn();
        }

        private void Spawn()
        {
            foreach (Transform positinon in _position)
                CreateRunes(positinon, rnd.Next(_listRunes.Length));
        }

        private void CreateRunes(Transform runePosition, int value)
        {
            Instantiate(_listRunes[value], new Vector3(runePosition.localPosition.x, runePosition.localPosition.y, runePosition.localPosition.z), Quaternion.Euler(_fixRotation));
        }
    }
}