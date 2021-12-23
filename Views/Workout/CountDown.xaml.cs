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
        private StrongReferenceMessenger Messenger { get; set; } = StrongReferenceMessenger.Default;

        //public const string START_COUNTDOWN_CMD = "start countdown";
        private DispatcherTimer dt { get; set; }

        public CountDown()
        {
            InitializeComponent();
            Messenger.Register<CountDown, ChangePageMessage>(this, (r, m) => r.Receive(m));
        }

        private void Receive(ChangePageMessage m)
        {
            if (m.Index == PageIndex.COUNT_DOWN_WORKOUT)
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
