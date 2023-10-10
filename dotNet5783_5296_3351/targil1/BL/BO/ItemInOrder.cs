 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO
{
    public class ItemInOrder
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int OrderId { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double TotalPrice { get; set; }
        public override string ToString() => $@"
         ID:{ID}
         ProductId:{ProductId} 
         ProductName: {ProductName}
         OrderId: {OrderId}
         Amount: {Amount}
         Price : {Price}
         TotalPrice: {TotalPrice}";
    }
}
