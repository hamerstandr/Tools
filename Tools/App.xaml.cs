using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Telerik.Windows.Controls;
using Tools.View;

namespace Tools
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Count() > 0)
            {
                switch (e.Args[0])
                {
                    case "Diagrams":
                        var win4 = new RadWindow() { Header = "Diagrams" };
                        win4.Activated += Win_Activated;
                        win4.Content = new Diagrams1();
                        Taskbar.ShowInTaskbar(win4, "Diagrams", "Diagrams.ico");
                        win4.Show();
                        break;
                    case "Pdf":
                        var win = new RadWindow() { Header = "Pdf Veiwer" };
                        win.Activated += Win_Activated;
                        win.Content = new PdfViewer();
                        Taskbar.ShowInTaskbar(win, "Pdf Veiwer", "PDF.ico");
                        
                        win.Show();
                        break;
                    case "Explorer":
                        var win2 = new RadWindow() { Header = "Flie Explorer" };
                        win2.Activated += Win_Activated;
                        win2.Content = new FileExplorer();
                        Taskbar.ShowInTaskbar(win2, "Flie Explorer", "Explorer.ico");
                        
                        win2.Show();
                        break;
                    case "Editor":
                        var win3 = new RadWindow() { Header = "Image Editor" };
                        win3.Activated += Win_Activated;
                        win3.Content = new ImageEditor();
                        Taskbar.ShowInTaskbar(win3, "Image Editor", "Editor.ico");
                        
                        win3.Show();
                        break;
                    default:
                        var win5 = new RadWindow() { Header = "Tools" };
                        win5.Activated += Win_Activated;
                        win5.Content = new ImageEditor();
                        Taskbar.ShowInTaskbar(win5, "Tools", "Tools.ico");

                        win5.Show();
                        break;
                }
            }
            else
            {
                var win = new RadWindow() { Header = "Tools" };
                Taskbar.ShowInTaskbar(win, "Tools", "Tools.ico");
                win.Content = new Mains();
                win.Show();
            }
            Taskbar.ExePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            Taskbar.Path = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);//System.AppDomain.CurrentDomain.BaseDirectory;

            Taskbar.CreateShortcut("Diagrams", "Diagrams");
            Taskbar.CreateShortcut("Flie Explorer", "Explorer");
            Taskbar.CreateShortcut("Image Editor", "Editor");
            Taskbar.CreateShortcut("Pdf Veiwer", "Pdf");
        }
        private void Win_Activated(object sender, EventArgs e)
        {
            RadWindow w = sender as RadWindow;
            w.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
    }
}
