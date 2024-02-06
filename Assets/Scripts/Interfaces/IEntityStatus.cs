namespace DefaultNamespace.Model
{
    public interface IEntityStatus
    {
        void Init(CharacterStats stats);

        void Update(float deltaTime);
        void Dispose();
        
        bool IsFinished { get; }
    }
}