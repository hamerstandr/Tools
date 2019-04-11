using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Word
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static LoginWindow Login = new LoginWindow();
        public App()
        {
            Login.Show();
            this.InitializeComponent();

            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Count() > 0)
            {
                string s = e.Args[0];
                if (e.Args.Count() > 1)
                    for (int i = 1; i < e.Args.Count(); i++)
                    {
                        s += e.Args[i];
                    }
                var w = this.MainWindow as MainWindow;
                if (w == null)
                    w.PathOpenFile = s;
                else
                    w.OpenFile(s);
            }
        }
    }
}
