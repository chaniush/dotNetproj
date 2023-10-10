using BlApi;
using BlImplementation;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Simulator
{
    public delegate void StatusChanged1(BO.Order order, int num);
    public delegate void StatusChanged2(BO.Status prev, BO.Status? next=null);
    public delegate void StatusChanged3(int? id);

    public static class Simulator
    {
        static Exception ex = new Exception();
        private static IBl bl = BlApi.Factory.Get() ?? throw new NullExeption(ex);
        private static volatile bool stop = true;
        public static event StatusChanged1 StatusChanged1;
        public static event StatusChanged2 StatusChanged2;
        public static event StatusChanged3 StatusChanged3;
        public static BO.Status prev;
        public static BO.Status next;

        static Simulator()
        {
            stop = true;
        }
      
        public static void run()
        {
            Thread t;
            t = new Thread(StartOrderTreatment);
            t.Start();
           // stop = true;
        }
        public static void StartOrderTreatment()
        {
            int id=0;
            BO.Order myOrder=new BO.Order();
            stop = true;
            while (stop)
            {

                try
                {
                     id = bl.Order.OrderForTreatment() ?? throw new Exception("all orders arrived");
                }
                catch (Exception)
                {
                    stop=false;
                    StatusChanged3(id);
                   
                    break;
                }
                BO.Order order = bl.Order.OrderDetailsRequestM(id);
                Random rnd = new Random();
                int num = (rnd.Next(1000, 5000))/1000;
                num*=1000;
                prev = order.OrderStatus;
                if (StatusChanged1!=null)
                {
                    StatusChanged1(order,num);//מציג את ההזמנה הנוכחית ואת המספר שהוגרל
                    //StatusChanged2(prev);//מציג את ההזמנה הנוכחית ואת המספר שהוגרל
                    StatusChanged2(prev, prev + 1);//מציג את הסטטוס הקודם והבא

                }


                Thread.Sleep(num);
                if (order.ShipDate <= DateTime.MinValue)
                {
                    myOrder =bl.Order.ShippingUpdate(id);
               
                }
                else if (order.DeliveryDate <= DateTime.MinValue)
                {
                    myOrder = bl.Order.DeliveryUpdate(id);
                }
                XElement? config = XDocument.Load("../config.xml").Root;
                XElement? OrderId = config?.Element("OrderId");
                int Id = Convert.ToInt32(OrderId.Value);
                next= myOrder.OrderStatus;
                StatusChanged2(prev, next);//מציג את הסטטוס הקודם והבא

            }
        }
        public static void Stop()
        {
            stop = false;
        }
    }
}