using Excel.Helpers;
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
using Telerik.Windows.Controls.Spreadsheet;
using Telerik.Windows.Controls.Spreadsheet.Commands;
using Telerik.Windows.Controls.Spreadsheet.Utilities;
using Telerik.Windows.Documents.Spreadsheet.FormatProviders.OpenXml.Xlsx;

namespace Excel
{
    /// <summary>
    /// Interaction logic for Mains.xaml
    /// </summary>
    public partial class Mains : UserControl
    {
        IRadSheetEditor activeSheetEditor;
        private const string SampleDocumentPath = "SampleData/SampleExcelDocument.xlsx";
        public Mains()
        {
            InitializeComponent();
            using (var stream = Application.GetResourceStream(FileHelper.GetResourceUri(SampleDocumentPath)).Stream)
            {
                this.RadSpreadsheet.Workbook = new XlsxFormatProvider().Import(stream);
            }

            this.RadSpreadsheet.ActiveSheetEditorChanged += this.RadSpreadsheet_ActiveSheetEditorChanged;

            this.Loaded += this.Example_Loaded;
            this.Unloaded += this.Example_Unloaded;
        }

        public RadSpreadsheet RadSpreadsheet
        {
            get
            {
                return this.exampleControl.radSpreadsheet;
            }
        }

        public string CurrentTheme
        {
            get
            {
                return ApplicationThemeManager.GetInstance().CurrentTheme;
            }
        }

        private void Example_Loaded(object sender, RoutedEventArgs e)
        {
            this.RadSpreadsheet.Focus();
            IconSources.ChangeIconsSet(IconsSet.Light);
            //ThemeSpecificIconsHelper.SubscribeForIconChanges(true);
            //ThemeSpecificIconsHelper.ApplyThemeSpecificIconsWithTwoVariations();
        }

        private void Example_Unloaded(object sender, RoutedEventArgs e)
        {
            //ThemeSpecificIconsHelper.UnsubscribeFromIconChanges(true);
        }

        private void RadSpreadsheet_ActiveSheetEditorChanged(object sender, EventArgs e)
        {
            if (this.activeSheetEditor != null)
            {
                this.activeSheetEditor.UICommandExecuted -= this.ActiveSheetEditor_UICommandExecuted;
            }

            this.activeSheetEditor = this.RadSpreadsheet.ActiveSheetEditor;
            if (this.activeSheetEditor != null)
            {
                this.activeSheetEditor.UICommandExecuted += this.ActiveSheetEditor_UICommandExecuted;
                this.ChangeScaleFactorIfTouchTheme();
            }
        }

        private void ActiveSheetEditor_UICommandExecuted(object sender, UICommandExecutedEventArgs e)
        {
            if (!this.RadSpreadsheet.IsKeyboardFocusWithin())
            {
                this.RadSpreadsheet.Focus();
            }
        }

        private void ChangeScaleFactorIfTouchTheme()
        {
            this.RadSpreadsheet.ActiveSheetEditor.ScaleFactor = this.CurrentTheme == "Windows8Touch" ? new Size(1.5, 1.5) : new Size(1, 1);
        }

        private void RadRibbonGroup_LaunchDialog(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            RadRibbonGroup group = (RadRibbonGroup)sender;
            this.activeSheetEditor.CommandDescriptors.ShowFormatCellsDialog.Command.Execute(group.Header);
        }
    }
}
