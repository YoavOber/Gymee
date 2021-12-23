using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.Toolkit.Mvvm.Messaging;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GymeeDestkopApp.Models;

namespace GymeeDestkopApp.Views
{
    /// <summary>
    /// Interaction logic for PreWorkoutView.xaml
    /// </summary>
    public partial class PreWorkoutView : UserControl
    {
        public PreWorkoutView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StrongReferenceMessenger.Default.Send(new ChangePageMessage(PageIndex.COUNT_DOWN_WORKOUT));//starts countdown timer
        }
    }
}
