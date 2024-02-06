using UnityEngine;

namespace UI
{
    public class CharacterHud : MonoBehaviour
    {
        [SerializeField] private CharacterEntity _target;

        public CharacterEntity Target => _target;

    }
}