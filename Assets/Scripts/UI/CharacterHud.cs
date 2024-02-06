using UnityEngine;

namespace UI
{
    /// <summary>
    /// Provides link for the character for all child widgets
    /// </summary>
    public class CharacterHud : MonoBehaviour
    {
        [SerializeField] private CharacterEntity _target;

        public CharacterEntity Target => _target;
    }
}