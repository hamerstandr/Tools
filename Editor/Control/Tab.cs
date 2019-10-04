using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Xml;
using Telerik.Windows.Controls;

namespace Editor.Control
{
    class Tab: RadDocumentPane
    {
        public static RoutedCommand SaveCommand = new RoutedCommand();
        string currentFileName;
        public bool usingFileSystem;
        TextEditor textEditor;
        private void SaveCommand_Execute(object sender, ExecutedRoutedEventArgs e)
        { Save(); }
        public bool Save()
        {
            try
            {
                if (!Saved)
                {
                    var r=MessageBox.Show("Save File?\n\r"+FullPath,"Editor",MessageBoxButton.YesNo,MessageBoxImage.Question);
                    if (r == MessageBoxResult.Yes)
                    {
                        using (StreamWriter writer = new StreamWriter(FullPath))
                        {
                            textEditor.Save(writer.BaseStream);
                            writer.Close();
                        }
                        Saved = true;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Saved;
        }
        public Tab()
        {
            
            
            CanUserClose = false;
            //Style = Editor1.RadDocumentPaneStyle1;
            this.HeaderTemplate = Editor1.HeaderPane;
            StyleManager.SetTheme(this, new FluentTheme());
            SaveCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
            CommandBinding command = new CommandBinding(SaveCommand, SaveCommand_Execute);
            this.CommandBindings.Add(command);
            InalizedText();
            
        }

        
        #region InalizedText
        private void InalizedText()
        {
            //// Load our custom highlighting definition
            //IHighlightingDefinition customHighlighting;
            //using (Stream s = typeof(App).Assembly.GetManifestResourceStream("Editor.CustomHighlighting.xshd"))
            //{
            //    if (s == null)
            //        throw new InvalidOperationException("Could not find embedded resource");
            //    using (XmlReader reader = new XmlTextReader(s))
            //    {
            //        customHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
            //            HighlightingLoader.Load(reader, HighlightingManager.Instance);
            //    }
            //}
            // and register it in the HighlightingManager
            //HighlightingManager.Instance.RegisterHighlighting("Custom Highlighting", new string[] { ".cool" }, customHighlighting);
            textEditor = new TextEditor();
            this.Content = textEditor;
            textEditor.ShowLineNumbers = true;
            //textEditor.TextArea.TextView.LinkTextForegroundBrush = System.Windows.Media.Brushes.White;
            textEditor.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            this.SetValue(TextOptions.TextFormattingModeProperty, TextFormattingMode.Display);
            textEditor.TextArea.TextEntering += textEditor_TextArea_TextEntering;
            textEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;
            textEditor.PreviewMouseWheel += TextEditor_PreviewMouseWheel;
            SearchPanel.Install(textEditor);
            DispatcherTimer foldingUpdateTimer = new DispatcherTimer();
            foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
            foldingUpdateTimer.Tick += delegate { UpdateFoldings(); };
            foldingUpdateTimer.Start();
        }
        // React to ctrl + mouse wheel to zoom in and out
        private void TextEditor_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            bool ctrl = Keyboard.Modifiers == ModifierKeys.Control;
            if (ctrl)
            {
                this.UpdateFontSize(e.Delta > 0);
                e.Handled = true;
            }
        }
        // Reasonable max and min font size values
        private const double FONT_MAX_SIZE = 60d;
        private const double FONT_MIN_SIZE = 5d;

        // Update function, increases/decreases by a specific increment
        public void UpdateFontSize(bool increase)
        {
            double currentSize = textEditor.FontSize;

            if (increase)
            {
                if (currentSize < FONT_MAX_SIZE)
                {
                    double newSize = Math.Min(FONT_MAX_SIZE, currentSize + 1);
                    textEditor.FontSize = newSize;
                }
            }
            else
            {
                if (currentSize > FONT_MIN_SIZE)
                {
                    double newSize = Math.Max(FONT_MIN_SIZE, currentSize - 1);
                    textEditor.FontSize = newSize;
                }
            }
        }

        CompletionWindow completionWindow;
        bool saved = true;
        public bool Saved { get => saved;
            set { saved = value;
                var h = (Header as HeaderPane);
                h.Change = value ? Visibility.Hidden : Visibility.Visible;
            }
        }
        void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == ".")
            {
                // open code completion after the user has pressed dot:
                completionWindow = new CompletionWindow(textEditor.TextArea);
                // provide AvalonEdit with the data:
                IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
                data.Add(new MyCompletionData("Item1"));
                data.Add(new MyCompletionData("Item2"));
                data.Add(new MyCompletionData("Item3"));
                data.Add(new MyCompletionData("Another item"));
                completionWindow.Show();
                completionWindow.Closed += delegate {
                    completionWindow = null;
                };
            }
            Saved = false;
        }
        void textEditor_TextArea_TextEntering(object sender, TextCompositionEventArgs e)
        {
            if (e.Text.Length > 0 && completionWindow != null)
            {
                if (!char.IsLetterOrDigit(e.Text[0]))
                {
                    // Whenever a non-letter is typed while the completion window is open,
                    // insert the currently selected element.
                    completionWindow.CompletionList.RequestInsertion(e);
                }
            }
            // do not set e.Handled=true - we still want to insert the character that was typed
        }

