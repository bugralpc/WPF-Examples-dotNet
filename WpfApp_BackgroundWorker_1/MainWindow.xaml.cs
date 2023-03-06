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

namespace WpfApp_BackgroundWorker_1
{
    public enum RunState
    {
        Idle,
        Running,
        Cancelling,
    }

    public class WorkerArgument
    {
        public int someArg;
        public WorkerArgument(int someArg)
        {
            this.someArg = someArg;
        }
    }

    public class WorkerResult
    {
        public List<string> results = new List<string>();
        public bool isCancelled = false;
    }

    public partial class MainWindow : Window
    {
        private RunState runState { get; set; } = RunState.Idle;

        private BackgroundWorker worker;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunButtonClick(object sender, RoutedEventArgs e)
        {
            if (runState == RunState.Idle)
            {
                // changing on GUI 
                textBox2.Text = "";
                runButton.Content = "Cancel";
                textBox1.IsEnabled = false;
                runState = RunState.Running;

                // BackgroundWorker

                worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.ProgressChanged += Worker_ProgressChanged;

                // inputs from GUI
                int arg = 0;
                int.TryParse(textBox1.Text, out arg);

                WorkerArgument workerArgument = new WorkerArgument(arg);

                // run worker
                worker.RunWorkerAsync(workerArgument);
            }
            else if (runState == RunState.Running)
            {
                runButton.Content = "Cancelling...";
                runButton.IsEnabled = false;
                runState = RunState.Cancelling;

                // signal cancellation to worker
                worker.CancelAsync();
            }
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            // GUI
            runState = RunState.Idle;
            runButton.Content = "Run";
            runButton.IsEnabled = true;
            textBox1.IsEnabled = true;

            // process results
            if (e.Result != null)
            {
                if ((e.Result as WorkerResult).isCancelled)
                {
                    textBox2.Text = "Cancelled result: ";
                }
                else
                {
                    textBox2.Text = "Completed result: ";
                }
            }
            else
            {
                textBox2.Text = "No result!";
            }
        }


        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            if (e.UserState != null)
            {
                textBox2.Text = "Running result: ";
                PrintResults(e.UserState as WorkerResult);
            }
        }

        private void PrintResults(WorkerResult workerResult)
        {
            foreach(var l in workerResult.results)
            {
                textBox2.Text += l + " ";
            }
        }


        // this method runs in worker thread
        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            WorkerArgument workerArgument = e.Argument as WorkerArgument;

            // initialize 
            WorkerResult workerResult = new WorkerResult();

            worker.ReportProgress(0);

            // Do work in loop
            for(int i = 0; i < 10; i++)
            {
                // check cancellation
                if (worker.CancellationPending)
                {
                    workerResult.isCancelled = true;
                    break;
                }

                Thread.Sleep(2000);
                workerResult.results.Add("Result " + (workerArgument.someArg + i));

                worker.ReportProgress(10 * (i + 1), workerResult);
            }

            e.Result = workerResult;

        }
    }
}
