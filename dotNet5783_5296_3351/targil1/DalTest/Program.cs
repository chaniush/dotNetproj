//See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
//using namespace DO;
using Dal;
using DO;
using System.Security.Cryptography;
using System.Collections;
using DalList;
using DalApi;

namespace DalTest { 
 // Note: actual namespace depends on the project name.dddddallwwwwwwwwww

    internal class Program
    {
        public static readonly Random nums = new Random();
        private static DalOrder order = new DalOrder();
        private static DalItemInOrder ItemInOrder = new DalItemInOrder();
        private static DO.Order myOrder = new DO.Order();
        private static IEnumerable<Order>? arrOrder;
        private static DalItemInOrder itemInOrder = new DalItemInOrder();
        private static DO.ItemInOrder myItemsInOrders = new DO.ItemInOrder();
        private static IEnumerable<ItemInOrder>? arrItemsInOrders;
        private static DalItem Item = new DalItem();
        private static DO.Item myItem = new DO.Item();
        private static IEnumerable<Item>? arrItem;
    //    private static IDal dalList = new Dal.DalList();
        static void Main(string[] args)
        {
              
            Item.output();
           //it
            int num=-1;
            while (num != 0)
            {
                Console.WriteLine("0-exit 1-items \n  2-orders \n 3-items in orders");
                //Item.del();
                  int.TryParse(Console.ReadLine(),out num);
                switch (num)
                {
                    case 1:
                        Items();
                        break;
                    case 2:
                        orders();
                        break;
                    case 3:
                        itemsinorders();
                        break;
                    default:
                        break;
                }
            }   
            
        }
    
        
        public static void Items(){
             Console.WriteLine("1-create \n 2-read \n 3-readall \n 4-update \n 5-delete");
            int ans = 0;
            int.TryParse(Console.ReadLine(),out ans);
                         switch (ans)
                         {
                     case 1:
                         items_create();
                    break;
                     case 2:
                         items_read();
                         break;
                     case 3:
                    items_readAll();
                         break;
                     case 4:
                         items_update();
                         break;
                     case 5:
                         items_delete();
                         break;
                     default:
                                 break;
                         }
        }
        public static void create_item()
        {
            Console.WriteLine("put in name :");
            string? s = myItem.Name;
           s= Console.ReadLine();
            myItem.Name = s==null?"":s;
            Console.WriteLine("put in Category: \n 1-EARRINGS \n 2-NECKLACE \n 3-BRACELET \n 4-RINGS \n 5-SETS");
            int category=0;
            int.TryParse(Console.ReadLine(),out category);
            switch (category)
            {
                case 1:
                    myItem.Category = DO.Category.EARRINGS;
                    break;
                case 2:
                    myItem.Category = DO.Category.NECKLACE;
                    break;
                case 3:
                    myItem.Category = DO.Category.BRACELET;
                    break;
                case 4:
                    myItem.Category = DO.Category.RINGS;
                    break;
                case 5:
                    myItem.Category = DO.Category.SETS;
                    break;
                default:
                    break;
            }
            Console.WriteLine("put in Color: \n 1-GOLD \n 2-SILVER \n 3-ROSE_GOLD \n 4-WHITE_GOLD \n 5-DIAMONDS");
            int color = 0;
            int.TryParse(Console.ReadLine(),out color);
            switch (color)
            {
                case 1:
                    myItem.Color = DO.Color.GOLD;
                    break;
                case 2:
                    myItem.Color = DO.Color.SILVER;
                    break;
                case 3:
                    myItem.Color = DO.Color.ROSE_GOLD;
                    break;
                case 4:
                    myItem.Color = DO.Color.WHITE_GOLD;
                    break;
                case 5:
                    myItem.Color = DO.Color.DIAMONDS;
                    break;
                default:
                    break;
            }
            Console.WriteLine("put in Price : ");
            double help = myItem.Price;
            int inStock = myItem.InStock;
             double.TryParse(Console.ReadLine(),out help);
            myItem.Price=help;
            Console.WriteLine("put in InStock : ");
            do
	        {
                int.TryParse(Console.ReadLine(),out inStock);
                myItem.InStock=inStock;

            } while (myItem.InStock<=0);
            
        }
        public static void items_create(){
            bool error=false;
            int id;
            create_item();
         //   Console.WriteLine("put in id: ");
         //   do
	        //{
         //     myItem.ID = int.Parse(Console.ReadLine());
         //       if ( myItem.ID<100000|| myItem.ID > 999999999)
         //       {
         //           Console.WriteLine("id is not valid. please insert at least 6 Digits. \n put in id: ");
         //       }
	        //} while (myItem.ID < 100000 || myItem.ID > 999999999);

            do
            {
                id = Convert.ToInt32(nums.Next(1000000, 999999999));
                //Console.WriteLine("id= "+ id);
            } while (Item.DoesItemIDExsists(id));
            myItem.ID = id;
            do
	        {
                try 
	            {	        
	            	  Item.create(myItem);
	            }
                //catch (Exception e)
                //{
                //       Console.WriteLine(e);
                //       error = true;
                //	//throw;
                //}
                catch (Exception )
                {
                    Console.WriteLine("item exists");
                    error = true;
                }
                //catch (ArgumentNullException e)
                //{
                //    Console.WriteLine("{0} First exception caught.", e);
                //}
            } while (error==true);
            
          
            Console.WriteLine("create succeded!  ");
            return;
            
        }
        public static void items_read(){
            DO.Item newItem=new DO.Item();
           bool error = false;
            int num;
            do
	        {
                try 
	            {
                    do
                    {
                        Console.WriteLine("put in id: ");
                         int.TryParse(Console.ReadLine(),out num); 
                    } while (num<100000);
                           
	              newItem= Item.Get(num);
	            }
	            catch (Exception e)
	            {
                    Console.WriteLine(e);
                    error = true;
	        
	            }
	        } while (error == true);
            Console.WriteLine(newItem);
        }

