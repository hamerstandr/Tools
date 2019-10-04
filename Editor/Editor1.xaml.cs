using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Editor.Control;
using System.Windows.Input;

namespace Editor
{
    /// <summary>
    /// Interaction logic for Editor1.xaml
    /// </summary>
    public partial class Editor1 : UserControl
    {
        #region Fields
        //private FoldingTagger foldingTagger;
        //private TextSearchHighlightTagger selectionWordTagger;
        //private TaggerBase<ClassificationTag> currentLanguageTagger;
        public static RoutedCommand MyCommand = new RoutedCommand();
        private RadOpenFolderDialog openFolderDialog;
        //public static Style RadDocumentPaneStyle1;
        public static DataTemplate HeaderPane;
        private bool usingFileSystem;
        #endregion

        public Editor1()
        {
            InitializeComponent();
            MyCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            this.openFolderDialog = new RadOpenFolderDialog();
            openFolderDialog.Owner = this;
            openFolderDialog.Header = "Open Directory";
        }
        public Editor1(string PathFolder)
        {
            InitializeComponent();
            MyCommand.InputGestures.Add(new KeyGesture(Key.O, ModifierKeys.Control));
            this.openFolderDialog = new RadOpenFolderDialog();
            //RadDocumentPaneStyle1 = this.FindResource("RadDocumentPaneStyle1") as Style;
            HeaderPane = this.FindResource("HeaderPane") as DataTemplate;
            openFolderDialog.Owner = this;
            openFolderDialog.Header = "Open Directory";
            if (!string.IsNullOrEmpty(PathFolder))
            {
                this.LoadDirectory(new System.IO.FileInfo( PathFolder).DirectoryName);
            }
        }
        void Def()
        {
            //var projectFiles = ExampleHelper.LoadProjectFiles();
            //ExampleHelper.PopulateTreeView(this.treeView, projectFiles);
            //this.treeView.ExpandAll();

            //this.foldingTagger = new FoldingTagger(this.syntaxEditor);
            //this.selectionWordTagger = new TextSearchHighlightTagger(this.syntaxEditor, TextSearchHighlightTagger.SearchFormatDefinition);
            //this.syntaxEditor.TaggersRegistry.RegisterTagger(foldingTagger);
            //this.syntaxEditor.TaggersRegistry.RegisterTagger(this.selectionWordTagger);

            //this.syntaxEditor.Selection.SelectionChanged += Selection_SelectionChanged;

            //ApplicationThemeManager.GetInstance().ThemeChanged += this.Example_ThemeChanged;
            //NavigationService.Instance.PreviewNavigated += Instance_PreviewNavigated;
        }
        #region Methods
        //private void RegisterTagger(string fileName)
        //{
        //    var extension = Path.GetExtension(fileName);

        //    if (this.currentLanguageTagger != null)
        //    {
        //        this.syntaxEditor.TaggersRegistry.UnregisterTagger(this.currentLanguageTagger);
        //    }

        //    switch (extension)
        //    {
        //        case ".cs":
        //        case ".cpp":
        //            this.currentLanguageTagger = new CSharpTagger(this.syntaxEditor);
        //            this.ClearXmlFormatDefinitions();
        //            break;
        //        case ".vb":
        //            this.currentLanguageTagger = new VisualBasicTagger(this.syntaxEditor);
        //            this.ClearXmlFormatDefinitions();
        //            break;
        //        case ".js":
        //            this.currentLanguageTagger = new JavaScriptTagger(this.syntaxEditor);
        //            this.ClearXmlFormatDefinitions();
        //            break;
        //        case ".sql":
        //            this.currentLanguageTagger = new SqlTagger(this.syntaxEditor);
        //            this.ClearXmlFormatDefinitions();
        //            break;
        //        case ".xaml":
        //        case ".php":
        //        case ".xml":
        //        case ".html":
        //        case ".csproj":
        //        case ".vbproj":
        //        case ".user":
        //        case ".config":
        //        case ".resx":
        //        case ".settings":
        //            this.currentLanguageTagger = new XmlTagger(this.syntaxEditor);
        //            this.AddXmlFormatDefinitions();
        //            break;
        //        default:
        //            this.ClearXmlFormatDefinitions();
        //            this.currentLanguageTagger = null;
        //            break;
        //    }

