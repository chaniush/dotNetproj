
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi
{
    public interface IOrder
    {
        public IEnumerable<OrderForList> OrderListRequest();//gets all order list
        public Order OrderDetailsRequestM(int id);//gets order details by id
        public Order ShippingUpdate(int id);//updates shipping date time
        public Order DeliveryUpdate(int id);//updates delivery date time
        //public OrderTracking OrderTracking(int id);
        public void OrderUpdate(Order order);//updates order 

        public OrderTracking OrderTracking(int id);

        public int? OrderForTreatment();

    }
}