        public static void items_readAll()
        {
            arrItem = Item.GetAll();
            arrItem.ToList().ForEach(item =>
            {
                Console.WriteLine(item);
            });
        }

        public static void items_update()
        {
            int id = myItem.ID;
            Console.WriteLine("put in id: ");
             int.TryParse(Console.ReadLine(),out id);
            myItem.ID = id;
            create_item();
            Item.Update(myItem);
        }
      
        public static void items_delete()
        {
            int id;
            bool error = false;
            Console.WriteLine("put in id: ");
            
            do
	        {
                try 
	            {
                    do
                    {
                        int.TryParse(Console.ReadLine(), out id);
                        if (id < 100000)
                        {
                            Console.WriteLine("id is not valid. please insert at least 6 Digits. \n put in id: ");
                        }
                    } while (id < 100000);
                    Item.Delete(id);
	            }
	            catch (Exception e)
	            {
                    Console.WriteLine(e);
                    error = true;
	            }
	        } while (error==true);
            
        }
        //ORDER
        public static void orders()
        {
            Console.WriteLine("1-create \n 2-read \n 3-readall \n 4-update \n 5-delete");
            int ans = 0;
            int.TryParse(Console.ReadLine(),out ans);
            switch (ans)
            {
                case 1:
                    orders_create();
                    break;
                case 2:
                    orders_read();
                    break;
                case 3:
                    orders_readAll();
                    break;
                case 4:
                    orders_update();
                    break;
                case 5:
                    orders_delete();
                    break;
                default:
                    break;
            }
        }
        public static void create_order()
        {
            string? s= myOrder.CustomerName; 
            Console.WriteLine("put in customer name :");
            s = Console.ReadLine();
            myOrder.CustomerName = s==null?"":s;
            Console.WriteLine("put in customer email :");
            s = Console.ReadLine();
            myOrder.CustomerEmail= s == null ? "" : s;
            Console.WriteLine("put in customer addres :");
             s= Console.ReadLine();
            myOrder.CustomerAddres= s == null ? "" : s;
            Console.WriteLine("put in order date :");
            DateTime date = myOrder.OrderDate;
              DateTime.TryParse(Console.ReadLine(),out date);
            myOrder.OrderDate= date;
            Console.WriteLine("put in ship date :");
             date = myOrder.ShipDate;
            DateTime.TryParse(Console.ReadLine(), out date);
            myOrder.ShipDate = date;
            Console.WriteLine("put in delivery date :");
            date = myOrder.DeliveryDate;
            DateTime.TryParse(Console.ReadLine(), out date);
            myOrder.DeliveryDate = date;
        }
        public static void orders_create()
        {
            int id = myOrder.ID;
            create_order();
            Console.WriteLine("put in id: ");
           int.TryParse(Console.ReadLine(),out id); 
            myOrder.ID = id;
            order.create(myOrder);
            Console.WriteLine("create succeded!  ");
            return;

        }
        public static void orders_read()
        {
            Console.WriteLine("put in id: ");
            int num = 0; 
            int.TryParse(Console.ReadLine(),out num);
            DO.Order newOrder = order.Get(num);
            Console.WriteLine(newOrder);
        }

