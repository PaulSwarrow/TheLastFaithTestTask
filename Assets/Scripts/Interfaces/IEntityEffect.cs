using Components;

namespace Interfaces
{
    public interface IEntityEffect
    {
        void Apply(CharacterStats stats, StatusHandlerComponent statusHandler);
    }
}