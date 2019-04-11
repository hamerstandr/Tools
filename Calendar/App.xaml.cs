using Calendar.Views.Schedule;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace Calendar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
           this.InitializeComponent();
            //this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            var win=new RadWindow();
            win.Activated += Win_Activated;
            win.Content =new Mains();
            win.Show();
        }
        private void Win_Activated(object sender, EventArgs e)
        {
            RadWindow w = sender as RadWindow;
            w.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
    }
}