        public static void orders_readAll()
        {
            arrOrder = order.GetAll();
            foreach (DO.Order order in arrOrder)
            {
                Console.WriteLine(order);
            }
        }

        public static void orders_update()
        {
            int id = myOrder.ID;
            Console.WriteLine("put in id: ");
             int.TryParse(Console.ReadLine(),out id);
            myOrder.ID = id;
            create_order();
            order.Update(myOrder);
        }

        public static void orders_delete()
        {
            int id;
            bool error = false;
            

            do
            {
                try
                {
                    Console.WriteLine("put in id: ");
                     int.TryParse(Console.ReadLine(),out id);
                    order.Delete(id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    error = true;
                }
            } while (error == true);

        }

        //ITEM IN ORDER

        public static void itemsinorders()
        {
            Console.WriteLine("1-create \n 2-read \n 3-readall \n 4-update \n 5-delete");
            int ans = 0; 
            int.TryParse(Console.ReadLine(),out ans);
            switch (ans)
            {
                case 1:
                    itemsinorders_create();
                    break;
                case 2:
                    itemsinorders_read();
                    break;
                case 3:
                    itemsinorders_readAll();
                    break;
                case 4:
                    itemsinorders_update();
                    break;
                case 5:
                    itemsinorders_delete();
                    break;
                default:
                    break;
            }
        }
        public static void create_iteminorders()
        {
            int id = 0;
            double price = 0;
            do
            {
                Console.WriteLine("put in product id :");
                id = myItemsInOrders.ProductId;
                  int.TryParse(Console.ReadLine(),out id);
                myItemsInOrders.ProductId= id;
            } while (!Item.DoesItemIDExsists(myItemsInOrders.ProductId));
            do
            {
                Console.WriteLine("put in order id :");
                id = myItemsInOrders.OrderId;
                int.TryParse(Console.ReadLine(), out id);
                myItemsInOrders.OrderId = id;
            } while (!order.DoesOrderIDExsists(myItemsInOrders.OrderId));
            
            Console.WriteLine("put in price :");
            price = myItemsInOrders.Price;
            double.TryParse(Console.ReadLine(), out price);
            myItemsInOrders.Price = price;
            Console.WriteLine("put in amount :");
            id = myItemsInOrders.Amount;
            int.TryParse(Console.ReadLine(), out id);
            myItemsInOrders.Amount = id;
        }
        public static void itemsinorders_create()
        {
            int id = 0;
            create_iteminorders();
            do
            {
                Console.WriteLine("put in id: ");
                id = myItemsInOrders.ID;
                int.TryParse(Console.ReadLine(), out id);
                myItemsInOrders.ID = id;
            } while (ItemInOrder.DoesItemsinorderIDExsists(myItemsInOrders.ID));
            
            itemInOrder.create(myItemsInOrders);
            Console.WriteLine("create succeded!  ");
            return;

        }
        public static void itemsinorders_read()
        {
            int id, ordId;
            do
            {
                Console.WriteLine("put in id: ");
                int.TryParse(Console.ReadLine(), out id);
            } while (!ItemInOrder.DoesItemsinorderIDExsists(id));
            do
            {
                Console.WriteLine("put in order id: ");
                int.TryParse(Console.ReadLine(), out ordId);
            } while (!order.DoesOrderIDExsists(ordId));

            try
            {
                DO.ItemInOrder newItemInOrder = itemInOrder.Get(id);
                Console.WriteLine(newItemInOrder);
            }
            catch (Exception)
            {

                Console.WriteLine("item does not exist");
            }
            
           
        }

        public static void itemsinorders_readAll()
        {
            arrItemsInOrders = itemInOrder.GetAll();
            foreach (DO.ItemInOrder itemin in arrItemsInOrders)
            {
                Console.WriteLine(itemin);
            }
        }

        public static void itemsinorders_update()
        {
            int id = 0;
            do
            {
                Console.WriteLine("put in id: ");
                id = myItemsInOrders.ID;
                int.TryParse(Console.ReadLine(), out id);
                myItemsInOrders.ID = id;
            } while (!ItemInOrder.DoesItemsinorderIDExsists(myItemsInOrders.ID));

            
            create_iteminorders();
            itemInOrder.Update(myItemsInOrders);
        }

        public static void itemsinorders_delete()
        {
            int id;
            bool error = false;

            do
            {
                try
                {
                    do
                    {
                        Console.WriteLine("put in id: ");
                        int.TryParse(Console.ReadLine(), out id);
                    } while (!ItemInOrder.DoesItemsinorderIDExsists(myItemsInOrders.ID));

                    ItemInOrder.Delete(id);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    error = true;
                }
            } while (error == true);

        }
    }

}
    
