using GymeeDestkopApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GymeeDestkopApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
        }

        protected override void OnDeactivated(EventArgs e)
        {
            base.OnDeactivated(e);
            KioskModeHelper.StartExplorerAndExit();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            KioskModeHelper.StartExplorerAndExit();
        }

    }
}
