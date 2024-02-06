namespace DefaultNamespace.Model
{
    public interface IEntityStatus : IEntityStatusInfo
    {
        void Init(CharacterStats stats);

        void Update(float deltaTime);
        void Dispose();
        
        bool IsFinished { get; }

        bool HasConflict(IEntityStatus other);
    }
}