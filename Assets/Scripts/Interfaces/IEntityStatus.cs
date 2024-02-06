using Components;

namespace Interfaces
{
    /// <summary>
    /// Common interface for any status on the entity
    /// </summary>
    public interface IEntityStatus : IEntityStatusInfo
    {
        void Init(CharacterStats stats);

        void Update(float deltaTime);
        void Dispose();
        
        bool IsFinished { get; }

        /// <summary>
        /// Check if status conflicts with another. Used to remove similar ones
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        bool HasConflict(IEntityStatus other);
    }
}