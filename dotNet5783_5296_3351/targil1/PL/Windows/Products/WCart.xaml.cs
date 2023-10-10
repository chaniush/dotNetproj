using BlApi;
using BO;
using PL.Windows.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Windows
{
    /// <summary>
    /// Interaction logic for Cart.xaml
    /// </summary>
    public partial class WCart : Window
    {
        private IBl? bl = BlApi.Factory.Get();
        private Cart myCart;
        public WCart(Cart cart)
        {
            InitializeComponent();
            lstCart.ItemsSource=cart.Items;
            myCart = cart;
        }

        private void lstCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProductLogic productView = new ProductLogic(bl, "cart", "Cart",lstCart.SelectedItem,  myCart);
            productView.Visibility = Visibility.Visible;
            Close();
        }

        private void btnOrder_click(object sender, RoutedEventArgs e)
        {
            Windows.Orders.UserDetails userDetails = new Windows.Orders.UserDetails(myCart);
            userDetails.Show();
            Close();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            ProductListView p=new ProductListView("client");
            Close();
            p.Visibility = Visibility.Visible;
        }
    }
}
