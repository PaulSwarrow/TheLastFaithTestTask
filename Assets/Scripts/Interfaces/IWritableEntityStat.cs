namespace Interfaces
{
    /// <summary>
    /// Write access for the stat
    /// </summary>
    public interface IWritableEntityStat : IEntityStat
    {
        new int Value { get; set; }
    }
}