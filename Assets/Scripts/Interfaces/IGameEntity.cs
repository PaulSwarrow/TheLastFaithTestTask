using System.Collections.Generic;

namespace DefaultNamespace.Model
{
    public interface IGameEntity
    {
        void ReceiveDamage(int amount);

        IEntityStat GetStat(StatId id);
        void ApplyEffect(IEntityEffect effect);
        bool CanPickUp { get; }
    }
}