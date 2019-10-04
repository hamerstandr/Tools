using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.MaterialControls;

namespace Calendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadRibbonWindow
    {
        static MainWindow()
        {
            RadRibbonWindow.IsWindowsThemeEnabled = false;
        }

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
