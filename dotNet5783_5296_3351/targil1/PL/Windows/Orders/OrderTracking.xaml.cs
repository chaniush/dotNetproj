using BlApi;
using BO;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Windows.Navigation;

namespace PL.Windows.Orders
{
    /// <summary>
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {
        private IBl bl = new BlImplementation.Bl();
        private BO.OrderTracking myOrderTracking;
        private BO.Order? myBackOrder;
        public OrderTracking(BO.OrderTracking orderTracking, BO.Order? myOrder=null)
        {
            InitializeComponent();
            myBackOrder = myOrder;
            myOrderTracking = orderTracking;
            listTrack.ItemsSource = (System.Collections.IEnumerable?)orderTracking.values;
            lbID.Content = orderTracking.ID;
            lbStatus.Content = orderTracking.OrderStatus;
        }
        private void btnOrderTrackingDetails_Click(object sender, RoutedEventArgs e)
        {
         Order order=bl.Order.OrderDetailsRequestM(myOrderTracking.ID);
            Windows.Orders.OrderTrackingDetails ord = new(order, myOrderTracking, myBackOrder);
            Close();
            ord.Visibility = Visibility.Visible;
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
           
                Windows.Orders.OrderLogic ordt = new(bl,"orderTracking", "orderListView" ,myBackOrder);
                ordt.Visibility = Visibility.Visible;
            Close();
            
        }
    }
}
