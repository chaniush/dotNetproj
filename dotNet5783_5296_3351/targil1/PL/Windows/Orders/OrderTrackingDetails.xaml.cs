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
using BlImplementation;
using static System.Windows.Forms.AxHost;

namespace PL.Windows.Orders
{
    /// <summary>
    /// Interaction logic for OrderTrackingDetails.xaml
    /// </summary>
    public partial class OrderTrackingDetails : Window
    {
        private IBl bl = new BlImplementation.Bl();
        private BO.OrderTracking orderTrackingBack=new();
       private BO.Order? myOrderBack = null;
        public OrderTrackingDetails(Order order, BO.OrderTracking? orderTracking=null, BO.Order? myOrder = null)
        {
            InitializeComponent();
                    Exception ex = new Exception();
            orderTrackingBack = orderTracking??new();
            myOrderBack = myOrder;
                    cmbOrderStatus.ItemsSource = Enum.GetValues(typeof(BO.Status));
                    this.DataContext = order;
                order?.Items.ToList().ForEach(item =>
                    {
                        item.ProductName = bl?.Item.ProductDetailsRequestM(item.ProductId)?.Name ?? throw new NullExeption(ex);
                    });
                    dataGrid.ItemsSource = order?.Items;
                

            
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            Windows.Orders.OrderTracking ordt = new(orderTrackingBack, myOrderBack);
            ordt.Visibility = Visibility.Visible;
            Close();
        }
    }
}
