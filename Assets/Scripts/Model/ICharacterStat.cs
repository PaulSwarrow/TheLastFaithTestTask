namespace DefaultNamespace.Model
{
    public interface ICharacterStat
    {
        string Label { get; }
        int CurrentValue { get; }
        int MaxValue { get; }
    }
}