        //    if (this.currentLanguageTagger != null)
        //    {
        //        this.syntaxEditor.TaggersRegistry.RegisterTagger(this.currentLanguageTagger);
        //    }
        //}

        //private void AddXmlFormatDefinitions()
        //{
        //    this.syntaxEditor.TextFormatDefinitions.AddLast(XmlSyntaxHighlightingHelper.XmlAttribute, XmlSyntaxHighlightingHelper.XmlAttributeFormatDefinition);
        //    this.syntaxEditor.TextFormatDefinitions.AddLast(XmlSyntaxHighlightingHelper.XmlElement, XmlSyntaxHighlightingHelper.XmlElementFormatDefinition);
        //    this.syntaxEditor.TextFormatDefinitions.AddLast(XmlSyntaxHighlightingHelper.XmlComment, XmlSyntaxHighlightingHelper.XmlCommentFormatDefinition);
        //    this.syntaxEditor.TextFormatDefinitions.AddLast(XmlSyntaxHighlightingHelper.XmlContent, XmlSyntaxHighlightingHelper.XmlContentFormatDefinition);
        //    this.syntaxEditor.TextFormatDefinitions.AddLast(XmlSyntaxHighlightingHelper.XmlString, XmlSyntaxHighlightingHelper.XmlStringFormatDefinition);
        //    this.syntaxEditor.TextFormatDefinitions.AddLast(XmlSyntaxHighlightingHelper.XmlTag, XmlSyntaxHighlightingHelper.XmlTagFormatDefinition);
        //}

        //private void ClearXmlFormatDefinitions()
        //{
        //    this.syntaxEditor.TextFormatDefinitions.Remove(XmlSyntaxHighlightingHelper.XmlAttribute);
        //    this.syntaxEditor.TextFormatDefinitions.Remove(XmlSyntaxHighlightingHelper.XmlElement);
        //    this.syntaxEditor.TextFormatDefinitions.Remove(XmlSyntaxHighlightingHelper.XmlComment);
        //    this.syntaxEditor.TextFormatDefinitions.Remove(XmlSyntaxHighlightingHelper.XmlContent);
        //    this.syntaxEditor.TextFormatDefinitions.Remove(XmlSyntaxHighlightingHelper.XmlString);
        //    this.syntaxEditor.TextFormatDefinitions.Remove(XmlSyntaxHighlightingHelper.XmlTag);
        //}

        private void LoadDirectory(string directoryPath)
        {
            System.Threading.Tasks.Task.Run(() => this.Dispatcher.Invoke(() => _LoadDirectory(directoryPath) ));
            //ExampleHelper.PopulateTreeView(this.treeView, items);
        }
        private void _LoadDirectory(string directoryPath)
        {
            ProgressBar1.IsIndeterminate = true;
            this.usingFileSystem = true;
            var directory = new Control.DirectoryInfo(directoryPath, Path.GetFileName(directoryPath));
            directory.Root = PaneGroup1;
            directory.LoadChildren();
            this.treeView.ItemsSource = (directory.Items);
            ProgressBar1.IsIndeterminate = false;
            //ExampleHelper.PopulateTreeView(this.treeView, items);
        }
        //private void LoadFile(string fileName)
        //{
        //    if (usingFileSystem)
        //    {
        //        try
        //        {
        //            using (FileStream stream = new FileStream(fileName, FileMode.Open))
        //            {
        //                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        //                this.syntaxEditor.Document = new TextDocument(reader);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        using (Stream stream = GetResourceStream(fileName))
        //        {
        //            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
        //            this.syntaxEditor.Document = new TextDocument(reader);
        //        }
        //    }

        //    this.RegisterTagger(fileName);
        //}

        //protected static Stream GetResourceStream(string resourcePath)
        //{
        //    Assembly assembly = Assembly.GetExecutingAssembly();
        //    List<string> resourceNames = new List<string>(assembly.GetManifestResourceNames());

        //    resourcePath = resourceNames.FirstOrDefault(r => r.EndsWith(resourcePath));

