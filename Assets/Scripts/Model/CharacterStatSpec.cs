using UnityEngine;

namespace DefaultNamespace.Model
{
    [CreateAssetMenu(menuName = "Game/StatSpec")]
    public class CharacterStatSpec: ScriptableObject
    {
        public StatId Id;
        public int DefaultValue;
        public StatId MaxValueStat;
    }
}