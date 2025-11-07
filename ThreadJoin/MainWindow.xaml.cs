using System;
using System.Threading;
using System.Windows;

namespace ThreadJoinDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartThreads_Click(object sender, RoutedEventArgs e)
        {
            Thread low = new Thread(() => Count(LowText, ThreadPriority.Lowest));
            Thread normal = new Thread(() => Count(NormText, ThreadPriority.Normal));
            Thread high = new Thread(() => Count(HighText, ThreadPriority.Highest));

            low.Start();
            normal.Start();
            high.Start();

            Thread waiter = new Thread(() =>
            {
                low.Join();
                normal.Join();
                high.Join();
                Dispatcher.Invoke(() => ResultText.Text = "All threads finished!");
            });
            waiter.Start();
        }

        void Count(System.Windows.Controls.TextBlock block, ThreadPriority priority)
        {
            Thread.CurrentThread.Priority = priority;
            for (int i = 1; i <= 100; i++)
            {
                Dispatcher.Invoke(() => block.Text = i.ToString());
                Thread.Sleep(100);
            }
        }
    }
}