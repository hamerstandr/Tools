using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Telerik.Windows.Controls;

namespace Excel
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
           this.InitializeComponent();
        }
        public static string[] Args;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Args = e.Args;
            //var win = new RadWindow() {Header=null, Margin=new Thickness(0) };
            //App.ShowInTaskbar(win, "Excel", "TelerikExcel.ico");
            //win.Content = new Mains() { VerticalAlignment = VerticalAlignment.Stretch };
            //win.Show();
        }
        public static void ShowInTaskbar(RadWindow control, string title, string icon)
        {

            System.Reflection.AssemblyName assemblyName = new System.Reflection.AssemblyName(typeof(App).Assembly.FullName);
            //control.IconTemplate = Application.Current.Resources[icon] as DataTemplate;
            //Image image = new Image();
            //image.Source = ImageExampleHelper.LoadImage("PDF.ico");
            //spFactory.AppendChild(image);

            control.Show();
            var window = control.ParentOfType<Window>();
            window.ShowInTaskbar = true;
            window.WindowStyle = WindowStyle.None;
            window.Title = title;
            var uri = new Uri("pack://application:,,,/" + assemblyName.Name + ";component/Images/" + icon);
            window.Icon = System.Windows.Media.Imaging.BitmapFrame.Create(uri);
        }
    }
}