        #region Folding
        FoldingManager foldingManager;
        object foldingStrategy;

        void HighlightingComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (textEditor.SyntaxHighlighting == null)
            {
                foldingStrategy = null;
            }
            else
            {
                switch (textEditor.SyntaxHighlighting.Name)
                {
                    case "XML":
                        foldingStrategy = new XmlFoldingStrategy();
                        textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
                        break;
                    case "C#":
                    case "C++":
                    case "PHP":
                    case "Java":
                        textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
                        foldingStrategy = new BraceFoldingStrategy();
                        break;
                    default:
                        textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.DefaultIndentationStrategy();
                        foldingStrategy = null;
                        break;
                }
            }
            if (foldingStrategy != null)
            {
                if (foldingManager == null)
                    foldingManager = FoldingManager.Install(textEditor.TextArea);
                UpdateFoldings();
            }
            else
            {
                if (foldingManager != null)
                {
                    FoldingManager.Uninstall(foldingManager);
                    foldingManager = null;
                }
            }
        }

        void UpdateFoldings()
        {
            if (foldingStrategy is BraceFoldingStrategy)
            {
                ((BraceFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, textEditor.Document);
            }
            if (foldingStrategy is XmlFoldingStrategy)
            {
                ((XmlFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, textEditor.Document);
            }
        }
        #endregion
        #endregion
        private List<string> supportedFileExtensions = new List<string>()
        {
            ".cs", ".vb", ".js", ".xaml", ".xml",
            ".html", "htm", ".sql", ".csproj",
            ".user", ".cpp", ".txt", ".config",
            ".resx", ".settings", ".php",".svg",
            ".cmd","sin",".md", ".json",".lock"
        };


        public string FullPath;
        public void LoadFile(string fileName)
        {
            FullPath = fileName;
            if (this.supportedFileExtensions.Contains(Path.GetExtension(fileName)))
            {
                if (!usingFileSystem)
                {
                    try
                    {
                        textEditor.Load(fileName);
                        textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(fileName));
                        Saved = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    using (Stream stream = GetResourceStream(fileName))
                    {
                        StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                        textEditor.Load(reader.BaseStream);
                        textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(fileName));
                        Saved = true;
                    }
                }
            }
            else
            {
                StackPanel stackPanel = new StackPanel() {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center
                };
                var text = new TextBlock()
                {
                    Text = "Type not supported",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    FontSize = 30,
                    TextWrapping = TextWrapping.Wrap,
                    
                };
                stackPanel.Children.Add(text);
                RadButton button = new RadButton() {Content="Open File",
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Tag = fileName
                };
                stackPanel.Children.Add(button);
                button.Click += Button_Click; ; ;
                this.Content = stackPanel;
            }

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //string fileName = (sender as RadButton).Tag.ToString();
            try
            {
                textEditor.Load(FullPath);
                textEditor.SyntaxHighlighting = HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(FullPath));
                this.Content = textEditor;
                Saved = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        protected static Stream GetResourceStream(string resourcePath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            List<string> resourceNames = new List<string>(assembly.GetManifestResourceNames());

            resourcePath = resourceNames.FirstOrDefault(r => r.EndsWith(resourcePath));

            if (resourcePath == null)
            {
                throw new FileNotFoundException("Resource not found");
            }

            return assembly.GetManifestResourceStream(resourcePath);
        }
    }
}
