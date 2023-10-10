using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Window
    {
        private IBl? bl = BlApi.Factory.Get();
        BO.Cart myCart;
        public UserDetails(BO.Cart cart)
        {
            InitializeComponent();
            myCart = cart;
        }

        private void btnOk_click(object sender, RoutedEventArgs e)
        {
            myCart.CustomerEmail=txtEmail.Text;
            string expression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

            if (!Regex.IsMatch(myCart.CustomerEmail, expression))
            {
                System.Windows.Forms.MessageBox.Show("email not valid.try again");
                txtEmail.Text = "@gmail.com";
            }
            else
            {
                myCart.CustomerName = txtName.Text;
                myCart.CustomerAddres = txtAddress.Text;
                try
                {
                    bl?.Cart.OrderConfirmation(myCart);
                    System.Windows.Forms.MessageBox.Show("The order has been received and we will contact you when it is ready");
                    Close();
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Visibility = Visibility.Visible;
                }
                catch (Exception ex)
                {

                    System.Windows.Forms.MessageBox.Show("cannot confirm order. try again.\n " + ex.Message);
                }
            }
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            WCart c = new WCart(myCart);
            c.Visibility = Visibility.Visible;
            Close();
        }
    }
}
