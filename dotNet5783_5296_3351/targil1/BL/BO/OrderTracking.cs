using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;
namespace BO
{
    public class OrderTracking
    {
        public int ID { get; set; }
        public Status OrderStatus { get; set; }
        public List<(DateTime, Status)> ?values { get; set; }
    }
}
