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
using Telerik.Windows.Controls.MaterialControls;
using Tools.View;

namespace Tools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Back = Background;
            this.Activated += MainWindow_Activated;
            this.Deactivated += MainWindow_Deactivated; ;
            this.Loaded += MainWindow_Loaded; ;
        }
        private void MainWindow_Deactivated(object sender, System.EventArgs e)
        {
            Background = new SolidColorBrush(Colors.White);
        }
        System.Windows.Media.Brush Back;
        private void MainWindow_Activated(object sender, System.EventArgs e)
        {
            Background = Back;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private void MainWindow_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ThemeEffectsHelper.IsAcrylicEnabled = true;
            ThemeEffectsHelper.SetIsAcrylic(this, true);
            FluentPalette.LoadPreset(FluentPalette.ColorVariation.Light);
        }

    }
}
