namespace Interfaces
{
    public interface IWritableEntityStat : IEntityStat
    {
        new int Value { get; set; }
    }
}