using DalApi;
using DO;
using System.Security.Cryptography;
using static Dal.DataSource;

namespace Dal;

public class DalOrder : DalApi.IOrder
{
    public bool DoesOrderIDExsists(int id)
    {
       
        DO.Order  order= DataSource.orders.Find(orderItem => orderItem.ID == id);
        if (order.ID != 0)
        {
            return true;
        }
        return false;
    }
    public int Add(DO.Order order)
    {
        orders.Add(order);
        return order.ID;
    }
    public int create(DO.Order order)
    {
        Add(order);
        return orders.Count;
    }
    //tyututfsg
    public DO.Order Get(int id)
    {
        DO.Order order = DataSource.orders.Find(orderItem => orderItem.ID == id);
        if (order.ID != 0)
        {
            return order;
        }
      
        else
        {
            throw new Exception("Order does not exist");
        }
    }
    public IEnumerable<DO.Order>  GetAll(Func<DO.Order, bool>? function=null)
    {
       
        DO.Order[] t = new DO.Order[orders.Count];
        for (int i = 0; i < orders.Count; i++)
        {
            t[i] = orders[i];
        }
        return function == null ? t : t.Where(function);
    }
    public DO.Order Get(Predicate<DO.Order> function)
    {
        try
        {
            return orders.Find(function);
        }
        catch (Exception)
        {

            throw;
        }
        
    }
    public void Update(DO.Order order)
    {
      

        for (int i = 0; i < DataSource.orders.Count; i++)
        {
            if (DataSource.orders[i].ID == order.ID)
            {
                DataSource.orders[i] = order;
                
            }
        }

    }
    public void Delete(int id)
    {
        DO.Order myOrder = DataSource.orders.Find(orderItem => orderItem.ID == id);
        if (myOrder.ID != 0)
        {
            throw new Exception("item is not found");
        }
        else
        {
            DataSource.orders.Remove(myOrder);
        }
       
    }
}

