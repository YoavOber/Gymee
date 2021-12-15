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

namespace GymeeDestkopApp.Views
{
    /// <summary>
    /// Interaction logic for GenderScroller.xaml
    /// </summary>
    public partial class GenderScroller : UserControl
    {
        public GenderScroller()
        {
            InitializeComponent();
            var ls = new List<string>() { "נקבה", "זכר" };
            gender.ItemsSource = ls;
        }

        private void heights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new SignupVMMessage(SignupProperty.Gender, gender.SelectedItem));
        }
    }
}
