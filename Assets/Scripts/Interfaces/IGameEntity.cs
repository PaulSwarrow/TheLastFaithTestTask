using Model;

namespace Interfaces
{
    /// <summary>
    /// Common facade for any game entities
    /// </summary>
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