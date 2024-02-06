using Components;

namespace Interfaces
{
    /// <summary>
    /// Effect that can be applied to the game entity
    /// </summary>
    public interface IEntityEffect
    {
        /// <summary>
        /// To be called within entity. Uses internal components to change the entity
        /// </summary>
        /// <param name="stats">stats access</param>
        /// <param name="statusHandler">status handler access</param>
        void Apply(CharacterStats stats, StatusHandlerComponent statusHandler);
    }
}