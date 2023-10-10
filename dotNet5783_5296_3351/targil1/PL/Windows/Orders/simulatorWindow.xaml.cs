using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Simulator;
using static System.Net.Mime.MediaTypeNames;

namespace PL.Windows.Orders
{
    /// <summary>
    /// Interaction logic for simulatorWindow.xaml
    /// </summary>
    public partial class simulatorWindow : Window
    {
       
        BackgroundWorker worker = new();
        private Stopwatch stopWatch;
         private  bool isTimerRun=true;
        private bool myE=true;

        const uint MF_GRAYED = 0x00000001;
        const uint MF_ENABLED = 0x00000000;
        const uint SC_CLOSE = 0xF060;
        public simulatorWindow()
        {
            //WindowStyle=WindowStyle.None;
              stopWatch = new Stopwatch();
                   stopWatch.Restart();
            InitializeComponent(); 
         if (isTimerRun)
            {
               // worker.WorkerSupportsCancellation = true;

             
                worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            
                stopWatch.Restart();
              //  isTimerRun = true;
            
            worker.RunWorkerAsync();
          }
        }
       
        public void OrderAndTime(BO.Order order, int num)
        {
            this.Dispatcher.Invoke(() =>
            {
                 txtId.Text =Convert.ToString( order.ID);
                 txtCurrentStatus.Text = Convert.ToString(order.OrderStatus);
                txtStartProccess.Text = Convert.ToString(DateTime.Now.Hour)+":"+ Convert.ToString(DateTime.Now.Minute)+":"+ Convert.ToString(DateTime.Now.Second);
                  txtEndingProccess.Text=Convert.ToString(Convert.ToDateTime(txtStartProccess.Text).AddSeconds(num/1000).ToString("HH:mm:ss"));
                if ( txtNextStatus.Text!=Convert.ToString(order.OrderStatus-1))
                {
                    txtNextStatus.Text = "";
                }


            });
            //הדפסה של הפרטים
        }
        public void PrevNextStatus(BO.Status prev, BO.Status? next=null)
        {
            this.Dispatcher.Invoke(() =>
            {
                txtCurrentStatus.Text = Convert.ToString(prev);
                txtNextStatus.Text = Convert.ToString(next);

            });
           //הדפסה של הפרטים
        }
        public void IsIdNull(int? id)
        {
            this.Dispatcher.Invoke(() =>
            {
                
                if (!worker.CancellationPending)
            {
             
                            StopProgress_Click(this,null);
                
            }
                OrderArrivedLabel.Visibility = Visibility.Visible;
                
                
            });


        }
     

            private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            while (isTimerRun)
            {
                if (worker.CancellationPending&&!isTimerRun)
                {
                    e.Cancel = true;
                    return;
                }
                Simulator.Simulator.StatusChanged1 += OrderAndTime;
            Simulator.Simulator.StatusChanged2 += PrevNextStatus;
            Simulator.Simulator.StatusChanged3 += IsIdNull;
           
            Simulator.Simulator.run();
                this.Dispatcher.Invoke(() =>
            {
                worker.ReportProgress(1);
            });
                Thread.Sleep(1000);
              
            }

        }
       
       
        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {

            if (worker.CancellationPending)
            {
                Close();
            }
        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
           
            string timerText = stopWatch.Elapsed.ToString();
               timerText = timerText.Substring(0, 8);
              this.timerTextBlock.Text = timerText;
        }

        private void StopProgress_Click(object sender, RoutedEventArgs? e)
        {
            this.Dispatcher.Invoke(() =>
            {
                //worker.CancelAsync();
                worker.WorkerReportsProgress = false;
                Simulator.Simulator.Stop();
                isTimerRun = false;
                myE = false;
               // Close();
            });
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = myE;
        }
    }
}
