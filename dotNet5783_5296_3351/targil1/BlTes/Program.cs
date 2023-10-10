using BlApi;
using BlImplementation;
using System.Text.RegularExpressions;


namespace BlTest
{
    internal class Program
    {
        private static IBl Bl = new Bl();

        private static void Item(char ch)
        {
            if (ch != 'g')
            {
                int id;
                switch (ch)
                {
                    case 'a'://get all products as product-for-list
                        IEnumerable<BO.ItemForList> listaOfProduct = Bl.Item.ProductListRequest();
                        foreach (var item in listaOfProduct)
                            Console.WriteLine(item);
                        break;
                    case 'b'://get all products as product-item
                        IEnumerable<BO.ProductItem> listOfProductItem = Bl.Item.ProductDetailsRequestC();
                        foreach (var item in listOfProductItem)
                            Console.WriteLine(item);
                        break;
                    case 'c'://get a product
                        Console.WriteLine("enter id of product");
                        int.TryParse(Console.ReadLine(), out id);
                         
                        try
                        {
                            Console.WriteLine(Bl.Item.ProductDetailsRequestM(id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex?.InnerException?.Message);
                        }
                        catch (BO.inValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'd'://add a product
                        BO.Item product = new BO.Item();
                        Console.WriteLine("enter product's id to add");
                        int helpId = product.ID;
                        int.TryParse(Console.ReadLine(),out helpId);
                        
                        Console.WriteLine("enter product's name");
                        product.Name = Console.ReadLine();

                        while (Regex.IsMatch(product.Name, @"^\d+$"))
                        {
                            Console.WriteLine("the name is incorrect, please enter name again");
                            product.Name = Console.ReadLine();
                        }
                        Console.WriteLine("enter product's price");
                        double help = product.Price;
                        double.TryParse(Console.ReadLine(), out help);
                      
                        Console.WriteLine("enter product's category(0-for EARRINGS,1-for NECKLACE,2-for BRACELET,3-for RINGS,4-for SETS)");
                        int category = 0;
                        int.TryParse(Console.ReadLine(), out category);
                        
                        while (category < 0 || category > 2)
                        {
                            Console.WriteLine("the category is incorrect, please enter category again");
                            int.TryParse(Console.ReadLine(), out category);
                          
                        }
                        product.Category = (BO.Category)category;
                        Console.WriteLine("enter product's instock");
                        helpId = product.InStock;
                        int.TryParse(Console.ReadLine(), out helpId);
                        Console.WriteLine("enter product's Color 0-GOLD, 1-SILVER, 2-ROSE_GOLD, 3-WHITE_GOLD, 4-DIAMONDS");
                        BO.Color color = product.Color;
                        BO.Color.TryParse(Console.ReadLine(), out color);
                        try
                        {
                            Bl.Item.Add(product);
                        }
                        catch (BO.inValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex?.InnerException?.Message);
                        }
                        break;
                    case 'e'://delete a product
                        Console.WriteLine("enter id of product");
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            Bl.Item.Delete(id);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex?.InnerException?.Message);
                        }
                        catch (BO.ItemInOrderException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'f'://update a product
                        BO.Item productForUpdate = new BO.Item();
                        Console.WriteLine("enter product's id to update");
                        id= productForUpdate.ID;
                        int.TryParse(Console.ReadLine(), out id);
                        Console.WriteLine("enter product's name");
                        productForUpdate.Name = Console.ReadLine();
                        while (Regex.IsMatch(productForUpdate.Name, @"^\d+$"))
                        {
                            Console.WriteLine("the name is incorrect, please enter name again");
                            productForUpdate.Name = Console.ReadLine();
                        }
                        Console.WriteLine("enter product's price");
                        help = productForUpdate.Price;
                        double.TryParse(Console.ReadLine(), out help);
                        Console.WriteLine("enter product's category(0-for EARRINGS,1-for NECKLACE,2-for BRACELET,3-for RINGS,4-for SETS)");
                        int categoryUpdate = 0;
                        int.TryParse(Console.ReadLine(),out categoryUpdate);
                        while (categoryUpdate < 0 || categoryUpdate > 4)
                        {
                            Console.WriteLine("the category is incorrect, please enter category again");
                            int.TryParse(Console.ReadLine(), out categoryUpdate);

                        }
                        productForUpdate.Category = (BO.Category)categoryUpdate;
                        Console.WriteLine("enter product's instock");
                        helpId = productForUpdate.InStock;
                        int.TryParse(Console.ReadLine(), out helpId);
                        Console.WriteLine("enter product's color 0-GOLD, 1-SILVER, 2-ROSE_GOLD, 3-WHITE_GOLD, 4-DIAMONDS");
                         color = productForUpdate.Color;
                        BO.Color.TryParse(Console.ReadLine(), out color);
                        try
                        {
                            Bl.Item.Update(productForUpdate);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex?.InnerException?.Message);
                        }
                        catch (BO.inValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private static void Order(char ch)
        {
            if (ch != 'e')
            {
                int id;
                switch (ch)
                {
                    case 'a'://get all orders 
                        IEnumerable<BO.OrderForList> listaOfOrders = Bl.Order.OrderListRequest();
                        foreach (var item in listaOfOrders)
                            Console.WriteLine(item);
                        break;
                    case 'b'://Get Details Of Order
                        Console.WriteLine("enter order's id");
                        int.TryParse(Console.ReadLine(),out id);
                        try
                        {
                            Console.WriteLine(Bl.Order.OrderDetailsRequestM(id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex?.InnerException?.Message);
                        }
                        catch (BO.inValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'c'://update sent of order
                        Console.WriteLine("enter id for update sent of order");
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            Console.WriteLine(Bl.Order.ShippingUpdate(id));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex?.InnerException?.Message);
                        }
                        catch (BO.DateException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'd'://update arrived of order
                        Console.WriteLine("enter id for update arrived of order");
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            Console.WriteLine(Bl.Order.DeliveryUpdate(id));
                        }
                        catch (BO.DateException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex?.InnerException?.Message);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        private static BO.Cart Cart(char num, BO.Cart cart)
        {
            if (num != 'd')
            {
                int productid, amount;
                switch (num)
                {
                    case 'a'://for add cart
                        Console.WriteLine("enter product's id to add");
                        int.TryParse(Console.ReadLine(), out productid);


                        try
                        {
                            Console.WriteLine(Bl.Cart.Add(cart, productid));
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex?.InnerException?.Message);
                        }
                        catch (BO.OutOfStockException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'b'://for update cart
                        Console.WriteLine("enter product's id to add");
                        int.TryParse(Console.ReadLine(), out productid);
                        Console.WriteLine("enter new amount");
                        int.TryParse(Console.ReadLine(), out amount);
                        try
                        {
                            Console.WriteLine(Bl.Cart.QuantityUpdate(cart, productid, amount));
                        }
                        catch (BO.NotFoundException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.OutOfStockException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 'c'://for save cart
                        Console.WriteLine("enter your name");
                        cart.CustomerName = Console.ReadLine();
                        Console.WriteLine("enter your address");
                        cart.CustomerAddres = Console.ReadLine();
                        Console.WriteLine("enter your email");
                        cart.CustomerEmail = Console.ReadLine();
                        try
                        {
                            Bl.Cart.OrderConfirmation(cart);
                            Console.WriteLine(cart);
                        }
                        catch (BO.DalException ex)
                        {
                            Console.WriteLine(ex.Message + " " + ex?.InnerException?.Message);
                        }
                        catch (BO.inValidException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.OutOfStockException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        break;
                }
            }
            return cart;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("for product press 1");
            Console.WriteLine("for order press 2");
            Console.WriteLine("for cart press 3");
            Console.WriteLine("for exit press 0");
            int selection = 0;
            int.TryParse(Console.ReadLine(),out selection);
            char input;
            BO.Cart cart = new BO.Cart();
            cart.Items = new List<BO.ItemInOrder>();
            while (selection != 0)
            {
                switch (selection)
                {
                    case 1:
                        Console.WriteLine("for watching all manager products press a");
                        Console.WriteLine("for watching all customer products press b");
                        Console.WriteLine("to get a certain product press c");
                        Console.WriteLine("for adding a product press d");
                        Console.WriteLine("for deleting a product press e");
                        Console.WriteLine("for updating a product press f");
                        Console.WriteLine("to exit press g");
                         char.TryParse(Console.ReadLine(),out input);
                        Item(input);//doing this function 
                        break;
                    case 2:
                        Console.WriteLine("for watching all manager orders press a");
                        Console.WriteLine("to get a certain order press b");
                        Console.WriteLine("for updating a sent order press c");
                        Console.WriteLine("for updating an arrived order press d");
                        Console.WriteLine("to exit press e");
                        char.TryParse(Console.ReadLine(), out input);
                        Order(input); 
                        break;
                    case 3:
                        Console.WriteLine("to add a cart press a");
                        Console.WriteLine("to update a cart press b");
                        Console.WriteLine("to save a cart press c");
                        Console.WriteLine("to exit press d");
                        char.TryParse(Console.ReadLine(), out input);
                        cart = Cart(input, cart);
                        break;
                    default:
                        break;
                }
                Console.WriteLine("for products press 1");
                Console.WriteLine("for orders press 2");
                Console.WriteLine("for carts press 3");
                Console.WriteLine("to exit press 0");
                 int.TryParse(Console.ReadLine(),out selection);
            }



        }
    }
}