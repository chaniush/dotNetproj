using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;

using BO;
using DO;
using System.Collections;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

namespace BlImplementation;

internal class BlCart :BlApi.ICart
{

    private IDal dal = DalApi.Factory.Get();
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Cart Add(Cart? cart, int id)
    {
        Exception ex = new();
        bool flag = false;
        IEnumerable<DO.Item> items;
        lock (dal)
        {
            items = dal.Item.GetAll();

        }
        BO.ItemInOrder itemInCart=new();
        try
        {

           itemInCart = cart?.Items?.Find(Item => Item.ProductId == id)
                ?? throw new NullExeption(ex);

        }
        catch (NullExeption e)
        {

            Console.WriteLine(e);
        }
        if (itemInCart.ProductId != 0)
        {
            flag = true;
            items?.ToList().ForEach(item =>
            {
                if (item.ID == id)
                {
                    if (item.InStock >= itemInCart.Amount)
                    {

                        if (itemInCart.Amount==0)
                        {
                            itemInCart.Amount++;

                        }
                        itemInCart.TotalPrice += item.Price;
                        cart.CartTotal += item.Price;
                    }
                    else
                    {
                        itemInCart.Amount--;
                        throw new Exception("this item out of stock");
                    }
                }
            });

            
        }
   
        if (!flag)
        {
           
            var myitem = (from item in items
                        where item.ID == id
                          select item);
            if (myitem!=null)
            {
                flag = true;
                List<BO.ItemInOrder> itemInOrder = (from item in myitem
                                                    where item.InStock > 0
                                                    select new BO.ItemInOrder {
                                                        ID = item.ID,
                                                        ProductId = id,
                                                        ProductName = item.Name,
                                                        Price = item.Price,
                                                        Amount = 1,
                                                        TotalPrice = item.Price,
                                                    }).ToList();
                cart?.Items?.AddRange(itemInOrder);
            }

            
            if (!flag)
                throw new Exception("the item is not exist");
        }
        return cart??throw new NullExeption(ex);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public Cart QuantityUpdate(Cart cart, int productid, int newAmount)
    {
        NotFoundExeption ex = new();
      
        BO.ItemInOrder? myItem = (from item in cart.Items
                                 where item.ProductId == productid
                                 select item).FirstOrDefault();
        if (myItem != null)
        {
            if (newAmount == 0)
            {
                cart?.Items?.Remove(myItem);
            }
            else
            {
                cart.CartTotal += (newAmount - myItem.Amount) * myItem.Price;
                myItem.Amount = newAmount;
                myItem.TotalPrice = myItem.Price * newAmount;
            }
        }
        else
        {
            throw new BO.NotFoundException(ex);
        }
 
        return cart??throw new NullExeption(ex);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void OrderConfirmation(Cart cart)
    {
        Exception ex = new();
        int numOrder=0;
        DO.Order order=new();
     
        cart.Items?.ToList().ForEach(item =>
        {
            DO.Item itemItem;
            lock (dal)
            {
                itemItem = dal.Item.Get(item.ProductId);
            }
            if (itemItem.ID == item.ProductId && item.Amount > 0 && itemItem.InStock - item.Amount >= 0 && cart.CustomerName != "" && cart.CustomerEmail != "" && cart.CustomerAddres != "")
            {
                try
                {
                     order = new()
                    {
                        ID = item.OrderId,
                        OrderDate = DateTime.Now,
                        CustomerAddres = cart?.CustomerAddres ?? throw new NullExeption(ex),
                        CustomerEmail = cart.CustomerEmail ?? throw new NullExeption(ex),
                        CustomerName = cart.CustomerName ?? throw new NullExeption(ex),
                        DeliveryDate = DateTime.MinValue,
                        ShipDate = DateTime.MinValue,
                    };

                }
                catch (Exception e)
                {
                    Console.WriteLine(e); 
                }

               
            }
            else
            {

                if (itemItem.ID != item.ProductId)
                    throw new Exception("the item does not exist\n");
                if ( item.Amount < 0 )
                    throw new Exception("amount is too low\n");
                if ( itemItem.InStock - item.Amount < 0)
                    throw new Exception("amount of"+item.ProductName+"cannot be more than: "+itemItem.InStock+ "\n");
                if ( cart.CustomerName == ""|| cart.CustomerEmail == ""||cart.CustomerAddres == "")
                    throw new Exception("you didnt fill up all the details\n");
                else
                    throw new Exception("the item does not exist\n");
            }
        });

        lock (dal)
        {
            numOrder = dal.Order.Add(order);
          

        }
        cart.Items?.ToList().ForEach(itemCart =>
        {
            DO.ItemInOrder itemInOrder = new()
            {
                ProductId = itemCart.ProductId,
                OrderId = numOrder,
                Price = itemCart.Price,
                Amount = itemCart.Amount,
            };
            try
            {
                lock (dal)
                {
                    dal.ItemInOrder.Add(itemInOrder);
                    DO.Item item1 = dal.Item.Get(itemCart.ProductId);
                    item1.InStock -= itemCart.Amount;
                    dal.Item.Update(item1);
                }

            }
            catch (Exception)
            {
                throw;
            }
        });
    }
}
