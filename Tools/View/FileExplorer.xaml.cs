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
using Telerik.Windows.Controls.FileDialogs;

namespace Tools.View
{
    /// <summary>
    /// Interaction logic for FileExplorer.xaml
    /// </summary>
    public partial class FileExplorer : UserControl
    {
        List<string> SelectedSafeFileNames = new List<string>();
        private DummyStorage storage=new DummyStorage();
        public FileExplorer()
        {
            InitializeComponent();
            //
            
        }
        void exp()
        {
            RadOpenFolderDialog openFolderDialog = new RadOpenFolderDialog();
            openFolderDialog.ShowDialog();
            this.storage.FolderName = openFolderDialog.FileName;
            RadOpenFileDialog radOpenFileDialog = new RadOpenFileDialog()
            {
                Filter = "Word Documents (*.doc)|*.doc|Excel Worksheets (*.xls;*.xlsx)|*.xls;*.xlsx|PowerPoint Presentations (*.ppt)|*.ppt" +
                         "|Office Files (*.doc;*.xls;*.ppt)|*.doc;*.xls;*.ppt" +
                         "|All Files (*.*)|*.*",
                FilterIndex = 5,
            };
            radOpenFileDialog.ShowDialog();
            RadSaveFileDialog saveFileDialog = new RadSaveFileDialog()
            {
                Filter = "Word Documents (*.doc)|*.doc|Excel Worksheets (*.xls;*.xlsx)|*.xls;*.xlsx|PowerPoint Presentations (*.ppt)|*.ppt" +
                         "|Office Files (*.doc;*.xls;*.ppt)|*.doc;*.xls;*.ppt" +
                         "|All Files (*.*)|*.*",
                FilterIndex = 5,
            };
            //saveFileDialog.FileName = storageFile != null ? storageFile.Name : null;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.DialogResult == true) { }

            //this.explorer.Filter = "Word Documents|*.doc;*.docx|Excel Worksheets|*.xls;*.xlsx|PowerPoint Presentations|*.ppt;*.pptx" +
            //                            "|Office Files|*.doc;*.docx*.xls;*.xlsx;*.ppt*.pptx" +
            //                            "|Image Files|*.jpg;*.png;*.bmp" +
            //                            "|Text Files|*.txt;" +
            //                            "|Archives Files|*.zip;*.rar" +
            //                            "|All Files|*.*";
        }
    }
}
