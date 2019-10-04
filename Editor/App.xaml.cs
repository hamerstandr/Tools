using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace Editor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
            Registr.DefaultProgram();
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            string Arg = "";
            if(e.Args.Length>0)
            {
                if (e.Args.Length == 1)
                    Arg=e.Args[0];
            }
            FluentPalette.LoadPreset(FluentPalette.ColorVariation.Dark);
            StyleManager.ApplicationTheme = new FluentTheme();
            FluentPalette.LoadPreset(FluentPalette.ColorVariation.Dark);
            Telerik.Windows.Controls.MaterialControls.ThemeEffectsHelper.IsAcrylicEnabled = false;
            var win = new RadWindow()
            {
                Header = "Editor",
                Icon = new Image()
                { Source = ImageExampleHelper.LoadImage("Image/Editor.ico") },
                Content = new Editor.Editor1(Arg),
                Height = 400,
                Width=600
            };
            win.Closed += Win_Closed;
            //win.Activated += Win_Activated;
            Taskbar.ShowInTaskbar(win, "Editor", "Image/Editor.ico");//ImageExampleHelper.LoadImage("PDF.ico")
            win.Show();
            
        }

        private void Win_Closed(object sender, WindowClosedEventArgs e)
        {
            this.Shutdown();
        }
    }
}
