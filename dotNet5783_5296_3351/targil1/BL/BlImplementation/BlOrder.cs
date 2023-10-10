using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;

using DalApi;
using DO;

namespace BlImplementation;

internal class BlOrder: BlApi.IOrder
{
    public IDal dal = DalApi.Factory.Get();
    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<OrderForList> OrderListRequest() {
        double sum = 0;
        IEnumerable<DO.Order> allOrders;
        lock (dal)
        {
            allOrders = dal.Order.GetAll();//from dal
        }
        List<BO.OrderForList> orders = new();
        int AmountOfItems = 0;
        allOrders.ToList().ForEach(item =>
        {
            AmountOfItems = 0;
            IEnumerable<DO.ItemInOrder> orderItemsById;
            lock (dal)
            {
                orderItemsById = dal.ItemInOrder.getByOrder(item.ID);//bring all orderitems according to orderId
            }
            orderItemsById.ToList().ForEach(orderItem =>
            {
                sum += orderItem.Price * orderItem.Amount;//calculate the price 
                AmountOfItems += orderItem.Amount;
            });
            orders.Add(new BO.OrderForList { ID = item.ID, CustomerName = item.CustomerName, AmountOfItems = AmountOfItems, OrderStatus = (int)StatusCalculation(item), OrderTotal = sum });
        });
        return orders;

    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int? OrderForTreatment()
    {
        IEnumerable<DO.Order> allOrders;
        lock (dal)
        {
            allOrders = dal.Order.GetAll();//from dal
        }
        DO.Order min;
        try
        {
             min = allOrders.ToArray()[0];

        }
        catch (Exception)
        {

            throw new Exception("there are no orders");
        }
        allOrders.ToList().ForEach(element =>
        {
            if (StatusCalculation(min) == BO.Status.APPROVED)
            {
                if (StatusCalculation(element) == BO.Status.APPROVED)
                {
                    if (element.OrderDate < min.OrderDate)
                    {
                        min = element;
                    }
                }
                else
                {
                    if (StatusCalculation(element) == BO.Status.SENT)
                    {
                        if (element.ShipDate < min.OrderDate)
                        {
                            min = element;
                        }
                    }
                    //else
                    //{
                    //    if (element.DeliveryDate < min.OrderDate)
                    //    {
                    //        min = element;
                    //    }
                    //}
                }
            }
            else if (StatusCalculation(min) == BO.Status.SENT)
            {
                if (StatusCalculation(element) == BO.Status.APPROVED)
                {
                    if (min.ShipDate > element.OrderDate)
                    {
                        min = element;
                    }

                }
                else if (StatusCalculation(element) == BO.Status.SENT)
                {
                    if (element.ShipDate < min.ShipDate)
                    {
                        min = element;
                    }
                }
                //else
                //{
                //    if (element.DeliveryDate < min.ShipDate)
                //    {
                //        min = element;
                //    }
                //}
            }
           else if (StatusCalculation(min) == BO.Status.ARRIVED)
            {
                //if ((StatusCalculation(element) == BO.Status.APPROVED&&element.OrderDate<min.DeliveryDate)||(StatusCalculation(element) == BO.Status.SENT&&element.ShipDate<min.DeliveryDate))
                //{
                    min = element;
               // }
               // else if (StatusCalculation(element) == BO.Status.ARRIVED)
               // {
               //     if (element.DeliveryDate < min.DeliveryDate)
               //     {
                //        min = element;
                //    }
               // }
            }
        });
        if (StatusCalculation(min) == BO.Status.ARRIVED)
        {
            return null;
        }
        return min.ID;
    }
    private static BO.Status StatusCalculation(DO.Order order)
    {
        DateTime today = DateTime.Now;
        if (order.DeliveryDate.CompareTo(today) < 0 && order.DeliveryDate.CompareTo(DateTime.MinValue) != 0)//if the delivery date already was
            return BO.Status.ARRIVED;
        if (order.ShipDate.CompareTo(today) < 0 && order.ShipDate.CompareTo(DateTime.MinValue) != 0)//if the ship date already was
            return BO.Status.SENT;
        return BO.Status.APPROVED;
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order OrderDetailsRequestM(int id) {
        BO.Order orderBo = new();
        if (id > 0)
        {
            DO.Order orderDo;
            try
            {
                lock (dal)
                {
                    orderDo = dal.Order.Get(id);
                }
                orderBo.ID = orderDo.ID;
                orderBo.CustomerName = orderDo.CustomerName;
                orderBo.CustomerEmail = orderDo.CustomerEmail;
                orderBo.CustomerAddres = orderDo.CustomerAddres;
                orderBo.OrderDate = orderDo.OrderDate;
                orderBo.ShipDate = orderDo.ShipDate;
                orderBo.DeliveryDate = orderDo.DeliveryDate;
                IEnumerable<DO.ItemInOrder> itemInOrderList;
                lock (dal)
                {
                    itemInOrderList = dal.ItemInOrder.getByOrder(id);
                }
                orderBo.OrderStatus = StatusCalculation(orderDo);
                double totalPrice = 0;
                var myItemInOrder = (from itemInOrder in itemInOrderList
                                     where itemInOrder.OrderId == id
                                     select new BO.ItemInOrder()
                                     {
                                         ID = itemInOrder.ID,
                                         OrderId=itemInOrder.OrderId,
                                         Amount = itemInOrder.Amount,
                                         Price = itemInOrder.Price,
                                         ProductId = itemInOrder.ProductId,
                                         TotalPrice = itemInOrder.Price * itemInOrder.Amount,
                                     });
                myItemInOrder.ToList().ForEach(itemInOrder =>
                {
                       orderBo.Items.Add(itemInOrder);
                        totalPrice += itemInOrder.Price * itemInOrder.Amount;
                });
                orderBo.OrderTotal = totalPrice;
            }
            catch
            {
                //לזרוק חריגה
            }
        }
        return orderBo;


    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order ShippingUpdate(int id) {
        try
        {
            BO.Order updatedOrder = OrderDetailsRequestM(id);//get the update order from the dal
            DO.Order order;
            lock (dal)
            {
                order = dal.Order.Get(id);//get order by id
            }
            if (order.ShipDate == DateTime.MinValue || order.ShipDate.CompareTo(DateTime.Now) > 0)//check that the date of ship didn't past
            {
                order.ShipDate = DateTime.Now;//update ship date
                lock (dal)
                {
                    dal.Order.Update(order);//update order
                }
                updatedOrder = OrderDetailsRequestM(id);
                return updatedOrder;
            }
            else
                return updatedOrder;

        }
        catch (ObjectDoesntExistException ex)
        {
            throw new BO.DalException(ex);
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Order DeliveryUpdate(int id)
    {
        try
        {
            BO.Order updatedOrder = OrderDetailsRequestM(id);//get the update order from the dal
            DO.Order order;
            lock (dal)
            {
                order = dal.Order.Get(id);//get order by id
            }
            if (order.DeliveryDate == DateTime.MinValue || order.DeliveryDate.CompareTo(DateTime.Now) > 0)//check that the date of delivery didn't past
            {
                order.DeliveryDate = DateTime.Now;//update delivery date
                lock (dal)
                {
                    dal.Order.Update(order);//update order
                }
                updatedOrder = OrderDetailsRequestM(id);
                return updatedOrder;
            }
            else
                return updatedOrder;
        }
        catch (ObjectDoesntExistException ex)
        {
            throw new BO.DalException(ex);
        }
    }
    [MethodImpl(MethodImplOptions.Synchronized)]

    public void OrderUpdate(BO.Order order) 
    { 
        
    }
    [MethodImpl(MethodImplOptions.Synchronized)]
    public OrderTracking OrderTracking(int id)
    {
        try
        {
            DO.Order newOrder;
            lock (dal)
            {
                newOrder = dal.Order.Get(id);
            }
            BO.Status status= StatusCalculation(newOrder);
            List<(DateTime, BO.Status)> DateAndStatus = new List<(DateTime,BO.Status)> { };
            if (status == BO.Status.APPROVED)
            {
                DateAndStatus.Add((newOrder.OrderDate, status));
            }
           else if (status == BO.Status.SENT)
            {
                DateAndStatus.Add((newOrder.ShipDate, status));
            }
            else if (status==BO.Status.ARRIVED)
            {
                DateAndStatus.Add((newOrder.DeliveryDate, status));
            }
            BO.OrderTracking orderTracking = new BO.OrderTracking { ID = id, OrderStatus = status,  values= DateAndStatus };
            return orderTracking;
        }
        catch (Exception ex)
        {
            throw new ObjectDoesntExistException(ex);
        }
    }
}
