namespace DefaultNamespace.Model
{
    public interface IEntityEffect
    {
        void Apply(CharacterStats stats, StatusHandlerComponent statusHandler);
    }
}