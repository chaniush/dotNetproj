using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO
{
    public class Order
    {
        public int ID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddres { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime DeliveryDate { get; set; }
        public Status OrderStatus { get; set; }
        public double OrderTotal { get; set; }
        public List<ItemInOrder> Items=new List<ItemInOrder>();
       
        public override string ToString()
        {
            string tostring = "CustomerName: " + CustomerName + "\n ID: " + ID + "\n CustomerEmail: " + CustomerEmail + "\n CustomerAddres: " + CustomerAddres + "\n OrderDate: " + OrderDate
                + "\n ShipDate: " + ShipDate + "\n DeliveryDate: " + DeliveryDate + "\n OrderStatus: " + OrderStatus + "\n OrderTotal: " + OrderTotal+"\n";
            int i = 0;
            if (Items.Count>0)
            {
                foreach (var item in Items)
                {
                    tostring += "\n item[" + (i + 1) + "]:" + item + "\n";
                }
                return tostring;
            }

            return tostring;
        }
    }
}
