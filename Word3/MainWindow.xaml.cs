using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.MaterialControls;
using Telerik.Windows.Documents.FormatProviders;
using Telerik.Windows.Documents.FormatProviders.OpenXml.Docx;
using Telerik.Windows.Documents.FormatProviders.Pdf;
using Telerik.Windows.Documents.Model;
using Telerik.Windows.Documents.RichTextBoxCommands;
using Word.Helpers;

namespace Word
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RadRibbonWindow
    {
        private const string SampleDocumentFile = "SampleWordDocument.docx";

        static MainWindow()
        {
            RadRibbonWindow.IsWindowsThemeEnabled = false;
        }

        public MainWindow()
        {
            Telerik.Windows.Controls.RichTextBoxUI.StylesGallery stylesGallery = new Telerik.Windows.Controls.RichTextBoxUI.StylesGallery();

            InitializeComponent();
            IconSources.ChangeIconsSet(IconsSet.Modern);
            Back = Background;
            this.Activated += MainWindow_Activated;
            this.Deactivated += MainWindow_Deactivated;
            this.Loaded += this.MainWindow_Loaded;
        }
        private void MainWindow_Deactivated(object sender, EventArgs e)
        {
            Background =new SolidColorBrush(Colors.White);
        }
        System.Windows.Media.Brush Back;
        private void MainWindow_Activated(object sender, EventArgs e)
        {
            Background = Back;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }
        public string PathOpenFile = "";
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ThemeEffectsHelper.IsAcrylicEnabled = true;
            ThemeEffectsHelper.SetIsAcrylic(this, true);
            this.LoadSampleDocument();
            this.radRichTextBox.CommandExecuting += this.RadRichTextBoxCommandExecuting;
            if (PathOpenFile != "")
            {
                OpenFile(PathOpenFile);
                PathOpenFile = "";
            }
            App.Login.Hide();
        }

        private void RadRichTextBoxCommandExecuting(object sender, CommandExecutingEventArgs e)
        {
            if (e.Command == this.radRichTextBox.Commands.OpenDocumentCommand)
            {
                e.Cancel = true;
                this.OpenFile(e.CommandParameter);
            }
            else if (e.Command == this.radRichTextBox.Commands.SaveCommand)
            {
                e.Cancel = true;
                this.SaveFile(e.CommandParameter);
            }
        }
        public void OpenFile(string parameter)
        {

            if (parameter != "")
            {
                string extension;
                extension = Path.GetExtension(parameter).ToLower();

                IDocumentFormatProvider provider =
                    DocumentFormatProvidersManager.GetProviderByExtension(extension);

                if (provider == null)
                {
                    MessageBox.Show(LocalizationManager.GetString("Documents_OpenDocumentCommand_UnsupportedFileFormat"));
                    return;
                }

                try
                {
                    Stream stream;
                    stream = File.Open(parameter, FileMode.Open);
                    using (stream)
                    {
                        RadDocument document = provider.Import(stream);
                        this.radRichTextBox.Document = document;
                        this.SetDocumentName(parameter);
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show(LocalizationManager.GetString("Documents_OpenDocumentCommand_TheFileIsLocked"));
                }
                catch (Exception)
                {
                    MessageBox.Show(LocalizationManager.GetString("Documents_OpenDocumentCommand_TheFileCannotBeOpened"));
                }
            }
        }
        private void OpenFile(object parameter)
        {
            RadOpenFileDialog ofd = new RadOpenFileDialog();

            string stringParameter = parameter as string;
            if (stringParameter != null && stringParameter.Contains("|"))
            {
                ofd.Filter = stringParameter;
            }
            else
            {
                string filter = string.Join("|", DocumentFormatProvidersManager.FormatProviders.Where(fp => fp.CanImport)
                                                                                               .OrderBy(fp => fp.Name)
                                                                                               .Select(fp => FileHelper.GetFilter(fp))
                                                                                               .ToArray()) + "|All Files|*.*";
                ofd.Filter = filter;
            }

            if (ofd.ShowDialog() == true)
            {
                string extension;
                extension = Path.GetExtension(ofd.FileName).ToLower();

                IDocumentFormatProvider provider =
                    DocumentFormatProvidersManager.GetProviderByExtension(extension);

                if (provider == null)
                {
                    MessageBox.Show(LocalizationManager.GetString("Documents_OpenDocumentCommand_UnsupportedFileFormat"));
                    return;
                }

                try
                {
                    Stream stream;
                    stream = ofd.OpenFile();
                    using (stream)
                    {
                        RadDocument document = provider.Import(stream);
                        this.radRichTextBox.Document = document;
                        this.SetDocumentName(ofd.FileName);
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show(LocalizationManager.GetString("Documents_OpenDocumentCommand_TheFileIsLocked"));
                }
                catch (Exception)
                {
                    MessageBox.Show(LocalizationManager.GetString("Documents_OpenDocumentCommand_TheFileCannotBeOpened"));
                }
            }
        }

        private void SaveFile(object parameter)
        {
            string extension = null;
            Stream outputStream = null;
            RadSaveFileDialog saveDialog = new RadSaveFileDialog();
            string exportFormat = parameter as string;

            if (exportFormat != null && exportFormat.Contains("|"))
            {
                saveDialog.Filter = exportFormat;
            }
            else
            {
                var formatProviders = DocumentFormatProvidersManager.FormatProviders;

                if (!string.IsNullOrEmpty(exportFormat))
                {
                    string[] extensions = exportFormat.Split(',', ';').Select(e => e.Trim('.').ToLower()).ToArray();
                    formatProviders = formatProviders.Where(fp => fp.SupportedExtensions.Any(ext => extensions.Contains(ext.Trim('.').ToLower())));
                }

                string filter = string.Join("|", formatProviders.Where(fp => fp.CanExport)
                                                                .OrderBy(fp => fp.Name)
                                                                .Select(fp => FileHelper.GetFilter(fp))
                                                                .ToArray());
                saveDialog.Filter = filter;
            }

            bool? dialogResult = saveDialog.ShowDialog();
            if (dialogResult == true)
            {
                extension = System.IO.Path.GetExtension(saveDialog.FileName);
                outputStream = saveDialog.OpenFile();

                IDocumentFormatProvider provider = DocumentFormatProvidersManager.GetProviderByExtension(extension);

                if (provider == null)
                {
                    MessageBox.Show(LocalizationManager.GetString("Documents_SaveCommand_UnsupportedFileFormat"));
                    return;
                }

                if (provider is IConfigurablePdfFormatProvider)
                {
                    IConfigurablePdfFormatProvider pdfFormatProvider = (IConfigurablePdfFormatProvider)provider;
                    pdfFormatProvider.ExportSettings.CommentsExportMode =
                        this.radRichTextBox.ShowComments ? PdfCommentsExportMode.NativePdfAnnotations : PdfCommentsExportMode.None;
                }

                try
                {
                    using (outputStream)
                    {
                        provider.Export(this.radRichTextBox.Document, outputStream);
                        this.SetDocumentName(saveDialog.FileName);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(LocalizationManager.GetString("Documents_SaveCommand_UnableToSaveFile"));
                }
            }
        }

        private void LoadSampleDocument()
        {
            using (Stream stream = FileHelper.GetSampleResourceStream(MainWindow.SampleDocumentFile))
            {
                stream.Seek(0, SeekOrigin.Begin);
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);

                this.radRichTextBox.Document = new DocxFormatProvider().Import(bytes);
                this.SetDocumentName(MainWindow.SampleDocumentFile);
            }
        }

        private void SetDocumentName(string name)
        {
            this.ribbon.ApplicationName = name;
        }
    }
}
