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
    /// Interaction logic for OrderLogic.xaml
    /// </summary>
    public partial class OrderLogic : Window
    {
        public static readonly Random nums = new Random();
        private IBl bl = new BlImplementation.Bl();
        private string state = "";
        public string ?canGoBack;
        public BO.Order? myOrder = new BO.Order();
        
            public OrderLogic(IBl IblObj, string state,string? goBack=null, object? product=null,  string? kindOfObject = null)
        {
            InitializeComponent();
            this.state = state;
            canGoBack = goBack;
            if (state == "Update")
            {
                logictext.Text = "Update";
              
                myOrder = product != null ? bl.Order.OrderDetailsRequestM(((OrderForList)product).ID) : null;
                try
                {
                    Exception ex = new Exception();
                    cmbOrderStatus.ItemsSource = Enum.GetValues(typeof(BO.Status));
                    this.DataContext = myOrder;
                    myOrder?.Items.ToList().ForEach(item =>
                    {
                        item.ProductName = bl?.Item.ProductDetailsRequestM(item.ProductId)?.Name ?? throw new NullExeption(ex);
                    });
                    dataGrid.ItemsSource = myOrder?.Items;
                }
                catch (NullExeption e)
                {
                    System.Windows.Forms.MessageBox.Show(e+"error");
                }
               
            }
            else
            {
                cmbOrderStatus.ItemsSource = Enum.GetValues(typeof(BO.Status));
                this.DataContext = product;
                dataGrid.ItemsSource =((BO.Order?) product)?.Items;

            }
        }
       
        private void UpdateShipDate_Click(object sender, RoutedEventArgs e)
        {
            BO.Order Order = new BO.Order();
            Order.ID = Convert.ToInt32(txtID.Text);
            Order= bl.Order.ShippingUpdate(Order.ID);
            bl.Order.OrderUpdate(Order);
            Windows.Orders.orderListView orderListView = new Windows.Orders.orderListView();
            orderListView.OrdersListview.ItemsSource = bl.Order.OrderListRequest();
            this.DataContext = Order;
        }
        private void UpdateDeliveryDate_Click(object sender, RoutedEventArgs e)
        {
            BO.Order Order = new BO.Order();
            Order.ID = Convert.ToInt32(txtID.Text);
            Order =bl.Order.DeliveryUpdate(Order.ID);
            bl.Order.OrderUpdate(Order);
            Windows.Orders.orderListView orderListView = new Windows.Orders.orderListView();
            orderListView.OrdersListview.ItemsSource = bl.Order.OrderListRequest();
            this.DataContext = Order;
        }
        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
           
             if (canGoBack== "orderListView")
             {
               Windows.Orders.orderListView orderListView = new Windows.Orders.orderListView();
               orderListView.Visibility = Visibility.Visible;
                Close();
             }
            else
            {
                MessageBox.Show("cannot go back (order logic line 215)");
            }
            
            
            

        }

        private void btnOrderTracking_Click(object sender, RoutedEventArgs e)
        {
            BO.OrderTracking orderTracking = bl.Order.OrderTracking(Convert.ToInt32(txtID.Text));
            Close();
            Windows.Orders.OrderTracking ordt = new(orderTracking, myOrder);
            ordt.Visibility = Visibility.Visible;
        }
    }
    public class CheckShipVisibility : IValueConverter
    {


        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {

           string Status = (string)value;
            if (Status== "APPROVED")
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
    public class CheckDeliveryVisibility : IValueConverter
    {


        public object Convert(
        object value,
        Type targetType,
        object parameter,
        CultureInfo culture)
        {

            string Status = (string)value;
            if (Status == "APPROVED"|| Status == "SENT")
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
