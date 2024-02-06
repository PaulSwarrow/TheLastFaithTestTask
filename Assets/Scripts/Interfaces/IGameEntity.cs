using System.Collections.Generic;

namespace DefaultNamespace.Model
{
    public interface IGameEntity
    {
        void ReceiveDamage(int amount, IGameEntity from);

        IEntityStat GetStat(StatId id);
        void ApplyEffect(IEntityEffect effect);
        bool CanPickUp { get; }
        
        int Team { get; }
        
        int KillReward { get; }
        void ClaimKill(IGameEntity target);
    }
}