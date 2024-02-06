namespace DefaultNamespace.Model
{
    public interface IWritableEntityStat : IEntityStat
    {
        new int Value { get; set; }
    }
}