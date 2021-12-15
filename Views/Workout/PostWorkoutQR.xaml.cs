using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;


namespace GymeeDestkopApp.Views
{
    /// <summary>
    /// Interaction logic for PostWorkoutQR.xaml
    /// </summary>
    public partial class PostWorkoutQR : UserControl,IDisposable
    {
        public PostWorkoutQR()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.INTRO_PAGE));
           // StrongReferenceMessenger.Default.Send("reset");
        }

        public void Dispose()
        {
            return;
        }
    }
}
