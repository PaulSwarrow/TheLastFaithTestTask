namespace Interfaces
{
    /// <summary>
    /// Public access for entity's status
    /// </summary>
    public interface IEntityStatusInfo
    {
        string Label { get; }
        float TimeLeft { get; }
    }
}