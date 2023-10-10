using BlApi;
using BO;
using DalApi;
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

namespace PL.Windows.Orders
{
    /// <summary>
    /// Interaction logic for orderListView.xaml
    /// </summary>
    public partial class orderListView : Window
    {
      static  Exception e =new Exception();
        private IBl bl = BlApi.Factory.Get() ?? throw new NullExeption(e);
        private BlImplementation.Bl iblObj = new BlImplementation.Bl();
        public string? canGoBack;

        public orderListView(string? goBack = null)
        {
            InitializeComponent();
            IEnumerable<OrderForList> ords = new List<OrderForList>();
            ords = bl.Order.OrderListRequest();
            OrdersListview.ItemsSource = ords;
            canGoBack = goBack;
        }


        private void OrdersListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Windows.Orders.OrderLogic logic = new Windows.Orders.OrderLogic(iblObj, "Update", "orderListView", OrdersListview.SelectedItem,  "OrderList");
            this.Visibility = Visibility.Hidden;
            logic.Visibility = Visibility.Visible;
        }

        private void btnProduct_Click(object sender, RoutedEventArgs e)
        {
            Windows.Products.ProductListView myitem = new Windows.Products.ProductListView("manager");
            myitem.Visibility = Visibility.Visible;
            Close();
        }

        private void OrdersListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Visibility = Visibility.Visible;
            Close();



        }

        private void btnOrderTracking_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
