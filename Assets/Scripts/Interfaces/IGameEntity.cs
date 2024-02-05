using System.Collections.Generic;

namespace DefaultNamespace.Model
{
    public interface IGameEntity
    {
        void ReceiveDamage(int amount);
        
        IReadOnlyDictionary<StatId, ICharacterStat> Stats { get; }
    }
}