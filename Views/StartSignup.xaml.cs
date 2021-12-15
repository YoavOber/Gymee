using GymeeDestkopApp.Models;
using Microsoft.Toolkit.Mvvm.Messaging;
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

namespace GymeeDesktopApp.Views
{
    /// <summary>
    /// Interaction logic for StartSignup.xaml
    /// </summary>
    public partial class StartSignup : UserControl,IDisposable
    {
        public StartSignup()
        {
            InitializeComponent();
        }
        public void Dispose()
        {
            InitializeComponent();
        }
        private void SignupStart_click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.TERMS_OF_USE));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.LOGIN));
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.WHAT_IS_GYMEE));
        }
    }
}
