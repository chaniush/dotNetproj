using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using DalApi;
using DO;

namespace Dal;

internal class DalOrder : IOrder
{
    public static string state = "Add";
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(DO.Order order)
    {
        XElement? myOrders = XDocument.Load("../Order.xml").Root;
        
        //OrderId.SetValue(31);
        var list = myOrders?.Elements().ToList().Where(myOrder => myOrder?.Element("ID")?.Value.ToString() == order.ID.ToString());
        if (list?.Count() > 0)
        {
            throw new Exception("item exists!!");
        }
        else
         {
            if (state == "Add")
            {
                XElement? config = XDocument.Load("../config.xml").Root;
                XElement? OrderId = config?.Element("OrderId");
                int Id = Convert.ToInt32(OrderId.Value);
                order.ID = Id++;
                OrderId.Value = Id.ToString();
                config.Save("../config.xml");
            }
            XElement? myOrder = new XElement("order");
            XElement? ID = new XElement("ID", order.ID);
            myOrder.Add(ID);
            XElement? CustomerName = new XElement("CustomerName", order.CustomerName);
            myOrder.Add(CustomerName);
            XElement? CustomerEmail = new XElement("CustomerEmail", order.CustomerEmail);
            myOrder.Add(CustomerEmail);
            XElement? CustomerAddres = new XElement("CustomerAddres", order.CustomerAddres);
            myOrder.Add(CustomerAddres);
            XElement? OrderDate = new XElement("OrderDate", order.OrderDate);
            myOrder.Add(OrderDate);
            XElement? ShipDate = new XElement("ShipDate", order.ShipDate);
            myOrder.Add(ShipDate);
            XElement? DeliveryDate = new XElement("DeliveryDate", order.DeliveryDate);
            myOrder.Add(DeliveryDate);
            myOrders?.Add(myOrder);
            myOrders?.Save("../Order.xml");
            return order.ID;
        }
       
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order Get(int id)
    {
        DateTime order, ship, delivery;

        XElement? myOrders = XDocument.Load("../Order.xml").Root;
        var found = myOrders?.Elements().ToList().Find(myOrder => Convert.ToInt32(myOrder?.Element("ID")?.Value) == id);
        if (found == null)
            throw new ExistingIDException();
        DateTime.TryParse(found.Element("OrderDate")?.Value, out order);
        DateTime.TryParse(found.Element("ShipDate")?.Value, out ship);
        DateTime.TryParse(found.Element("DeliveryDate")?.Value, out delivery);
        return new DO.Order
        {
            ID = Convert.ToInt32(found?.Element("ID")?.Value),
            CustomerName = found?.Element("CustomerName")?.Value.ToString() ?? throw new Exception("name exeption"),
            CustomerEmail = found?.Element("CustomerEmail")?.Value ?? throw new Exception("CustomerEmail exeption"),
            CustomerAddres = found?.Element("CustomerAddres")?.Value ?? throw new Exception("CustomerAddres exeption"),
            OrderDate = order,
            ShipDate = ship,
            DeliveryDate = delivery,
        };
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<DO.Order>  GetAll(Func<DO.Order, bool>? function=null)
    {
        //Random nums = new Random();
        //List<DO.Order> orders = new List<DO.Order>();


        //(string, string, string)[] ord = new[] {
        //       ("chani", "chani@gmail.com", "jerusalem"),
        //       ("dani", "dani@gmail.com", "haifa"),
        //       ("riki", "riki@gmail.com", "afula"),
        //       ("levi", "levi@gmail.com", "gush-etzyun"),
        //       ("tzipi", "tzipi@gmail.com", "bnei-brack"),
        //       ("noa", "noa@gmail.com", "neve_yaakob"),
        //       ("avi", "avi@gmail.com", "givaat_shaul"),
        //       ("shuki", "shuki@gmail.com", "kiryat_moshe"),
        //       ("miri", "miri@gmail.com","beitar"),
        //       ("eli", "eli@gmail.com", "raanana")
        //   };
        //TimeSpan date = new TimeSpan(Convert.ToInt32(nums.Next(1, 10)), 0, 0, 0);
        //DO.Order dr = new DO.Order();
        //XElement? myOrders = XDocument.Load("../Order.xml").Root;

        //for (int i = 0; i < ord.Length; i++)
        //{

        //    dr.CustomerName = ord[i].Item1;
        //    dr.CustomerEmail = ord[i].Item2;
        //    dr.CustomerAddres = ord[i].Item3;
        //    do
        //    {
        //        date = new TimeSpan(Convert.ToInt32(nums.Next(1, 10)), 0, 0, 0);
        //        dr.OrderDate = DateTime.MinValue.AddDays(date.Days);
        //    } while (dr.OrderDate >= DateTime.Now);
        //    dr.OrderDate = dr.OrderDate;
        //    if (i < ord.Length * 0.2)
        //    {
        //        dr.ShipDate = DateTime.MinValue;
        //        dr.DeliveryDate = DateTime.MinValue;
        //    }
        //    else
        //    {
        //        do
        //        {
        //            date = new TimeSpan(Convert.ToInt32(nums.Next(0, 3)), 0, 0, 0);
        //            dr.ShipDate = dr.OrderDate.AddDays(date.Days);
        //        } while (dr.ShipDate > DateTime.Now || dr.ShipDate < dr.OrderDate);
        //        dr.ShipDate = dr.ShipDate;

        //        if (i < (ord.Length * 0.2 + ord.Length * 0.8) * 0.6)
        //        {
        //            do
        //            {
        //                date = new TimeSpan(Convert.ToInt32(nums.Next(1, 7)), 0, 0, 0);
        //                dr.DeliveryDate = dr.ShipDate.AddDays(date.Days);
        //            } while (dr.DeliveryDate > DateTime.Now || dr.DeliveryDate < dr.ShipDate);
        //            dr.DeliveryDate = dr.DeliveryDate;
        //        }
        //        else
        //        {
        //            dr.DeliveryDate = DateTime.MinValue;
        //        }
        //    }

        //    dr.ID = i + 1;
        //    // orders.Add(dr);
        //    XElement? myOrder = new XElement("order");
        //    XElement? ID = new XElement("ID", dr.ID);
        //    myOrder.Add(ID);
        //    XElement? CustomerName = new XElement("CustomerName", dr.CustomerName);
        //    myOrder.Add(CustomerName);
        //    XElement? CustomerEmail = new XElement("CustomerEmail", dr.CustomerEmail);
        //    myOrder.Add(CustomerEmail);
        //    XElement? CustomerAddres = new XElement("CustomerAddres", dr.CustomerAddres);
        //    myOrder.Add(CustomerAddres);
        //    XElement? OrderDate = new XElement("OrderDate", dr.OrderDate);
        //    myOrder.Add(OrderDate);
        //    XElement? ShipDate = new XElement("ShipDate", dr.ShipDate);
        //    myOrder.Add(ShipDate);
        //    XElement? DeliveryDate = new XElement("DeliveryDate", dr.DeliveryDate);
        //    myOrder.Add(DeliveryDate);
        //    myOrders?.Add(myOrder);
        //    myOrders?.Save("../Order.xml");

        //}

        List<DO.Order> lst = new List<DO.Order> { };
        DateTime order, ship, delivery;
        Exception ex = new Exception();
        XElement? myOrders = XDocument.Load("../Order.xml").Root;
        myOrders?.Elements().ToList().ForEach(e =>
        {
            int ID = Convert.ToInt32(e.Element("ID")?.Value);

            DateTime.TryParse(e.Element("OrderDate")?.Value, out order);
            DateTime.TryParse(e.Element("ShipDate")?.Value, out ship);
            DateTime.TryParse(e.Element("DeliveryDate")?.Value, out delivery);

            lst.Add(new DO.Order
            {
                ID = Convert.ToInt32(e.Element("ID")?.Value),
                CustomerName = Regex.Replace(e.Element("CustomerName")?.Value.ToString() ?? throw new Exception("name exeption"), "<.*?>", string.Empty),
                CustomerEmail = Regex.Replace(e.Element("CustomerEmail")?.Value.ToString() ?? throw new Exception("CustomerEmail exeption"), "<.*?>", string.Empty),
                CustomerAddres = Regex.Replace(e.Element("CustomerAddres")?.Value.ToString() ?? throw new Exception("CustomerAddres exeption"), "<.*?>", string.Empty),
                OrderDate = order,
                ShipDate = ship,
                DeliveryDate = delivery,
            });
        });
        return function == null ? lst : lst.Where(function);

    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public DO.Order Get(Predicate<DO.Order> function)
    {
        XElement? myOrders = XDocument.Load("../Order.xml").Root;
        List<DO.Order> lst = new List<DO.Order> { };
        myOrders?.Elements().ToList().ForEach(e =>
        {
            int ID = Convert.ToInt32(e.Element("ID")?.Value);

            lst.Add(new DO.Order
            {
                ID = Convert.ToInt32(e.Element("ID")?.Value),
                CustomerName = e.Element("CustomerName")?.Value.ToString() ?? throw new Exception("name exeption"),
                CustomerEmail = e.Element("CustomerEmail")?.Value ?? throw new Exception("CustomerEmail exeption"),
                CustomerAddres = e.Element("CustomerAddres")?.Value ?? throw new Exception("CustomerAddres exeption"),
                OrderDate = Convert.ToDateTime(e.Element("OrderDate")?.Value),
                ShipDate = Convert.ToDateTime(e.Element("ShipDate")?.Value),
                DeliveryDate = Convert.ToDateTime(e.Element("DeliveryDate")?.Value)
            });
        });
        Func<DO.Order, bool> func = new Func<DO.Order, bool>(function);
        return lst.Where(func).FirstOrDefault();
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(DO.Order order)
    {
        state = "update";
        Delete(order.ID);
        Add(order);
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void Delete(int id)
    {
        XElement? myOrders = XDocument.Load("../Order.xml").Root;
        XElement? config = XDocument.Load("../config.xml").Root;
        XElement? OrderId = config?.Element("OrderId");
        int Id = Convert.ToInt32(OrderId?.Value);
        Id -= 1;
        OrderId.Value = Id.ToString();
        config?.Save("../config.xml");
        myOrders?.Elements().ToList().Find(myOrder => Convert.ToInt32(myOrder?.Element("ID")?.Value) == id)?.Remove();
        myOrders?.Save("../Order.xml");
        XElement? ItemInOrders = XDocument.Load("../ItemInOrder.xml").Root;
        ItemInOrders?.Elements().ToList().Find(ItemInOrder => Convert.ToInt32(ItemInOrder?.Element("OrderId")?.Value) == id)?.Remove();
        ItemInOrders?.Save("../ItemInOrder.xml");
        XElement? ItemInOrderId = config?.Element("ItemInOrderId");
         Id = Convert.ToInt32(ItemInOrderId?.Value);
        Id = Id--;
        ItemInOrderId.Value = Id.ToString();
        config?.Save("../config.xml");
    }
}

