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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using MessageBox = System.Windows.MessageBox;

namespace PL.Windows.Products
{
    /// <summary>
    /// Interaction logic for ProductLogic.xaml
    /// </summary>
    public partial class ProductLogic : Window
    {
        public static readonly Random nums = new Random();
        private IBl bl = new BlImplementation.Bl();
        private string state = "";
        private static int publicId;
        private static Cart? Cart;
        public string? canGoBack;
        object? myProduct;
        public ProductLogic(IBl IblObj, string state, string? goBack = null, object? product = null, Cart? myCart = null)
        {
            InitializeComponent();
            Cart = myCart;
            canGoBack = goBack;
            //System.Windows.Controls.Button bb = new System.Windows.Controls.Button();
            this.state = state;
            myProduct = product;
            cmbCategory.ItemsSource = Enum.GetValues(typeof(BO.Category));
            cmbColor.ItemsSource = Enum.GetValues(typeof(BO.Color));
            BO.Item? item = new BO.Item();
            ItemInOrder itemInOrder = new ItemInOrder();
            BO.Order myOrder = new BO.Order();
            Exception ex = new Exception();

            if (state == "Update")
            {
                logictext.Text = "Update";
                btnSave.Content = "Update";
                lblAmount.Visibility = Visibility.Hidden;
                txtAmount.Visibility = Visibility.Hidden;
                lblInStock.Visibility = Visibility.Visible;
                txtInStock.Visibility = Visibility.Visible;
                btnRemove.Visibility = Visibility.Hidden;
                btnAddCart.Visibility = Visibility.Hidden;
                txtID.IsEnabled = true;
                item = product != null ? bl.Item.ProductDetailsRequestM(((ItemForList)product).ID) : null;
                try
                {
                    publicId = item?.ID ?? throw new NullExeption(ex);

                }
                catch (NullExeption e)
                {

                    Console.WriteLine(e);
                }
                txtID.Text = Convert.ToString(publicId);
                try
                {
                    txtName.Text = item?.Name?? throw new NullExeption(ex);
                    cmbCategory.SelectedItem = item.Category;
                    cmbColor.Text = Convert.ToString(item.Color);
                    txtPrice.Text = Convert.ToString(item.Price);
                    txtInStock.Text = Convert.ToString(item.InStock);

                }
                catch (NullExeption e)
                {

                    Console.WriteLine(e);
                }

                this.DataContext = item;
               
            }
            else   if (state == "Add")
            {
                logictext.Text = "Add";
                btnSave.Content = "Add";
                lblAmount.Visibility = Visibility.Hidden;
                txtAmount.Visibility = Visibility.Hidden;
                lblInStock.Visibility = Visibility.Visible;
                txtInStock.Visibility = Visibility.Visible;
                btnRemove.Visibility = Visibility.Hidden;
                btnAddCart.Visibility = Visibility.Hidden;
                txtID.IsEnabled = true;
                do
                {           
                    
                    publicId = Convert.ToInt32(nums.Next(1000000, 999999999));
                } while (DoesIdExist());
                txtID.Text = Convert.ToString(publicId);
                txtName.Text = "";
                cmbCategory.Text = "None";
                cmbColor.Text = "None";
                txtPrice.Text = "0";
                txtInStock.Text = "0";
            }
            else if (state == "cart")
            {
                //try
                //{
                //    publicId = item?.ID ?? throw new NullExeption(ex);

                //}
                //catch (NullExeption e)
                //{

                //    Console.WriteLine(e);
                //}
                //lblInStock.Visibility = Visibility.Hidden;
                //txtInStock.Visibility= Visibility.Hidden;
                //cmbCategory.Visibility = Visibility.Hidden;
                //lblCategory.Visibility = Visibility.Visible;
                //cmbColor.Visibility = Visibility.Hidden;
                //btnRemove.Visibility = Visibility.Visible;
                //lblColor.Visibility = Visibility.Hidden;
                //lblAmount.Visibility= Visibility.Visible;
                //txtAmount.Visibility= Visibility.Visible;
                // txtID.IsEnabled=false;
                // txtName.IsEnabled=false;
                //cmbCategory.IsEnabled=false;
                //cmbColor.IsEnabled=false;
                //txtPrice.IsEnabled=false;
                //txtAmount.IsEnabled = true;
                //try
                //{
                // itemInOrder = (ItemInOrder?)product?? throw new NullExeption(ex);
                // itemInOrder =  (ItemInOrder)product;
                //  publicId = itemInOrder?.ProductId ?? throw new NullExeption(ex);
                //txtID.Text = Convert.ToString(publicId);

                // txtName.Text = itemInOrder.ProductName;
                //txtPrice.Text = Convert.ToString(itemInOrder.Price);
                //txtAmount.Text = Convert.ToString(itemInOrder.Amount);
                // }
                // catch (NullExeption e)
                // {
                //
                //     Console.WriteLine(e);
                // }
                // this.DataContext = itemInOrder;
                //txtID.Text = Convert.ToString(publicId);
                
                lblInStock.Visibility = Visibility.Hidden;
                txtInStock.Visibility = Visibility.Hidden;
                cmbCategory.Visibility = Visibility.Hidden;
                lblCategory.Visibility = Visibility.Hidden;
                cmbColor.Visibility = Visibility.Hidden;
                btnRemove.Visibility = Visibility.Visible;
                lblColor.Visibility = Visibility.Hidden;
                lblAmount.Visibility = Visibility.Visible;
                txtAmount.Visibility = Visibility.Visible;
                txtAmount.IsEnabled = true;
                txtID.IsEnabled = false;
                txtName.IsEnabled = false;
                cmbCategory.IsEnabled = false;
                cmbColor.IsEnabled = false;
                txtPrice.IsEnabled = false;
                this.DataContext = myProduct;
                txtName.Text = (myProduct as ItemInOrder).ProductName;
                //try
                //{
                //    //var itemInOrder5 = new
                //    //{
                //    //   Name = "chaga",
                //    //   ID=0,
                //    //   Price=99,
                //    //   Amount=1111,
                //    //};
                //    //this.DataContext = itemInOrder5;

                //    //Exception ex = new Exception();
                //    //itemInOrder = (ItemInOrder?)product ?? throw new NullExeption(ex);
                //    //itemInOrder = (ItemInOrder)product;
                //    //publicId = itemInOrder?.ProductId ?? throw new NullExeption(ex);
                //    //txtID.Text = Convert.ToString(publicId);
                //    //txtName.Text = itemInOrder.ProductName;
                //    //txtPrice.Text = Convert.ToString(itemInOrder.Price);
                //    //txtAmount.Text = Convert.ToString(itemInOrder.Amount);
                //}
                //catch (NullExeption e)
                //{

                //    Console.WriteLine(e);
                //}
            }
            else if (state == "acart")
            {
                btnSave.Visibility = Visibility.Hidden;
                lblAmount.Visibility = Visibility.Hidden;
                txtAmount.Visibility = Visibility.Hidden;
                // ItemInOrder itemInOrder1 = (ItemInOrder?)myProduct?? throw new NullExeption(ex);
                //lblName.Content = "ProductName:";
                //txtName.Text = itemInOrder1?.ProductName;
                //lblCategory.Content = "ProductID:";
                //cmbCategory.Visibility = Visibility.Hidden;
                //txtProductId.Visibility = Visibility.Visible;
                //this.DataContext = itemInOrder1;
                //publicId = itemInOrder1?.ProductId?? throw new NullExeption(ex);
                this.DataContext = product;
                do
                {

                    publicId = ((BO.Item)product).ID;
                } while (!DoesIdExist());
            }
        }
        public bool DoesIdExist()
        {
            bool check = false;
            IEnumerable<ItemForList> items = new List<ItemForList>();
            items = bl.Item.ProductListRequest();
            var myid = (from item in items where publicId == item.ID select item).FirstOrDefault();
            if (myid!=null)
            {
                check = true;
            }
            //for (int i = 0; i < items.Count; i++)
            //{
            //    if (publicId == items[i].ID)
            //    {
            //        check = true;
            //        break;
            //    }
            //}
            if (check)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (state == "cart")
            {
                if (txtAmount.Text == ""||Convert.ToInt32(txtAmount.Text)<=0)
                    System.Windows.Forms.MessageBox.Show("amount is not valid");
                else
                {
                    bl.Cart.QuantityUpdate(Cart, publicId, Convert.ToInt32(txtAmount.Text));
                    Close();
                    ProductListView productListView = new ProductListView("client");
                    productListView.Show();
                }
            }
            else
            {
                BO.Item item = new BO.Item();
                if (txtID.Text.Length > 11 || publicId != Convert.ToInt32(txtID.Text))
                {
                    if (txtID.Text.Length > 11)
                    {
                        System.Windows.Forms.MessageBox.Show("id is to long");
                    }
                    if (publicId != Convert.ToInt32(txtID.Text))
                    {
                        System.Windows.Forms.MessageBox.Show("id cannot be changed");
                        txtID.Text = Convert.ToString(publicId);
                    }
                }
                else
                {
                    if (txtID.Text != "" && txtName.Text != "" && txtPrice.Text != "" && txtInStock.Text != ""&&txtName.Text != "" && txtPrice.Text != "0" )
                    {
                        item.ID = Convert.ToInt32(txtID.Text);
                        item.Name = txtName.Text;
                        item.Category = (BO.Category)cmbCategory.SelectedItem;
                        item.Color = (BO.Color)cmbColor.SelectedItem;
                        item.Price = Convert.ToDouble(txtPrice.Text);
                        item.InStock = Convert.ToInt32(txtInStock.Text);
                        try
                        {
                            if (state == "Add")
                            {
                                bl.Item.Add(item);
                                System.Windows.Forms.MessageBox.Show("Added successfully");
                                Windows.Products.ProductListView product = new Windows.Products.ProductListView("manager");
                                Close();
                                product.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                logictext.Text = "Update";
                                btnSave.Content = "Update";
                                bl.Item.Update(item);
                                System.Windows.Forms.MessageBox.Show("Updated successfully");
                                Windows.Products.ProductListView product = new Windows.Products.ProductListView("manager");
                                Close();
                                product.Visibility = Visibility.Visible;
                            }
                        }
                        catch (Exception err)
                        {

                            System.Windows.Forms.MessageBox.Show("error happened: \n" + err);
                            Windows.Products.ProductListView product = new Windows.Products.ProductListView("manager");
                            Close();
                            product.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("you didnt fill all the details");
                    }
                }
            }  
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            string message = "Are you sure you want to remove the item from the cart?";
            string title = "Message Window";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = System.Windows.Forms.MessageBox.Show(message, title, buttons);
            WCart c = new WCart(Cart);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                Close();
                 bl.Cart.QuantityUpdate(Cart, publicId, 0);
                c.Visibility = Visibility.Visible;
            }
            else
            {
                Close();
                c.Visibility = Visibility.Visible;

            }
        }

