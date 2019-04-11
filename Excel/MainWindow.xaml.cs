using Excel.Helpers;
using Excel.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.MaterialControls;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.Pdf;

namespace Excel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadRibbonWindow
    {
        private readonly MainViewModel viewModel;

        static MainWindow()
        {
            RadRibbonWindow.IsWindowsThemeEnabled = false;
        }

        public MainWindow()
        {
            //StyleManager.ApplicationTheme = new FluentTheme();
            
            
            InitializeComponent();
            
            this.viewModel = new MainViewModel();
            this.DataContext = this.viewModel;
            Back = Background;
            this.Activated += MainWindow_Activated;
            this.Deactivated += MainWindow_Deactivated;
            this.Loaded += this.MainWindow_Loaded;
        }
        private void MainWindow_Deactivated(object sender, EventArgs e)
        {
            Background = new SolidColorBrush(Colors.White);
        }
        System.Windows.Media.Brush Back;
        private void MainWindow_Activated(object sender, EventArgs e)
        {
            Background = Back; this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ThemeEffectsHelper.IsAcrylicEnabled = true;
            ThemeEffectsHelper.SetIsAcrylic(this, true);
            FluentPalette.LoadPreset(FluentPalette.ColorVariation.Light);
            //FluentPalette.Palette.DisabledOpacity = 0.5d;
            //FluentPalette.Palette.PrimaryColor = FluentPalette.Palette.AccentColor;
            //FluentPalette.Palette.
            if (App.Args!=null)
                if(App.Args.Length>0)
                    using (var stream =new StreamReader(App.Args[0]))
                    {
                        this.radSpreadsheet.Workbook = new XlsxFormatProvider().Import(stream.BaseStream);
                    }

            this.viewModel.OpenSampleCommand.Execute(null);
        }
    }
}
