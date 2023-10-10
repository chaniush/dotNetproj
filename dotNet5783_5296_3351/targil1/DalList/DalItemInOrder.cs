using DO;
using static Dal.DataSource;

namespace Dal;

public class DalItemInOrder : DalApi.IItemInOrder
{
    public bool DoesItemsinorderIDExsists(int id)
    {
        DO.ItemInOrder itemInOrder = DataSource.itemsInOrders.Find(itemInOrderItem => itemInOrderItem.ID == id);
        if (itemInOrder.ID != 0)
        {
            return true;
        }
      
        return false;
    }
    public  int Add(DO.ItemInOrder itemsInOrder)
    {
        itemsInOrders.Add(itemsInOrder);
        return itemsInOrder.ID;
    }
   // creating a new item in order
    public int create(DO.ItemInOrder itemsInOrder)
    {
        Add(itemsInOrder);
        return itemsInOrders.Count;
    }//creating a new item in order
    public DO.ItemInOrder Get(int id)
    {
        DO.ItemInOrder itemInOrder = DataSource.itemsInOrders.Find(itemInOrderItem => itemInOrderItem.ID == id);
        if (itemInOrder.ID != 0)
        {
            return itemInOrder;
        }
        
        else
        {
            throw new Exception("item does not exist");
        }
    }//getting items in order by id and order id
    public DO.ItemInOrder Get(Predicate<DO.ItemInOrder> function)
    {
        return itemsInOrders.Find(function);
    }
    public IEnumerable<DO.ItemInOrder>  GetAll(Func<DO.ItemInOrder, bool>? function = null)
    {
      
        DO.ItemInOrder[] t = new DO.ItemInOrder[itemsInOrders.Count];
        for (int i = 0; i < itemsInOrders.Count; i++)
        {
            t[i] = itemsInOrders[i];
        }
        return function == null ? t : t.Where(function);
    }//getting all items in orders
    public void Update(DO.ItemInOrder itemsInOrder)
    {
        DO.ItemInOrder itemInOrder = DataSource.itemsInOrders.Find(itemInOrderItem => itemInOrderItem.ID == itemsInOrder.ID);
        if (itemInOrder.ID != 0)
        {
            itemInOrder = itemsInOrder;
        }
        
    }//updating an item in order
    public void Delete(int id)
    {
        DO.ItemInOrder itemInOrder = DataSource.itemsInOrders.Find(itemInOrderItem => itemInOrderItem.ID == id);
        if (itemInOrder.ID != 0)
        {
            itemsInOrders.Remove(itemInOrder);
        }
       
    }//delete by id
    
    public IEnumerable<ItemInOrder> getByOrder(int Orderid)
    {
        List<ItemInOrder> myList = new List<ItemInOrder>();
        DataSource.itemsInOrders.ToList().ForEach(itemInOrderItem =>
        {
            if (itemInOrderItem.OrderId >= 0 && itemInOrderItem.OrderId == Orderid)
            {
                myList.Add(itemInOrderItem);
            }
        });
        
     
        return myList;
    }//read by order id
}
