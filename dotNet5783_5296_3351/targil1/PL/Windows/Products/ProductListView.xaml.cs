using BlApi;
using BlImplementation;
using BO;
using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace PL.Windows.Products
{
    /// <summary>
    /// Interaction logic for ProductListView.xaml
    /// </summary>
    public partial class ProductListView : Window
    {
       static Exception ex = new Exception();
        public IBl? bl = BlApi.Factory.Get();
        private BlImplementation.Bl iblObj = new BlImplementation.Bl();
        private string? mytype;
        private static Cart cart = new Cart();
        
        public ProductListView(string? type=null)
        {
            InitializeComponent();
           
            mytype = type;
            IEnumerable<ItemForList> productList = new List<ItemForList>();
            try
            {
                productList = bl?.Item.ProductListRequest() ?? throw new NullExeption(ex);
            List<ItemForList> items = new List<ItemForList>();
                items = (from element in productList
                         select new ItemForList
                         {
                             ID = element.ID,
                             Name = element.Name,
                             Category = element.Category,
                             Color = element.Color,
                             Price = element.Price
                         }).ToList();
                //items = new List<ItemForList>();
                //foreach (var element in productList)
                //{
                //    items.Add(new ItemForList()
                //    {
                //        ID = element.ID,
                //        Name = element.Name,
                //        Category = element.Category,
                //        Color = element.Color,
                //        Price = element.Price
                //    });
                //}

                ProductsListview.ItemsSource = items;
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ProductsListview.ItemsSource);
                PropertyGroupDescription groupDescription = new PropertyGroupDescription("Category");
                view.GroupDescriptions.Add(groupDescription);
                CategorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            if (mytype == "manager")
            {
                btnOrder.Visibility = Visibility.Visible;
              //  btnAddProduct.Visibility = Visibility.Visible;
            }
            else
            {
                    if (mytype == "client")
                    {
                        btnOrder.Visibility = Visibility.Visible;
                        //  btnAddProduct.Visibility = Visibility.Visible;
                    }
                    btnOrder.Visibility = Visibility.Hidden;
               // btnAddProduct.Visibility = Visibility.Hidden;
            }
            }
            catch (Exception e)
            {

                MessageBox.Show("there was an exceotion thrown: " + e);
            }
        }
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductsListview.ItemsSource = bl?.Item.ProductListRequest(element => element.Category == ((DO.Category)CategorySelector.SelectedItem));
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            Windows.Products.ProductLogic product = new Windows.Products.ProductLogic(iblObj, "Add");
            this.Visibility = Visibility.Hidden;
            product.Visibility = Visibility.Visible;
        }

        private void ProductsListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IEnumerable<ItemForList> productList = new List<ItemForList>();
            productList = bl?.Item.ProductListRequest() ?? throw new NullExeption(ex);

            //  IEnumerable<Item> items = (IEnumerable<Item>?)bl?.Item.ProductListRequest() ?? throw new NullExeption(ex); ;
            if (mytype=="manager")
            {
                Windows.Products.ProductLogic logic = new Windows.Products.ProductLogic(iblObj, "Update", "ProductListView", ProductsListview.SelectedItem);
                logic.Visibility = Visibility.Visible;
                Close();
            }
            else if (mytype=="client")
            {
                BO.ItemForList itemFor = (BO.ItemForList)ProductsListview.SelectedItem;
                BO.ItemInOrder item = new BO.ItemInOrder();
                try
                {
                   item = new BO.ItemInOrder
                    {
                        ID = cart?.items?.Count ?? throw new NullExeption(ex),
                        ProductId = itemFor.ID,
                        OrderId =0,
                        ProductName = itemFor.Name,
                        Price = itemFor.Price,
                        Amount = 1,
                        TotalPrice = itemFor.Price,
                    };
                }
                catch (Exception ex)
                {

                    MessageBox.Show("id exeption"+ex);
                }

                //BO.Cart cart1 = bl..(itemFor.ID);

                //var myitem = (from element in productList
                //              where element.ID == itemFor.ID
                //              select element);
                //if (myitem != null)
                //{

                //    List<BO.ItemInOrder> itemInOrder = (from element in myitem
                //                                        select new BO.ItemInOrder
                //                                        {
                //                                            ID = item.ID,
                //                                            ProductId = element.ID,
                //                                            ProductName = element.Name,
                //                                            OrderId= item.OrderId,
                //                                            Price = item.Price,
                //                                            Amount = 1,
                //                                            TotalPrice = item.Price,
                //                                        }).ToList();
              
                BO.Item item2 = bl.Item.ProductDetailsRequestM(itemFor.ID);
                //List<BO.ItemInOrder> myitem = (from element in cart.Items
                //              where element.ProductId == itemFor.ID
                //              select element).ToList();
                //if (myitem.Count()>0)
                //{
                //    if (myitem.Count()==1)
                //    {
                //        bl.Cart.QuantityUpdate(cart, itemFor.ID, myitem[0].Amount + 1);
                //    }
                //    else
                //    {
                //        myitem.ForEach(element =>
                //        {
                //            bl.Cart.QuantityUpdate(cart, itemFor.ID, element.Amount + 1);
                //        });
                //    }
                //}
                //else
                //{
                //    cart?.Items?.Add(item);
                //}

                //item.Price = itemFor.Price;
                //item.ProductName = itemFor.Name ?? throw new NullExeption(ex);
                //item.ProductId = itemFor.ID;
                ////if(itemFor.ID)
                //item.Amount = 1;
                //item.TotalPrice = itemFor.Price;
                //cart.Items.Add(item);
                ProductLogic productLogic = new ProductLogic(bl, "acart", "Cart", item2, cart);
                productLogic.Visibility = Visibility.Visible;   
                Close();
            }
        }

       

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            Windows.Orders.orderListView order = new Windows.Orders.orderListView("ProductListView");
            order.Visibility = Visibility.Visible;
            Close();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
                    MainWindow MainWindow = new MainWindow();
                    MainWindow.Visibility = Visibility.Visible;
                    Close();
        }

        private void ProductsListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ProductsListview_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void btnGoToCart_Click(object sender, RoutedEventArgs e)
        {
            WCart wcart = new WCart(cart);
            wcart.Visibility = Visibility.Visible;
            Close();
        }

        private void btnOrderTracking_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    public class CheckCartBtnVisibility : IValueConverter
    {


        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {

            Visibility s = (Visibility)value;
            if (s == Visibility.Visible)
            {
                return Visibility.Hidden;
            }
            else
            {

                return Visibility.Visible;

            }
        }
        public object ConvertBack(
        object value,
        Type targetType,
        object parameter,

        CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

