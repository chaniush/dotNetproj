namespace DalApi;

public interface IDal
{
    public IItem Item { get; }
    public IOrder Order { get; }
    public IItemInOrder ItemInOrder { get; }
}
