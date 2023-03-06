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

namespace WpfApp_BackgroundWorker_2
{
    public enum RunState
    {
        Idle,
        Running,
        Cancelling,
    }
    public partial class MainWindow : Window
    {
        private RunState runState = RunState.Idle;

        private BackgroundWorker worker1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {

            if (runState == RunState.Idle)
            {
                // Changing on GUI
                textBox2.Text = ""; // reset the input argument textBox
                textBox1.IsEnabled = false;
                runButton.Content = "Cancel";

                runState = RunState.Running;

                // BackgrounWorker class object
                worker1 = new BackgroundWorker();
                worker1.WorkerReportsProgress = true;

                // public event DoWorkEventHandler? DoWork;
                // public event ProgressChangedEventHandler? ProgressChanged; 
                // public event RunWorkerCompletedEventHandler? RunWorkerCompleted; 
                // these are events, their event-handlers (methods) should match their delegate types.
                // public delegate void DoWorkEventHandler(object? sender, DoWorkEventArgs e);
                // public delegate void RunWorkerCompletedEventHandler(object? sender, RunWorkerCompletedEventArgs e);
                // public delegate void ProgressChangedEventHandler(object? sender, ProgressChangedEventArgs e);

                worker1.WorkerSupportsCancellation = true;
                worker1.DoWork += DoWork1; // Occurs when BackgroundWorker.RunWorkerAsync() is called.
                worker1.DoWork += DoWork2;
                worker1.RunWorkerCompleted += RunWorkerCompleted1; // Occurs when the background operation has completed, has been cancelled, or has raised an exception
                worker1.RunWorkerCompleted += RunWorkerCompleted2;

                // Take input from input argument textBox
                int inputArg = 11;
                int.TryParse(textBox1.Text, out inputArg);

                worker1.RunWorkerAsync(inputArg);

            }
            else if (runState == RunState.Running)
            {

            }
        }

        // Important if you leave "private void DoWork1()" you get follows,
        // No overload for DoWork1 mathces the delegate "DoWorkEventHandler",
        // event-handlers (methods) should match their event delegate type

        // DoWork1 and DoWork2
        private void DoWork1(object? sender, DoWorkEventArgs e)
        {
            string message = "1 \nCurrent Thread => " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n" +
                "input => " + e.Argument.ToString();

            MessageBox.Show(message);
        }

        private void DoWork2(object? sender, DoWorkEventArgs e)
        {
            string message = "2 \nCurrent Thread => " + Thread.CurrentThread.ManagedThreadId.ToString() + "\n" +
                "input => " + e.Argument.ToString();

            MessageBox.Show(message);
        }

        private void RunWorkerCompleted1(object? sender, RunWorkerCompletedEventArgs e)
        {
            string message = "3 \nCurrent Thread => " + Thread.CurrentThread.ManagedThreadId.ToString();

            MessageBox.Show(message);

            textBox2.Text = "RunWorkerCompleted1 done on Thread Id: " + Thread.CurrentThread.ManagedThreadId.ToString();
        }

        private void RunWorkerCompleted2(object? sender, RunWorkerCompletedEventArgs e)
        {
            string message = "4 \nCurrent Thread => " + Thread.CurrentThread.ManagedThreadId.ToString();

            MessageBox.Show(message);

            textBox3.Text = "RunWorkerCompleted2 done on Thread Id: " + Thread.CurrentThread.ManagedThreadId.ToString();
        }
    }
}
