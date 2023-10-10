using System;
using DO;
using DalApi;
namespace DalApi;
public interface IItemInOrder : ICrud<ItemInOrder> {
    //public ItemInOrder getByOrderAndProduct(int Orderid, int Productid);
    public IEnumerable<ItemInOrder> getByOrder(int Orderid);

}

