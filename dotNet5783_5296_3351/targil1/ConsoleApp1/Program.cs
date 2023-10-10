
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

internal class Program
{
    public static readonly Random nums = new Random();
    public static List<DO.Order> orders = new List<DO.Order>();

    private static void Main(string[] args)
    {
        (string, string, string)[] ord = new[] {
             ("chani", "chani@gmail.com", "jerusalem"),
             ("dani", "dani@gmail.com", "haifa"),
             ("riki", "riki@gmail.com", "afula"),
             ("levi", "levi@gmail.com", "gush-etzyun"),
             ("tzipi", "tzipi@gmail.com", "bnei-brack"),
             ("noa", "noa@gmail.com", "neve_yaakob"),
             ("avi", "avi@gmail.com", "givaat_shaul"),
             ("shuki", "shuki@gmail.com", "kiryat_moshe"),
             ("miri", "miri@gmail.com","beitar"),
             ("eli", "eli@gmail.com", "raanana")
         };
        TimeSpan date = new TimeSpan(Convert.ToInt32(nums.Next(1, 10)), 0, 0, 0);
        DO.Order dr = new DO.Order();
        XElement? myOrders = XDocument.Load("../Order.xml").Root;

        for (int i = 0; i < ord.Length; i++)
        {

            dr.CustomerName = ord[i].Item1;
            dr.CustomerEmail = ord[i].Item2;
            dr.CustomerAddres = ord[i].Item3;
            do
            {
                date = new TimeSpan(Convert.ToInt32(nums.Next(1, 10)), 0, 0, 0);
                dr.OrderDate = DateTime.MinValue.AddDays(date.Days);
            } while (dr.OrderDate >= DateTime.Now);
            dr.OrderDate = dr.OrderDate;
            if (i < ord.Length * 0.2)
            {
                dr.ShipDate = DateTime.MinValue;
                dr.DeliveryDate = DateTime.MinValue;
            }
            else
            {
                do
                {
                    date = new TimeSpan(Convert.ToInt32(nums.Next(0, 3)), 0, 0, 0);
                    dr.ShipDate = dr.OrderDate.AddDays(date.Days);
                } while (dr.ShipDate > DateTime.Now || dr.ShipDate < dr.OrderDate);
                dr.ShipDate = dr.ShipDate;

                if (i < (ord.Length * 0.2 + ord.Length * 0.8) * 0.6)
                {
                    do
                    {
                        date = new TimeSpan(Convert.ToInt32(nums.Next(1, 7)), 0, 0, 0);
                        dr.DeliveryDate = dr.ShipDate.AddDays(date.Days);
                    } while (dr.DeliveryDate > DateTime.Now || dr.DeliveryDate < dr.ShipDate);
                    dr.DeliveryDate = dr.DeliveryDate;
                }
                else
                {
                    dr.DeliveryDate = DateTime.MinValue;
                }
            }

            dr.ID =ord.Length;
          // orders.Add(dr);
                XElement? myOrder = new XElement("order");
                XElement? ID = new XElement("ID", dr.ID);
                myOrder.Add(ID);
                XElement? CustomerName = new XElement("CustomerName", dr.CustomerName);
                myOrder.Add(CustomerName);
                XElement? CustomerEmail = new XElement("CustomerEmail", dr.CustomerEmail);
                myOrder.Add(CustomerEmail);
                XElement? CustomerAddres = new XElement("CustomerAddres", dr.CustomerAddres);
                myOrder.Add(CustomerAddres);
                XElement? OrderDate = new XElement("OrderDate", dr.OrderDate);
                myOrder.Add(OrderDate);
                XElement? ShipDate = new XElement("ShipDate", dr.ShipDate);
                myOrder.Add(ShipDate);
                XElement? DeliveryDate = new XElement("DeliveryDate", dr.DeliveryDate);
                myOrder.Add(DeliveryDate);
                myOrders?.Add(myOrder);
                myOrders?.Save("../Order.xml");

        }
        
    }
}