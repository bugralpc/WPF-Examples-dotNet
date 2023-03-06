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

namespace WpfApp_BackgroundWorker_4
{
    public enum RunState
    {
        Idle,
        Running,
        Cancelling
    }

    public class WorkerArgument
    {
        public string name;
        public WorkerArgument(string name)
        {
            this.name = name;
        }
    }

    public class WorkerResult
    {
        public List<string> results = new List<string>();
        public bool isCancelled = false;
    }

    public partial class MainWindow : Window
    {
        private BackgroundWorker worker1;

        private RunState runState = RunState.Idle;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            if (runState == RunState.Idle)
            {
                // Changing on UI
                textBox2.Text = "";
                textBox3.Text = "";
                textBox1.IsEnabled = false;
                runState = RunState.Running;
                button1.Content = "Cancel";

                // BackgroundWorker Class
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

                // Input from UI
                string inputArg = textBox1.Text;

                WorkerArgument workerArgument = new WorkerArgument(inputArg);

                worker1.RunWorkerAsync(workerArgument);

            }
            else if (runState == RunState.Running)
            {
                button1.Content = "Cancelling...";
                button1.IsEnabled = false;
                runState = RunState.Cancelling;

                // Signal cancellation to worker
                worker1.CancelAsync();
            }

        }

        // Important if you leave "private void DoWork1()" you get follows,
        // No overload for DoWork1 mathces the delegate "DoWorkEventHandler",
        // event-handlers (methods) should match their event delegate type

        // DoWork1 event-handler runs on worker thread
        private void DoWork1(object? sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker1 = sender as BackgroundWorker;

            WorkerArgument workerArgument = e.Argument as WorkerArgument;

            // initialize WorkerResult object
            WorkerResult workerResult = new WorkerResult();

            string doWork;

            for (int i = 0; i < 10; i++)
            {
                if (worker1.CancellationPending)
                {
                    workerResult.isCancelled = true;
                    break;
                }

                Thread.Sleep(500);

                doWork = "DoWork1 => " + workerArgument.name + "\n";
                workerResult.results.Add(doWork);

                // worker1.ReportProgess(10 * (i + 1), doWork); // AREA1
                worker1.ReportProgress(10 * (i + 1), workerResult); // AREA2
            }

            e.Result = workerResult;
        }

        private void ProgressChanged1(object? sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

            //textBox2.Text += e.UserState as string; // AREA1

            WorkerResult workerResult = e.UserState as WorkerResult; //AREA2

            if (e.UserState != null)
            {
                textBox2.Text = "Running results: \n";

                foreach(var item in workerResult.results)
                {
                    textBox2.Text += item;
                }
            }

        }

        private void RunWorkerCompleted1(object? sender, RunWorkerCompletedEventArgs e)
        {
            // Changin on UI
            textBox1.IsEnabled = true;
            button1.Content = "Run";
            runState = RunState.Idle;

            WorkerResult workerResult = e.Result as WorkerResult;

            if (e.Result != null)
            {
                if (workerResult.isCancelled)
                {
                    Thread.Sleep(500);

                    progressBar1.Value = 0;

                    textBox3.Text = "Cancelled";

                    runState = RunState.Idle;
                    button1.IsEnabled = true;
                }
                else
                {
                    textBox3.Text = "Background Work done. \n";
                    foreach(var item in workerResult.results)
                    {
                        textBox3.Text += item;
                    }
                }
            }
            else
            {
                textBox3.Text = "No results";
            }
        }

    }
}
