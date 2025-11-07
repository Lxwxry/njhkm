using System;
using System.Threading;
using System.Windows;

namespace ThreadPriorityDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartProgress_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                for (int i = 0; i <= 100; i++)
                {
                    Dispatcher.Invoke(() => Progress.Value = i);
                    Thread.Sleep(50);
                }
                Dispatcher.Invoke(() => StatusText.Text = "Progress complete");
            });
            thread.Start();
        }

        private void StartPriority_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                Dispatcher.Invoke(() => StatusText.Text = "Thread started with normal priority");
                Thread.Sleep(1000);
                Thread.CurrentThread.Priority = ThreadPriority.Highest;
                Dispatcher.Invoke(() => StatusText.Text = "Priority changed to Highest");
                Thread.Sleep(1000);
                Dispatcher.Invoke(() => StatusText.Text = "Thread finished");
            });
            thread.Priority = ThreadPriority.Normal;
            thread.Start();
        }

        private void StartFail_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                try
                {
                    Progress.Value = 50;
                }
                catch (Exception ex)
                {
                    Dispatcher.Invoke(() => StatusText.Text = "Error: " + ex.Message);
                }
            });
            thread.Start();
        }
    }
}