using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfApp_BackgroundWorker_3
{
    public enum RunState
    {
        Idle,
        Running,
        Cancelling
    }
    public partial class MainWindow : Window
    {
        private BackgroundWorker worker1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            // Changing on GUI
            textBox2.Text = "";
            runButton.Content = "Cancel";
            textBox1.IsEnabled = false;

            // BackgroundWorker class

            worker1 = new BackgroundWorker();
            worker1.WorkerReportsProgress = true;
            worker1.WorkerSupportsCancellation = true;

            // public event DoWorkEventHandler? DoWork;
            // public event ProgressChangedEventHandler? ProgressChanged; 
            // public event RunWorkerCompletedEventHandler? RunWorkerCompleted; 
            // these are events, their event-handlers (methods) should match their delegate types.
            // public delegate void DoWorkEventHandler(object? sender, DoWorkEventArgs e);
            // public delegate void RunWorkerCompletedEventHandler(object? sender, RunWorkerCompletedEventArgs e);
            // public delegate void ProgressChangedEventHandler(object? sender, ProgressChangedEventArgs e);

            worker1.DoWork += DoWork1;
            worker1.RunWorkerCompleted += RunWorkerCompleted1;
            worker1.ProgressChanged += ProgressChanged1;

            // Input from GUI
            string inputArg = textBox1.Text;

            worker1.RunWorkerAsync(inputArg);

        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            worker1.CancelAsync();
        }

        // Important if you leave "private void DoWork1()" you get follows,
        // No overload for DoWork1 mathces the delegate "DoWorkEventHandler",
        // event-handlers (methods) should match their event delegate type

        // DoWork1 event-handler runs on worker thread
        private void DoWork1(object? sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            string inputArg = e.Argument as string;

            List<string> list = new List<string>();

            string doWork;

            for(int i = 0; i < 10; i++)
            {
                // check cancellation
                if (worker.CancellationPending)
                {
                    break;
                }

                doWork = "DoWork1 => " + inputArg + "\n";

                list.Add(doWork);

                // ReportProgress has two overload method.
                worker.ReportProgress(10 * (i + 1), doWork);
                Thread.Sleep(500);
            }

            e.Result = list;
        }

        private void ProgressChanged1(object? sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

            textBox2.Text += e.UserState as string;
        }

        private void RunWorkerCompleted1(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                List<string> list = e.Result as List<string>;

                foreach(var item in list)
                {
                    textBox3.Text += item;
                }
            }
        }

    }
}
