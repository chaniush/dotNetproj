using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

using DalApi;
using DO;
using Category = BO.Category;
using Color = BO.Color;
using Item = BO.Item;

namespace BlImplementation;

internal class BlItem : BlApi.IItem
{
    public IDal dal = DalApi.Factory.Get();
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<ItemForList> ProductListRequest(Func<DO.Item, bool>? function = null)
    {
        IEnumerable<ItemForList> itemList= new List<ItemForList>();

        IEnumerable<DO.Item> item;
        lock (dal)
        {
            item= dal.Item.GetAll(function);
        }
       // IEnumerable<BO.ItemForList> 
            itemList = (from product in item
                      select new BO.ItemForList
                      {
                          ID = product.ID,
                          Name = product.Name,
                          Price = product.Price,
                          Color = (BO.Color)product.Color,
                          Category = (BO.Category)product.Category,
                      });
       // ((List<ItemForList>)itemList).AddRange(myitem);
        //itemList = new List<ItemForList>();

        //foreach (DO.Item product in item)
        //{
        //    BO.ItemForList itemInList = new()
        //    {
        //        ID = product.ID,
        //        Name = product.Name,
        //        Price = product.Price,
        //        Color = (BO.Color)product.Color,
        //        Category = (BO.Category)product.Category,
        //    };

        //    ((List<ItemForList>)itemList).Add(itemInList);
        //}
        return itemList;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Item ProductDetailsRequestM(int id)
    {
        DO.Item itemd = new DO.Item();
        BO.Item itemb = new BO.Item();
        if (id > 0)
        {
            lock (dal)
            {
                itemd = dal.Item.Get(id);

            }
            itemb.ID = id;
            itemb.Name = itemd.Name;
            itemb.Price = itemd.Price;
            itemb.Category = (Category)itemd.Category;
            itemb.InStock = itemd.InStock;
            itemb.Color = (Color)itemd.Color;
        }
        else
        {
            throw new Exception("item does not exist");
        }
        return itemb;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public List<ProductItem> ProductDetailsRequestC()
    {
        IEnumerable<DO.Item> listOfProducts;
        lock (dal)
        {
            listOfProducts= dal.Item.GetAll();
        }
        List<ProductItem> productItem = new List<ProductItem> { };
        IEnumerable<BO.ProductItem> productItemAdd=(from product in listOfProducts select new BO.ProductItem
        {
            ID = product.ID,
            Price = product.Price,
            Name = product.Name,
            Category = (BO.Category)product.Category,
            InStock = product.InStock > 0 ? 1 : 0,
            AmountInCart = 0
        });
        //foreach (var product in listOfProducts)
        //{
        //    BO.ProductItem productItemAdd = new BO.ProductItem
        //    {
        //        ID = product.ID,
        //        Price = product.Price,
        //        Name = product.Name,
        //        Category = (BO.Category)product.Category,
        //        InStock = product.InStock > 0 ? 1 : 0,
        //        AmountInCart = 0
        //    };
            productItem.AddRange(productItemAdd);
        //}
        return productItem;

    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(Item item)
    {
        NotFoundExeption ex = new NotFoundExeption();

        DO.Item itemdo = new DO.Item();
        if (item.ID > 0 && item.Name != "" && item.Price > 0 && item.InStock >= 0)
        {
            itemdo.ID = item.ID;
            try
            {
                itemdo.Name = item?.Name ?? throw new NullExeption(ex); 
                itemdo.Price = item.Price;
                itemdo.Category = (DO.Category)item.Category;
                itemdo.InStock = item.InStock;
                itemdo.Color = (DO.Color)item.Color;
                try
                {
                    lock (dal)
                    {
                        dal.Item.Add(itemdo);
                    }
                   
                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        else
        {
            throw new Exception("add item is failed");
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        IEnumerable<DO.Order> orderList;
        lock (dal)
        {
            orderList= dal.Order.GetAll();
        }
        bool flag = false;
        var delOrder=(from order in orderList where order.ID==id select order)
            ?? throw new Exception("the item is exist in order");
        if (delOrder!=null)
        {
            flag = true;

        }
        //foreach (DO.Order order in orderList)
        //{
        //    if (order.ID == id)
        //    {
        //        flag = true;
        //        throw new Exception("the item is exist in order");
                
        //    }
        //}
        if (!flag)
        {
            try
            {
                lock (dal)
                {
                    dal.Item.Delete(id);
                }
               
            }
            catch (Exception )
            {

                throw ;
            }
        }
        
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Item item)
    {
        NotFoundExeption ex = new NotFoundExeption();
        DO.Item itemdo = new DO.Item();
        if (item.ID > 0 && item.Name != "" && item.Price > 0 && item.InStock >= 0)
        {
            itemdo.ID = item.ID;
            try
            {
                itemdo.Name = item.Name ?? throw new NullExeption(ex);
                itemdo.Price = item.Price;
                itemdo.Category = (DO.Category)item.Category;
                itemdo.InStock = item.InStock;
                itemdo.Color = (DO.Color)item.Color;
                try
                {
                    lock (dal)
                    {
                        dal.Item.Update(itemdo);
                    }
                }
                catch (Exception)
                {

                    throw ;
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        else
        {
            throw new Exception("add item is failed");
        }
    }
    
}