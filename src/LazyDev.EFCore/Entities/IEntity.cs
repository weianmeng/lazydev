namespace LazyDev.EFCore.Entities
{
    public interface IEntity<T>
    {
        T Id { get; set; }
    }
}