using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;

namespace GymeeDestkopApp.Views
{
    /// <summary>
    /// Interaction logic for CountDown.xaml
    /// </summary>
    public partial class CountDown : UserControl
    {
        public const string START_CMD_STR = "start countdown";
        private DispatcherTimer dt { get; set; }

        public CountDown()
        {
            InitializeComponent();
            Messenger.Register<CountDown, string>(this, (r, m) => r.Receive(m));
        }
        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;

        private void Receive(string m)
        {
            if (m == START_CMD_STR)
                Countdown(5, TimeSpan.FromSeconds(1), cur => Digit.Text = cur.ToString());
        }

        void Countdown(int count, TimeSpan interval, Action<int> ts)
        {
            dt = new DispatcherTimer();
            dt.Interval = interval;
            dt.Tick += (_, a) =>
            {
                if (count != 0)
                {
                    ts(count);
                    count--;
                }
                else
                {
                    dt.Stop();
                    Messenger.Send(new ChangePageMessage(PageIndex.WORKOUT_VIDEO));
                }
            };
            ts(count);
            dt.Start();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            dt.IsEnabled = false;
        }

    }
}
