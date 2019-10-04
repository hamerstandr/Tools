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
using Telerik.Windows.Controls;

namespace Tools.View
{
    /// <summary>
    /// Interaction logic for Mains.xaml
    /// </summary>
    public partial class Mains : UserControl
    {
        public Mains()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var win = new RadWindow() { Header = "Image Editor" };
            win.Activated += Win_Activated;
            //win.IconTemplate = this.Resources["EditorWindowIconTemplate"] as DataTemplate;
            Taskbar.ShowInTaskbar(win, "Image Editor", "Editor.ico");
            win.Content = new ImageEditor();
            
            win.Show();
            
        }

        private void Win_Activated(object sender, EventArgs e)
        {
            RadWindow w = sender as RadWindow;
            w.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var win = new RadWindow() { Header = "Flie Explorer" };
            win.Activated += Win_Activated;
            /*win.IconTemplate = this.Resources["EditorWindowIconTemplate"] as DataTemplate;*/
            Taskbar.ShowInTaskbar(win, "Flie Explorer", "Explorer.ico");
            win.Content = new FileExplorer();
            win.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var win = new RadWindow() { Header = "Pdf Veiwer" };
            win.Activated += Win_Activated;
            win.Icon = new Image()
            {
                Source = ImageExampleHelper.LoadImage("PDF.ico")
            };
            //win.IconTemplate = this.Resources["PdfWindowIconTemplate"] as DataTemplate;
            Taskbar.ShowInTaskbar(win, "Pdf Veiwer", "Pdf.ico");//ImageExampleHelper.LoadImage("PDF.ico")
            win.Content = new PdfViewer();
            win.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var win = new RadWindow() { Header = "Diagrams",
                Left = SystemParameters.WorkArea.Left,
                Top = SystemParameters.WorkArea.Top,
                Height = SystemParameters.WorkArea.Height,
                Width = SystemParameters.WorkArea.Width
            };
            win.Activated += Win_Activated;
            win.Icon = new Image()
            {
                Source = ImageExampleHelper.LoadImage("Diagrams.ico")
            };
            //win.IconTemplate = this.Resources["PdfWindowIconTemplate"] as DataTemplate;
            Taskbar.ShowInTaskbar(win, "Diagrams", "Diagrams.ico");//ImageExampleHelper.LoadImage("PDF.ico")
            win.Content = new Diagrams1();
            win.Show();
        }

        //private void Button_Click_4(object sender, RoutedEventArgs e)
        //{
        //    var win = new RadWindow()
        //    {
        //        Header = "Editor",
        //    };
        //    win.Activated += Win_Activated;
        //    win.Icon = new Image()
        //    {
        //        Source = ImageExampleHelper.LoadImage("Editor.ico")
        //    };
        //    //win.IconTemplate = this.Resources["PdfWindowIconTemplate"] as DataTemplate;
        //    Taskbar.ShowInTaskbar(win, "Editor", "Editor.ico");//ImageExampleHelper.LoadImage("PDF.ico")
        //    win.Content = new Editor.Editor1();
        //    win.Show();
        //}
    }
}