        private void btnAddCart_Click(object sender, RoutedEventArgs e)
        {
            Exception ex = new Exception();

            BO.Item itemFor = bl.Item.ProductDetailsRequestM(publicId);

            List<BO.ItemInOrder> myitem = (from element in Cart.Items
                                           where element.ProductId == publicId
                                           select element).ToList();
            if (myitem.Count() > 0)
            {
                if (myitem.Count() == 1)
                {
                    bl.Cart.QuantityUpdate(Cart, publicId, myitem[0].Amount + 1);
                }
                else
                {
                    myitem.ForEach(element =>
                    {
                        bl.Cart.QuantityUpdate(Cart, publicId, element.Amount + 1);
                    });
                }
            }
            else
            {

                BO.ItemInOrder item = new BO.ItemInOrder();
                item = new BO.ItemInOrder
                {
                    ID = Cart?.items?.Count ?? throw new NullExeption(ex),
                    ProductId = itemFor.ID,
                    OrderId = 0,
                    ProductName = itemFor.Name,
                    Price = itemFor.Price,
                    Amount = 1,
                    TotalPrice = itemFor.Price,
                };
                //Cart?.Items?.Add(item);
            }
            try
            {
                bl.Cart.Add(Cart, publicId);

            }
            catch (Exception )
            {

                System.Windows.Forms.MessageBox.Show("error: item is out of stock. cannot increace amount. ");
            }
            Close();
            ProductListView productListView = new ProductListView("client");
            productListView.Show();
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            if (canGoBack == "ProductListView")
            {
                ProductListView orderListView = new ProductListView("manager");
                orderListView.Visibility = Visibility.Visible;
                Close();
            }
            else
            {
                if (canGoBack == "Cart")
                {
                    Windows.Products.ProductListView productsListView = new Windows.Products.ProductListView("client");
                   //MainWindow MainWindow = new ();
                    productsListView.Visibility = Visibility.Visible;
                    Close();
                }
                else
                {
                    if (canGoBack == "userCart")
                    {
                        WCart c = new WCart(Cart);
                        Close();
                        c.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        Windows.Products.ProductListView productsListView = new Windows.Products.ProductListView("manager");
                        productsListView.Visibility = Visibility.Visible;
                        Close();
                        // MessageBox.Show("cannot go back (product logic line 300)");
                    }
                }
            }


           
        }
    }
    public class CheckAddCartBtnVisibility : IValueConverter
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