        //    if (resourcePath == null)
        //    {
        //        throw new FileNotFoundException("Resource not found");
        //    }

        //    return assembly.GetManifestResourceStream(resourcePath);
        //}

        //private void Selection_SelectionChanged(object sender, EventArgs e)
        //{
        //    if (!this.syntaxEditor.Selection.IsEmpty)
        //    {
        //        string searchedWord = this.syntaxEditor.Selection.GetSelectedText();
        //        if (!string.IsNullOrWhiteSpace(searchedWord))
        //        {
        //            this.selectionWordTagger.UpdateSearchWord(searchedWord);
        //        }
        //    }
        //    else
        //    {
        //        this.selectionWordTagger.UpdateSearchWord(string.Empty);
        //    }
        //}
        private void TreeView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Tab tab;
            //if (e.AddedItems != null && e.AddedItems.Count > 0)
            //{
            //    var file = e.AddedItems[0] as FileInfo;
            //    tab= new Tab();tab.usingFileSystem = usingFileSystem;
            //    if (file != null && !(file is DirectoryInfo))
            //    {
            //        tab.Header = file.Name;
            //        tab.LoadFile(file.FullPath);
            //        PaneGroup1.Items.Add(tab);
            //    }
            //}
        }

        private void TreeView_LoadOnDemand(object sender, RadRoutedEventArgs e)
        {
            e.Handled = true;

            RadTreeViewItem expandedItem = e.OriginalSource as RadTreeViewItem;
            if (expandedItem == null)
            {
                return;
            }

            Control.DirectoryInfo directory = expandedItem.Item as Control.DirectoryInfo;
            if (directory != null && this.usingFileSystem)
            {
                directory.LoadChildren();
                if (directory.Items.Count == 0)
                {
                    expandedItem.IsLoadOnDemandEnabled = false;
                }
            }
        }

        private void TreeView_ItemPrepared(object sender, RadTreeViewItemPreparedEventArgs e)
        {
            if (!(e.PreparedItem.DataContext is Control.DirectoryInfo))
            {
                e.PreparedItem.IsLoadOnDemandEnabled = false;
            }
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            this.openFolderDialog.ShowDialog();
            if (this.openFolderDialog.DialogResult == true)
            {
                string selectedFolder = this.openFolderDialog.FileName;
                if (!string.IsNullOrEmpty(selectedFolder))
                {
                    this.LoadDirectory(selectedFolder);
                }
            }
        }
        private void MyCommandExecuted(object sender, ExecutedRoutedEventArgs e) { RadButton_Click(sender,e); }
        //private void Example_ThemeChanged(object sender, EventArgs e)
        //{
        //    string currentTheme = ApplicationThemeManager.GetInstance().CurrentTheme;

        //    switch (currentTheme)
        //    {
        //        case "Crystal_Dark":
        //            this.syntaxEditor.Palette = SyntaxPalettes.Dark;
        //            break;
        //        case "Fluent_Dark":
        //            this.syntaxEditor.Palette = SyntaxPalettes.Dark;
        //            break;
        //        case "Green_Light":
        //            this.syntaxEditor.Palette = SyntaxPalettes.Neutral;
        //            break;
        //        case "Green_Dark":
        //            this.syntaxEditor.Palette = SyntaxPalettes.NeutralDark;
        //            break;
        //        case "VisualStudio2013_Dark":
        //            this.syntaxEditor.Palette = SyntaxPalettes.Dark;
        //            break;
        //        case "Expression_Dark":
        //            this.syntaxEditor.Palette = SyntaxPalettes.NeutralDark;
        //            break;
        //        case "Transparent":
        //            this.syntaxEditor.Palette = SyntaxPalettes.Neutral;
        //            break;
        //        default:
        //            this.syntaxEditor.Palette = SyntaxPalettes.Light;
        //            break;
        //    }

        //    if (!this.usingFileSystem)
        //    {
        //        this.treeView.ExpandAll();
        //    }
        //}

        //private void Instance_PreviewNavigated(object sender, EventArgs e)
        //{
        //    ApplicationThemeManager.GetInstance().ThemeChanged -= this.Example_ThemeChanged;
        //}
        #endregion
    }
}
