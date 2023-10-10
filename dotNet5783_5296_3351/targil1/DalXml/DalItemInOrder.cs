using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;


internal class DalItemInOrder : IItemInOrder
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public  int Add(DO.ItemInOrder itemsInOrder)
    {
        XElement? ItemInOrders = XDocument.Load("../ItemInOrder.xml").Root;
        XElement? config = XDocument.Load("../config.xml").Root;
        XElement? ItemInOrderId = config?.Element("ItemInOrderId");
        int Id = Convert.ToInt32(ItemInOrderId.Value);
        itemsInOrder.ID = Id++;
        ItemInOrderId.Value = Id.ToString();
        config.Save("../config.xml");
        var list = ItemInOrders?.Elements().ToList().Where(ItemInOrder => ItemInOrder?.Element("id")?.Value.ToString() == itemsInOrder.ID.ToString());
        if (list?.Count() > 0)
        {
            throw new Exception("item exists!!");
        }
        else
        {
            XElement? ItemInOrder = new XElement("itemInOrder");
            XElement? id = new XElement("id", itemsInOrder.ID);
            ItemInOrder.Add(id);
            XElement? OrderId = new XElement("OrderId", itemsInOrder.OrderId);
            ItemInOrder.Add(OrderId);
            XElement? ProductId = new XElement("ProductId", itemsInOrder.ProductId);
            ItemInOrder.Add(ProductId);
            XElement? price = new XElement("price", itemsInOrder.Price);
            ItemInOrder.Add(price);
            XElement? Amount = new XElement("Amount", itemsInOrder.Amount);
            ItemInOrder.Add(Amount);
            ItemInOrders?.Add(ItemInOrder);
            ItemInOrders?.Save("../ItemInOrder.xml");
            return itemsInOrder.ID;
        }
    }
    // creating a new item in order
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.ItemInOrder Get(int id)
    {
        XElement? ItemInOrders = XDocument.Load("../ItemInOrder.xml").Root;
        var found = ItemInOrders?.Elements().ToList().Find(ItemInOrder => Convert.ToInt32(ItemInOrder?.Element("id")?.Value) ==id);
        if (found == null)
            throw new ExistingIDException();
         return new DO.ItemInOrder
        {
            ID = Convert.ToInt32(found.Element("id")?.Value),
             OrderId = Convert.ToInt32(found.Element("OrderId")?.Value.ToString()) ,
             ProductId = Convert.ToInt32(found.Element("ProductId")?.Value),
            Price = Convert.ToInt32(found.Element("price")?.Value),
             Amount = Convert.ToInt32(found.Element("Amount")?.Value.ToString()),
         };
    }//getting items in order by id and order id
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.ItemInOrder Get(Predicate<DO.ItemInOrder> function)
    {
        XElement? ItemInOrders = XDocument.Load("../ItemInOrder.xml").Root;
        List<DO.ItemInOrder> lst = new List<DO.ItemInOrder> { };
        ItemInOrders?.Elements().ToList().ForEach(e =>
        {
            lst.Add(new DO.ItemInOrder
            {
                ID = Convert.ToInt32(e.Element("id")?.Value),
                OrderId = Convert.ToInt32(e.Element("OrderId")?.Value.ToString()),
                ProductId = Convert.ToInt32(e.Element("ProductId")?.Value),
                Price = Convert.ToInt32(e.Element("price")?.Value),
                Amount = Convert.ToInt32(e.Element("Amount")?.Value.ToString()),
            });
        });
        Func<DO.ItemInOrder, bool> func = new Func<DO.ItemInOrder, bool>(function);
        return lst.Where(func).FirstOrDefault();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.ItemInOrder>  GetAll(Func<DO.ItemInOrder, bool>? function = null)
    {
        XElement? ItemInOrders = XDocument.Load("../ItemInOrder.xml").Root;
        List<DO.ItemInOrder> lst = new List<DO.ItemInOrder> { };
        ItemInOrders?.Elements().ToList().ForEach(e =>
        {
             lst.Add(new DO.ItemInOrder
             {
                 ID = Convert.ToInt32(e.Element("id")?.Value),
                 OrderId = Convert.ToInt32(e.Element("OrderId")?.Value.ToString()),
                 ProductId = Convert.ToInt32(e.Element("ProductId")?.Value),
                 Price = Convert.ToInt32(e.Element("price")?.Value),
                 Amount = Convert.ToInt32(e.Element("Amount")?.Value.ToString()),
             });
        });
        return function == null ? lst : lst.Where(function);
    }//getting all items in orders
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.ItemInOrder itemsInOrder)
    {
        Delete(itemsInOrder.ID);
        Add(itemsInOrder);
    }//updating an item in order
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        XElement? ItemInOrders = XDocument.Load("../ItemInOrder.xml").Root;
        ItemInOrders?.Elements().ToList().Find(ItemInOrder => Convert.ToInt32(ItemInOrder?.Element("id")?.Value) == id)?.Remove();
        ItemInOrders.Save("../ItemInOrder.xml");
        XElement? config = XDocument.Load("../config.xml").Root;
        XElement? ItemInOrderId = config?.Element("ItemInOrderId");
        int Id = Convert.ToInt32(ItemInOrderId.Value);
        Id= Id--;
        ItemInOrderId.Value = Id.ToString();
        config.Save("../config.xml");
    }//delete by id
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.ItemInOrder> getByOrder(int Orderid)
    {
        XElement? ItemInOrders = XDocument.Load("../ItemInOrder.xml").Root;
        List<DO.ItemInOrder> myList = new List<DO.ItemInOrder>();

        ItemInOrders?.Elements().ToList().ForEach(ItemInOrder =>
           {
               if (Convert.ToInt32(ItemInOrder?.Element("OrderId")?.Value) == Orderid)
               {
                   myList.Add(
                     new DO.ItemInOrder
                     {
                         ID = Convert.ToInt32(ItemInOrder?.Element("id")?.Value),
                         OrderId = Convert.ToInt32(ItemInOrder?.Element("OrderId")?.Value.ToString()),
                         ProductId = Convert.ToInt32(ItemInOrder?.Element("ProductId")?.Value),
                         Price = Convert.ToInt32(ItemInOrder?.Element("price")?.Value),
                         Amount = Convert.ToInt32(ItemInOrder?.Element("Amount")?.Value.ToString()),
                     }
                   );
               }
           });
        return myList;
    }//read by order id
}
