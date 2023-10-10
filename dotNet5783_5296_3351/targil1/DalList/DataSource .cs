using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace Dal;

static internal class DataSource
{
    public static readonly Random nums = new Random();
    public static List<DO.Item> items = new List<DO.Item>();
    public static List<DO.Order> orders = new List<DO.Order>();
    public static List<DO.ItemInOrder> itemsInOrders = new List<DO.ItemInOrder>();
  
    
    static internal class Config
    {

        private static int Itemid;
        public static int ItemId
        {
            get
            {
                //Console.WriteLine("Itemid= "+ Itemid); 
                return (Itemid = Convert.ToInt32(nums.Next(1000000, 999999999)));
            }
            set
            {
                //Console.WriteLine("Itemid= "+ Itemid);
                Itemid = Convert.ToInt32(nums.Next(1000000, 999999999));
            }
        }
        private static int Orderid = 1;
        public static int OrderId
        {
            get { return Orderid; }
            set { Orderid = value; }
        }
        private static int ItemInOrderid;
        public static int ItemInOrderId
        {
            get { return ItemInOrderid++; }
            set { ItemInOrderid = value; }
        }
    }

    static DataSource()
    {
        
        s_Initialize();
    }
    public static bool DoesItemIDExsists(int id)
    {
        DO.Item item = DataSource.items.Find(productItem => productItem.ID == id);
        if (item.ID != 0)
        {
            return true;
        }
        return false;
    }
    public static void s_Initialize()
    {
        (string, DO.Category, DO.Color)[] prods = new[] {
             ("Brilliant Earth White Gold Four-prong Round Diamond Stud Earrings", DO.Category.EARRINGS, DO.Color.WHITE_GOLD),
             ("beautiful silver oxidised Jhumki earing with mirror detailing", DO.Category.EARRINGS, DO.Color.SILVER),
             ("chain bracelet in rose gold", DO.Category.BRACELET, DO.Color.ROSE_GOLD),
             ("gold link bracelet", DO.Category.BRACELET, DO.Color.GOLD),
             ("threads-diamond-necklace", DO.Category.NECKLACE, DO.Color.DIAMONDS),
             ("white gold hart pendant", DO.Category.NECKLACE, DO.Color.WHITE_GOLD),
             ("Custom Diamond Engagement Ring", DO.Category.RINGS, DO.Color.DIAMONDS),
             ("mens ring 925 sterling silver amber stone", DO.Category.RINGS, DO.Color.SILVER),
             ("solitaire lab diamond jewelry set", DO.Category.SETS, DO.Color.DIAMONDS),
             ("hart gold set", DO.Category.SETS, DO.Color.GOLD)
         };
        DO.Item ti = new DO.Item();
        int id = 0;
        for (int i = 0; i < prods.Length; i++)
        {
            ti.Name = prods[i].Item1;
            ti.Color = prods[i].Item3;
            ti.Category = prods[i].Item2;
            do
            {
                id =Config.ItemId;
            } while (DoesItemIDExsists(id));
            ti.ID = id;
            ti.Price = Convert.ToInt32(nums.Next(200, 20000));
            ti.InStock = Convert.ToInt32(nums.Next(1, 15));
            items.Add(ti);
        }


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
        for (int i = 0; i < ord.Length; i++)
        {
            
            dr.CustomerName = ord[i].Item1;
            dr.CustomerEmail = ord[i].Item2;
            dr.CustomerAddres = ord[i].Item3;
            do
            {
                date = new TimeSpan(Convert.ToInt32(nums.Next(1, 10)), 0, 0, 0);
                dr.OrderDate = DateTime.MinValue.AddDays(date.Days);
            } while (dr.OrderDate>= DateTime.Now);
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
                } while (dr.ShipDate>DateTime.Now|| dr.ShipDate< dr.OrderDate);
                dr.ShipDate = dr.ShipDate;

                if (i < (ord.Length * 0.2 + ord.Length * 0.8) * 0.6)
                {
                    do
                    {
                        date = new TimeSpan(Convert.ToInt32(nums.Next(1, 7)), 0, 0, 0);
                        dr.DeliveryDate = dr.ShipDate.AddDays(date.Days);
                    } while (dr.DeliveryDate>DateTime.Now|| dr.DeliveryDate< dr.ShipDate);
                    dr.DeliveryDate = dr.DeliveryDate;
                }
                else
                {
                    dr.DeliveryDate = DateTime.MinValue;
                }
            }
            
            dr.ID=Config.OrderId++;
            orders.Add(dr);
            

        }

        const int THREE = 3;
        int ten = 0;
        DO.ItemInOrder itord = new DO.ItemInOrder();
        int sum = 0;
        List<DO.ItemInOrder> check;
        for (int i = 0; i < THREE; i++)
        {
            for (int j =itemsInOrders.Count, ind = 0; j < items.Count + ten; j++, ind++)
            {
                itord.ID = Config.ItemInOrderId;
                do
                {
                    sum = nums.Next(1, items.Count+1);
                    itord.OrderId = sum;
                    itord.ProductId = items[ind].ID;
                    check = (from myitemsInOrders in itemsInOrders
                                 where myitemsInOrders.OrderId == itord.OrderId && itord.ProductId == myitemsInOrders.ProductId
                                 select myitemsInOrders).ToList();
                } while (check.Count!=0);
               


                items.ToList().ForEach(item =>
                {
                    if (itord.ProductId == item.ID)
                    {

                        itord.Price = item.Price;
                       
                    }
                });
                
                itord.Amount = Convert.ToInt32(nums.Next(1, 4));
                //add(itord[i]);
                
                itemsInOrders.Add(itord);
            }
            ten += 10;
        }

    }
   
  
}

