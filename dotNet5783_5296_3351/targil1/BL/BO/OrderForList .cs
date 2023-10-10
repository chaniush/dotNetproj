using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO
{
    public class OrderForList
    {
        public int ID { get; set; }
        public string? CustomerName { get; set; }
        public int OrderStatus { get; set; }
        public int AmountOfItems { get; set; }
        public double OrderTotal { get; set; }
        public override string ToString() => $@"
         CustomerName:{CustomerName} 
         ID:{ID}
         AmountOfItems : {AmountOfItems}
         OrderStatus: {OrderStatus}
         OrderTotal: {OrderTotal}";
    }
}

