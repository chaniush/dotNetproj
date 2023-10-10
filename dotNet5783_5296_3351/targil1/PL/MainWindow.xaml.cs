using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
using BlImplementation;
using BO;
using DalApi;
using PL.Windows.Orders;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       static Exception ex = new Exception();
       // private Stopwatch stopWatch;
       // private bool isTimerRun;
       // BackgroundWorker timerworker;
        private IBl bl= BlApi.Factory.Get() ?? throw new NullExeption(ex);

       
        public MainWindow()
        {

            InitializeComponent();
           
            //stopWatch = new Stopwatch();
            //timerworker = new BackgroundWorker();
            //timerworker.DoWork += Worker_DoWork;
            //timerworker.ProgressChanged += Worker_ProgressChanged;
            //timerworker.WorkerReportsProgress = true;

        }
       
        //private void startTimerButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!isTimerRun)
        //    {
        //        stopWatch.Restart();
        //        isTimerRun = true;
        //        timerworker.RunWorkerAsync();
        //    }
        //}
        //private void stopTimerButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (isTimerRun)
        //    {
        //        stopWatch.Stop();
        //        isTimerRun = false;
        //    }
        //}
        //private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    string timerText = stopWatch.Elapsed.ToString();
        //    timerText = timerText.Substring(0, 8);
        //    this.timerTextBlock.Text = timerText;
        //}
        //private void Worker_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    while (isTimerRun)
        //    {
        //        timerworker.ReportProgress(1);
        //        Thread.Sleep(1000);
        //    }
        //}


        private void AttributeSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ManagerButton_Click(object sender, RoutedEventArgs e)
        {
            Windows.Products.ProductListView product = new Windows.Products.ProductListView("manager");
            this.Visibility = Visibility.Hidden;
            product.Visibility = Visibility.Visible;
        }
        private void ItemListWindowButton_Click(object sender, RoutedEventArgs e) => new Windows.Products.ProductListView("manager").Visibility = Visibility.Visible;

        private void client_Click(object sender, RoutedEventArgs e)
        {
            Windows.Products.ProductListView product = new Windows.Products.ProductListView("client");
            this.Visibility = Visibility.Hidden;
            product.Visibility = Visibility.Visible;
        }

        private void StartProgress_Click(object sender, RoutedEventArgs e)
        {
            simulatorWindow s= new simulatorWindow();
            s.Show();
        }
    }
}
