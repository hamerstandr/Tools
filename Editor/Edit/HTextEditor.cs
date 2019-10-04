

using System.Windows;

namespace AvalonEdit
{
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Media;
    using AvalonEdit.BlockSurround;

    /// <summary>
    /// This part of the AvalonEdit extension contains additional
    /// dependendency properties and their OnChanged...() method (if any)
    /// </summary>
    public partial class HTextEditor : TextEditor
    {
        #region fields
        /// <summary>
        /// InsertBlocks DependencyProperty is the backing store for InsertBlocks.
        /// These are text blocks user can insert at the beginning and/or end of a selection.
        /// </summary>
        public static readonly DependencyProperty InsertBlocksProperty =
            DependencyProperty.Register("InsertBlocks",
                                        typeof(ObservableCollection<BlockDefinition>),
                                        typeof(HTextEditor),
                                        new PropertyMetadata(null));

        #region EditorCurrentLineBackground
        // Style the background color of the current editor line
        private static readonly DependencyProperty EditorCurrentLineBackgroundProperty =
            DependencyProperty.Register("EditorCurrentLineBackground",
                                         typeof(SolidColorBrush),
                                         typeof(HTextEditor),
                                         new UIPropertyMetadata(new SolidColorBrush(Color.FromArgb(33, 33, 33, 33)),
                                         HTextEditor.OnCurrentLineBackgroundChanged));
        #endregion EditorCurrentLineBackground

        #region CaretPosition
        private static readonly DependencyProperty ColumnProperty =
            DependencyProperty.Register("Column", typeof(int), typeof(HTextEditor), new UIPropertyMetadata(1));


        private static readonly DependencyProperty LineProperty =
            DependencyProperty.Register("Line", typeof(int), typeof(HTextEditor), new UIPropertyMetadata(1));
        #endregion CaretPosition

        #region EditorStateProperties
        /// <summary>
        /// Editor selection start
        /// </summary>
        private static readonly DependencyProperty EditorSelectionStartProperty =
            DependencyProperty.Register("EditorSelectionStart", typeof(int), typeof(HTextEditor),
                                        new FrameworkPropertyMetadata(0,
                                                      FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        /// <summary>
        /// Editor selection length
        /// </summary>
        private static readonly DependencyProperty EditorSelectionLengthProperty =
            DependencyProperty.Register("EditorSelectionLength", typeof(int), typeof(HTextEditor),
                                        new FrameworkPropertyMetadata(0));

        /// <summary>
        /// Selected text (if any) in text editor
        /// </summary>
        private static readonly DependencyProperty EditorSelectedTextProperty =
            DependencyProperty.Register("EditorSelectedText", typeof(string), typeof(HTextEditor),
                                                new FrameworkPropertyMetadata(string.Empty));

        /// <summary>
        /// TextEditor caret position
        /// </summary>
        private static readonly DependencyProperty EditorCaretOffsetProperty =
            DependencyProperty.Register("EditorCaretOffset", typeof(int), typeof(HTextEditor),
                                        new FrameworkPropertyMetadata(0));

        /// <summary>
        /// Determine whether current selection is a rectangle or not
        /// </summary>
        private static readonly DependencyProperty EditorIsRectangleSelectionProperty =
            DependencyProperty.Register("EditorIsRectangleSelection", typeof(bool), typeof(HTextEditor), new UIPropertyMetadata(false));

        #region EditorScrollOffsetXY
        /// <summary>
        /// Current editor view scroll X position
        /// </summary>
        public static readonly DependencyProperty EditorScrollOffsetXProperty =
            DependencyProperty.Register("EditorScrollOffsetX", typeof(double), typeof(HTextEditor), new UIPropertyMetadata(0.0));

        /// <summary>
        /// Current editor view scroll Y position
        /// </summary>
        public static readonly DependencyProperty EditorScrollOffsetYProperty =
            DependencyProperty.Register("EditorScrollOffsetY", typeof(double), typeof(HTextEditor), new UIPropertyMetadata(0.0));
        #endregion EditorScrollOffsetXY
        #endregion EditorStateProperties
        #endregion

        #region properties
        #region EditorCurrentLineBackground
        /// <summary>
        /// Style the background color of the current editor line
        /// </summary>
        public SolidColorBrush EditorCurrentLineBackground
        {
            get { return (SolidColorBrush)GetValue(EditorCurrentLineBackgroundProperty); }
            set { SetValue(EditorCurrentLineBackgroundProperty, value); }
        }
        #endregion EditorCurrentLineBackground

        #region CaretPosition
        /// <summary>
        /// Get/set the current column of the editor caret.
        /// </summary>
        public int Column
        {
            get
            {
                return (int)GetValue(ColumnProperty);
            }

            set
            {
                SetValue(ColumnProperty, value);
            }
        }

        /// <summary>
        /// Get/set the current line of the editor caret.
        /// </summary>
        public int Line
        {
            get
            {
                return (int)GetValue(LineProperty);
            }

            set
            {
                SetValue(LineProperty, value);
            }
        }
        #endregion CaretPosition

        #region EditorStateProperties
        /// <summary>
        /// Dependency property to allow ViewModel binding
        /// </summary>
        public int EditorSelectionStart
        {
            get
            {
                return (int)GetValue(HTextEditor.EditorSelectionStartProperty);
            }

            set
            {
                SetValue(HTextEditor.EditorSelectionStartProperty, value);
            }
        }

        /// <summary>
        /// Dependency property to allow ViewModel binding
        /// </summary>
        public int EditorSelectionLength
        {
            get
            {
                return (int)GetValue(HTextEditor.EditorSelectionLengthProperty);
            }

            set
            {
                SetValue(HTextEditor.EditorSelectionLengthProperty, value);
            }
        }

        /// <summary>
        /// Selected text (if any) in text editor
        /// </summary>
        public string EditorSelectedText
        {
            get
            {
                return (string)GetValue(EditorSelectedTextProperty);
            }

            set
            {
                SetValue(EditorSelectedTextProperty, value);
            }
        }

        /// <summary>
        /// Dependency property to allow ViewModel binding
        /// </summary>
        public int EditorCaretOffset
        {
            get
            {
                return (int)GetValue(HTextEditor.EditorCaretOffsetProperty);
            }

            set
            {
                SetValue(HTextEditor.EditorCaretOffsetProperty, value);
            }
        }

        /// <summary>
        /// Get property to determine whether ot not rectangle selection was used or not.
        /// </summary>
        public bool EditorIsRectangleSelection
        {
            get
            {
                return (bool)GetValue(EditorIsRectangleSelectionProperty);
            }

            set
            {
                SetValue(EditorIsRectangleSelectionProperty, value);
            }
        }

        #region EditorScrollOffsetXY
        /// <summary>
        /// Get/set dependency property to scroll editor by an offset in X direction.
        /// </summary>
        public double EditorScrollOffsetX
        {
            get
            {
                return (double)GetValue(EditorScrollOffsetXProperty);
            }

            set
            {
                SetValue(EditorScrollOffsetXProperty, value);
            }
        }

        /// <summary>
        /// Get/set dependency property to scroll editor by an offset in Y direction.
        /// </summary>
        public double EditorScrollOffsetY
        {
            get
            {
                return (double)GetValue(EditorScrollOffsetYProperty);
            }

            set
            {
                SetValue(EditorScrollOffsetYProperty, value);
            }
        }
        #endregion EditorScrollOffsetXY
        #endregion EditorStateProperties

        /// <summary>
        /// InsertBlocks DependencyProperty is the backing store for InsertBlocks.
        /// These are text blocks user can insert at the beginning and/or end of a selection.
        /// </summary>
        public ObservableCollection<BlockDefinition> InsertBlocks
        {
            get { return (ObservableCollection<BlockDefinition>)GetValue(InsertBlocksProperty); }
            set { SetValue(InsertBlocksProperty, value); }
        }
        #endregion properties

        #region methods
        /// <summary>
        /// The dependency property for has changed.
        /// Change the <seealso cref="SolidColorBrush"/> to be used for highlighting the current editor line
        /// in the particular <seealso cref="HTextEditor"/> control.
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void OnCurrentLineBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

            if (d is HTextEditor && e != null)
            {
                HTextEditor view = d as HTextEditor;

                if (e.NewValue is SolidColorBrush)
                {
                    SolidColorBrush newValue = e.NewValue as SolidColorBrush;
                    view.AdjustCurrentLineBackground(newValue);
                }
            }
        }
    }
    #endregion methods
}
namespace AvalonEdit.BracketRenderer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Searches matching brackets {[(...)]} for highlighting.
    /// </summary>
    public class EdiBracketSearcher : IBracketSearcher
    {
        string openingBrackets = "<([{";
        string closingBrackets = ">)]}";

        #region constructor
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="document"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public BracketSearchResult SearchBracket(AvalonEdit.Document.TextDocument document, int offset)
        {


            if (offset > 0)
            {
                char c = document.GetCharAt(offset - 1);
                int index = openingBrackets.IndexOf(c);
                int otherOffset = -1;
                if (index > -1)
                    otherOffset = SearchBracketForward(document, offset, openingBrackets[index], closingBrackets[index]);

                index = closingBrackets.IndexOf(c);
                if (index > -1)
                    otherOffset = SearchBracketBackward(document, offset - 2, openingBrackets[index], closingBrackets[index]);

                if (otherOffset > -1)
                    return new BracketSearchResult(Math.Min(offset - 1, otherOffset), 1,
                                                   Math.Max(offset - 1, otherOffset), 1);
            }

            return null;
        }
        #endregion constructor

        #region methods
        #region SearchBracket helper functions
        private static int ScanLineStart(AvalonEdit.Document.TextDocument document, int offset)
        {
            for (int i = offset - 1; i > 0; --i)
            {
                if (document.GetCharAt(i) == '\n')
                    return i + 1;
            }
            return 0;
        }

        /// <summary>
        /// Gets the type of code at offset.<br/>
        /// 0 = Code,<br/>
        /// 1 = Comment,<br/>
        /// 2 = String<br/>
        /// Block comments and multiline strings are not supported.
        /// </summary>
        private static int GetStartType(AvalonEdit.Document.TextDocument document, int linestart, int offset)
        {
            bool inString = false;
            bool inChar = false;
            bool verbatim = false;
            int result = 0;
            for (int i = linestart; i < offset; i++)
            {
                switch (document.GetCharAt(i))
                {
                    case '/':
                        if (!inString && !inChar && i + 1 < document.TextLength)
                        {
                            if (document.GetCharAt(i + 1) == '/')
                            {
                                result = 1;
                            }
                        }
                        break;
                    case '"':
                        if (!inChar)
                        {
                            if (inString && verbatim)
                            {
                                if (i + 1 < document.TextLength && document.GetCharAt(i + 1) == '"')
                                {
                                    ++i; // skip escaped quote
                                    inString = false; // let the string go on
                                }
                                else
                                {
                                    verbatim = false;
                                }
                            }
                            else if (!inString && i > 0 && document.GetCharAt(i - 1) == '@')
                            {
                                verbatim = true;
                            }
                            inString = !inString;
                        }
                        break;
                    case '\'':
                        if (!inString) inChar = !inChar;
                        break;
                    case '\\':
                        if ((inString && !verbatim) || inChar)
                            ++i; // skip next character
                        break;
                }
            }

            return (inString || inChar) ? 2 : result;
        }
        #endregion

        #region SearchBracketBackward
        private int SearchBracketBackward(AvalonEdit.Document.TextDocument document, int offset, char openBracket, char closingBracket)
        {


            if (offset + 1 >= document.TextLength) return -1;
            // this method parses a c# document backwards to find the matching bracket

            // first try "quick find" - find the matching bracket if there is no string/comment in the way
            int quickResult = QuickSearchBracketBackward(document, offset, openBracket, closingBracket);
            if (quickResult >= 0) return quickResult;

            // we need to parse the line from the beginning, so get the line start position
            int linestart = ScanLineStart(document, offset + 1);

            // we need to know where offset is - in a string/comment or in normal code?
            // ignore cases where offset is in a block comment
            int starttype = GetStartType(document, linestart, offset + 1);
            if (starttype == 1)
            {
                return -1; // start position is in a comment
            }

            // I don't see any possibility to parse a C# document backwards...
            // We have to do it forwards and push all bracket positions on a stack.
            Stack<int> bracketStack = new Stack<int>();
            bool blockComment = false;
            bool lineComment = false;
            bool inChar = false;
            bool inString = false;
            bool verbatim = false;

            for (int i = 0; i <= offset; ++i)
            {
                char ch = document.GetCharAt(i);
                switch (ch)
                {
                    case '\r':
                    case '\n':
                        lineComment = false;
                        inChar = false;
                        if (!verbatim) inString = false;
                        break;
                    case '/':
                        if (blockComment)
                        {
                            Debug.Assert(i > 0);
                            if (document.GetCharAt(i - 1) == '*')
                            {
                                blockComment = false;
                            }
                        }
                        if (!inString && !inChar && i + 1 < document.TextLength)
                        {
                            if (!blockComment && document.GetCharAt(i + 1) == '/')
                            {
                                lineComment = true;
                            }
                            if (!lineComment && document.GetCharAt(i + 1) == '*')
                            {
                                blockComment = true;
                            }
                        }
                        break;
                    case '"':
                        if (!(inChar || lineComment || blockComment))
                        {
                            if (inString && verbatim)
                            {
                                if (i + 1 < document.TextLength && document.GetCharAt(i + 1) == '"')
                                {
                                    ++i; // skip escaped quote
                                    inString = false; // let the string go
                                }
                                else
                                {
                                    verbatim = false;
                                }
                            }
                            else if (!inString && offset > 0 && document.GetCharAt(i - 1) == '@')
                            {
                                verbatim = true;
                            }
                            inString = !inString;
                        }
                        break;
                    case '\'':
                        if (!(inString || lineComment || blockComment))
                        {
                            inChar = !inChar;
                        }
                        break;
                    case '\\':
                        if ((inString && !verbatim) || inChar)
                            ++i; // skip next character
                        break;
                    default:
                        if (ch == openBracket)
                        {
                            if (!(inString || inChar || lineComment || blockComment))
                            {
                                bracketStack.Push(i);
                            }
                        }
                        else if (ch == closingBracket)
                        {
                            if (!(inString || inChar || lineComment || blockComment))
                            {
                                if (bracketStack.Count > 0)
                                    bracketStack.Pop();
                            }
                        }
                        break;
                }
            }
            if (bracketStack.Count > 0) return (int)bracketStack.Pop();
            return -1;
        }
        #endregion

        #region SearchBracketForward
        private int SearchBracketForward(AvalonEdit.Document.TextDocument document, int offset, char openBracket, char closingBracket)
        {
            bool inString = false;
            bool inChar = false;
            bool verbatim = false;

            bool lineComment = false;
            bool blockComment = false;

            if (offset < 0) return -1;

            // first try "quick find" - find the matching bracket if there is no string/comment in the way
            int quickResult = QuickSearchBracketForward(document, offset, openBracket, closingBracket);
            if (quickResult >= 0) return quickResult;

            // we need to parse the line from the beginning, so get the line start position
            int linestart = ScanLineStart(document, offset);

            // we need to know where offset is - in a string/comment or in normal code?
            // ignore cases where offset is in a block comment
            int starttype = GetStartType(document, linestart, offset);
            if (starttype != 0) return -1; // start position is in a comment/string

            int brackets = 1;

            while (offset < document.TextLength)
            {
                char ch = document.GetCharAt(offset);
                switch (ch)
                {
                    case '\r':
                    case '\n':
                        lineComment = false;
                        inChar = false;
                        if (!verbatim) inString = false;
                        break;
                    case '/':
                        if (blockComment)
                        {
                            Debug.Assert(offset > 0);
                            if (document.GetCharAt(offset - 1) == '*')
                            {
                                blockComment = false;
                            }
                        }
                        if (!inString && !inChar && offset + 1 < document.TextLength)
                        {
                            if (!blockComment && document.GetCharAt(offset + 1) == '/')
                            {
                                lineComment = true;
                            }
                            if (!lineComment && document.GetCharAt(offset + 1) == '*')
                            {
                                blockComment = true;
                            }
                        }
                        break;
                    case '"':
                        if (!(inChar || lineComment || blockComment))
                        {
                            if (inString && verbatim)
                            {
                                if (offset + 1 < document.TextLength && document.GetCharAt(offset + 1) == '"')
                                {
                                    ++offset; // skip escaped quote
                                    inString = false; // let the string go
                                }
                                else
                                {
                                    verbatim = false;
                                }
                            }
                            else if (!inString && offset > 0 && document.GetCharAt(offset - 1) == '@')
                            {
                                verbatim = true;
                            }
                            inString = !inString;
                        }
                        break;
                    case '\'':
                        if (!(inString || lineComment || blockComment))
                        {
                            inChar = !inChar;
                        }
                        break;
                    case '\\':
                        if ((inString && !verbatim) || inChar)
                            ++offset; // skip next character
                        break;
                    default:
                        if (ch == openBracket)
                        {
                            if (!(inString || inChar || lineComment || blockComment))
                            {
                                ++brackets;
                            }
                        }
                        else if (ch == closingBracket)
                        {
                            if (!(inString || inChar || lineComment || blockComment))
                            {
                                --brackets;
                                if (brackets == 0)
                                {
                                    return offset;
                                }
                            }
                        }
                        break;
                }
                ++offset;
            }
            return -1;
        }
        #endregion

        private int QuickSearchBracketBackward(AvalonEdit.Document.TextDocument document, int offset, char openBracket, char closingBracket)
        {
            int brackets = -1;
            // first try "quick find" - find the matching bracket if there is no string/comment in the way
            for (int i = offset; i >= 0; --i)
            {
                char ch = document.GetCharAt(i);
                if (ch == openBracket)
                {
                    ++brackets;
                    if (brackets == 0) return i;
                }
                else if (ch == closingBracket)
                {
                    --brackets;
                }
                else if (ch == '"')
                {
                    break;
                }
                else if (ch == '\'')
                {
                    break;
                }
                else if (ch == '/' && i > 0)
                {
                    if (document.GetCharAt(i - 1) == '/') break;
                    if (document.GetCharAt(i - 1) == '*') break;
                }
            }
            return -1;
        }

        private int QuickSearchBracketForward(AvalonEdit.Document.TextDocument document, int offset, char openBracket, char closingBracket)
        {
            int brackets = 1;
            // try "quick find" - find the matching bracket if there is no string/comment in the way
            for (int i = offset; i < document.TextLength; ++i)
            {
                char ch = document.GetCharAt(i);
                if (ch == openBracket)
                {
                    ++brackets;
                }
                else if (ch == closingBracket)
                {
                    --brackets;
                    if (brackets == 0) return i;
                }
                else if (ch == '"')
                {
                    break;
                }
                else if (ch == '\'')
                {
                    break;
                }
                else if (ch == '/' && i > 0)
                {
                    if (document.GetCharAt(i - 1) == '/') break;
                }
                else if (ch == '*' && i > 0)
                {
                    if (document.GetCharAt(i - 1) == '/') break;
                }
            }
            return -1;
        }
        #endregion methods
    }
}
namespace AvalonEdit.BracketRenderer
{
    /// <summary>
    /// Allows language specific search for matching brackets.
    /// </summary>
    public interface IBracketSearcher
    {
        /// <summary>
        /// Searches for a matching bracket from the given offset to the start of the document.
        /// </summary>
        /// <returns>A BracketSearchResult that contains the positions and lengths of the brackets. Return null if there is nothing to highlight.</returns>
        BracketSearchResult SearchBracket(AvalonEdit.Document.TextDocument document, int offset);
    }
}
namespace AvalonEdit.BracketRenderer
{
    /// <summary>
    /// Describes a pair of matching brackets found by IBracketSearcher.
    /// </summary>
    public class BracketSearchResult
    {
        #region class constructor
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="openingBracketOffset"></param>
        /// <param name="openingBracketLength"></param>
        /// <param name="closingBracketOffset"></param>
        /// <param name="closingBracketLength"></param>
        public BracketSearchResult(int openingBracketOffset, int openingBracketLength,
                                   int closingBracketOffset, int closingBracketLength)
        {
            this.OpeningBracketOffset = openingBracketOffset;
            this.OpeningBracketLength = openingBracketLength;
            this.ClosingBracketOffset = closingBracketOffset;
            this.ClosingBracketLength = closingBracketLength;
        }
        #endregion class constructor

        #region properties
        /// <summary>
        /// Text offset of the opening/starting bracket.
        /// </summary>
        public int OpeningBracketOffset { get; private set; }

        /// <summary>
        /// Length of the opening/starting bracket.
        /// </summary>
        public int OpeningBracketLength { get; private set; }

        /// <summary>
        /// Text offset of the closing/ending bracket.
        /// </summary>
        public int ClosingBracketOffset { get; private set; }

        /// <summary>
        /// Length of the closing/ending bracket.
        /// </summary>
        public int ClosingBracketLength { get; private set; }
        #endregion properties
    }
}
namespace AvalonEdit.BlockSurround
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using AvalonEdit.BracketRenderer;
    using AvalonEdit.BlockSurround;
    using AvalonEdit.Editing;
    using AvalonEdit.Rendering;
    using AvalonEdit.Utils;

    /// <summary>
    /// </summary>
    public partial class HTextEditor : TextEditor
    {
        #region fields
        static readonly List<CommandBinding> CmdBindings = new List<CommandBinding>();
        ////static readonly List<InputBinding> InputBindings = new List<InputBinding>();

        // Highlight opening and closing brackets in editor
        BracketHighlightRenderer mBracketRenderer = null;
        EdiBracketSearcher FindBrackets = null;
        #endregion fields

        #region constructor
        /// <summary>
        /// Static class constructor to register style key and commands
        /// </summary>
        static HTextEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HTextEditor), new FrameworkPropertyMetadata(typeof(HTextEditor)));
            FocusableProperty.OverrideMetadata(typeof(HTextEditor), new FrameworkPropertyMetadata(Boxes.True));

            CmdBindings.Add(new CommandBinding(HTextEditorCommands.FoldsCollapseAll, HTextEditor.FoldsCollapseAll, HTextEditor.FoldsColapseExpandCanExecute));
            CmdBindings.Add(new CommandBinding(HTextEditorCommands.FoldsExpandAll, HTextEditor.FoldsExpandAll, HTextEditor.FoldsColapseExpandCanExecute));
            CmdBindings.Add(new CommandBinding(HTextEditorCommands.PrintDocument, HTextEditor.PrintDocument, HTextEditor.PrintDocumentCanExecute));
        }

        /// <summary>
        /// Class Constructor
        /// (review the OnApplyTemplate() method for most construction routines)
        /// </summary>
        public HTextEditor()
          : base()
        {
            // This is default list of viewing scales that is bound by the default XAML skin in HTextEditor.xaml
            // Consumers can use other lists by binding to their viewmodel when re-styling the control in the
            // applications resource dictionary (therefore, this list is not localized)
            this.ScaleList = new ObservableCollection<string>(){ "20 %",
                                                           "50 %",
                                                           "75 %",
                                                           "100 %",
                                                           "125 %",
                                                           "150 %",
                                                           "175 %",
                                                           "200 %",
                                                           "300 %",
                                                           "400 %"};

            // Copy static collection of commands to collection of commands of this instance
            this.CommandBindings.AddRange(HTextEditor.CmdBindings);
        }
        #endregion constructor

        #region properties
        #region ScaleViewOutput
        /// <summary>
        /// Get/Set viewmodel to display a customizable font size
        /// This is default list of viewing scales that is bound by the default XAML skin in HTextEditor.xaml
        /// Consumers can use other lists by binding to their viewmodel when re-styling the control in the
        /// applications resource dictionary
        /// </summary>
        public ObservableCollection<string> ScaleList { get; set; }
        #endregion ScaleViewOutput
        #endregion properties

        #region methods
        /// <summary>
        /// Standard <seealso cref="System.Windows.Controls.Control"/> method to be executed when the
        /// framework applies the XAML definition of the lookless control.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.Loaded += new RoutedEventHandler(this.OnLoaded);
            this.Unloaded += new RoutedEventHandler(this.OnUnloaded);

            // Highlight current line in editor (even if editor is not focused) via themable dp-property
            this.AdjustCurrentLineBackground(this.EditorCurrentLineBackground);

            // Update highlighting of current line when caret position is changed
            this.TextArea.Caret.PositionChanged += Caret_PositionChanged;
        }

        /// <summary>
        /// Hock event handlers and restore editor states (if any) or defaults
        /// when the control is fully loaded.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        private void OnLoaded(object obj, RoutedEventArgs args)
        {
            // Call folding once upon loading so that first run is executed right away
            this.foldingUpdateTimer_Tick(null, null);

            this.mFoldingUpdateTimer = new DispatcherTimer();
            this.mFoldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
            this.mFoldingUpdateTimer.Tick += this.foldingUpdateTimer_Tick;
            this.mFoldingUpdateTimer.Start();

            // Connect CompletionWindow Listners
            try
            {
                this.HTextEditor_OptionChanged(null, null);
                this.OptionChanged += HTextEditor_OptionChanged;
            }
            catch
            {
            }

            try
            {
                this.Focus();
                this.ForceCursor = true;

                // Restore cusor position for CTRL-TAB Support http://avalondock.codeplex.com/workitem/15079
                this.ScrollToHorizontalOffset(this.EditorScrollOffsetX);
                this.ScrollToVerticalOffset(this.EditorScrollOffsetY);
            }
            catch
            {
            }

            try
            {
                if (this.EditorIsRectangleSelection == true)
                {
                    this.CaretOffset = this.EditorCaretOffset;

                    this.SelectionStart = this.EditorSelectionStart;
                    this.SelectionLength = this.EditorSelectionLength;

                    // See OnMouseCaretBoxSelection in Editing\CaretNavigationCommandHandler.cs
                    // Convert normal selection to rectangle selection
                    this.TextArea.Selection = new AvalonEdit.Editing.RectangleSelection(this.TextArea,
                                                                                                    this.TextArea.Selection.StartPosition,
                                                                                                    this.TextArea.Selection.EndPosition);
                }
                else
                {
                    this.CaretOffset = this.EditorCaretOffset;

                    // http://www.codeproject.com/Articles/42490/Using-AvalonEdit-WPF-Text-Editor?msg=4388281#xx4388281xx
                    this.Select(this.EditorSelectionStart, this.EditorSelectionLength);
                }
            }
            catch
            {
            }

            // Attach mouse wheel CTRL-key zoom support
            this.PreviewMouseWheel += new System.Windows.Input.MouseWheelEventHandler(textEditor_PreviewMouseWheel);
            this.KeyDown += new KeyEventHandler(this.textEditor_KeyDown);
        }

        /// <summary>
        /// Setup options and parameters that may depend on chnaging options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HTextEditor_OptionChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (this.Options != null)
            {
                this.TextArea.TextEntering -= TextEditorTextAreaTextEntering;
                this.TextArea.TextEntered -= TextEditorTextAreaTextEntered;

                if (this.Options.EnableCodeCompletion == true)
                {
                    this.TextArea.TextEntering += TextEditorTextAreaTextEntering;
                    this.TextArea.TextEntered += TextEditorTextAreaTextEntered;
                }
            }
        }

        /// <summary>
        /// Add/Remove surrounding tags when pressing a certain key sequence (eg.: Ctrl+1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditor_KeyDown(object sender, KeyEventArgs e)
        {
            ModifierKeys mod = System.Windows.Input.Keyboard.Modifiers;

            if (this.InsertBlocks != null)
            {
                IEnumerable<BlockDefinition> sel = this.InsertBlocks.Where(i => i.Key == e.Key && e.KeyboardDevice.Modifiers == i.Modifier);

                if (sel == null)
                    return;

                foreach (var item in sel)           // Press 'Ctrl+1' to add remove tags surrounding current selection
                {
                    //// if (e.Key == System.Windows.Input.Key.D1 &&
                    //// (System.Windows.Input.Keyboard.Modifiers & System.Windows.Input.ModifierKeys.Control) ==
                    ////     System.Windows.Input.ModifierKeys.Control)
                    {
                        try
                        {
                            // Make sure that this change is in one UNDO/Redo step
                            this.BeginChange();
                            AvalonHelper.SurroundSelectionWithBlockComment(this, item);
                        }
                        catch (Exception exp)
                        {
                            Console.WriteLine(exp.ToString());
                        }
                        finally
                        {
                            // Make sure that this change is in one UNDO/Redo step
                            this.EndChange();
                            e.Handled = true;
                        }

                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Unhock event handlers and save editor states (to be recovered later)
        /// when the control is unloaded.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="args"></param>
        private void OnUnloaded(object obj, RoutedEventArgs args)
        {
            if (this.mFoldingUpdateTimer != null)
                this.mFoldingUpdateTimer = null;

            this.TextArea.TextEntering -= TextEditorTextAreaTextEntering;
            this.TextArea.TextEntered -= TextEditorTextAreaTextEntered;

            // Save cusor position for CTRL-TAB Support http://avalondock.codeplex.com/workitem/15079
            this.EditorCaretOffset = this.CaretOffset;
            this.EditorSelectionStart = this.SelectionStart;
            this.EditorSelectionLength = this.SelectionLength;
            this.EditorIsRectangleSelection = this.TextArea.Selection is RectangleSelection;

            // http://stackoverflow.com/questions/11863273/avalonedit-how-to-get-the-top-visible-line
            this.EditorScrollOffsetX = this.TextArea.TextView.ScrollOffset.X;
            this.EditorScrollOffsetY = this.TextArea.TextView.ScrollOffset.Y;

            // Detach mouse wheel CTRL-key zoom support
            // This does not work when doing mouse zoom and CTRL-TAB between two documents and trying to do mouse zoom???
            this.PreviewMouseWheel -= textEditor_PreviewMouseWheel;
        }

        /// <summary>
        /// Reset the <seealso cref="SolidColorBrush"/> to be used for highlighting the current editor line.
        /// </summary>
        /// <param name="newValue"></param>
        private void AdjustCurrentLineBackground(SolidColorBrush newValue)
        {
            if (newValue != null)
            {
                HighlightCurrentLineBackgroundRenderer oldRenderer = null;

                // Make sure there is only one of this type of background renderer
                // Otherwise, we might keep adding and WPF keeps drawing them on top of each other
                foreach (var item in this.TextArea.TextView.BackgroundRenderers)
                {
                    if (item != null)
                    {
                        if (item is HighlightCurrentLineBackgroundRenderer)
                        {
                            oldRenderer = item as HighlightCurrentLineBackgroundRenderer;
                        }
                    }
                }

                this.TextArea.TextView.BackgroundRenderers.Remove(oldRenderer);

                this.TextArea.TextView.BackgroundRenderers.Add(new HighlightCurrentLineBackgroundRenderer(this, newValue.Clone()));

                // Remove reference to old background renderer instance (if any) and construct BracketRenderer from scratch
                if (this.mBracketRenderer != null)
                {
                    this.TextArea.TextView.BackgroundRenderers.Remove(this.mBracketRenderer);
                    this.mBracketRenderer = null;
                }

                this.mBracketRenderer = new BracketHighlightRenderer(this.TextArea.TextView);
                this.TextArea.TextView.BackgroundRenderers.Add(this.mBracketRenderer);
            }
        }

        /// <summary>
        /// Update Column and Line position properties when caret position is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Caret_PositionChanged(object sender, EventArgs e)
        {
            // Highlight opening and closing brackets when the carret position changes
            try
            {
                HighlightBrackets(sender, e);
            }
            catch
            {
            }

            this.TextArea.TextView.InvalidateLayer(KnownLayer.Background); //Update current line highlighting

            if (this.TextArea != null)
            {
                this.Column = this.TextArea.Caret.Column;
                this.Line = this.TextArea.Caret.Line;
            }
            else
                this.Column = this.Line = 0;
        }

        /// <summary>
        /// Highlights matching brackets.
        /// </summary>
        private void HighlightBrackets(object sender, EventArgs e)
        {
            if (this.TextArea.Options.EnableHighlightBrackets == true)
            {
                if (this.FindBrackets == null)
                    this.FindBrackets = new EdiBracketSearcher();

                var bracketSearchResult = FindBrackets.SearchBracket(this.Document, this.TextArea.Caret.Offset);
                this.mBracketRenderer.SetHighlight(bracketSearchResult);
            }
            else
            {
                this.mBracketRenderer.SetHighlight(null);
            }
        }

        /// <summary>
        /// This method is triggered on a MouseWheel preview event to check if the user
        /// is also holding down the CTRL Key and adjust the current font size if so.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textEditor_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                double fontSize = this.FontSize + e.Delta / 25.0;

                if (fontSize < 6)
                    this.FontSize = 6;
                else
                {
                    if (fontSize > 200)
                        this.FontSize = 200;
                    else
                        this.FontSize = fontSize;
                }

                e.Handled = true;
            }
        }
        #endregion methods
    }
}
namespace AvalonEdit.BlockSurround
{
    /// <summary>
    /// Class can be used to mark/unmark text in a block of code.
    /// 
    /// Workflow:
    /// - The user selects a part of the text and hits CTRL+1
    /// - The editor marks the selected text as comment or
    ///   uncomments the selected section if the user selected
    ///   a comment.
    /// </summary>
    public class BlockDefinition
    {
        #region constructor
        /// <summary>
        /// Class constructor
        /// </summary>
        public BlockDefinition(string blockstart,
                             string blockend,
                             BlockAt typeOfBlock,
                             string fileextension,
                             System.Windows.Input.Key key,
                             System.Windows.Input.ModifierKeys modifier = 0)
          : this()
        {
            this.TypeOfBlock = typeOfBlock;
            this.StartBlock = blockstart;
            this.EndBlock = blockend;
            this.FileExtension = fileextension;

            this.Key = key;
            this.Modifier = modifier;
        }

        /// <summary>
        /// Hide standard constructor to ensur construction with minimal
        /// required data items.
        /// </summary>
        /// <returns></returns>
        protected BlockDefinition()
        {
            this.TypeOfBlock = BlockAt.StartAndEnd;
            this.StartBlock = this.EndBlock = this.FileExtension = string.Empty;

            this.Key = System.Windows.Input.Key.D1;
            this.Modifier = System.Windows.Input.ModifierKeys.Control;
        }
        #endregion constructor

        #region enum
        /// <summary>
        /// Define whether a selection is parsed/edit
        /// at both ends or only at one end.
        /// </summary>
        public enum BlockAt
        {
            /// <summary>
            /// Edit selected text at its start only
            /// </summary>
            Start,

            /// <summary>
            /// Edit selected text at its end only
            /// </summary>
            End,

            /// <summary>
            /// Edit selected text at start and end.
            /// </summary>
            StartAndEnd,
        }
        #endregion enum

        #region properties
        /// <summary>
        /// Get/set type of block selection/change
        /// </summary>
        public BlockAt TypeOfBlock { get; set; }

        /// <summary>
        /// String to remove/add at the begining of a selection.
        /// </summary>
        public string StartBlock { get; set; }

        /// <summary>
        /// String to remove/add at the end of a selection.
        /// </summary>
        public string EndBlock { get; set; }

        /// <summary>
        /// Configures the file extension for which this selection should be applied.
        /// </summary>
        public string FileExtension { get; set; }

        /// <summary>
        /// Configures the key that a user can use to apply the selection add/remove function.
        /// </summary>
        public System.Windows.Input.Key Key { get; set; }

        /// <summary>
        /// Configures the key modifier (if any) that a user can use to apply the selection add/remove function.
        /// </summary>
        public System.Windows.Input.ModifierKeys Modifier { get; set; }
        #endregion properties
    }
}
namespace AvalonEdit.BracketRenderer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Media;
    using AvalonEdit.Document;
    using AvalonEdit.Rendering;

    /// <summary>
    /// Highlight opening and closing brackets when when moving the carret in the text
    /// 
    /// Source: https://github.com/icsharpcode/SharpDevelop/blob/master/src/AddIns/DisplayBindings/AvalonEdit.AddIn/Src/BracketHighlightRenderer.cs
    /// </summary>
    public class BracketHighlightRenderer : IBackgroundRenderer
    {
        #region fields
        /// <summary>
        /// Default color (blue) used for bracket highlighting.
        /// </summary>
        public static readonly Color DefaultBackground = Color.FromArgb(100, 0, 0, 255);

        /// <summary>
        /// Default border (bright blue) used for bracket highlighting.
        /// </summary>
        public static readonly Color DefaultBorder = Color.FromArgb(52, 0, 0, 255);

        ////public const string BracketHighlight = "Bracket highlight";

        private BracketSearchResult mResult;
        private Pen mBorderPen;
        private Brush mBackgroundBrush;
        private TextView mTextView;
        #endregion fields

        #region constructor
        /// <summary>
        /// Class constructor from <seealso cref="TextView"/> instance.
        /// </summary>
        /// <param name="textView"></param>
        public BracketHighlightRenderer(TextView textView)
        {
            if (textView == null)
                throw new ArgumentNullException("textView");

            this.mTextView = textView;

            this.mTextView.BackgroundRenderers.Add(this);
        }
        #endregion constructor

        #region methods
        ////    public static void ApplyCustomizationsToRendering(BracketHighlightRenderer renderer, IEnumerable<Color> customizations)
        ////    {
        ////      renderer.UpdateColors(DefaultBackground, DefaultBorder);
        ////
        ////      foreach (Color color in customizations)
        ////      {
        ////        //if (color.Name == BracketHighlight) {
        ////        renderer.UpdateColors(color, color);
        ////        //					renderer.UpdateColors(color.Background ?? Colors.Blue, color.Foreground ?? Colors.Blue);
        ////        // 'break;' is necessary because more specific customizations come first in the list
        ////        // (language-specific customizations are first, followed by 'all languages' customizations)
        ////        break;
        ////        //}
        ////      }
        ////    }

        /// <summary>
        /// Assigns the indicated highlighting mResult in <paramref name="result"/>
        /// and invalidates the corresponding layer to force a redraw.
        /// </summary>
        /// <param name="result"></param>
        public void SetHighlight(BracketSearchResult result)
        {
            if (this.mResult != result)
            {
                this.mResult = result;
                mTextView.InvalidateLayer(this.Layer);
            }
        }

        /// <summary>
        /// Gets the <seealso cref="KnownLayer"/> that is used to highlight brackets
        /// within the text.
        /// </summary>
        public KnownLayer Layer
        {
            get
            {
                return KnownLayer.Selection;
            }
        }

        /// <summary>
        /// Draw method for drawing highlighted brackets.
        /// </summary>
        /// <param name="textView"></param>
        /// <param name="drawingContext"></param>
        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (this.mResult == null)
                return;

            BackgroundGeometryBuilder builder = new BackgroundGeometryBuilder();

            builder.CornerRadius = 1;
            builder.AlignToMiddleOfPixels = true;

            builder.AddSegment(textView, new TextSegment()
            {
                StartOffset = mResult.OpeningBracketOffset,
                Length = mResult.OpeningBracketLength
            });

            builder.CloseFigure(); // prevent connecting the two segments

            builder.AddSegment(textView, new TextSegment()
            {
                StartOffset = mResult.ClosingBracketOffset,
                Length = mResult.ClosingBracketLength
            });

            Geometry geometry = builder.CreateGeometry();

            if (mBorderPen == null)
                this.UpdateColors(DefaultBackground, DefaultBackground);

            if (geometry != null)
            {
                drawingContext.DrawGeometry(mBackgroundBrush, mBorderPen, geometry);
            }
        }

        /// <summary>
        /// Updates the color definition used for the highlighting of brackets.
        /// </summary>
        /// <param name="background"></param>
        /// <param name="foreground"></param>
        private void UpdateColors(Color background, Color foreground)
        {
            this.mBorderPen = new Pen(new SolidColorBrush(foreground), 1);
            this.mBorderPen.Freeze();

            this.mBackgroundBrush = new SolidColorBrush(background);
            this.mBackgroundBrush.Freeze();
        }
        #endregion methods
    }
}
namespace AvalonEdit.Rendering
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.TextFormatting;
    using AvalonEdit.Document;
    using AvalonEdit.Editing;
    using AvalonEdit.Utils;
    /// <summary>
    /// Helper for creating a PathGeometry.
    /// </summary>
    public sealed class BackgroundGeometryBuilder
    {
        double cornerRadius;

        /// <summary>
        /// Gets/sets the radius of the rounded corners.
        /// </summary>
        public double CornerRadius
        {
            get { return cornerRadius; }
            set { cornerRadius = value; }
        }

        /// <summary>
        /// Gets/Sets whether to align to whole pixels.
        /// 
        /// If BorderThickness is set to 0, the geometry is aligned to whole pixels.
        /// If BorderThickness is set to a non-zero value, the outer edge of the border is aligned
        /// to whole pixels.
        /// 
        /// The default value is <c>false</c>.
        /// </summary>
        public bool AlignToWholePixels { get; set; }

        /// <summary>
        /// Gets/sets the border thickness.
        /// 
        /// This property only has an effect if <c>AlignToWholePixels</c> is enabled.
        /// When using the resulting geometry to paint a border, set this property to the border thickness.
        /// Otherwise, leave the property set to the default value <c>0</c>.
        /// </summary>
        public double BorderThickness { get; set; }

        bool alignToMiddleOfPixels;

        /// <summary>
        /// Gets/Sets whether to align the geometry to the middle of pixels.
        /// </summary>
        [Obsolete("Use the AlignToWholePixels and BorderThickness properties instead. "
                  + "Setting AlignToWholePixels=true and setting the BorderThickness to the pixel size "
                  + "is equivalent to aligning the geometry to the middle of pixels.")]
        public bool AlignToMiddleOfPixels
        {
            get
            {
                return alignToMiddleOfPixels;
            }
            set
            {
                alignToMiddleOfPixels = value;
            }
        }

        /// <summary>
        /// Gets/Sets whether to extend the rectangles to full width at line end.
        /// </summary>
        public bool ExtendToFullWidthAtLineEnd { get; set; }

        /// <summary>
        /// Creates a new BackgroundGeometryBuilder instance.
        /// </summary>
        public BackgroundGeometryBuilder()
        {
        }

        /// <summary>
        /// Adds the specified segment to the geometry.
        /// </summary>
        public void AddSegment(TextView textView, ISegment segment)
        {
            if (textView == null)
                throw new ArgumentNullException("textView");
            Size pixelSize = PixelSnapHelpers.GetPixelSize(textView);
            foreach (Rect r in GetRectsForSegment(textView, segment, ExtendToFullWidthAtLineEnd))
            {
                AddRectangle(pixelSize, r);
            }
        }

        /// <summary>
        /// Adds a rectangle to the geometry.
        /// </summary>
        /// <remarks>
        /// This overload will align the coordinates according to
        /// <see cref="AlignToWholePixels"/> or <see cref="AlignToMiddleOfPixels"/>.
        /// Use the <see cref="AddRectangle(double,double,double,double)"/>-overload instead if the coordinates should not be aligned.
        /// </remarks>
        public void AddRectangle(TextView textView, Rect rectangle)
        {
            AddRectangle(PixelSnapHelpers.GetPixelSize(textView), rectangle);
        }

        void AddRectangle(Size pixelSize, Rect r)
        {
            if (AlignToWholePixels)
            {
                double halfBorder = 0.5 * BorderThickness;
                AddRectangle(PixelSnapHelpers.Round(r.Left - halfBorder, pixelSize.Width) + halfBorder,
                             PixelSnapHelpers.Round(r.Top - halfBorder, pixelSize.Height) + halfBorder,
                             PixelSnapHelpers.Round(r.Right + halfBorder, pixelSize.Width) - halfBorder,
                             PixelSnapHelpers.Round(r.Bottom + halfBorder, pixelSize.Height) - halfBorder);
                //Debug.WriteLine(r.ToString() + " -> " + new Rect(lastLeft, lastTop, lastRight-lastLeft, lastBottom-lastTop).ToString());
            }
            else if (alignToMiddleOfPixels)
            {
                AddRectangle(PixelSnapHelpers.PixelAlign(r.Left, pixelSize.Width),
                             PixelSnapHelpers.PixelAlign(r.Top, pixelSize.Height),
                             PixelSnapHelpers.PixelAlign(r.Right, pixelSize.Width),
                             PixelSnapHelpers.PixelAlign(r.Bottom, pixelSize.Height));
            }
            else
            {
                AddRectangle(r.Left, r.Top, r.Right, r.Bottom);
            }
        }

        /// <summary>
        /// Calculates the list of rectangle where the segment in shown.
        /// This method usually returns one rectangle for each line inside the segment
        /// (but potentially more, e.g. when bidirectional text is involved).
        /// </summary>
        public static IEnumerable<Rect> GetRectsForSegment(TextView textView, ISegment segment, bool extendToFullWidthAtLineEnd = false)
        {
            if (textView == null)
                throw new ArgumentNullException("textView");
            if (segment == null)
                throw new ArgumentNullException("segment");
            return GetRectsForSegmentImpl(textView, segment, extendToFullWidthAtLineEnd);
        }

        static IEnumerable<Rect> GetRectsForSegmentImpl(TextView textView, ISegment segment, bool extendToFullWidthAtLineEnd)
        {
            int segmentStart = segment.Offset;
            int segmentEnd = segment.Offset + segment.Length;

            segmentStart = segmentStart.CoerceValue(0, textView.Document.TextLength);
            segmentEnd = segmentEnd.CoerceValue(0, textView.Document.TextLength);

            TextViewPosition start;
            TextViewPosition end;

            if (segment is SelectionSegment)
            {
                SelectionSegment sel = (SelectionSegment)segment;
                start = new TextViewPosition(textView.Document.GetLocation(sel.StartOffset), sel.StartVisualColumn);
                end = new TextViewPosition(textView.Document.GetLocation(sel.EndOffset), sel.EndVisualColumn);
            }
            else
            {
                start = new TextViewPosition(textView.Document.GetLocation(segmentStart));
                end = new TextViewPosition(textView.Document.GetLocation(segmentEnd));
            }

            foreach (VisualLine vl in textView.VisualLines)
            {
                int vlStartOffset = vl.FirstDocumentLine.Offset;
                if (vlStartOffset > segmentEnd)
                    break;
                int vlEndOffset = vl.LastDocumentLine.Offset + vl.LastDocumentLine.Length;
                if (vlEndOffset < segmentStart)
                    continue;

                int segmentStartVC;
                if (segmentStart < vlStartOffset)
                    segmentStartVC = 0;
                else
                    segmentStartVC = vl.ValidateVisualColumn(start, extendToFullWidthAtLineEnd);

                int segmentEndVC;
                if (segmentEnd > vlEndOffset)
                    segmentEndVC = extendToFullWidthAtLineEnd ? int.MaxValue : vl.VisualLengthWithEndOfLineMarker;
                else
                    segmentEndVC = vl.ValidateVisualColumn(end, extendToFullWidthAtLineEnd);

                foreach (var rect in ProcessTextLines(textView, vl, segmentStartVC, segmentEndVC))
                    yield return rect;
            }
        }

        /// <summary>
        /// Calculates the rectangles for the visual column segment.
        /// This returns one rectangle for each line inside the segment.
        /// </summary>
        public static IEnumerable<Rect> GetRectsFromVisualSegment(TextView textView, VisualLine line, int startVC, int endVC)
        {
            if (textView == null)
                throw new ArgumentNullException("textView");
            if (line == null)
                throw new ArgumentNullException("line");
            return ProcessTextLines(textView, line, startVC, endVC);
        }

        static IEnumerable<Rect> ProcessTextLines(TextView textView, VisualLine visualLine, int segmentStartVC, int segmentEndVC)
        {
            TextLine lastTextLine = visualLine.TextLines.Last();
            Vector scrollOffset = textView.ScrollOffset;

            for (int i = 0; i < visualLine.TextLines.Count; i++)
            {
                TextLine line = visualLine.TextLines[i];
                double y = visualLine.GetTextLineVisualYPosition(line, VisualYPosition.LineTop);
                int visualStartCol = visualLine.GetTextLineVisualStartColumn(line);
                int visualEndCol = visualStartCol + line.Length;
                if (line != lastTextLine)
                    visualEndCol -= line.TrailingWhitespaceLength;

                if (segmentEndVC < visualStartCol)
                    break;
                if (lastTextLine != line && segmentStartVC > visualEndCol)
                    continue;
                int segmentStartVCInLine = Math.Max(segmentStartVC, visualStartCol);
                int segmentEndVCInLine = Math.Min(segmentEndVC, visualEndCol);
                y -= scrollOffset.Y;
                if (segmentStartVCInLine == segmentEndVCInLine)
                {
                    // GetTextBounds crashes for length=0, so we'll handle this case with GetDistanceFromCharacterHit
                    // We need to return a rectangle to ensure empty lines are still visible
                    double pos = visualLine.GetTextLineVisualXPosition(line, segmentStartVCInLine);
                    pos -= scrollOffset.X;
                    // The following special cases are necessary to get rid of empty rectangles at the end of a TextLine if "Show Spaces" is active.
                    // If not excluded once, the same rectangle is calculated (and added) twice (since the offset could be mapped to two visual positions; end/start of line), if there is no trailing whitespace.
                    // Skip this TextLine segment, if it is at the end of this line and this line is not the last line of the VisualLine and the selection continues and there is no trailing whitespace.
                    if (segmentEndVCInLine == visualEndCol && i < visualLine.TextLines.Count - 1 && segmentEndVC > segmentEndVCInLine && line.TrailingWhitespaceLength == 0)
                        continue;
                    if (segmentStartVCInLine == visualStartCol && i > 0 && segmentStartVC < segmentStartVCInLine && visualLine.TextLines[i - 1].TrailingWhitespaceLength == 0)
                        continue;
                    yield return new Rect(pos, y, 1, line.Height);
                }
                else
                {
                    Rect lastRect = Rect.Empty;
                    if (segmentStartVCInLine <= visualEndCol)
                    {
                        foreach (TextBounds b in line.GetTextBounds(segmentStartVCInLine, segmentEndVCInLine - segmentStartVCInLine))
                        {
                            double left = b.Rectangle.Left - scrollOffset.X;
                            double right = b.Rectangle.Right - scrollOffset.X;
                            if (!lastRect.IsEmpty)
                                yield return lastRect;
                            // left>right is possible in RTL languages
                            lastRect = new Rect(Math.Min(left, right), y, Math.Abs(right - left), line.Height);
                        }
                    }
                    if (segmentEndVC >= visualLine.VisualLengthWithEndOfLineMarker)
                    {
                        double left = (segmentStartVC > visualLine.VisualLengthWithEndOfLineMarker ? visualLine.GetTextLineVisualXPosition(lastTextLine, segmentStartVC) : line.Width) - scrollOffset.X;
                        double right = ((segmentEndVC == int.MaxValue || line != lastTextLine) ? Math.Max(((IScrollInfo)textView).ExtentWidth, ((IScrollInfo)textView).ViewportWidth) : visualLine.GetTextLineVisualXPosition(lastTextLine, segmentEndVC)) - scrollOffset.X;
                        Rect extendSelection = new Rect(Math.Min(left, right), y, Math.Abs(right - left), line.Height);
                        if (!lastRect.IsEmpty)
                        {
                            if (extendSelection.IntersectsWith(lastRect))
                            {
                                lastRect.Union(extendSelection);
                                yield return lastRect;
                            }
                            else
                            {
                                yield return lastRect;
                                yield return extendSelection;
                            }
                        }
                        else
                            yield return extendSelection;
                    }
                    else
                        yield return lastRect;
                }
            }
        }

        PathFigureCollection figures = new PathFigureCollection();
        PathFigure figure;
        int insertionIndex;
        double lastTop, lastBottom;
        double lastLeft, lastRight;

        /// <summary>
        /// Adds a rectangle to the geometry.
        /// </summary>
        /// <remarks>
        /// This overload assumes that the coordinates are aligned properly
        /// (see <see cref="AlignToWholePixels"/>, <see cref="AlignToMiddleOfPixels"/>).
        /// Use the <see cref="AddRectangle(TextView,Rect)"/>-overload instead if the coordinates are not yet aligned.
        /// </remarks>
        public void AddRectangle(double left, double top, double right, double bottom)
        {
            if (!top.IsClose(lastBottom))
            {
                CloseFigure();
            }
            if (figure == null)
            {
                figure = new PathFigure();
                figure.StartPoint = new Point(left, top + cornerRadius);
                if (Math.Abs(left - right) > cornerRadius)
                {
                    figure.Segments.Add(MakeArc(left + cornerRadius, top, SweepDirection.Clockwise));
                    figure.Segments.Add(MakeLineSegment(right - cornerRadius, top));
                    figure.Segments.Add(MakeArc(right, top + cornerRadius, SweepDirection.Clockwise));
                }
                figure.Segments.Add(MakeLineSegment(right, bottom - cornerRadius));
                insertionIndex = figure.Segments.Count;
                //figure.Segments.Add(MakeArc(left, bottom - cornerRadius, SweepDirection.Clockwise));
            }
            else
            {
                if (!lastRight.IsClose(right))
                {
                    double cr = right < lastRight ? -cornerRadius : cornerRadius;
                    SweepDirection dir1 = right < lastRight ? SweepDirection.Clockwise : SweepDirection.Counterclockwise;
                    SweepDirection dir2 = right < lastRight ? SweepDirection.Counterclockwise : SweepDirection.Clockwise;
                    figure.Segments.Insert(insertionIndex++, MakeArc(lastRight + cr, lastBottom, dir1));
                    figure.Segments.Insert(insertionIndex++, MakeLineSegment(right - cr, top));
                    figure.Segments.Insert(insertionIndex++, MakeArc(right, top + cornerRadius, dir2));
                }
                figure.Segments.Insert(insertionIndex++, MakeLineSegment(right, bottom - cornerRadius));
                figure.Segments.Insert(insertionIndex, MakeLineSegment(lastLeft, lastTop + cornerRadius));
                if (!lastLeft.IsClose(left))
                {
                    double cr = left < lastLeft ? cornerRadius : -cornerRadius;
                    SweepDirection dir1 = left < lastLeft ? SweepDirection.Counterclockwise : SweepDirection.Clockwise;
                    SweepDirection dir2 = left < lastLeft ? SweepDirection.Clockwise : SweepDirection.Counterclockwise;
                    figure.Segments.Insert(insertionIndex, MakeArc(lastLeft, lastBottom - cornerRadius, dir1));
                    figure.Segments.Insert(insertionIndex, MakeLineSegment(lastLeft - cr, lastBottom));
                    figure.Segments.Insert(insertionIndex, MakeArc(left + cr, lastBottom, dir2));
                }
            }
            this.lastTop = top;
            this.lastBottom = bottom;
            this.lastLeft = left;
            this.lastRight = right;
        }

        ArcSegment MakeArc(double x, double y, SweepDirection dir)
        {
            ArcSegment arc = new ArcSegment(
                new Point(x, y),
                new Size(cornerRadius, cornerRadius),
                0, false, dir, true);
            arc.Freeze();
            return arc;
        }

        static LineSegment MakeLineSegment(double x, double y)
        {
            LineSegment ls = new LineSegment(new Point(x, y), true);
            ls.Freeze();
            return ls;
        }

        /// <summary>
        /// Closes the current figure.
        /// </summary>
        public void CloseFigure()
        {
            if (figure != null)
            {
                figure.Segments.Insert(insertionIndex, MakeLineSegment(lastLeft, lastTop + cornerRadius));
                if (Math.Abs(lastLeft - lastRight) > cornerRadius)
                {
                    figure.Segments.Insert(insertionIndex, MakeArc(lastLeft, lastBottom - cornerRadius, SweepDirection.Clockwise));
                    figure.Segments.Insert(insertionIndex, MakeLineSegment(lastLeft + cornerRadius, lastBottom));
                    figure.Segments.Insert(insertionIndex, MakeArc(lastRight - cornerRadius, lastBottom, SweepDirection.Clockwise));
                }

                figure.IsClosed = true;
                figures.Add(figure);
                figure = null;
            }
        }

        /// <summary>
        /// Creates the geometry.
        /// Returns null when the geometry is empty!
        /// </summary>
        public Geometry CreateGeometry()
        {
            CloseFigure();
            if (figures.Count != 0)
            {
                PathGeometry g = new PathGeometry(figures);
                g.Freeze();
                return g;
            }
            else
            {
                return null;
            }
        }
    }
}
namespace AvalonEdit.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Xml;
    static class ExtensionMethods
    {
        #region Epsilon / IsClose / CoerceValue
        /// <summary>
        /// Epsilon used for <c>IsClose()</c> implementations.
        /// We can use up quite a few digits in front of the decimal point (due to visual positions being relative to document origin),
        /// and there's no need to be too accurate (we're dealing with pixels here),
        /// so we will use the value 0.01.
        /// Previosly we used 1e-8 but that was causing issues:
        /// http://community.sharpdevelop.net/forums/t/16048.aspx
        /// </summary>
        public const double Epsilon = 0.01;

        /// <summary>
        /// Returns true if the doubles are close (difference smaller than 0.01).
        /// </summary>
        public static bool IsClose(this double d1, double d2)
        {
            if (d1 == d2) // required for infinities
                return true;
            return Math.Abs(d1 - d2) < Epsilon;
        }

        /// <summary>
        /// Returns true if the doubles are close (difference smaller than 0.01).
        /// </summary>
        public static bool IsClose(this Size d1, Size d2)
        {
            return IsClose(d1.Width, d2.Width) && IsClose(d1.Height, d2.Height);
        }

        /// <summary>
        /// Returns true if the doubles are close (difference smaller than 0.01).
        /// </summary>
        public static bool IsClose(this Vector d1, Vector d2)
        {
            return IsClose(d1.X, d2.X) && IsClose(d1.Y, d2.Y);
        }

        /// <summary>
        /// Forces the value to stay between mininum and maximum.
        /// </summary>
        /// <returns>minimum, if value is less than minimum.
        /// Maximum, if value is greater than maximum.
        /// Otherwise, value.</returns>
        public static double CoerceValue(this double value, double minimum, double maximum)
        {
            return Math.Max(Math.Min(value, maximum), minimum);
        }

        /// <summary>
        /// Forces the value to stay between mininum and maximum.
        /// </summary>
        /// <returns>minimum, if value is less than minimum.
        /// Maximum, if value is greater than maximum.
        /// Otherwise, value.</returns>
        public static int CoerceValue(this int value, int minimum, int maximum)
        {
            return Math.Max(Math.Min(value, maximum), minimum);
        }
        #endregion

        #region CreateTypeface
        /// <summary>
        /// Creates typeface from the framework element.
        /// </summary>
        public static Typeface CreateTypeface(this FrameworkElement fe)
        {
            return new Typeface((FontFamily)fe.GetValue(TextBlock.FontFamilyProperty),
                                (FontStyle)fe.GetValue(TextBlock.FontStyleProperty),
                                (FontWeight)fe.GetValue(TextBlock.FontWeightProperty),
                                (FontStretch)fe.GetValue(TextBlock.FontStretchProperty));
        }
        #endregion

        #region AddRange / Sequence
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> elements)
        {
            foreach (T e in elements)
                collection.Add(e);
        }

        /// <summary>
        /// Creates an IEnumerable with a single value.
        /// </summary>
        public static IEnumerable<T> Sequence<T>(T value)
        {
            yield return value;
        }
        #endregion

        #region XML reading
        /// <summary>
        /// Gets the value of the attribute, or null if the attribute does not exist.
        /// </summary>
        public static string GetAttributeOrNull(this XmlElement element, string attributeName)
        {
            XmlAttribute attr = element.GetAttributeNode(attributeName);
            return attr != null ? attr.Value : null;
        }

        /// <summary>
        /// Gets the value of the attribute as boolean, or null if the attribute does not exist.
        /// </summary>
        public static bool? GetBoolAttribute(this XmlElement element, string attributeName)
        {
            XmlAttribute attr = element.GetAttributeNode(attributeName);
            return attr != null ? (bool?)XmlConvert.ToBoolean(attr.Value) : null;
        }

        /// <summary>
        /// Gets the value of the attribute as boolean, or null if the attribute does not exist.
        /// </summary>
        public static bool? GetBoolAttribute(this XmlReader reader, string attributeName)
        {
            string attributeValue = reader.GetAttribute(attributeName);
            if (attributeValue == null)
                return null;
            else
                return XmlConvert.ToBoolean(attributeValue);
        }
        #endregion

        #region DPI independence
        public static Rect TransformToDevice(this Rect rect, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformToDevice;
            return Rect.Transform(rect, matrix);
        }

        public static Rect TransformFromDevice(this Rect rect, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformFromDevice;
            return Rect.Transform(rect, matrix);
        }

        public static Size TransformToDevice(this Size size, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformToDevice;
            return new Size(size.Width * matrix.M11, size.Height * matrix.M22);
        }

        public static Size TransformFromDevice(this Size size, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformFromDevice;
            return new Size(size.Width * matrix.M11, size.Height * matrix.M22);
        }

        public static Point TransformToDevice(this Point point, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformToDevice;
            return new Point(point.X * matrix.M11, point.Y * matrix.M22);
        }

        public static Point TransformFromDevice(this Point point, Visual visual)
        {
            Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformFromDevice;
            return new Point(point.X * matrix.M11, point.Y * matrix.M22);
        }
        #endregion

        #region System.Drawing <-> WPF conversions
        public static System.Drawing.Point ToSystemDrawing(this Point p)
        {
            return new System.Drawing.Point((int)p.X, (int)p.Y);
        }

        public static Point ToWpf(this System.Drawing.Point p)
        {
            return new Point(p.X, p.Y);
        }

        public static Size ToWpf(this System.Drawing.Size s)
        {
            return new Size(s.Width, s.Height);
        }

        public static Rect ToWpf(this System.Drawing.Rectangle rect)
        {
            return new Rect(rect.Location.ToWpf(), rect.Size.ToWpf());
        }
        #endregion

        public static IEnumerable<DependencyObject> VisualAncestorsAndSelf(this DependencyObject obj)
        {
            while (obj != null)
            {
                yield return obj;
                obj = VisualTreeHelper.GetParent(obj);
            }
        }

        [Conditional("DEBUG")]
        public static void CheckIsFrozen(Freezable f)
        {
            if (f != null && !f.IsFrozen)
                Debug.WriteLine("Performance warning: Not frozen: " + f.ToString());
        }

        [Conditional("DEBUG")]
        public static void Log(bool condition, string format, params object[] args)
        {
            if (condition)
            {
                string output = DateTime.Now.ToString("hh:MM:ss") + ": " + string.Format(format, args) + Environment.NewLine + Environment.StackTrace;
                Console.WriteLine(output);
                Debug.WriteLine(output);
            }
        }
    }
}
namespace AvalonEdit.Edi.Folding
{
    using System;
    using AvalonEdit.Document;
    using System.Collections.Generic;
    using AvalonEdit.Folding;


    /// <summary>
    /// Base class for folding strategies.
    /// </summary>
    public abstract class AbstractFoldingStrategy
    {
        /// <summary>
        /// Create <see cref="NewFolding"/>s for the specified document and updates the folding manager with them.
        /// </summary>
        public void UpdateFoldings(FoldingManager manager, TextDocument document)
        {
            int firstErrorOffset;
            IEnumerable<NewFolding> foldings = CreateNewFoldings(document, out firstErrorOffset);
            manager.UpdateFoldings(foldings, firstErrorOffset);
        }

        /// <summary>
        /// Create <see cref="NewFolding"/>s for the specified document.
        /// </summary>
        public abstract IEnumerable<NewFolding> CreateNewFoldings(TextDocument document, out int firstErrorOffset);
    }
}
namespace AvalonEdit
{
    using System.Windows.Input;

    /// <summary>
    /// Static class that contains the commands that can be executed by the <seealso cref="HTextEditor"/> control.
    /// </summary>
    public static class HTextEditorCommands
    {
        /// <summary>
        /// The Collapse all folds commmand folds all text folds (if any) such that users
        /// can get an overview on the presented text (using a top to bottom approach).
        /// </summary>
        public static readonly RoutedCommand FoldsCollapseAll = new RoutedCommand("CollapseAllFolds", typeof(TextEditor)
          ////, new InputGestureCollection { new KeyGesture(Key.D, ModifierKeys.Control) }
          );

        /// <summary>
        /// The Expand all folds commmand unfolds all text folds (if any) such that users
        /// can read all text items in a given text without having to worry about foldings.
        /// </summary>
        public static readonly RoutedCommand FoldsExpandAll = new RoutedCommand("CollapseAllFolds", typeof(TextEditor)
          ////, new InputGestureCollection { new KeyGesture(Key.D, ModifierKeys.Control) }
          );

        /// <summary>
        /// Show Print Document Dialog and allow the user to print the current document.
        /// </summary>
        public static readonly RoutedCommand PrintDocument = new RoutedCommand("PrintDocument", typeof(TextEditor)
          ////, new InputGestureCollection { new KeyGesture(Key.D, ModifierKeys.Control) }
          );
    }
}
namespace AvalonEdit //Folding
{
    using AvalonEdit.Folding;
    using AvalonEdit.Highlighting;
    using System;
    using System.Windows.Input;
    using System.Windows.Threading;

    /// <summary>
    /// This part of the AvalonEdit extension contains the code
    /// necessary to manage folding strategies for various types of text.
    /// </summary>
    public partial class HTextEditor : TextEditor
    {
        #region fields
        FoldingManager mFoldingManager = null;
        object mFoldingStrategy = null;

        private bool mInstallFoldingManager = false;
        private DispatcherTimer mFoldingUpdateTimer = null;
        #endregion fields

        #region Methods
        /// <summary>
        /// This method is executed via <seealso cref="TextEditor"/> class when the Highlighting for
        /// a text display is changed durring the live time of the control.
        /// </summary>
        /// <param name="newValue"></param>
        protected override void OnSyntaxHighlightingChanged(IHighlightingDefinition newValue)
        {
            base.OnSyntaxHighlightingChanged(newValue);

            if (newValue != null)
                this.SetFolding(newValue);
        }

        /// <summary>
        /// Determine whether or not highlighting can be
        /// suppported by a particular folding strategy.
        /// </summary>
        /// <param name="syntaxHighlighting"></param>
        public void SetFolding(IHighlightingDefinition syntaxHighlighting)
        {
            if (syntaxHighlighting == null)
            {
                this.mFoldingStrategy = null;
            }
            else
            {
                switch (syntaxHighlighting.Name)
                {
                    case "XML":
                    case "HTML":
                        mFoldingStrategy = new XmlFoldingStrategy() { ShowAttributesWhenFolded = true };
                        this.TextArea.IndentationStrategy = new AvalonEdit.Indentation.DefaultIndentationStrategy();
                        break;
                    case "C#":
                        this.TextArea.IndentationStrategy = new AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(this.Options);
                        mFoldingStrategy = new CSharpBraceFoldingStrategy();
                        break;
                    case "C++":
                    case "PHP":
                    case "Java":
                        this.TextArea.IndentationStrategy = new AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(this.Options);
                        mFoldingStrategy = new CSharpBraceFoldingStrategy();
                        break;
                    case "VBNET":
                        this.TextArea.IndentationStrategy = new AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(this.Options);
                        mFoldingStrategy = new VBNetFoldingStrategy();
                        break;
                    default:
                        this.TextArea.IndentationStrategy = new AvalonEdit.Indentation.DefaultIndentationStrategy();
                        mFoldingStrategy = null;
                        break;
                }

                if (mFoldingStrategy != null)
                {
                    if (this.Document != null)
                    {
                        if (mFoldingManager == null)
                            mFoldingManager = FoldingManager.Install(this.TextArea);


                        if (this.mFoldingStrategy is AbstractFoldingStrategy)
                        {
                            AbstractFoldingStrategy abstractFolder = this.mFoldingStrategy as AbstractFoldingStrategy;
                            abstractFolder.UpdateFoldings(mFoldingManager, this.Document);
                        }
                    }
                    else
                        this.mInstallFoldingManager = true;
                }
                else
                {
                    if (mFoldingManager != null)
                    {
                        FoldingManager.Uninstall(mFoldingManager);
                        mFoldingManager = null;
                    }
                }
            }
        }

        /// <summary>
        /// Update the folding in the text editor when requested per call on this method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void foldingUpdateTimer_Tick(object sender, EventArgs e)
        {
            if (this.IsVisible == true)
            {
                if (mInstallFoldingManager == true)
                {
                    if (this.Document != null)
                    {
                        if (mFoldingManager == null)
                        {
                            this.mFoldingManager = FoldingManager.Install(this.TextArea);

                            mInstallFoldingManager = false;
                        }
                    }
                    else
                        return;
                }

                if (mFoldingStrategy != null)
                {

                    if (this.mFoldingStrategy is AbstractFoldingStrategy)
                    {
                        AbstractFoldingStrategy abstractFolder = this.mFoldingStrategy as AbstractFoldingStrategy;
                        abstractFolder.UpdateFoldings(mFoldingManager, this.Document);
                    }
                }
            }
        }

        #region Fold Unfold Command
        /// <summary>
        /// Determines whether a folding command can be executed or not and sets correspondind
        /// <paramref name="e"/>.CanExecute property value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void FoldsColapseExpandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = false;
            e.Handled = true;

            HTextEditor edi = sender as HTextEditor;

            if (edi == null)
                return;

            if (edi.mFoldingManager == null)
                return;

            if (edi.mFoldingManager.AllFoldings == null)
                return;

            e.CanExecute = true;
        }

        /// <summary>
        /// Executes the collapse all folds command (which folds all text foldings but the first).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void FoldsCollapseAll(object sender, ExecutedRoutedEventArgs e)
        {
            HTextEditor edi = sender as HTextEditor;

            if (edi == null)
                return;

            edi.CollapseAllTextfoldings();
        }

        /// <summary>
        /// Executes the collapse all folds command (which folds all text foldings but the first).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void FoldsExpandAll(object sender, ExecutedRoutedEventArgs e)
        {
            HTextEditor edi = sender as HTextEditor;

            if (edi == null)
                return;

            edi.ExpandAllTextFoldings();
        }

        /// <summary>
        /// Goes through all foldings in the displayed text and folds them
        /// so that users can explore the text in a top down manner.
        /// </summary>
        private void CollapseAllTextfoldings()
        {
            if (this.mFoldingManager == null)
                return;

            if (this.mFoldingManager.AllFoldings == null)
                return;

            foreach (var loFolding in this.mFoldingManager.AllFoldings)
            {
                loFolding.IsFolded = true;
            }

            // Unfold the first fold (if any) to give a useful overview on content
            FoldingSection foldSection = this.mFoldingManager.GetNextFolding(0);

            if (foldSection != null)
                foldSection.IsFolded = false;
        }

        /// <summary>
        /// Goes through all foldings in the displayed text and unfolds them
        /// so that users can see all text items (without having to play with folding).
        /// </summary>
        private void ExpandAllTextFoldings()
        {
            if (this.mFoldingManager == null)
                return;

            if (this.mFoldingManager.AllFoldings == null)
                return;

            foreach (var loFolding in this.mFoldingManager.AllFoldings)
            {
                loFolding.IsFolded = false;
            }
        }
        #endregion Fold Unfold Command
        #endregion
    }
}
namespace AvalonEdit
{

    using AvalonEdit.Document;
    using AvalonEdit.Editing;
    using AvalonEdit.Highlighting;
    using AvalonEdit.Rendering;
    using AvalonEdit.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    /// <summary>
    /// The text editor control.
    /// Contains a scrollable TextArea.
    /// </summary>
    [Localizability(LocalizationCategory.Text), ContentProperty("Text")]
    public class TextEditor : Control, ITextEditorComponent, IServiceProvider, IWeakEventListener
    {
        #region Constructors
        static TextEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextEditor),
                                                     new FrameworkPropertyMetadata(typeof(TextEditor)));
            FocusableProperty.OverrideMetadata(typeof(TextEditor),
                                               new FrameworkPropertyMetadata(Boxes.True));
        }

        /// <summary>
        /// Creates a new TextEditor instance.
        /// </summary>
        public TextEditor() : this(new TextArea())
        {
        }

        /// <summary>
        /// Creates a new TextEditor instance.
        /// </summary>
        protected TextEditor(TextArea textArea)
        {
            if (textArea == null)
                throw new ArgumentNullException("textArea");
            this.textArea = textArea;

            textArea.TextView.Services.AddService(typeof(TextEditor), this);

            SetCurrentValue(OptionsProperty, textArea.Options);
            SetCurrentValue(DocumentProperty, new TextDocument());
        }

#if !DOTNET4
        void SetCurrentValue(DependencyProperty property, object value)
        {
            SetValue(property, value);
        }
#endif
        #endregion

        /// <inheritdoc/>
        protected override System.Windows.Automation.Peers.AutomationPeer OnCreateAutomationPeer()
        {
            return new TextEditorAutomationPeer(this);
        }

        /// Forward focus to TextArea.
        /// <inheritdoc/>
        protected override void OnGotKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            if (e.NewFocus == this)
            {
                Keyboard.Focus(this.TextArea);
                e.Handled = true;
            }
        }

        #region Document property
        /// <summary>
        /// Document property.
        /// </summary>
        public static readonly DependencyProperty DocumentProperty
            = TextView.DocumentProperty.AddOwner(
                typeof(TextEditor), new FrameworkPropertyMetadata(OnDocumentChanged));

        /// <summary>
        /// Gets/Sets the document displayed by the text editor.
        /// This is a dependency property.
        /// </summary>
        public TextDocument Document
        {
            get { return (TextDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        /// <summary>
        /// Occurs when the document property has changed.
        /// </summary>
        public event EventHandler DocumentChanged;

        /// <summary>
        /// Raises the <see cref="DocumentChanged"/> event.
        /// </summary>
        protected virtual void OnDocumentChanged(EventArgs e)
        {
            if (DocumentChanged != null)
            {
                DocumentChanged(this, e);
            }
        }

        static void OnDocumentChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            ((TextEditor)dp).OnDocumentChanged((TextDocument)e.OldValue, (TextDocument)e.NewValue);
        }

        void OnDocumentChanged(TextDocument oldValue, TextDocument newValue)
        {
            if (oldValue != null)
            {
                TextDocumentWeakEventManager.TextChanged.RemoveListener(oldValue, this);
                PropertyChangedEventManager.RemoveListener(oldValue.UndoStack, this, "IsOriginalFile");
            }
            textArea.Document = newValue;
            if (newValue != null)
            {
                TextDocumentWeakEventManager.TextChanged.AddListener(newValue, this);
                PropertyChangedEventManager.AddListener(newValue.UndoStack, this, "IsOriginalFile");
            }
            OnDocumentChanged(EventArgs.Empty);
            OnTextChanged(EventArgs.Empty);
        }
        #endregion

        #region Options property
        /// <summary>
        /// Options property.
        /// </summary>
        public static readonly DependencyProperty OptionsProperty
            = TextView.OptionsProperty.AddOwner(typeof(TextEditor), new FrameworkPropertyMetadata(OnOptionsChanged));

        /// <summary>
        /// Gets/Sets the options currently used by the text editor.
        /// </summary>
        public TextEditorOptions Options
        {
            get { return (TextEditorOptions)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        /// <summary>
        /// Occurs when a text editor option has changed.
        /// </summary>
        public event PropertyChangedEventHandler OptionChanged;

        /// <summary>
        /// Raises the <see cref="OptionChanged"/> event.
        /// </summary>
        protected virtual void OnOptionChanged(PropertyChangedEventArgs e)
        {
            if (OptionChanged != null)
            {
                OptionChanged(this, e);
            }
        }

        static void OnOptionsChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            ((TextEditor)dp).OnOptionsChanged((TextEditorOptions)e.OldValue, (TextEditorOptions)e.NewValue);
        }

        void OnOptionsChanged(TextEditorOptions oldValue, TextEditorOptions newValue)
        {
            if (oldValue != null)
            {
                PropertyChangedWeakEventManager.RemoveListener(oldValue, this);
            }
            textArea.Options = newValue;
            if (newValue != null)
            {
                PropertyChangedWeakEventManager.AddListener(newValue, this);
            }
            OnOptionChanged(new PropertyChangedEventArgs(null));
        }

        /// <inheritdoc cref="IWeakEventListener.ReceiveWeakEvent"/>
        protected virtual bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(PropertyChangedWeakEventManager))
            {
                OnOptionChanged((PropertyChangedEventArgs)e);
                return true;
            }
            else if (managerType == typeof(TextDocumentWeakEventManager.TextChanged))
            {
                OnTextChanged(e);
                return true;
            }
            else if (managerType == typeof(PropertyChangedEventManager))
            {
                return HandleIsOriginalChanged((PropertyChangedEventArgs)e);
            }
            return false;
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            return ReceiveWeakEvent(managerType, sender, e);
        }
        #endregion

        #region Text property
        /// <summary>
        /// Gets/Sets the text of the current document.
        /// </summary>
        [Localizability(LocalizationCategory.Text), DefaultValue("")]
        public string Text
        {
            get
            {
                TextDocument document = this.Document;
                return document != null ? document.Text : string.Empty;
            }
            set
            {
                TextDocument document = GetDocument();
                document.Text = value ?? string.Empty;
                // after replacing the full text, the caret is positioned at the end of the document
                // - reset it to the beginning.
                this.CaretOffset = 0;
                document.UndoStack.ClearAll();
            }
        }

        TextDocument GetDocument()
        {
            TextDocument document = this.Document;
            if (document == null)
                throw ThrowUtil.NoDocumentAssigned();
            return document;
        }

        /// <summary>
        /// Occurs when the Text property changes.
        /// </summary>
        public event EventHandler TextChanged;

        /// <summary>
        /// Raises the <see cref="TextChanged"/> event.
        /// </summary>
        protected virtual void OnTextChanged(EventArgs e)
        {
            if (TextChanged != null)
            {
                TextChanged(this, e);
            }
        }
        #endregion

        #region TextArea / ScrollViewer properties
        readonly TextArea textArea;
        ScrollViewer scrollViewer;

        /// <summary>
        /// Is called after the template was applied.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            scrollViewer = (ScrollViewer)Template.FindName("PART_ScrollViewer", this);
        }

        /// <summary>
        /// Gets the text area.
        /// </summary>
        public TextArea TextArea
        {
            get
            {
                return textArea;
            }
        }

        /// <summary>
        /// Gets the scroll viewer used by the text editor.
        /// This property can return null if the template has not been applied / does not contain a scroll viewer.
        /// </summary>
        internal ScrollViewer ScrollViewer
        {
            get { return scrollViewer; }
        }

        bool CanExecute(RoutedUICommand command)
        {
            TextArea textArea = this.TextArea;
            if (textArea == null)
                return false;
            else
                return command.CanExecute(null, textArea);
        }

        void Execute(RoutedUICommand command)
        {
            TextArea textArea = this.TextArea;
            if (textArea != null)
                command.Execute(null, textArea);
        }
        #endregion

        #region Syntax highlighting
        /// <summary>
        /// The <see cref="SyntaxHighlighting"/> property.
        /// </summary>
        public static readonly DependencyProperty SyntaxHighlightingProperty =
            DependencyProperty.Register("SyntaxHighlighting", typeof(IHighlightingDefinition), typeof(TextEditor),
                                        new FrameworkPropertyMetadata(OnSyntaxHighlightingChanged));


        /// <summary>
        /// Gets/sets the syntax highlighting definition used to colorize the text.
        /// </summary>
        public IHighlightingDefinition SyntaxHighlighting
        {
            get { return (IHighlightingDefinition)GetValue(SyntaxHighlightingProperty); }
            set { SetValue(SyntaxHighlightingProperty, value); }
        }

        IVisualLineTransformer colorizer;

        static void OnSyntaxHighlightingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TextEditor)d).OnSyntaxHighlightingChanged(e.NewValue as IHighlightingDefinition);
        }

        /// <summary>
        /// Method is executed when the syntax highlighting defined through dp has changed.
        /// Dirkster99 made this protected virtual to enable descendents to override this
        /// </summary>
        /// <param name="newValue"></param>
        protected virtual void OnSyntaxHighlightingChanged(IHighlightingDefinition newValue)
        {
            if (colorizer != null)
            {
                this.TextArea.TextView.LineTransformers.Remove(colorizer);
                colorizer = null;
            }
            if (newValue != null)
            {
                colorizer = CreateColorizer(newValue);
                if (colorizer != null)
                    this.TextArea.TextView.LineTransformers.Insert(0, colorizer);
            }
        }

        /// <summary>
        /// Creates the highlighting colorizer for the specified highlighting definition.
        /// Allows derived classes to provide custom colorizer implementations for special highlighting definitions.
        /// </summary>
        /// <returns></returns>
        protected virtual IVisualLineTransformer CreateColorizer(IHighlightingDefinition highlightingDefinition)
        {
            if (highlightingDefinition == null)
                throw new ArgumentNullException("highlightingDefinition");
            return new HighlightingColorizer(highlightingDefinition);
        }
        #endregion

        #region WordWrap
        /// <summary>
        /// Word wrap dependency property.
        /// </summary>
        public static readonly DependencyProperty WordWrapProperty =
            DependencyProperty.Register("WordWrap", typeof(bool), typeof(TextEditor),
                                        new FrameworkPropertyMetadata(Boxes.False));

        /// <summary>
        /// Specifies whether the text editor uses word wrapping.
        /// </summary>
        /// <remarks>
        /// Setting WordWrap=true has the same effect as setting HorizontalScrollBarVisibility=Disabled and will override the
        /// HorizontalScrollBarVisibility setting.
        /// </remarks>
        public bool WordWrap
        {
            get { return (bool)GetValue(WordWrapProperty); }
            set { SetValue(WordWrapProperty, Boxes.Box(value)); }
        }
        #endregion

        #region IsReadOnly
        /// <summary>
        /// IsReadOnly dependency property.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(TextEditor),
                                        new FrameworkPropertyMetadata(Boxes.False, OnIsReadOnlyChanged));

        /// <summary>
        /// Specifies whether the user can change the text editor content.
        /// Setting this property will replace the
        /// <see cref="Editing.TextArea.ReadOnlySectionProvider">TextArea.ReadOnlySectionProvider</see>.
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, Boxes.Box(value)); }
        }

        static void OnIsReadOnlyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextEditor)
            {
                TextEditor editor = d as TextEditor;
                if ((bool)e.NewValue)
                    editor.TextArea.ReadOnlySectionProvider = ReadOnlySectionDocument.Instance;
                else
                    editor.TextArea.ReadOnlySectionProvider = NoReadOnlySections.Instance;

                if (TextEditorAutomationPeer.FromElement(editor) is TextEditorAutomationPeer)
                {
                    TextEditorAutomationPeer peer = TextEditorAutomationPeer.FromElement(editor) as TextEditorAutomationPeer;
                    peer.RaiseIsReadOnlyChanged((bool)e.OldValue, (bool)e.NewValue);
                }
            }
        }
        #endregion

        #region IsModified
        /// <summary>
        /// Dependency property for <see cref="IsModified"/>
        /// </summary>
        public static readonly DependencyProperty IsModifiedProperty =
            DependencyProperty.Register("IsModified", typeof(bool), typeof(TextEditor),
                                        new FrameworkPropertyMetadata(Boxes.False, OnIsModifiedChanged));

        /// <summary>
        /// Gets/Sets the 'modified' flag.
        /// </summary>
        public bool IsModified
        {
            get { return (bool)GetValue(IsModifiedProperty); }
            set { SetValue(IsModifiedProperty, Boxes.Box(value)); }
        }

        static void OnIsModifiedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextEditor)
            {
                TextEditor editor = d as TextEditor;

                TextDocument document = editor.Document;
                if (document != null)
                {
                    UndoStack undoStack = document.UndoStack;
                    if ((bool)e.NewValue)
                    {
                        if (undoStack.IsOriginalFile)
                            undoStack.DiscardOriginalFileMarker();
                    }
                    else
                    {
                        undoStack.MarkAsOriginalFile();
                    }
                }
            }
        }

        bool HandleIsOriginalChanged(PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsOriginalFile")
            {
                TextDocument document = this.Document;
                if (document != null)
                {
                    SetCurrentValue(IsModifiedProperty, Boxes.Box(!document.UndoStack.IsOriginalFile));
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region ShowLineNumbers
        /// <summary>
        /// ShowLineNumbers dependency property.
        /// </summary>
        public static readonly DependencyProperty ShowLineNumbersProperty =
            DependencyProperty.Register("ShowLineNumbers", typeof(bool), typeof(TextEditor),
                                        new FrameworkPropertyMetadata(Boxes.False, OnShowLineNumbersChanged));

        /// <summary>
        /// Specifies whether line numbers are shown on the left to the text view.
        /// </summary>
        public bool ShowLineNumbers
        {
            get { return (bool)GetValue(ShowLineNumbersProperty); }
            set { SetValue(ShowLineNumbersProperty, Boxes.Box(value)); }
        }

        static void OnShowLineNumbersChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextEditor editor = (TextEditor)d;
            var leftMargins = editor.TextArea.LeftMargins;
            if ((bool)e.NewValue)
            {
                LineNumberMargin lineNumbers = new LineNumberMargin();
                Line line = (Line)DottedLineMargin.Create();
                leftMargins.Insert(0, lineNumbers);
                leftMargins.Insert(1, line);
                var lineNumbersForeground = new Binding("LineNumbersForeground") { Source = editor };
                line.SetBinding(Line.StrokeProperty, lineNumbersForeground);
                lineNumbers.SetBinding(Control.ForegroundProperty, lineNumbersForeground);
            }
            else
            {
                for (int i = 0; i < leftMargins.Count; i++)
                {
                    if (leftMargins[i] is LineNumberMargin)
                    {
                        leftMargins.RemoveAt(i);
                        if (i < leftMargins.Count && DottedLineMargin.IsDottedLineMargin(leftMargins[i]))
                        {
                            leftMargins.RemoveAt(i);
                        }
                        break;
                    }
                }
            }
        }
        #endregion

        #region LineNumbersForeground
        /// <summary>
        /// LineNumbersForeground dependency property.
        /// </summary>
        public static readonly DependencyProperty LineNumbersForegroundProperty =
            DependencyProperty.Register("LineNumbersForeground", typeof(Brush), typeof(TextEditor),
                                        new FrameworkPropertyMetadata(Brushes.Gray, OnLineNumbersForegroundChanged));

        /// <summary>
        /// Gets/sets the Brush used for displaying the foreground color of line numbers.
        /// </summary>
        public Brush LineNumbersForeground
        {
            get { return (Brush)GetValue(LineNumbersForegroundProperty); }
            set { SetValue(LineNumbersForegroundProperty, value); }
        }

        static void OnLineNumbersForegroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextEditor editor = (TextEditor)d;
            var lineNumberMargin = editor.TextArea.LeftMargins.FirstOrDefault(margin => margin is LineNumberMargin) as LineNumberMargin; ;

            if (lineNumberMargin != null)
            {
                lineNumberMargin.SetValue(Control.ForegroundProperty, e.NewValue);
            }
        }
        #endregion

        #region TextBoxBase-like methods
        /// <summary>
        /// Appends text to the end of the document.
        /// </summary>
        public void AppendText(string textData)
        {
            var document = GetDocument();
            document.Insert(document.TextLength, textData);
        }

        /// <summary>
        /// Begins a group of document changes.
        /// </summary>
        public void BeginChange()
        {
            GetDocument().BeginUpdate();
        }

        /// <summary>
        /// Copies the current selection to the clipboard.
        /// </summary>
        public void Copy()
        {
            Execute(ApplicationCommands.Copy);
        }

        /// <summary>
        /// Removes the current selection and copies it to the clipboard.
        /// </summary>
        public void Cut()
        {
            Execute(ApplicationCommands.Cut);
        }

        /// <summary>
        /// Begins a group of document changes and returns an object that ends the group of document
        /// changes when it is disposed.
        /// </summary>
        public IDisposable DeclareChangeBlock()
        {
            return GetDocument().RunUpdate();
        }

        /// <summary>
        /// Ends the current group of document changes.
        /// </summary>
        public void EndChange()
        {
            GetDocument().EndUpdate();
        }

        /// <summary>
        /// Scrolls one line down.
        /// </summary>
        public void LineDown()
        {
            if (scrollViewer != null)
                scrollViewer.LineDown();
        }

        /// <summary>
        /// Scrolls to the left.
        /// </summary>
        public void LineLeft()
        {
            if (scrollViewer != null)
                scrollViewer.LineLeft();
        }

        /// <summary>
        /// Scrolls to the right.
        /// </summary>
        public void LineRight()
        {
            if (scrollViewer != null)
                scrollViewer.LineRight();
        }

        /// <summary>
        /// Scrolls one line up.
        /// </summary>
        public void LineUp()
        {
            if (scrollViewer != null)
                scrollViewer.LineUp();
        }

        /// <summary>
        /// Scrolls one page down.
        /// </summary>
        public void PageDown()
        {
            if (scrollViewer != null)
                scrollViewer.PageDown();
        }

        /// <summary>
        /// Scrolls one page up.
        /// </summary>
        public void PageUp()
        {
            if (scrollViewer != null)
                scrollViewer.PageUp();
        }

        /// <summary>
        /// Scrolls one page left.
        /// </summary>
        public void PageLeft()
        {
            if (scrollViewer != null)
                scrollViewer.PageLeft();
        }

        /// <summary>
        /// Scrolls one page right.
        /// </summary>
        public void PageRight()
        {
            if (scrollViewer != null)
                scrollViewer.PageRight();
        }

        /// <summary>
        /// Pastes the clipboard content.
        /// </summary>
        public void Paste()
        {
            Execute(ApplicationCommands.Paste);
        }

        /// <summary>
        /// Redoes the most recent undone command.
        /// </summary>
        /// <returns>True is the redo operation was successful, false is the redo stack is empty.</returns>
        public bool Redo()
        {
            if (CanExecute(ApplicationCommands.Redo))
            {
                Execute(ApplicationCommands.Redo);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Scrolls to the end of the document.
        /// </summary>
        public void ScrollToEnd()
        {
            ApplyTemplate(); // ensure scrollViewer is created
            if (scrollViewer != null)
                scrollViewer.ScrollToEnd();
        }

        /// <summary>
        /// Scrolls to the start of the document.
        /// </summary>
        public void ScrollToHome()
        {
            ApplyTemplate(); // ensure scrollViewer is created
            if (scrollViewer != null)
                scrollViewer.ScrollToHome();
        }

        /// <summary>
        /// Scrolls to the specified position in the document.
        /// </summary>
        public void ScrollToHorizontalOffset(double offset)
        {
            ApplyTemplate(); // ensure scrollViewer is created
            if (scrollViewer != null)
                scrollViewer.ScrollToHorizontalOffset(offset);
        }

        /// <summary>
        /// Scrolls to the specified position in the document.
        /// </summary>
        public void ScrollToVerticalOffset(double offset)
        {
            ApplyTemplate(); // ensure scrollViewer is created
            if (scrollViewer != null)
                scrollViewer.ScrollToVerticalOffset(offset);
        }

        /// <summary>
        /// Selects the entire text.
        /// </summary>
        public void SelectAll()
        {
            Execute(ApplicationCommands.SelectAll);
        }

        /// <summary>
        /// Undoes the most recent command.
        /// </summary>
        /// <returns>True is the undo operation was successful, false is the undo stack is empty.</returns>
        public bool Undo()
        {
            if (CanExecute(ApplicationCommands.Undo))
            {
                Execute(ApplicationCommands.Undo);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets if the most recent undone command can be redone.
        /// </summary>
        public bool CanRedo
        {
            get { return CanExecute(ApplicationCommands.Redo); }
        }

        /// <summary>
        /// Gets if the most recent command can be undone.
        /// </summary>
        public bool CanUndo
        {
            get { return CanExecute(ApplicationCommands.Undo); }
        }

        /// <summary>
        /// Gets the vertical size of the document.
        /// </summary>
        public double ExtentHeight
        {
            get
            {
                return scrollViewer != null ? scrollViewer.ExtentHeight : 0;
            }
        }

        /// <summary>
        /// Gets the horizontal size of the current document region.
        /// </summary>
        public double ExtentWidth
        {
            get
            {
                return scrollViewer != null ? scrollViewer.ExtentWidth : 0;
            }
        }

        /// <summary>
        /// Gets the horizontal size of the viewport.
        /// </summary>
        public double ViewportHeight
        {
            get
            {
                return scrollViewer != null ? scrollViewer.ViewportHeight : 0;
            }
        }

        /// <summary>
        /// Gets the horizontal size of the viewport.
        /// </summary>
        public double ViewportWidth
        {
            get
            {
                return scrollViewer != null ? scrollViewer.ViewportWidth : 0;
            }
        }

        /// <summary>
        /// Gets the vertical scroll position.
        /// </summary>
        public double VerticalOffset
        {
            get
            {
                return scrollViewer != null ? scrollViewer.VerticalOffset : 0;
            }
        }

        /// <summary>
        /// Gets the horizontal scroll position.
        /// </summary>
        public double HorizontalOffset
        {
            get
            {
                return scrollViewer != null ? scrollViewer.HorizontalOffset : 0;
            }
        }
        #endregion

        #region TextBox methods
        /// <summary>
        /// Gets/Sets the selected text.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get
            {
                TextArea textArea = this.TextArea;
                // We'll get the text from the whole surrounding segment.
                // This is done to ensure that SelectedText.Length == SelectionLength.
                if (textArea != null && textArea.Document != null && !textArea.Selection.IsEmpty)
                    return textArea.Document.GetText(textArea.Selection.SurroundingSegment);
                else
                    return string.Empty;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                TextArea textArea = this.TextArea;
                if (textArea != null && textArea.Document != null)
                {
                    int offset = this.SelectionStart;
                    int length = this.SelectionLength;
                    textArea.Document.Replace(offset, length, value);
                    // keep inserted text selected
                    textArea.Selection = SimpleSelection.Create(textArea, offset, offset + value.Length);
                }
            }
        }

        /// <summary>
        /// Gets/sets the caret position.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int CaretOffset
        {
            get
            {
                TextArea textArea = this.TextArea;
                if (textArea != null)
                    return textArea.Caret.Offset;
                else
                    return 0;
            }
            set
            {
                TextArea textArea = this.TextArea;
                if (textArea != null)
                    textArea.Caret.Offset = value;
            }
        }

        /// <summary>
        /// Gets/sets the start position of the selection.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionStart
        {
            get
            {
                TextArea textArea = this.TextArea;
                if (textArea != null)
                {
                    if (textArea.Selection.IsEmpty)
                        return textArea.Caret.Offset;
                    else
                        return textArea.Selection.SurroundingSegment.Offset;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                Select(value, SelectionLength);
            }
        }

        /// <summary>
        /// Gets/sets the length of the selection.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectionLength
        {
            get
            {
                TextArea textArea = this.TextArea;
                if (textArea != null && !textArea.Selection.IsEmpty)
                    return textArea.Selection.SurroundingSegment.Length;
                else
                    return 0;
            }
            set
            {
                Select(SelectionStart, value);
            }
        }

        /// <summary>
        /// Selects the specified text section.
        /// </summary>
        public void Select(int start, int length)
        {
            int documentLength = Document != null ? Document.TextLength : 0;

            // Dirkster99 BugFix
            if (start < 0 || start > documentLength)
                return;
            ////throw new ArgumentOutOfRangeException("start", start, "Value must be between 0 and " + documentLength);

            // Dirkster99 BugFix
            if (length < 0 || start + length > documentLength)
                return;
            ////throw new ArgumentOutOfRangeException("length", length, "Value must be between 0 and " + (documentLength - length));

            textArea.Selection = SimpleSelection.Create(textArea, start, start + length);
            textArea.Caret.Offset = start + length;
        }

        /// <summary>
        /// Gets the number of lines in the document.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int LineCount
        {
            get
            {
                TextDocument document = this.Document;
                if (document != null)
                    return document.LineCount;
                else
                    return 1;
            }
        }

        /// <summary>
        /// Clears the text.
        /// </summary>
        public void Clear()
        {
            this.Text = string.Empty;
        }
        #endregion

        #region Loading from stream
        /// <summary>
        /// Loads the text from the stream, auto-detecting the encoding.
        /// </summary>
        /// <remarks>
        /// This method sets <see cref="IsModified"/> to false.
        /// </remarks>
        public void Load(Stream stream)
        {
            using (StreamReader reader = FileReader.OpenStream(stream, this.Encoding ?? Encoding.UTF8))
            {
                this.Text = reader.ReadToEnd();
                SetCurrentValue(EncodingProperty, reader.CurrentEncoding); // assign encoding after ReadToEnd() so that the StreamReader can autodetect the encoding
            }
            SetCurrentValue(IsModifiedProperty, Boxes.False);
        }

        /// <summary>
        /// Loads the text from the stream, auto-detecting the encoding.
        /// </summary>
        public void Load(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Load(fs);
            }
        }

        /// <summary>
        /// Encoding dependency property.
        /// </summary>
        public static readonly DependencyProperty EncodingProperty =
            DependencyProperty.Register("Encoding", typeof(Encoding), typeof(TextEditor));

        /// <summary>
        /// Gets/sets the encoding used when the file is saved.
        /// </summary>
        /// <remarks>
        /// The <see cref="Load(Stream)"/> method autodetects the encoding of the file and sets this property accordingly.
        /// The <see cref="Save(Stream)"/> method uses the encoding specified in this property.
        /// </remarks>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Encoding Encoding
        {
            get { return (Encoding)GetValue(EncodingProperty); }
            set { SetValue(EncodingProperty, value); }
        }

        /// <summary>
        /// Saves the text to the stream.
        /// </summary>
        /// <remarks>
        /// This method sets <see cref="IsModified"/> to false.
        /// </remarks>
        public void Save(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            var encoding = this.Encoding;
            var document = this.Document;
            StreamWriter writer = encoding != null ? new StreamWriter(stream, encoding) : new StreamWriter(stream);
            if (document != null)
                document.WriteTextTo(writer);
            writer.Flush();
            // do not close the stream
            SetCurrentValue(IsModifiedProperty, Boxes.False);
        }

        /// <summary>
        /// Saves the text to the file.
        /// </summary>
        public void Save(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Save(fs);
            }
        }
        #endregion

        #region MouseHover events
        /// <summary>
        /// The PreviewMouseHover event.
        /// </summary>
        public static readonly RoutedEvent PreviewMouseHoverEvent =
            TextView.PreviewMouseHoverEvent.AddOwner(typeof(TextEditor));

        /// <summary>
        /// The MouseHover event.
        /// </summary>
        public static readonly RoutedEvent MouseHoverEvent =
            TextView.MouseHoverEvent.AddOwner(typeof(TextEditor));


        /// <summary>
        /// The PreviewMouseHoverStopped event.
        /// </summary>
        public static readonly RoutedEvent PreviewMouseHoverStoppedEvent =
            TextView.PreviewMouseHoverStoppedEvent.AddOwner(typeof(TextEditor));

        /// <summary>
        /// The MouseHoverStopped event.
        /// </summary>
        public static readonly RoutedEvent MouseHoverStoppedEvent =
            TextView.MouseHoverStoppedEvent.AddOwner(typeof(TextEditor));


        /// <summary>
        /// Occurs when the mouse has hovered over a fixed location for some time.
        /// </summary>
        public event MouseEventHandler PreviewMouseHover
        {
            add { AddHandler(PreviewMouseHoverEvent, value); }
            remove { RemoveHandler(PreviewMouseHoverEvent, value); }
        }

        /// <summary>
        /// Occurs when the mouse has hovered over a fixed location for some time.
        /// </summary>
        public event MouseEventHandler MouseHover
        {
            add { AddHandler(MouseHoverEvent, value); }
            remove { RemoveHandler(MouseHoverEvent, value); }
        }

        /// <summary>
        /// Occurs when the mouse had previously hovered but now started moving again.
        /// </summary>
        public event MouseEventHandler PreviewMouseHoverStopped
        {
            add { AddHandler(PreviewMouseHoverStoppedEvent, value); }
            remove { RemoveHandler(PreviewMouseHoverStoppedEvent, value); }
        }

        /// <summary>
        /// Occurs when the mouse had previously hovered but now started moving again.
        /// </summary>
        public event MouseEventHandler MouseHoverStopped
        {
            add { AddHandler(MouseHoverStoppedEvent, value); }
            remove { RemoveHandler(MouseHoverStoppedEvent, value); }
        }
        #endregion

        #region ScrollBarVisibility
        /// <summary>
        /// Dependency property for <see cref="HorizontalScrollBarVisibility"/>
        /// </summary>
        public static readonly DependencyProperty HorizontalScrollBarVisibilityProperty = ScrollViewer.HorizontalScrollBarVisibilityProperty.AddOwner(typeof(TextEditor), new FrameworkPropertyMetadata(ScrollBarVisibility.Visible));

        /// <summary>
        /// Gets/Sets the horizontal scroll bar visibility.
        /// </summary>
        public ScrollBarVisibility HorizontalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibilityProperty); }
            set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
        }

        /// <summary>
        /// Dependency property for <see cref="VerticalScrollBarVisibility"/>
        /// </summary>
        public static readonly DependencyProperty VerticalScrollBarVisibilityProperty = ScrollViewer.VerticalScrollBarVisibilityProperty.AddOwner(typeof(TextEditor), new FrameworkPropertyMetadata(ScrollBarVisibility.Visible));

        /// <summary>
        /// Gets/Sets the vertical scroll bar visibility.
        /// </summary>
        public ScrollBarVisibility VerticalScrollBarVisibility
        {
            get { return (ScrollBarVisibility)GetValue(VerticalScrollBarVisibilityProperty); }
            set { SetValue(VerticalScrollBarVisibilityProperty, value); }
        }
        #endregion

        object IServiceProvider.GetService(Type serviceType)
        {
            return textArea.GetService(serviceType);
        }

        /// <summary>
        /// Gets the text view position from a point inside the editor.
        /// </summary>
        /// <param name="point">The position, relative to top left
        /// corner of TextEditor control</param>
        /// <returns>The text view position, or null if the point is outside the document.</returns>
        public TextViewPosition? GetPositionFromPoint(Point point)
        {
            if (this.Document == null)
                return null;
            TextView textView = this.TextArea.TextView;
            return textView.GetPosition(TranslatePoint(point, textView) + textView.ScrollOffset);
        }

        /// <summary>
        /// Scrolls to the specified line.
        /// This method requires that the TextEditor was already assigned a size (WPF layout must have run prior).
        /// </summary>
        public void ScrollToLine(int line)
        {
            ScrollTo(line, -1);
        }

        /// <summary>
        /// Scrolls to the specified line/column.
        /// This method requires that the TextEditor was already assigned a size (WPF layout must have run prior).
        /// </summary>
        public void ScrollTo(int line, int column)
        {
            const double MinimumScrollPercentage = 0.3;

            TextView textView = textArea.TextView;
            TextDocument document = textView.Document;
            if (scrollViewer != null && document != null)
            {
                if (line < 1)
                    line = 1;
                if (line > document.LineCount)
                    line = document.LineCount;

                IScrollInfo scrollInfo = textView;
                if (!scrollInfo.CanHorizontallyScroll)
                {
                    // Word wrap is enabled. Ensure that we have up-to-date info about line height so that we scroll
                    // to the correct position.
                    // This avoids that the user has to repeat the ScrollTo() call several times when there are very long lines.
                    VisualLine vl = textView.GetOrConstructVisualLine(document.GetLineByNumber(line));
                    double remainingHeight = scrollViewer.ViewportHeight / 2;
                    while (remainingHeight > 0)
                    {
                        DocumentLine prevLine = vl.FirstDocumentLine.PreviousLine;
                        if (prevLine == null)
                            break;
                        vl = textView.GetOrConstructVisualLine(prevLine);
                        remainingHeight -= vl.Height;
                    }
                }

                Point p = textArea.TextView.GetVisualPosition(new TextViewPosition(line, Math.Max(1, column)), VisualYPosition.LineMiddle);
                double verticalPos = p.Y - scrollViewer.ViewportHeight / 2;
                if (Math.Abs(verticalPos - scrollViewer.VerticalOffset) > MinimumScrollPercentage * scrollViewer.ViewportHeight)
                {
                    scrollViewer.ScrollToVerticalOffset(Math.Max(0, verticalPos));
                }
                if (column > 0)
                {
                    if (p.X > scrollViewer.ViewportWidth - Caret.MinimumDistanceToViewBorder * 2)
                    {
                        double horizontalPos = Math.Max(0, p.X - scrollViewer.ViewportWidth / 2);
                        if (Math.Abs(horizontalPos - scrollViewer.HorizontalOffset) > MinimumScrollPercentage * scrollViewer.ViewportWidth)
                        {
                            scrollViewer.ScrollToHorizontalOffset(horizontalPos);
                        }
                    }
                    else
                    {
                        scrollViewer.ScrollToHorizontalOffset(0);
                    }
                }
            }
        }
    }
}
namespace AvalonEdit
{
    using System;
    using System.ComponentModel;
    using AvalonEdit.Document;
    using AvalonEdit.Editing;
    using AvalonEdit.Rendering;
    /// <summary>
    /// Represents a text editor control (<see cref="TextEditor"/>, <see cref="TextArea"/>
    /// or <see cref="TextView"/>).
    /// </summary>
    public interface ITextEditorComponent : IServiceProvider
    {
        /// <summary>
        /// Gets the document being edited.
        /// </summary>
        TextDocument Document { get; }

        /// <summary>
        /// Occurs when the Document property changes (when the text editor is connected to another
        /// document - not when the document content changes).
        /// </summary>
        event EventHandler DocumentChanged;

        /// <summary>
        /// Gets the options of the text editor.
        /// </summary>
        TextEditorOptions Options { get; }

        /// <summary>
        /// Occurs when the Options property changes, or when an option inside the current option list
        /// changes.
        /// </summary>
        event PropertyChangedEventHandler OptionChanged;
    }
}
namespace AvalonEdit.Utils
{
    /// <summary>
    /// Reuse the same instances for boxed booleans.
    /// </summary>
    static class Boxes
    {
        public static readonly object True = true;
        public static readonly object False = false;

        public static object Box(bool value)
        {
            return value ? True : False;
        }
    }
}
namespace AvalonEdit
{
    using System;
    using System.ComponentModel;
    using System.Reflection;
    /// <summary>
    /// A container for the text editor options.
    /// </summary>
    [Serializable]
    public class TextEditorOptions : INotifyPropertyChanged
    {
        #region ctor
        /// <summary>
        /// Initializes an empty instance of TextEditorOptions.
        /// </summary>
        public TextEditorOptions()
        {
        }

        /// <summary>
        /// Initializes a new instance of TextEditorOptions by copying all values
        /// from <paramref name="options"/> to the new instance.
        /// </summary>
        public TextEditorOptions(TextEditorOptions options)
        {
            // get all the fields in the class
            FieldInfo[] fields = typeof(TextEditorOptions).GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            // copy each value over to 'this'
            foreach (FieldInfo fi in fields)
            {
                if (!fi.IsNotSerialized)
                    fi.SetValue(this, fi.GetValue(options));
            }
        }
        #endregion

        #region PropertyChanged handling
        /// <inheritdoc/>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the changed property.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }
        #endregion

        #region ShowSpaces / ShowTabs / ShowEndOfLine / ShowBoxForControlCharacters
        bool showSpaces;

        /// <summary>
        /// Gets/Sets whether to show · for spaces.
        /// </summary>
        /// <remarks>The default value is <c>false</c>.</remarks>
        [DefaultValue(false)]
        public virtual bool ShowSpaces
        {
            get { return showSpaces; }
            set
            {
                if (showSpaces != value)
                {
                    showSpaces = value;
                    OnPropertyChanged("ShowSpaces");
                }
            }
        }

        bool showTabs;

        /// <summary>
        /// Gets/Sets whether to show » for tabs.
        /// </summary>
        /// <remarks>The default value is <c>false</c>.</remarks>
        [DefaultValue(false)]
        public virtual bool ShowTabs
        {
            get { return showTabs; }
            set
            {
                if (showTabs != value)
                {
                    showTabs = value;
                    OnPropertyChanged("ShowTabs");
                }
            }
        }

        bool showEndOfLine;

        /// <summary>
        /// Gets/Sets whether to show ¶ at the end of lines.
        /// </summary>
        /// <remarks>The default value is <c>false</c>.</remarks>
        [DefaultValue(false)]
        public virtual bool ShowEndOfLine
        {
            get { return showEndOfLine; }
            set
            {
                if (showEndOfLine != value)
                {
                    showEndOfLine = value;
                    OnPropertyChanged("ShowEndOfLine");
                }
            }
        }

        bool showBoxForControlCharacters = true;

        /// <summary>
        /// Gets/Sets whether to show a box with the hex code for control characters.
        /// </summary>
        /// <remarks>The default value is <c>true</c>.</remarks>
        [DefaultValue(true)]
        public virtual bool ShowBoxForControlCharacters
        {
            get { return showBoxForControlCharacters; }
            set
            {
                if (showBoxForControlCharacters != value)
                {
                    showBoxForControlCharacters = value;
                    OnPropertyChanged("ShowBoxForControlCharacters");
                }
            }
        }
        #endregion

        #region EnableHyperlinks
        bool enableHyperlinks = true;

        /// <summary>
        /// Gets/Sets whether to enable clickable hyperlinks in the editor.
        /// </summary>
        /// <remarks>The default value is <c>true</c>.</remarks>
        [DefaultValue(true)]
        public virtual bool EnableHyperlinks
        {
            get { return enableHyperlinks; }
            set
            {
                if (enableHyperlinks != value)
                {
                    enableHyperlinks = value;
                    OnPropertyChanged("EnableHyperlinks");
                }
            }
        }

        #region file hyperlinks
        private bool mEnableFileHyperlinks = true;

        /// <summary>
        /// Gets/Sets whether to enable clickable hyperlinks in the editor.
        /// </summary>
        /// <remarks>The default value is <c>true</c>.</remarks>
        [DefaultValue(true)]
        public virtual bool EnableFileHyperlinks
        {
            get { return mEnableFileHyperlinks; }
            set
            {
                if (mEnableFileHyperlinks != value)
                {
                    mEnableFileHyperlinks = value;
                    OnPropertyChanged("EnableFileHyperlinks");
                }
            }
        }
        #endregion file hyperlinks

        bool enableEmailHyperlinks = true;

        /// <summary>
        /// Gets/Sets whether to enable clickable hyperlinks for e-mail addresses in the editor.
        /// </summary>
        /// <remarks>The default value is <c>true</c>.</remarks>
        [DefaultValue(true)]
        public virtual bool EnableEmailHyperlinks
        {
            get { return enableEmailHyperlinks; }
            set
            {
                if (enableEmailHyperlinks != value)
                {
                    enableEmailHyperlinks = value;
                    OnPropertyChanged("EnableEMailHyperlinks");
                }
            }
        }

        bool requireControlModifierForHyperlinkClick = true;

        /// <summary>
        /// Gets/Sets whether the user needs to press Control to click hyperlinks.
        /// The default value is true.
        /// </summary>
        /// <remarks>The default value is <c>true</c>.</remarks>
        [DefaultValue(true)]
        public virtual bool RequireControlModifierForHyperlinkClick
        {
            get { return requireControlModifierForHyperlinkClick; }
            set
            {
                if (requireControlModifierForHyperlinkClick != value)
                {
                    requireControlModifierForHyperlinkClick = value;
                    OnPropertyChanged("RequireControlModifierForHyperlinkClick");
                }
            }
        }
        #endregion

        #region TabSize / IndentationSize / ConvertTabsToSpaces / GetIndentationString
        // I'm using '_' prefixes for the fields here to avoid confusion with the local variables
        // in the methods below.
        // The fields should be accessed only by their property - the fields might not be used
        // if someone overrides the property.

        int indentationSize = 4;

        /// <summary>
        /// Gets/Sets the width of one indentation unit.
        /// </summary>
        /// <remarks>The default value is 4.</remarks>
        [DefaultValue(4)]
        public virtual int IndentationSize
        {
            get { return indentationSize; }
            set
            {
                if (value < 1)
                    throw new ArgumentOutOfRangeException("value", value, "value must be positive");
                // sanity check; a too large value might cause WPF to crash internally much later
                // (it only crashed in the hundred thousands for me; but might crash earlier with larger fonts)
                if (value > 1000)
                    throw new ArgumentOutOfRangeException("value", value, "indentation size is too large");
                if (indentationSize != value)
                {
                    indentationSize = value;
                    OnPropertyChanged("IndentationSize");
                    OnPropertyChanged("IndentationString");
                }
            }
        }

        bool convertTabsToSpaces;

        /// <summary>
        /// Gets/Sets whether to use spaces for indentation instead of tabs.
        /// </summary>
        /// <remarks>The default value is <c>false</c>.</remarks>
        [DefaultValue(false)]
        public virtual bool ConvertTabsToSpaces
        {
            get { return convertTabsToSpaces; }
            set
            {
                if (convertTabsToSpaces != value)
                {
                    convertTabsToSpaces = value;
                    OnPropertyChanged("ConvertTabsToSpaces");
                    OnPropertyChanged("IndentationString");
                }
            }
        }

        /// <summary>
        /// Gets the text used for indentation.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods")]
        [Browsable(false)]
        public string IndentationString
        {
            get { return GetIndentationString(1); }
        }

        /// <summary>
        /// Gets text required to indent from the specified <paramref name="column"/> to the next indentation level.
        /// </summary>
        public virtual string GetIndentationString(int column)
        {
            if (column < 1)
                throw new ArgumentOutOfRangeException("column", column, "Value must be at least 1.");
            int indentationSize = this.IndentationSize;
            if (ConvertTabsToSpaces)
            {
                return new string(' ', indentationSize - ((column - 1) % indentationSize));
            }
            else
            {
                return "\t";
            }
        }
        #endregion

        bool cutCopyWholeLine = true;

        /// <summary>
        /// Gets/Sets whether copying without a selection copies the whole current line.
        /// </summary>
        [DefaultValue(true)]
        public virtual bool CutCopyWholeLine
        {
            get { return cutCopyWholeLine; }
            set
            {
                if (cutCopyWholeLine != value)
                {
                    cutCopyWholeLine = value;
                    OnPropertyChanged("CutCopyWholeLine");
                }
            }
        }

        bool allowScrollBelowDocument;

        #region InsertMode
        [NonSerialized]
        bool mIsInsertMode = true;

        /// <summary>
        /// Dirkster99 Extension
        /// Gets/Sets whether Insert/Overtype mode is active or not:
        /// </summary>
        [DefaultValue(true)]
        public virtual bool IsInsertMode
        {
            get { return this.mIsInsertMode; }
            set
            {
                if (this.mIsInsertMode != value)
                {
                    this.mIsInsertMode = value;
                    OnPropertyChanged("IsInsertMode");
                }
            }
        }
        #endregion InsertMode

        /// <summary>
        /// Gets/Sets whether the user can scroll below the bottom of the document.
        /// The default value is false; but it a good idea to set this property to true when using folding.
        /// </summary>
        [DefaultValue(false)]
        public virtual bool AllowScrollBelowDocument
        {
            get { return allowScrollBelowDocument; }
            set
            {
                if (allowScrollBelowDocument != value)
                {
                    allowScrollBelowDocument = value;
                    OnPropertyChanged("AllowScrollBelowDocument");
                }
            }
        }

        double wordWrapIndentation = 0;

        /// <summary>
        /// Gets/Sets the indentation used for all lines except the first when word-wrapping.
        /// The default value is 0.
        /// </summary>
        [DefaultValue(0.0)]
        public virtual double WordWrapIndentation
        {
            get { return wordWrapIndentation; }
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                    throw new ArgumentOutOfRangeException("value", value, "value must not be NaN/infinity");
                if (value != wordWrapIndentation)
                {
                    wordWrapIndentation = value;
                    OnPropertyChanged("WordWrapIndentation");
                }
            }
        }

        bool inheritWordWrapIndentation = true;

        /// <summary>
        /// Gets/Sets whether the indentation is inherited from the first line when word-wrapping.
        /// The default value is true.
        /// </summary>
        /// <remarks>When combined with <see cref="WordWrapIndentation"/>, the inherited indentation is added to the word wrap indentation.</remarks>
        [DefaultValue(true)]
        public virtual bool InheritWordWrapIndentation
        {
            get { return inheritWordWrapIndentation; }
            set
            {
                if (value != inheritWordWrapIndentation)
                {
                    inheritWordWrapIndentation = value;
                    OnPropertyChanged("InheritWordWrapIndentation");
                }
            }
        }

        bool enableRectangularSelection = true;

        /// <summary>
        /// Enables rectangular selection (press ALT and select a rectangle)
        /// </summary>
        [DefaultValue(true)]
        public bool EnableRectangularSelection
        {
            get { return enableRectangularSelection; }
            set
            {
                if (enableRectangularSelection != value)
                {
                    enableRectangularSelection = value;
                    OnPropertyChanged("EnableRectangularSelection");
                }
            }
        }

        bool enableTextDragDrop = true;

        /// <summary>
        /// Enable dragging text within the text area.
        /// </summary>
        [DefaultValue(true)]
        public bool EnableTextDragDrop
        {
            get { return enableTextDragDrop; }
            set
            {
                if (enableTextDragDrop != value)
                {
                    enableTextDragDrop = value;
                    OnPropertyChanged("EnableTextDragDrop");
                }
            }
        }

        bool enableVirtualSpace;

        /// <summary>
        /// Gets/Sets whether the user can set the caret behind the line ending
        /// (into "virtual space").
        /// Note that virtual space is always used (independent from this setting)
        /// when doing rectangle selections.
        /// </summary>
        [DefaultValue(false)]
        public virtual bool EnableVirtualSpace
        {
            get { return enableVirtualSpace; }
            set
            {
                if (enableVirtualSpace != value)
                {
                    enableVirtualSpace = value;
                    OnPropertyChanged("EnableVirtualSpace");
                }
            }
        }

        bool enableImeSupport = true;

        /// <summary>
        /// Gets/Sets whether the support for Input Method Editors (IME)
        /// for non-alphanumeric scripts (Chinese, Japanese, Korean, ...) is enabled.
        /// </summary>
        [DefaultValue(true)]
        public virtual bool EnableImeSupport
        {
            get { return enableImeSupport; }
            set
            {
                if (enableImeSupport != value)
                {
                    enableImeSupport = value;
                    OnPropertyChanged("EnableImeSupport");
                }
            }
        }

        bool showColumnRuler = false;

        /// <summary>
        /// Gets/Sets whether the column ruler should be shown.
        /// </summary>
        [DefaultValue(false)]
        public virtual bool ShowColumnRuler
        {
            get { return showColumnRuler; }
            set
            {
                if (showColumnRuler != value)
                {
                    showColumnRuler = value;
                    OnPropertyChanged("ShowColumnRuler");
                }
            }
        }

        int columnRulerPosition = 80;

        /// <summary>
        /// Gets/Sets where the column ruler should be shown.
        /// </summary>
        [DefaultValue(80)]
        public virtual int ColumnRulerPosition
        {
            get { return columnRulerPosition; }
            set
            {
                if (columnRulerPosition != value)
                {
                    columnRulerPosition = value;
                    OnPropertyChanged("ColumnRulerPosition");
                }
            }
        }

        bool highlightCurrentLine = false;

        /// <summary>
        /// Gets/Sets if current line should be shown.
        /// </summary>
        [DefaultValue(false)]
        public virtual bool HighlightCurrentLine
        {
            get { return highlightCurrentLine; }
            set
            {
                if (highlightCurrentLine != value)
                {
                    highlightCurrentLine = value;
                    OnPropertyChanged("HighlightCurrentLine");
                }
            }
        }

        bool hideCursorWhileTyping = true;

        /// <summary>
        /// Gets/Sets if mouse cursor should be hidden while user is typing.
        /// </summary>
        [DefaultValue(true)]
        public bool HideCursorWhileTyping
        {
            get { return hideCursorWhileTyping; }
            set
            {
                if (hideCursorWhileTyping != value)
                {
                    hideCursorWhileTyping = value;
                    OnPropertyChanged("HideCursorWhileTyping");
                }
            }
        }

        bool allowToggleOverstrikeMode = false;

        /// <summary>
        /// Gets/Sets if the user is allowed to enable/disable overstrike mode.
        /// </summary>
        [DefaultValue(false)]
        public bool AllowToggleOverstrikeMode
        {
            get { return allowToggleOverstrikeMode; }
            set
            {
                if (allowToggleOverstrikeMode != value)
                {
                    allowToggleOverstrikeMode = value;
                    OnPropertyChanged("AllowToggleOverstrikeMode");
                }
            }
        }


        private bool mEnableHighlightBrackets = true;
        /// <summary>
        /// Get/set option to determine whether bracket should
        /// be highlighted in the code or not.
        /// The two brackets "( .... )" are highlighted if answer is yes.
        /// </summary>
        [DefaultValue(true)]
        public virtual bool EnableHighlightBrackets
        {
            get
            {
                return this.mEnableHighlightBrackets;
            }

            set
            {
                if (this.mEnableHighlightBrackets != value)
                {
                    this.mEnableHighlightBrackets = value;
                    OnPropertyChanged("EnableHighlightBrackets");
                }
            }
        }

        private bool mEnableCodeCompletion = false;
        /// <summary>
        /// Get/set option to determine whether Code Completion - complete
        /// typed entries with completion window is activ or not.
        /// </summary>
        [DefaultValue(false)]
        public virtual bool EnableCodeCompletion
        {
            get { return this.mEnableCodeCompletion; }
            set
            {
                if (this.mEnableCodeCompletion != value)
                {
                    this.mEnableCodeCompletion = value;
                    OnPropertyChanged("EnableCodeCompletion");
                }
            }
        }
    }
}
namespace AvalonEdit
{
    using AvalonEdit.Rendering;
    using System.Windows.Media;
    using System.Windows;

    /// <summary>
    /// AvalonEdit: highlight current line even when not focused
    /// 
    /// Source: http://stackoverflow.com/questions/5072761/avalonedit-highlight-current-line-even-when-not-focused
    /// </summary>
    public class HighlightCurrentLineBackgroundRenderer : IBackgroundRenderer
    {
        private readonly TextEditor _Editor;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="editor"></param>
        /// <param name="highlightBackgroundColorBrush"></param>
        public HighlightCurrentLineBackgroundRenderer(TextEditor editor,
                                                      SolidColorBrush highlightBackgroundColorBrush = null)
        {
            this._Editor = editor;

            // Light Blue 0x100000FF
            this.BackgroundColorBrush = new SolidColorBrush((highlightBackgroundColorBrush == null ? Color.FromArgb(0x10, 0x80, 0x80, 0x80) :
                                                                                                     highlightBackgroundColorBrush.Color));
        }

        /// <summary>
        /// Get the <seealso cref="KnownLayer"/> of the <seealso cref="TextEditor"/> control.
        /// </summary>
        public KnownLayer Layer
        {
            get { return KnownLayer.Background; }
        }

        /// <summary>
        /// Get/Set color brush to show for highlighting current line
        /// </summary>
        public SolidColorBrush BackgroundColorBrush { get; set; }

        /// <summary>
        /// Draw the background line highlighting of the current line.
        /// </summary>
        /// <param name="textView"></param>
        /// <param name="drawingContext"></param>
        public void Draw(TextView textView, DrawingContext drawingContext)
        {
            if (this._Editor.Document == null)
                return;

            textView.EnsureVisualLines();
            var currentLine = _Editor.Document.GetLineByOffset(_Editor.CaretOffset);

            foreach (var rect in BackgroundGeometryBuilder.GetRectsForSegment(textView, currentLine))
            {
                drawingContext.DrawRectangle(new SolidColorBrush(this.BackgroundColorBrush.Color), null,
                                             new Rect(rect.Location, new Size(textView.ActualWidth, rect.Height)));
            }
        }
    }
}
namespace AvalonEdit.Rendering
{
    using System.Windows.Media;
    /// <summary>
    /// Background renderers draw in the background of a known layer.
    /// You can use background renderers to draw non-interactive elements on the TextView
    /// without introducing new UIElements.
    /// </summary>
    public interface IBackgroundRenderer
    {
        /// <summary>
        /// Gets the layer on which this background renderer should draw.
        /// </summary>
        KnownLayer Layer { get; }

        /// <summary>
        /// Causes the background renderer to draw.
        /// </summary>
        void Draw(TextView textView, DrawingContext drawingContext);
    }
}
namespace AvalonEdit.Rendering
{
    /// <summary>
    /// Base class for known layers.
    /// </summary>
    class Layer : UIElement
    {
        protected readonly TextView textView;
        protected readonly KnownLayer knownLayer;

        public Layer(TextView textView, KnownLayer knownLayer)
        {
            Debug.Assert(textView != null);
            this.textView = textView;
            this.knownLayer = knownLayer;
            this.Focusable = false;
        }

        protected override GeometryHitTestResult HitTestCore(GeometryHitTestParameters hitTestParameters)
        {
            return null;
        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            return null;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            textView.RenderBackground(drawingContext, knownLayer);
        }
    }
}
namespace AvalonEdit.Rendering
{
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.TextFormatting;
    using System.Windows.Threading;
    using AvalonEdit.Utils;
    /// <summary>
    /// A virtualizing panel producing+showing <see cref="VisualLine"/>s for a <see cref="TextDocument"/>.
    /// 
    /// This is the heart of the text editor, this class controls the text rendering process.
    /// 
    /// Taken as a standalone control, it's a text viewer without any editing capability.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable",
                                                     Justification = "The user usually doesn't work with TextView but with TextEditor; and nulling the Document property is sufficient to dispose everything.")]
    public class TextView : FrameworkElement, IScrollInfo, IWeakEventListener, ITextEditorComponent, IServiceProvider
    {
        #region Constructor
        static TextView()
        {
            ClipToBoundsProperty.OverrideMetadata(typeof(TextView), new FrameworkPropertyMetadata(Boxes.True));
            FocusableProperty.OverrideMetadata(typeof(TextView), new FrameworkPropertyMetadata(Boxes.False));
        }

        ColumnRulerRenderer columnRulerRenderer;
        CurrentLineHighlightRenderer currentLineHighlighRenderer;

        /// <summary>
        /// Creates a new TextView instance.
        /// </summary>
        public TextView()
        {
            services.AddService(typeof(TextView), this);
            textLayer = new TextLayer(this);
            elementGenerators = new ObserveAddRemoveCollection<VisualLineElementGenerator>(ElementGenerator_Added, ElementGenerator_Removed);
            lineTransformers = new ObserveAddRemoveCollection<IVisualLineTransformer>(LineTransformer_Added, LineTransformer_Removed);
            backgroundRenderers = new ObserveAddRemoveCollection<IBackgroundRenderer>(BackgroundRenderer_Added, BackgroundRenderer_Removed);
            columnRulerRenderer = new ColumnRulerRenderer(this);
            currentLineHighlighRenderer = new CurrentLineHighlightRenderer(this);
            this.Options = new TextEditorOptions();

            Debug.Assert(singleCharacterElementGenerator != null); // assert that the option change created the builtin element generators

            layers = new LayerCollection(this);
            InsertLayer(textLayer, KnownLayer.Text, LayerInsertionPosition.Replace);

            this.hoverLogic = new MouseHoverLogic(this);
            this.hoverLogic.MouseHover += (sender, e) => RaiseHoverEventPair(e, PreviewMouseHoverEvent, MouseHoverEvent);
            this.hoverLogic.MouseHoverStopped += (sender, e) => RaiseHoverEventPair(e, PreviewMouseHoverStoppedEvent, MouseHoverStoppedEvent);
        }

        #endregion

        #region Document Property
        /// <summary>
        /// Document property.
        /// </summary>
        public static readonly DependencyProperty DocumentProperty =
            DependencyProperty.Register("Document", typeof(TextDocument), typeof(TextView),
                                        new FrameworkPropertyMetadata(OnDocumentChanged));

        TextDocument document;
        HeightTree heightTree;

        /// <summary>
        /// Gets/Sets the document displayed by the text editor.
        /// </summary>
        public TextDocument Document
        {
            get { return (TextDocument)GetValue(DocumentProperty); }
            set { SetValue(DocumentProperty, value); }
        }

        static void OnDocumentChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            ((TextView)dp).OnDocumentChanged((TextDocument)e.OldValue, (TextDocument)e.NewValue);
        }

        internal double FontSize
        {
            get
            {
                return (double)GetValue(TextBlock.FontSizeProperty);
            }
        }

        /// <summary>
        /// Occurs when the document property has changed.
        /// </summary>
        public event EventHandler DocumentChanged;

        void OnDocumentChanged(TextDocument oldValue, TextDocument newValue)
        {
            if (oldValue != null)
            {
                heightTree.Dispose();
                heightTree = null;
                formatter.Dispose();
                formatter = null;
                cachedElements.Dispose();
                cachedElements = null;
                TextDocumentWeakEventManager.Changing.RemoveListener(oldValue, this);
            }
            this.document = newValue;
            ClearScrollData();
            ClearVisualLines();
            if (newValue != null)
            {
                TextDocumentWeakEventManager.Changing.AddListener(newValue, this);
                formatter = TextFormatterFactory.Create(this);
                InvalidateDefaultTextMetrics(); // measuring DefaultLineHeight depends on formatter
                heightTree = new HeightTree(newValue, DefaultLineHeight);
                cachedElements = new TextViewCachedElements();
            }
            InvalidateMeasure(DispatcherPriority.Normal);
            DocumentChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Recreates the text formatter that is used internally
        /// by calling <see cref="TextFormatterFactory.Create"/>.
        /// </summary>
        void RecreateTextFormatter()
        {
            if (formatter != null)
            {
                formatter.Dispose();
                formatter = TextFormatterFactory.Create(this);
                Redraw();
            }
        }

        void RecreateCachedElements()
        {
            if (cachedElements != null)
            {
                cachedElements.Dispose();
                cachedElements = new TextViewCachedElements();
            }
        }

        /// <inheritdoc cref="IWeakEventListener.ReceiveWeakEvent"/>
        protected virtual bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(TextDocumentWeakEventManager.Changing))
            {
                // TODO: put redraw into background so that other input events can be handled before the redraw.
                // Unfortunately the "easy" approach (just use DispatcherPriority.Background) here makes the editor twice as slow because
                // the caret position change forces an immediate redraw, and the text input then forces a background redraw.
                // When fixing this, make sure performance on the SharpDevelop "type text in C# comment" stress test doesn't get significantly worse.
                DocumentChangeEventArgs change = (DocumentChangeEventArgs)e;
                Redraw(change.Offset, change.RemovalLength, DispatcherPriority.Normal);
                return true;
            }
            else if (managerType == typeof(PropertyChangedWeakEventManager))
            {
                OnOptionChanged((PropertyChangedEventArgs)e);
                return true;
            }
            return false;
        }

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            return ReceiveWeakEvent(managerType, sender, e);
        }
        #endregion

        #region Options property
        /// <summary>
        /// Options property.
        /// </summary>
        public static readonly DependencyProperty OptionsProperty =
            DependencyProperty.Register("Options", typeof(TextEditorOptions), typeof(TextView),
                                        new FrameworkPropertyMetadata(OnOptionsChanged));

        /// <summary>
        /// Gets/Sets the options used by the text editor.
        /// </summary>
        public TextEditorOptions Options
        {
            get { return (TextEditorOptions)GetValue(OptionsProperty); }
            set { SetValue(OptionsProperty, value); }
        }

        /// <summary>
        /// Occurs when a text editor option has changed.
        /// </summary>
        public event PropertyChangedEventHandler OptionChanged;

        /// <summary>
        /// Raises the <see cref="OptionChanged"/> event.
        /// </summary>
        protected virtual void OnOptionChanged(PropertyChangedEventArgs e)
        {
            OptionChanged?.Invoke(this, e);

            // Dirkster99 Bugfix for binding to options (assumption: ColumRulers are not shown by default)
            if ((Options != null ? Options.ShowColumnRuler : false) == true)
                columnRulerRenderer.SetRuler(Options.ColumnRulerPosition, ColumnRulerPen);
            else
                columnRulerRenderer.SetRuler(-1, ColumnRulerPen);

            UpdateBuiltinElementGeneratorsFromOptions();
            Redraw();
        }

        static void OnOptionsChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            ((TextView)dp).OnOptionsChanged((TextEditorOptions)e.OldValue, (TextEditorOptions)e.NewValue);
        }

        void OnOptionsChanged(TextEditorOptions oldValue, TextEditorOptions newValue)
        {
            if (oldValue != null)
            {
                PropertyChangedWeakEventManager.RemoveListener(oldValue, this);
            }
            if (newValue != null)
            {
                PropertyChangedWeakEventManager.AddListener(newValue, this);
            }
            OnOptionChanged(new PropertyChangedEventArgs(null));
        }
        #endregion

        #region ElementGenerators+LineTransformers Properties
        readonly ObserveAddRemoveCollection<VisualLineElementGenerator> elementGenerators;

        /// <summary>
        /// Gets a collection where element generators can be registered.
        /// </summary>
        public IList<VisualLineElementGenerator> ElementGenerators
        {
            get { return elementGenerators; }
        }

        void ElementGenerator_Added(VisualLineElementGenerator generator)
        {
            ConnectToTextView(generator);
            Redraw();
        }

        void ElementGenerator_Removed(VisualLineElementGenerator generator)
        {
            DisconnectFromTextView(generator);
            Redraw();
        }

        readonly ObserveAddRemoveCollection<IVisualLineTransformer> lineTransformers;

        /// <summary>
        /// Gets a collection where line transformers can be registered.
        /// </summary>
        public IList<IVisualLineTransformer> LineTransformers
        {
            get { return lineTransformers; }
        }

        void LineTransformer_Added(IVisualLineTransformer lineTransformer)
        {
            ConnectToTextView(lineTransformer);
            Redraw();
        }

        void LineTransformer_Removed(IVisualLineTransformer lineTransformer)
        {
            DisconnectFromTextView(lineTransformer);
            Redraw();
        }
        #endregion

        #region Builtin ElementGenerators
        //		NewLineElementGenerator newLineElementGenerator;
        SingleCharacterElementGenerator singleCharacterElementGenerator;
        LinkElementGenerator linkElementGenerator;
        MailLinkElementGenerator mailLinkElementGenerator;
        FileLinkElementGenerator fileLinkElementGenerator;

        void UpdateBuiltinElementGeneratorsFromOptions()
        {
            TextEditorOptions options = this.Options;

            // Dirkster99 BugFix for binding options in VS2010			
            if (options == null)
                return;

            // AddRemoveDefaultElementGeneratorOnDemand(ref newLineElementGenerator, options.ShowEndOfLine);
            AddRemoveDefaultElementGeneratorOnDemand(ref singleCharacterElementGenerator, options.ShowBoxForControlCharacters || options.ShowSpaces || options.ShowTabs);
            AddRemoveDefaultElementGeneratorOnDemand(ref linkElementGenerator, options.EnableHyperlinks);
            AddRemoveDefaultElementGeneratorOnDemand(ref mailLinkElementGenerator, options.EnableEmailHyperlinks);
            AddRemoveDefaultElementGeneratorOnDemand(ref fileLinkElementGenerator, options.EnableFileHyperlinks);
        }

        void AddRemoveDefaultElementGeneratorOnDemand<T>(ref T generator, bool demand)
            where T : VisualLineElementGenerator, IBuiltinElementGenerator, new()
        {
            bool hasGenerator = generator != null;
            if (hasGenerator != demand)
            {
                if (demand)
                {
                    generator = new T();
                    this.ElementGenerators.Add(generator);
                }
                else
                {
                    this.ElementGenerators.Remove(generator);
                    generator = null;
                }
            }
            if (generator != null)
                generator.FetchOptions(this.Options);
        }
        #endregion

        #region Layers
        internal readonly TextLayer textLayer;
        readonly LayerCollection layers;

        /// <summary>
        /// Gets the list of layers displayed in the text view.
        /// </summary>
        public UIElementCollection Layers
        {
            get { return layers; }
        }

        sealed class LayerCollection : UIElementCollection
        {
            readonly TextView textView;

            public LayerCollection(TextView textView)
                : base(textView, textView)
            {
                this.textView = textView;
            }

            public override void Clear()
            {
                base.Clear();
                textView.LayersChanged();
            }

            public override int Add(UIElement element)
            {
                int r = base.Add(element);
                textView.LayersChanged();
                return r;
            }

            public override void RemoveAt(int index)
            {
                base.RemoveAt(index);
                textView.LayersChanged();
            }

            public override void RemoveRange(int index, int count)
            {
                base.RemoveRange(index, count);
                textView.LayersChanged();
            }
        }

        void LayersChanged()
        {
            textLayer.index = layers.IndexOf(textLayer);
        }

        /// <summary>
        /// Inserts a new layer at a position specified relative to an existing layer.
        /// </summary>
        /// <param name="layer">The new layer to insert.</param>
        /// <param name="referencedLayer">The existing layer</param>
        /// <param name="position">Specifies whether the layer is inserted above,below, or replaces the referenced layer</param>
        public void InsertLayer(UIElement layer, KnownLayer referencedLayer, LayerInsertionPosition position)
        {
            if (layer == null)
                throw new ArgumentNullException("layer");
            if (!Enum.IsDefined(typeof(KnownLayer), referencedLayer))
                throw new InvalidEnumArgumentException("referencedLayer", (int)referencedLayer, typeof(KnownLayer));
            if (!Enum.IsDefined(typeof(LayerInsertionPosition), position))
                throw new InvalidEnumArgumentException("position", (int)position, typeof(LayerInsertionPosition));
            if (referencedLayer == KnownLayer.Background && position != LayerInsertionPosition.Above)
                throw new InvalidOperationException("Cannot replace or insert below the background layer.");

            LayerPosition newPosition = new LayerPosition(referencedLayer, position);
            LayerPosition.SetLayerPosition(layer, newPosition);
            for (int i = 0; i < layers.Count; i++)
            {
                LayerPosition p = LayerPosition.GetLayerPosition(layers[i]);
                if (p != null)
                {
                    if (p.KnownLayer == referencedLayer && p.Position == LayerInsertionPosition.Replace)
                    {
                        // found the referenced layer
                        switch (position)
                        {
                            case LayerInsertionPosition.Below:
                                layers.Insert(i, layer);
                                return;
                            case LayerInsertionPosition.Above:
                                layers.Insert(i + 1, layer);
                                return;
                            case LayerInsertionPosition.Replace:
                                layers[i] = layer;
                                return;
                        }
                    }
                    else if (p.KnownLayer == referencedLayer && p.Position == LayerInsertionPosition.Above
                             || p.KnownLayer > referencedLayer)
                    {
                        // we skipped the insertion position (referenced layer does not exist?)
                        layers.Insert(i, layer);
                        return;
                    }
                }
            }
            // inserting after all existing layers:
            layers.Add(layer);
        }

        /// <inheritdoc/>
        protected override int VisualChildrenCount
        {
            get { return layers.Count + inlineObjects.Count; }
        }

        /// <inheritdoc/>
        protected override Visual GetVisualChild(int index)
        {
            int cut = textLayer.index + 1;
            if (index < cut)
                return layers[index];
            else if (index < cut + inlineObjects.Count)
                return inlineObjects[index - cut].Element;
            else
                return layers[index - inlineObjects.Count];
        }

        /// <inheritdoc/>
        protected override System.Collections.IEnumerator LogicalChildren
        {
            get
            {
                return inlineObjects.Select(io => io.Element).Concat(layers.Cast<UIElement>()).GetEnumerator();
            }
        }
        #endregion

        #region Inline object handling
        List<InlineObjectRun> inlineObjects = new List<InlineObjectRun>();

        /// <summary>
        /// Adds a new inline object.
        /// </summary>
        internal void AddInlineObject(InlineObjectRun inlineObject)
        {
            Debug.Assert(inlineObject.VisualLine != null);

            // Remove inline object if its already added, can happen e.g. when recreating textrun for word-wrapping
            bool alreadyAdded = false;
            for (int i = 0; i < inlineObjects.Count; i++)
            {
                if (inlineObjects[i].Element == inlineObject.Element)
                {
                    RemoveInlineObjectRun(inlineObjects[i], true);
                    inlineObjects.RemoveAt(i);
                    alreadyAdded = true;
                    break;
                }
            }

            inlineObjects.Add(inlineObject);
            if (!alreadyAdded)
            {
                AddVisualChild(inlineObject.Element);
            }
            inlineObject.Element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            inlineObject.desiredSize = inlineObject.Element.DesiredSize;
        }

        void MeasureInlineObjects()
        {
            // As part of MeasureOverride(), re-measure the inline objects
            foreach (InlineObjectRun inlineObject in inlineObjects)
            {
                if (inlineObject.VisualLine.IsDisposed)
                {
                    // Don't re-measure inline objects that are going to be removed anyways.
                    // If the inline object will be reused in a different VisualLine, we'll measure it in the AddInlineObject() call.
                    continue;
                }
                inlineObject.Element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                if (!inlineObject.Element.DesiredSize.IsClose(inlineObject.desiredSize))
                {
                    // the element changed size -> recreate its parent visual line
                    inlineObject.desiredSize = inlineObject.Element.DesiredSize;
                    if (allVisualLines.Remove(inlineObject.VisualLine))
                    {
                        DisposeVisualLine(inlineObject.VisualLine);
                    }
                }
            }
        }

        List<VisualLine> visualLinesWithOutstandingInlineObjects = new List<VisualLine>();

        void RemoveInlineObjects(VisualLine visualLine)
        {
            // Delay removing inline objects:
            // A document change immediately invalidates affected visual lines, but it does not
            // cause an immediate redraw.
            // To prevent inline objects from flickering when they are recreated, we delay removing
            // inline objects until the next redraw.
            if (visualLine.hasInlineObjects)
            {
                visualLinesWithOutstandingInlineObjects.Add(visualLine);
            }
        }

        /// <summary>
        /// Remove the inline objects that were marked for removal.
        /// </summary>
        void RemoveInlineObjectsNow()
        {
            if (visualLinesWithOutstandingInlineObjects.Count == 0)
                return;
            inlineObjects.RemoveAll(
                ior =>
                {
                    if (visualLinesWithOutstandingInlineObjects.Contains(ior.VisualLine))
                    {
                        RemoveInlineObjectRun(ior, false);
                        return true;
                    }
                    return false;
                });
            visualLinesWithOutstandingInlineObjects.Clear();
        }

        // Remove InlineObjectRun.Element from TextLayer.
        // Caller of RemoveInlineObjectRun will remove it from inlineObjects collection.
        void RemoveInlineObjectRun(InlineObjectRun ior, bool keepElement)
        {
            if (!keepElement && ior.Element.IsKeyboardFocusWithin)
            {
                // When the inline element that has the focus is removed, WPF will reset the
                // focus to the main window without raising appropriate LostKeyboardFocus events.
                // To work around this, we manually set focus to the next focusable parent.
                UIElement element = this;
                while (element != null && !element.Focusable)
                {
                    element = VisualTreeHelper.GetParent(element) as UIElement;
                }
                if (element != null)
                    Keyboard.Focus(element);
            }
            ior.VisualLine = null;
            if (!keepElement)
                RemoveVisualChild(ior.Element);
        }
        #endregion

        #region Brushes
        /// <summary>
        /// NonPrintableCharacterBrush dependency property.
        /// </summary>
        public static readonly DependencyProperty NonPrintableCharacterBrushProperty =
            DependencyProperty.Register("NonPrintableCharacterBrush", typeof(Brush), typeof(TextView),
                                        new FrameworkPropertyMetadata(Brushes.LightGray));

        /// <summary>
        /// Gets/sets the Brush used for displaying non-printable characters.
        /// </summary>
        public Brush NonPrintableCharacterBrush
        {
            get { return (Brush)GetValue(NonPrintableCharacterBrushProperty); }
            set { SetValue(NonPrintableCharacterBrushProperty, value); }
        }

        /// <summary>
        /// LinkTextForegroundBrush dependency property.
        /// </summary>
        public static readonly DependencyProperty LinkTextForegroundBrushProperty =
            DependencyProperty.Register("LinkTextForegroundBrush", typeof(Brush), typeof(TextView),
                                        new FrameworkPropertyMetadata(Brushes.Blue));

        /// <summary>
        /// Gets/sets the Brush used for displaying link texts.
        /// </summary>
        public Brush LinkTextForegroundBrush
        {
            get { return (Brush)GetValue(LinkTextForegroundBrushProperty); }
            set { SetValue(LinkTextForegroundBrushProperty, value); }
        }

        /// <summary>
        /// LinkTextBackgroundBrush dependency property.
        /// </summary>
        public static readonly DependencyProperty LinkTextBackgroundBrushProperty =
            DependencyProperty.Register("LinkTextBackgroundBrush", typeof(Brush), typeof(TextView),
                                        new FrameworkPropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// Gets/sets the Brush used for the background of link texts.
        /// </summary>
        public Brush LinkTextBackgroundBrush
        {
            get { return (Brush)GetValue(LinkTextBackgroundBrushProperty); }
            set { SetValue(LinkTextBackgroundBrushProperty, value); }
        }
        #endregion

        /// <summary>
        /// LinkTextUnderlinedBrush dependency property.
        /// </summary>
        public static readonly DependencyProperty LinkTextUnderlineProperty =
            DependencyProperty.Register("LinkTextUnderline", typeof(bool), typeof(TextView),
                                        new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets/sets whether to underline link texts.
        /// </summary>
        /// <remarks>
        /// Note that when setting this property to false, link text remains clickable and the LinkTextForegroundBrush (if any) is still applied.
        /// Set TextEditorOptions.EnableHyperlinks and EnableEmailHyperlinks to false to disable links completely.
        /// </remarks>
        public bool LinkTextUnderline
        {
            get { return (bool)GetValue(LinkTextUnderlineProperty); }
            set { SetValue(LinkTextUnderlineProperty, value); }
        }

        #region Redraw methods / VisualLine invalidation
        /// <summary>
        /// Causes the text editor to regenerate all visual lines.
        /// </summary>
        public void Redraw()
        {
            Redraw(DispatcherPriority.Normal);
        }

        /// <summary>
        /// Causes the text editor to regenerate all visual lines.
        /// </summary>
        public void Redraw(DispatcherPriority redrawPriority)
        {
            VerifyAccess();
            ClearVisualLines();
            InvalidateMeasure(redrawPriority);
        }

        /// <summary>
        /// Causes the text editor to regenerate the specified visual line.
        /// </summary>
        public void Redraw(VisualLine visualLine, DispatcherPriority redrawPriority = DispatcherPriority.Normal)
        {
            VerifyAccess();
            if (allVisualLines.Remove(visualLine))
            {
                DisposeVisualLine(visualLine);
                InvalidateMeasure(redrawPriority);
            }
        }

        /// <summary>
        /// Causes the text editor to redraw all lines overlapping with the specified segment.
        /// </summary>
        public void Redraw(int offset, int length, DispatcherPriority redrawPriority = DispatcherPriority.Normal)
        {
            VerifyAccess();
            bool changedSomethingBeforeOrInLine = false;
            for (int i = 0; i < allVisualLines.Count; i++)
            {
                VisualLine visualLine = allVisualLines[i];
                int lineStart = visualLine.FirstDocumentLine.Offset;
                int lineEnd = visualLine.LastDocumentLine.Offset + visualLine.LastDocumentLine.TotalLength;
                if (offset <= lineEnd)
                {
                    changedSomethingBeforeOrInLine = true;
                    if (offset + length >= lineStart)
                    {
                        allVisualLines.RemoveAt(i--);
                        DisposeVisualLine(visualLine);
                    }
                }
            }
            if (changedSomethingBeforeOrInLine)
            {
                // Repaint not only when something in visible area was changed, but also when anything in front of it
                // was changed. We might have to redraw the line number margin. Or the highlighting changed.
                // However, we'll try to reuse the existing VisualLines.
                InvalidateMeasure(redrawPriority);
            }
        }

        /// <summary>
        /// Causes a known layer to redraw.
        /// This method does not invalidate visual lines;
        /// use the <see cref="Redraw()"/> method to do that.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "knownLayer",
                                                         Justification = "This method is meant to invalidate only a specific layer - I just haven't figured out how to do that, yet.")]
        public void InvalidateLayer(KnownLayer knownLayer)
        {
            InvalidateMeasure(DispatcherPriority.Normal);
        }

        /// <summary>
        /// Causes a known layer to redraw.
        /// This method does not invalidate visual lines;
        /// use the <see cref="Redraw()"/> method to do that.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "knownLayer",
                                                         Justification = "This method is meant to invalidate only a specific layer - I just haven't figured out how to do that, yet.")]
        public void InvalidateLayer(KnownLayer knownLayer, DispatcherPriority priority)
        {
            InvalidateMeasure(priority);
        }

        /// <summary>
        /// Causes the text editor to redraw all lines overlapping with the specified segment.
        /// Does nothing if segment is null.
        /// </summary>
        public void Redraw(ISegment segment, DispatcherPriority redrawPriority = DispatcherPriority.Normal)
        {
            if (segment != null)
            {
                Redraw(segment.Offset, segment.Length, redrawPriority);
            }
        }

        /// <summary>
        /// Invalidates all visual lines.
        /// The caller of ClearVisualLines() must also call InvalidateMeasure() to ensure
        /// that the visual lines will be recreated.
        /// </summary>
        void ClearVisualLines()
        {
            visibleVisualLines = null;
            if (allVisualLines.Count != 0)
            {
                foreach (VisualLine visualLine in allVisualLines)
                {
                    DisposeVisualLine(visualLine);
                }
                allVisualLines.Clear();
            }
        }

        void DisposeVisualLine(VisualLine visualLine)
        {
            if (newVisualLines != null && newVisualLines.Contains(visualLine))
            {
                throw new ArgumentException("Cannot dispose visual line because it is in construction!");
            }
            visibleVisualLines = null;
            visualLine.Dispose();
            RemoveInlineObjects(visualLine);
        }
        #endregion

        #region InvalidateMeasure(DispatcherPriority)
        DispatcherOperation invalidateMeasureOperation;

        void InvalidateMeasure(DispatcherPriority priority)
        {
            if (priority >= DispatcherPriority.Render)
            {
                if (invalidateMeasureOperation != null)
                {
                    invalidateMeasureOperation.Abort();
                    invalidateMeasureOperation = null;
                }
                base.InvalidateMeasure();
            }
            else
            {
                if (invalidateMeasureOperation != null)
                {
                    invalidateMeasureOperation.Priority = priority;
                }
                else
                {
                    invalidateMeasureOperation = Dispatcher.BeginInvoke(
                        priority,
                        new Action(
                            delegate
                            {
                                invalidateMeasureOperation = null;
                                base.InvalidateMeasure();
                            }
                        )
                    );
                }
            }
        }
        #endregion

        #region Get(OrConstruct)VisualLine
        /// <summary>
        /// Gets the visual line that contains the document line with the specified number.
        /// Returns null if the document line is outside the visible range.
        /// </summary>
        public VisualLine GetVisualLine(int documentLineNumber)
        {
            // TODO: EnsureVisualLines() ?
            foreach (VisualLine visualLine in allVisualLines)
            {
                Debug.Assert(visualLine.IsDisposed == false);
                int start = visualLine.FirstDocumentLine.LineNumber;
                int end = visualLine.LastDocumentLine.LineNumber;
                if (documentLineNumber >= start && documentLineNumber <= end)
                    return visualLine;
            }
            return null;
        }

        /// <summary>
        /// Gets the visual line that contains the document line with the specified number.
        /// If that line is outside the visible range, a new VisualLine for that document line is constructed.
        /// </summary>
        public VisualLine GetOrConstructVisualLine(DocumentLine documentLine)
        {
            if (documentLine == null)
                throw new ArgumentNullException("documentLine");
            if (!this.Document.Lines.Contains(documentLine))
                throw new InvalidOperationException("Line belongs to wrong document");
            VerifyAccess();

            VisualLine l = GetVisualLine(documentLine.LineNumber);
            if (l == null)
            {
                TextRunProperties globalTextRunProperties = CreateGlobalTextRunProperties();
                VisualLineTextParagraphProperties paragraphProperties = CreateParagraphProperties(globalTextRunProperties);

                while (heightTree.GetIsCollapsed(documentLine.LineNumber))
                {
                    documentLine = documentLine.PreviousLine;
                }

                l = BuildVisualLine(documentLine,
                                    globalTextRunProperties, paragraphProperties,
                                    elementGenerators.ToArray(), lineTransformers.ToArray(),
                                    lastAvailableSize);
                allVisualLines.Add(l);
                // update all visual top values (building the line might have changed visual top of other lines due to word wrapping)
                foreach (var line in allVisualLines)
                {
                    line.VisualTop = heightTree.GetVisualPosition(line.FirstDocumentLine);
                }
            }
            return l;
        }
        #endregion

        #region Visual Lines (fields and properties)
        List<VisualLine> allVisualLines = new List<VisualLine>();
        ReadOnlyCollection<VisualLine> visibleVisualLines;
        double clippedPixelsOnTop;
        List<VisualLine> newVisualLines;

        /// <summary>
        /// Gets the currently visible visual lines.
        /// </summary>
        /// <exception cref="VisualLinesInvalidException">
        /// Gets thrown if there are invalid visual lines when this property is accessed.
        /// You can use the <see cref="VisualLinesValid"/> property to check for this case,
        /// or use the <see cref="EnsureVisualLines()"/> method to force creating the visual lines
        /// when they are invalid.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        public ReadOnlyCollection<VisualLine> VisualLines
        {
            get
            {
                if (visibleVisualLines == null)
                    throw new VisualLinesInvalidException();
                return visibleVisualLines;
            }
        }

        /// <summary>
        /// Gets whether the visual lines are valid.
        /// Will return false after a call to Redraw().
        /// Accessing the visual lines property will cause a <see cref="VisualLinesInvalidException"/>
        /// if this property is <c>false</c>.
        /// </summary>
        public bool VisualLinesValid
        {
            get { return visibleVisualLines != null; }
        }

        /// <summary>
        /// Occurs when the TextView is about to be measured and will regenerate its visual lines.
        /// This event may be used to mark visual lines as invalid that would otherwise be reused.
        /// </summary>
        public event EventHandler<VisualLineConstructionStartEventArgs> VisualLineConstructionStarting;

        /// <summary>
        /// Occurs when the TextView was measured and changed its visual lines.
        /// </summary>
        public event EventHandler VisualLinesChanged;

        /// <summary>
        /// If the visual lines are invalid, creates new visual lines for the visible part
        /// of the document.
        /// If all visual lines are valid, this method does nothing.
        /// </summary>
        /// <exception cref="InvalidOperationException">The visual line build process is already running.
        /// It is not allowed to call this method during the construction of a visual line.</exception>
        public void EnsureVisualLines()
        {
            Dispatcher.VerifyAccess();
            if (inMeasure)
                throw new InvalidOperationException("The visual line build process is already running! Cannot EnsureVisualLines() during Measure!");
            if (!VisualLinesValid)
            {
                // increase priority for re-measure
                InvalidateMeasure(DispatcherPriority.Normal);
                // force immediate re-measure
                UpdateLayout();
            }
            // Sometimes we still have invalid lines after UpdateLayout - work around the problem
            // by calling MeasureOverride directly.
            if (!VisualLinesValid)
            {
                Debug.WriteLine("UpdateLayout() failed in EnsureVisualLines");
                MeasureOverride(lastAvailableSize);
            }
            if (!VisualLinesValid)
                throw new VisualLinesInvalidException("Internal error: visual lines invalid after EnsureVisualLines call");
        }
        #endregion

        #region Measure
        /// <summary>
        /// Additonal amount that allows horizontal scrolling past the end of the longest line.
        /// This is necessary to ensure the caret always is visible, even when it is at the end of the longest line.
        /// </summary>
        const double AdditionalHorizontalScrollAmount = 3;

        Size lastAvailableSize;
        bool inMeasure;

        /// <inheritdoc/>
        protected override Size MeasureOverride(Size availableSize)
        {
            // We don't support infinite available width, so we'll limit it to 32000 pixels.
            if (availableSize.Width > 32000)
                availableSize.Width = 32000;

            if (!canHorizontallyScroll && !availableSize.Width.IsClose(lastAvailableSize.Width))
                ClearVisualLines();
            lastAvailableSize = availableSize;

            foreach (UIElement layer in layers)
            {
                layer.Measure(availableSize);
            }
            MeasureInlineObjects();

            InvalidateVisual(); // = InvalidateArrange+InvalidateRender

            double maxWidth;
            if (document == null)
            {
                // no document -> create empty list of lines
                allVisualLines = new List<VisualLine>();
                visibleVisualLines = allVisualLines.AsReadOnly();
                maxWidth = 0;
            }
            else
            {
                inMeasure = true;
                try
                {
                    maxWidth = CreateAndMeasureVisualLines(availableSize);
                }
                finally
                {
                    inMeasure = false;
                }
            }

            // remove inline objects only at the end, so that inline objects that were re-used are not removed from the editor
            RemoveInlineObjectsNow();

            maxWidth += AdditionalHorizontalScrollAmount;
            double heightTreeHeight = this.DocumentHeight;
            TextEditorOptions options = this.Options;

            // Dirkster99 BugFix for binding options in VS2010			
            if (options != null)
            {
                if (options.AllowScrollBelowDocument)
                {
                    if (!double.IsInfinity(scrollViewport.Height))
                    {
                        // HACK: we need to keep at least Caret.MinimumDistanceToViewBorder visible so that we don't scroll back up when the user types after
                        // scrolling to the very bottom.
                        double minVisibleDocumentHeight = Math.Max(DefaultLineHeight, Editing.Caret.MinimumDistanceToViewBorder);
                        // scrollViewportBottom: bottom of scroll view port, but clamped so that at least minVisibleDocumentHeight of the document stays visible.
                        double scrollViewportBottom = Math.Min(heightTreeHeight - minVisibleDocumentHeight, scrollOffset.Y) + scrollViewport.Height;
                        // increase the extend height to allow scrolling below the document
                        heightTreeHeight = Math.Max(heightTreeHeight, scrollViewportBottom);
                    }
                }

                if (options.AllowScrollBelowDocument)
                {
                    if (!double.IsInfinity(scrollViewport.Height))
                    {
                        heightTreeHeight = Math.Max(heightTreeHeight, Math.Min(heightTreeHeight - 50, scrollOffset.Y) + scrollViewport.Height);
                    }
                }
            }

            textLayer.SetVisualLines(visibleVisualLines);

            SetScrollData(availableSize,
                          new Size(maxWidth, heightTreeHeight),
                          scrollOffset);
            VisualLinesChanged?.Invoke(this, EventArgs.Empty);

            return new Size(Math.Min(availableSize.Width, maxWidth), Math.Min(availableSize.Height, heightTreeHeight));
        }

        /// <summary>
        /// Build all VisualLines in the visible range.
        /// </summary>
        /// <returns>Width the longest line</returns>
        double CreateAndMeasureVisualLines(Size availableSize)
        {
            TextRunProperties globalTextRunProperties = CreateGlobalTextRunProperties();
            VisualLineTextParagraphProperties paragraphProperties = CreateParagraphProperties(globalTextRunProperties);

            ////            Debug.WriteLine("Measure availableSize=" + availableSize + ", scrollOffset=" + scrollOffset);
            var firstLineInView = heightTree.GetLineByVisualPosition(scrollOffset.Y);

            // number of pixels clipped from the first visual line(s)
            clippedPixelsOnTop = scrollOffset.Y - heightTree.GetVisualPosition(firstLineInView);
            // clippedPixelsOnTop should be >= 0, except for floating point inaccurracy.
            Debug.Assert(clippedPixelsOnTop >= -ExtensionMethods.Epsilon);

            newVisualLines = new List<VisualLine>();

            VisualLineConstructionStarting?.Invoke(this, new VisualLineConstructionStartEventArgs(firstLineInView));

            var elementGeneratorsArray = elementGenerators.ToArray();
            var lineTransformersArray = lineTransformers.ToArray();
            var nextLine = firstLineInView;
            double maxWidth = 0;
            double yPos = -clippedPixelsOnTop;
            while (yPos < availableSize.Height && nextLine != null)
            {
                VisualLine visualLine = GetVisualLine(nextLine.LineNumber);
                if (visualLine == null)
                {
                    visualLine = BuildVisualLine(nextLine,
                                                 globalTextRunProperties, paragraphProperties,
                                                 elementGeneratorsArray, lineTransformersArray,
                                                 availableSize);
                }

                visualLine.VisualTop = scrollOffset.Y + yPos;

                nextLine = visualLine.LastDocumentLine.NextLine;

                yPos += visualLine.Height;

                foreach (TextLine textLine in visualLine.TextLines)
                {
                    if (textLine.WidthIncludingTrailingWhitespace > maxWidth)
                        maxWidth = textLine.WidthIncludingTrailingWhitespace;
                }

                newVisualLines.Add(visualLine);
            }

            foreach (VisualLine line in allVisualLines)
            {
                Debug.Assert(line.IsDisposed == false);
                if (!newVisualLines.Contains(line))
                    DisposeVisualLine(line);
            }

            allVisualLines = newVisualLines;
            // visibleVisualLines = readonly copy of visual lines
            visibleVisualLines = new ReadOnlyCollection<VisualLine>(newVisualLines.ToArray());
            newVisualLines = null;

            if (allVisualLines.Any(line => line.IsDisposed))
            {
                throw new InvalidOperationException("A visual line was disposed even though it is still in use.\n" +
                                                    "This can happen when Redraw() is called during measure for lines " +
                                                    "that are already constructed.");
            }
            return maxWidth;
        }
        #endregion

        #region BuildVisualLine
        TextFormatter formatter;
        internal TextViewCachedElements cachedElements;

        TextRunProperties CreateGlobalTextRunProperties()
        {
            var p = new GlobalTextRunProperties()
            {
                typeface = this.CreateTypeface(),
                fontRenderingEmSize = FontSize,
                foregroundBrush = (Brush)GetValue(Control.ForegroundProperty)
            };
            ExtensionMethods.CheckIsFrozen(p.foregroundBrush);
            p.cultureInfo = CultureInfo.CurrentCulture;
            return p;
        }

        VisualLineTextParagraphProperties CreateParagraphProperties(TextRunProperties defaultTextRunProperties)
        {
            // Dirkster99 BugFix for binding options in VS2010
            return new VisualLineTextParagraphProperties
            {
                defaultTextRunProperties = defaultTextRunProperties,
                textWrapping = canHorizontallyScroll ? TextWrapping.NoWrap : TextWrapping.Wrap,
                tabSize = (Options == null ? 4 : Options.IndentationSize) * WideSpaceWidth
            };
        }

        VisualLine BuildVisualLine(DocumentLine documentLine,
                                   TextRunProperties globalTextRunProperties,
                                   VisualLineTextParagraphProperties paragraphProperties,
                                   VisualLineElementGenerator[] elementGeneratorsArray,
                                   IVisualLineTransformer[] lineTransformersArray,
                                   Size availableSize)
        {
            if (heightTree.GetIsCollapsed(documentLine.LineNumber))
                throw new InvalidOperationException("Trying to build visual line from collapsed line");

            //Debug.WriteLine("Building line " + documentLine.LineNumber);

            VisualLine visualLine = new VisualLine(this, documentLine);
            VisualLineTextSource textSource = new VisualLineTextSource(visualLine)
            {
                Document = document,
                GlobalTextRunProperties = globalTextRunProperties,
                TextView = this
            };

            visualLine.ConstructVisualElements(textSource, elementGeneratorsArray);

            if (visualLine.FirstDocumentLine != visualLine.LastDocumentLine)
            {
                // Check whether the lines are collapsed correctly:
                double firstLinePos = heightTree.GetVisualPosition(visualLine.FirstDocumentLine.NextLine);
                double lastLinePos = heightTree.GetVisualPosition(visualLine.LastDocumentLine.NextLine ?? visualLine.LastDocumentLine);
                if (!firstLinePos.IsClose(lastLinePos))
                {
                    for (int i = visualLine.FirstDocumentLine.LineNumber + 1; i <= visualLine.LastDocumentLine.LineNumber; i++)
                    {
                        if (!heightTree.GetIsCollapsed(i))
                            throw new InvalidOperationException("Line " + i + " was skipped by a VisualLineElementGenerator, but it is not collapsed.");
                    }
                    throw new InvalidOperationException("All lines collapsed but visual pos different - height tree inconsistency?");
                }
            }

            visualLine.RunTransformers(textSource, lineTransformersArray);

            // now construct textLines:
            int textOffset = 0;
            TextLineBreak lastLineBreak = null;
            var textLines = new List<TextLine>();
            paragraphProperties.indent = 0;
            paragraphProperties.firstLineInParagraph = true;
            while (textOffset <= visualLine.VisualLengthWithEndOfLineMarker)
            {
                TextLine textLine = formatter.FormatLine(
                    textSource,
                    textOffset,
                    availableSize.Width,
                    paragraphProperties,
                    lastLineBreak
                );
                textLines.Add(textLine);
                textOffset += textLine.Length;

                // exit loop so that we don't do the indentation calculation if there's only a single line
                if (textOffset >= visualLine.VisualLengthWithEndOfLineMarker)
                    break;

                if (paragraphProperties.firstLineInParagraph)
                {
                    paragraphProperties.firstLineInParagraph = false;

                    TextEditorOptions options = this.Options;
                    double indentation = 0;
                    if (options.InheritWordWrapIndentation)
                    {
                        // determine indentation for next line:
                        int indentVisualColumn = GetIndentationVisualColumn(visualLine);
                        if (indentVisualColumn > 0 && indentVisualColumn < textOffset)
                        {
                            indentation = textLine.GetDistanceFromCharacterHit(new CharacterHit(indentVisualColumn, 0));
                        }
                    }
                    indentation += options.WordWrapIndentation;
                    // apply the calculated indentation unless it's more than half of the text editor size:
                    if (indentation > 0 && indentation * 2 < availableSize.Width)
                        paragraphProperties.indent = indentation;
                }
                lastLineBreak = textLine.GetTextLineBreak();
            }
            visualLine.SetTextLines(textLines);
            heightTree.SetHeight(visualLine.FirstDocumentLine, visualLine.Height);
            return visualLine;
        }

        static int GetIndentationVisualColumn(VisualLine visualLine)
        {
            if (visualLine.Elements.Count == 0)
                return 0;
            int column = 0;
            int elementIndex = 0;
            VisualLineElement element = visualLine.Elements[elementIndex];
            while (element.IsWhitespace(column))
            {
                column++;
                if (column == element.VisualColumn + element.VisualLength)
                {
                    elementIndex++;
                    if (elementIndex == visualLine.Elements.Count)
                        break;
                    element = visualLine.Elements[elementIndex];
                }
            }
            return column;
        }
        #endregion

        #region Arrange
        /// <summary>
        /// Arrange implementation.
        /// </summary>
        protected override Size ArrangeOverride(Size finalSize)
        {
            EnsureVisualLines();

            foreach (UIElement layer in layers)
            {
                layer.Arrange(new Rect(new Point(0, 0), finalSize));
            }

            if (document == null || allVisualLines.Count == 0)
                return finalSize;

            // validate scroll position
            Vector newScrollOffset = scrollOffset;
            if (scrollOffset.X + finalSize.Width > scrollExtent.Width)
            {
                newScrollOffset.X = Math.Max(0, scrollExtent.Width - finalSize.Width);
            }
            if (scrollOffset.Y + finalSize.Height > scrollExtent.Height)
            {
                newScrollOffset.Y = Math.Max(0, scrollExtent.Height - finalSize.Height);
            }
            if (SetScrollData(scrollViewport, scrollExtent, newScrollOffset))
                InvalidateMeasure(DispatcherPriority.Normal);

            //Debug.WriteLine("Arrange finalSize=" + finalSize + ", scrollOffset=" + scrollOffset);

            //			double maxWidth = 0;

            if (visibleVisualLines != null)
            {
                Point pos = new Point(-scrollOffset.X, -clippedPixelsOnTop);
                foreach (VisualLine visualLine in visibleVisualLines)
                {
                    int offset = 0;
                    foreach (TextLine textLine in visualLine.TextLines)
                    {
                        foreach (var span in textLine.GetTextRunSpans())
                        {
                            if (span.Value is InlineObjectRun)
                            {
                                InlineObjectRun inline = span.Value as InlineObjectRun;

                                if (inline.VisualLine != null)
                                {
                                    Debug.Assert(inlineObjects.Contains(inline));
                                    double distance = textLine.GetDistanceFromCharacterHit(new CharacterHit(offset, 0));
                                    inline.Element.Arrange(new Rect(new Point(pos.X + distance, pos.Y), inline.Element.DesiredSize));
                                }
                            }
                            offset += span.Length;
                        }
                        pos.Y += textLine.Height;
                    }
                }
            }
            InvalidateCursorIfMouseWithinTextView();

            return finalSize;
        }
        #endregion

        #region Render
        readonly ObserveAddRemoveCollection<IBackgroundRenderer> backgroundRenderers;

        /// <summary>
        /// Gets the list of background renderers.
        /// </summary>
        public IList<IBackgroundRenderer> BackgroundRenderers
        {
            get { return backgroundRenderers; }
        }

        void BackgroundRenderer_Added(IBackgroundRenderer renderer)
        {
            ConnectToTextView(renderer);
            InvalidateLayer(renderer.Layer);
        }

        void BackgroundRenderer_Removed(IBackgroundRenderer renderer)
        {
            DisconnectFromTextView(renderer);
            InvalidateLayer(renderer.Layer);
        }

        /// <inheritdoc/>
        protected override void OnRender(DrawingContext drawingContext)
        {
            RenderBackground(drawingContext, KnownLayer.Background);
            foreach (var line in visibleVisualLines)
            {
                Brush currentBrush = null;
                int startVC = 0;
                int length = 0;
                foreach (var element in line.Elements)
                {
                    if (currentBrush == null || !currentBrush.Equals(element.BackgroundBrush))
                    {
                        if (currentBrush != null)
                        {
                            BackgroundGeometryBuilder builder = new BackgroundGeometryBuilder()
                            {
                                AlignToWholePixels = true,
                                CornerRadius = 3
                            };
                            foreach (var rect in BackgroundGeometryBuilder.GetRectsFromVisualSegment(this, line, startVC, startVC + length))
                                builder.AddRectangle(this, rect);
                            Geometry geometry = builder.CreateGeometry();
                            if (geometry != null)
                            {
                                drawingContext.DrawGeometry(currentBrush, null, geometry);
                            }
                        }
                        startVC = element.VisualColumn;
                        length = element.DocumentLength;
                        currentBrush = element.BackgroundBrush;
                    }
                    else
                    {
                        length += element.VisualLength;
                    }
                }
                if (currentBrush != null)
                {
                    BackgroundGeometryBuilder builder = new BackgroundGeometryBuilder()
                    {
                        AlignToWholePixels = true,
                        CornerRadius = 3
                    };
                    foreach (var rect in BackgroundGeometryBuilder.GetRectsFromVisualSegment(this, line, startVC, startVC + length))
                        builder.AddRectangle(this, rect);
                    Geometry geometry = builder.CreateGeometry();
                    if (geometry != null)
                    {
                        drawingContext.DrawGeometry(currentBrush, null, geometry);
                    }
                }
            }
        }

        internal void RenderBackground(DrawingContext drawingContext, KnownLayer layer)
        {
            foreach (IBackgroundRenderer bg in backgroundRenderers)
            {
                if (bg.Layer == layer)
                {
                    bg.Draw(this, drawingContext);
                }
            }
        }

        internal void ArrangeTextLayer(IList<VisualLineDrawingVisual> visuals)
        {
            Point pos = new Point(-scrollOffset.X, -clippedPixelsOnTop);
            foreach (VisualLineDrawingVisual visual in visuals)
            {
                TranslateTransform t = visual.Transform as TranslateTransform;
                if (t == null || t.X != pos.X || t.Y != pos.Y)
                {
                    visual.Transform = new TranslateTransform(pos.X, pos.Y);
                    visual.Transform.Freeze();
                }
                pos.Y += visual.Height;
            }
        }
        #endregion

        #region IScrollInfo implementation
        /// <summary>
        /// Size of the document, in pixels.
        /// </summary>
        Size scrollExtent;

        /// <summary>
        /// Offset of the scroll position.
        /// </summary>
        Vector scrollOffset;

        /// <summary>
        /// Size of the viewport.
        /// </summary>
        Size scrollViewport;

        void ClearScrollData()
        {
            SetScrollData(new Size(), new Size(), new Vector());
        }

        bool SetScrollData(Size viewport, Size extent, Vector offset)
        {
            if (!(viewport.IsClose(this.scrollViewport)
                  && extent.IsClose(this.scrollExtent)
                  && offset.IsClose(this.scrollOffset)))
            {
                this.scrollViewport = viewport;
                this.scrollExtent = extent;
                SetScrollOffset(offset);
                this.OnScrollChange();
                return true;
            }
            return false;
        }

        void OnScrollChange()
        {
            ScrollViewer scrollOwner = ((IScrollInfo)this).ScrollOwner;
            if (scrollOwner != null)
            {
                scrollOwner.InvalidateScrollInfo();
            }
        }

        bool canVerticallyScroll;
        bool IScrollInfo.CanVerticallyScroll
        {
            get { return canVerticallyScroll; }
            set
            {
                if (canVerticallyScroll != value)
                {
                    canVerticallyScroll = value;
                    InvalidateMeasure(DispatcherPriority.Normal);
                }
            }
        }
        bool canHorizontallyScroll;
        bool IScrollInfo.CanHorizontallyScroll
        {
            get { return canHorizontallyScroll; }
            set
            {
                if (canHorizontallyScroll != value)
                {
                    canHorizontallyScroll = value;
                    ClearVisualLines();
                    InvalidateMeasure(DispatcherPriority.Normal);
                }
            }
        }

        double IScrollInfo.ExtentWidth
        {
            get { return scrollExtent.Width; }
        }

        double IScrollInfo.ExtentHeight
        {
            get { return scrollExtent.Height; }
        }

        double IScrollInfo.ViewportWidth
        {
            get { return scrollViewport.Width; }
        }

        double IScrollInfo.ViewportHeight
        {
            get { return scrollViewport.Height; }
        }

        /// <summary>
        /// Gets the horizontal scroll offset.
        /// </summary>
        public double HorizontalOffset
        {
            get { return scrollOffset.X; }
        }

        /// <summary>
        /// Gets the vertical scroll offset.
        /// </summary>
        public double VerticalOffset
        {
            get { return scrollOffset.Y; }
        }

        /// <summary>
        /// Gets the scroll offset;
        /// </summary>
        public Vector ScrollOffset
        {
            get { return scrollOffset; }
        }

        /// <summary>
        /// Occurs when the scroll offset has changed.
        /// </summary>
        public event EventHandler ScrollOffsetChanged;

        void SetScrollOffset(Vector vector)
        {
            if (!canHorizontallyScroll)
                vector.X = 0;
            if (!canVerticallyScroll)
                vector.Y = 0;

            if (!scrollOffset.IsClose(vector))
            {
                scrollOffset = vector;
                ScrollOffsetChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        ScrollViewer IScrollInfo.ScrollOwner { get; set; }

        void IScrollInfo.LineUp()
        {
            ((IScrollInfo)this).SetVerticalOffset(scrollOffset.Y - DefaultLineHeight);
        }

        void IScrollInfo.LineDown()
        {
            ((IScrollInfo)this).SetVerticalOffset(scrollOffset.Y + DefaultLineHeight);
        }

        void IScrollInfo.LineLeft()
        {
            ((IScrollInfo)this).SetHorizontalOffset(scrollOffset.X - WideSpaceWidth);
        }

        void IScrollInfo.LineRight()
        {
            ((IScrollInfo)this).SetHorizontalOffset(scrollOffset.X + WideSpaceWidth);
        }

        void IScrollInfo.PageUp()
        {
            ((IScrollInfo)this).SetVerticalOffset(scrollOffset.Y - scrollViewport.Height);
        }

        void IScrollInfo.PageDown()
        {
            ((IScrollInfo)this).SetVerticalOffset(scrollOffset.Y + scrollViewport.Height);
        }

        void IScrollInfo.PageLeft()
        {
            ((IScrollInfo)this).SetHorizontalOffset(scrollOffset.X - scrollViewport.Width);
        }

        void IScrollInfo.PageRight()
        {
            ((IScrollInfo)this).SetHorizontalOffset(scrollOffset.X + scrollViewport.Width);
        }

        void IScrollInfo.MouseWheelUp()
        {
            ((IScrollInfo)this).SetVerticalOffset(
                scrollOffset.Y - (SystemParameters.WheelScrollLines * DefaultLineHeight));
            OnScrollChange();
        }

        void IScrollInfo.MouseWheelDown()
        {
            ((IScrollInfo)this).SetVerticalOffset(
                scrollOffset.Y + (SystemParameters.WheelScrollLines * DefaultLineHeight));
            OnScrollChange();
        }

        void IScrollInfo.MouseWheelLeft()
        {
            ((IScrollInfo)this).SetHorizontalOffset(
                scrollOffset.X - (SystemParameters.WheelScrollLines * WideSpaceWidth));
            OnScrollChange();
        }

        void IScrollInfo.MouseWheelRight()
        {
            ((IScrollInfo)this).SetHorizontalOffset(
                scrollOffset.X + (SystemParameters.WheelScrollLines * WideSpaceWidth));
            OnScrollChange();
        }

        bool defaultTextMetricsValid;
        double wideSpaceWidth; // Width of an 'x'. Used as basis for the tab width, and for scrolling.
        double defaultLineHeight; // Height of a line containing 'x'. Used for scrolling.
        double defaultBaseline; // Baseline of a line containing 'x'. Used for TextTop/TextBottom calculation.

        /// <summary>
        /// Gets the width of a 'wide space' (the space width used for calculating the tab size).
        /// </summary>
        /// <remarks>
        /// This is the width of an 'x' in the current font.
        /// We do not measure the width of an actual space as that would lead to tiny tabs in
        /// some proportional fonts.
        /// For monospaced fonts, this property will return the expected value, as 'x' and ' ' have the same width.
        /// </remarks>
        public double WideSpaceWidth
        {
            get
            {
                CalculateDefaultTextMetrics();
                return wideSpaceWidth;
            }
        }

        /// <summary>
        /// Gets the default line height. This is the height of an empty line or a line containing regular text.
        /// Lines that include formatted text or custom UI elements may have a different line height.
        /// </summary>
        public double DefaultLineHeight
        {
            get
            {
                CalculateDefaultTextMetrics();
                return defaultLineHeight;
            }
        }

        /// <summary>
        /// Gets the default baseline position. This is the difference between <see cref="VisualYPosition.TextTop"/>
        /// and <see cref="VisualYPosition.Baseline"/> for a line containing regular text.
        /// Lines that include formatted text or custom UI elements may have a different baseline.
        /// </summary>
        public double DefaultBaseline
        {
            get
            {
                CalculateDefaultTextMetrics();
                return defaultBaseline;
            }
        }

        void InvalidateDefaultTextMetrics()
        {
            defaultTextMetricsValid = false;
            if (heightTree != null)
            {
                // calculate immediately so that height tree gets updated
                CalculateDefaultTextMetrics();
            }
        }

        void CalculateDefaultTextMetrics()
        {
            if (defaultTextMetricsValid)
                return;
            defaultTextMetricsValid = true;
            if (formatter != null)
            {
                var textRunProperties = CreateGlobalTextRunProperties();
                using (var line = formatter.FormatLine(
                    new SimpleTextSource("x", textRunProperties),
                    0, 32000,
                    new VisualLineTextParagraphProperties { defaultTextRunProperties = textRunProperties },
                    null))
                {
                    wideSpaceWidth = Math.Max(1, line.WidthIncludingTrailingWhitespace);
                    defaultBaseline = Math.Max(1, line.Baseline);
                    defaultLineHeight = Math.Max(1, line.Height);
                }
            }
            else
            {
                wideSpaceWidth = FontSize / 2;
                defaultBaseline = FontSize;
                defaultLineHeight = FontSize + 3;
            }
            // Update heightTree.DefaultLineHeight, if a document is loaded.
            if (heightTree != null)
                heightTree.DefaultLineHeight = defaultLineHeight;
        }

        static double ValidateVisualOffset(double offset)
        {
            if (double.IsNaN(offset))
                throw new ArgumentException("offset must not be NaN");
            if (offset < 0)
                return 0;
            else
                return offset;
        }

        void IScrollInfo.SetHorizontalOffset(double offset)
        {
            offset = ValidateVisualOffset(offset);
            if (!scrollOffset.X.IsClose(offset))
            {
                SetScrollOffset(new Vector(offset, scrollOffset.Y));
                InvalidateVisual();
                textLayer.InvalidateVisual();
            }
        }

        void IScrollInfo.SetVerticalOffset(double offset)
        {
            offset = ValidateVisualOffset(offset);
            if (!scrollOffset.Y.IsClose(offset))
            {
                SetScrollOffset(new Vector(scrollOffset.X, offset));
                InvalidateMeasure(DispatcherPriority.Normal);
            }
        }

        Rect IScrollInfo.MakeVisible(Visual visual, Rect rectangle)
        {
            if (rectangle.IsEmpty || visual == null || visual == this || !this.IsAncestorOf(visual))
            {
                return Rect.Empty;
            }
            // Convert rectangle into our coordinate space.
            GeneralTransform childTransform = visual.TransformToAncestor(this);
            rectangle = childTransform.TransformBounds(rectangle);

            MakeVisible(Rect.Offset(rectangle, scrollOffset));

            return rectangle;
        }

        /// <summary>
        /// Scrolls the text view so that the specified rectangle gets visible.
        /// </summary>
        public void MakeVisible(Rect rectangle)
        {
            Rect visibleRectangle = new Rect(scrollOffset.X, scrollOffset.Y,
                                             scrollViewport.Width, scrollViewport.Height);
            Vector newScrollOffset = scrollOffset;
            if (rectangle.Left < visibleRectangle.Left)
            {
                if (rectangle.Right > visibleRectangle.Right)
                {
                    newScrollOffset.X = rectangle.Left + rectangle.Width / 2;
                }
                else
                {
                    newScrollOffset.X = rectangle.Left;
                }
            }
            else if (rectangle.Right > visibleRectangle.Right)
            {
                newScrollOffset.X = rectangle.Right - scrollViewport.Width;
            }
            if (rectangle.Top < visibleRectangle.Top)
            {
                if (rectangle.Bottom > visibleRectangle.Bottom)
                {
                    newScrollOffset.Y = rectangle.Top + rectangle.Height / 2;
                }
                else
                {
                    newScrollOffset.Y = rectangle.Top;
                }
            }
            else if (rectangle.Bottom > visibleRectangle.Bottom)
            {
                newScrollOffset.Y = rectangle.Bottom - scrollViewport.Height;
            }
            newScrollOffset.X = ValidateVisualOffset(newScrollOffset.X);
            newScrollOffset.Y = ValidateVisualOffset(newScrollOffset.Y);
            if (!scrollOffset.IsClose(newScrollOffset))
            {
                SetScrollOffset(newScrollOffset);
                this.OnScrollChange();
                InvalidateMeasure(DispatcherPriority.Normal);
            }
        }
        #endregion

        #region Visual element mouse handling
        /// <inheritdoc/>
        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
        {
            // accept clicks even where the text area draws no background
            return new PointHitTestResult(this, hitTestParameters.HitPoint);
        }

        [ThreadStatic] static bool invalidCursor;

        /// <summary>
        /// Updates the mouse cursor by calling <see cref="Mouse.UpdateCursor"/>, but with background priority.
        /// </summary>
        public static void InvalidateCursor()
        {
            if (!invalidCursor)
            {
                invalidCursor = true;
                Dispatcher.CurrentDispatcher.BeginInvoke(
                    DispatcherPriority.Background, // fixes issue #288
                    new Action(
                        delegate
                        {
                            invalidCursor = false;
                            Mouse.UpdateCursor();
                        }));
            }
        }

        internal void InvalidateCursorIfMouseWithinTextView()
        {
            // Don't unnecessarily call Mouse.UpdateCursor() if the mouse is outside the text view.
            // Unnecessary updates may cause the mouse pointer to flicker
            // (e.g. if it is over a window border, it blinks between Resize and Normal)
            if (this.IsMouseOver)
                InvalidateCursor();
        }

        /// <inheritdoc/>
        protected override void OnQueryCursor(QueryCursorEventArgs e)
        {
            VisualLineElement element = GetVisualLineElementFromPosition(e.GetPosition(this) + scrollOffset);
            if (element != null)
            {
                element.OnQueryCursor(e);
            }
        }

        /// <inheritdoc/>
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if (!e.Handled)
            {
                EnsureVisualLines();
                VisualLineElement element = GetVisualLineElementFromPosition(e.GetPosition(this) + scrollOffset);
                if (element != null)
                {
                    element.OnMouseDown(e);
                }
            }
        }

        /// <inheritdoc/>
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            if (!e.Handled)
            {
                EnsureVisualLines();
                VisualLineElement element = GetVisualLineElementFromPosition(e.GetPosition(this) + scrollOffset);
                if (element != null)
                {
                    element.OnMouseUp(e);
                }
            }
        }
        #endregion

        #region Getting elements from Visual Position
        /// <summary>
        /// Gets the visual line at the specified document position (relative to start of document).
        /// Returns null if there is no visual line for the position (e.g. the position is outside the visible
        /// text area).
        /// </summary>
        public VisualLine GetVisualLineFromVisualTop(double visualTop)
        {
            // TODO: change this method to also work outside the visible range -
            // required to make GetPosition work as expected!
            EnsureVisualLines();
            foreach (VisualLine vl in this.VisualLines)
            {
                if (visualTop < vl.VisualTop)
                    continue;
                if (visualTop < vl.VisualTop + vl.Height)
                    return vl;
            }
            return null;
        }

        /// <summary>
        /// Gets the visual top position (relative to start of document) from a document line number.
        /// </summary>
        public double GetVisualTopByDocumentLine(int line)
        {
            VerifyAccess();
            if (heightTree == null)
                throw ThrowUtil.NoDocumentAssigned();
            return heightTree.GetVisualPosition(heightTree.GetLineByNumber(line));
        }

        VisualLineElement GetVisualLineElementFromPosition(Point visualPosition)
        {
            VisualLine vl = GetVisualLineFromVisualTop(visualPosition.Y);
            if (vl != null)
            {
                int column = vl.GetVisualColumnFloor(visualPosition);
                //				Debug.WriteLine(vl.FirstDocumentLine.LineNumber + " vc " + column);
                foreach (VisualLineElement element in vl.Elements)
                {
                    if (element.VisualColumn + element.VisualLength <= column)
                        continue;
                    return element;
                }
            }
            return null;
        }
        #endregion

        #region Visual Position <-> TextViewPosition
        /// <summary>
        /// Gets the visual position from a text view position.
        /// </summary>
        /// <param name="position">The text view position.</param>
        /// <param name="yPositionMode">The mode how to retrieve the Y position.</param>
        /// <returns>The position in WPF device-independent pixels relative
        /// to the top left corner of the document.</returns>
        public Point GetVisualPosition(TextViewPosition position, VisualYPosition yPositionMode)
        {
            VerifyAccess();
            if (this.Document == null)
                throw ThrowUtil.NoDocumentAssigned();
            DocumentLine documentLine = this.Document.GetLineByNumber(position.Line);
            VisualLine visualLine = GetOrConstructVisualLine(documentLine);
            int visualColumn = position.VisualColumn;
            if (visualColumn < 0)
            {
                int offset = documentLine.Offset + position.Column - 1;
                visualColumn = visualLine.GetVisualColumn(offset - visualLine.FirstDocumentLine.Offset);
            }
            return visualLine.GetVisualPosition(visualColumn, position.IsAtEndOfLine, yPositionMode);
        }

        /// <summary>
        /// Gets the text view position from the specified visual position.
        /// If the position is within a character, it is rounded to the next character boundary.
        /// </summary>
        /// <param name="visualPosition">The position in WPF device-independent pixels relative
        /// to the top left corner of the document.</param>
        /// <returns>The logical position, or null if the position is outside the document.</returns>
        public TextViewPosition? GetPosition(Point visualPosition)
        {
            VerifyAccess();
            if (this.Document == null)
                throw ThrowUtil.NoDocumentAssigned();
            VisualLine line = GetVisualLineFromVisualTop(visualPosition.Y);
            if (line == null)
                return null;
            return line.GetTextViewPosition(visualPosition, Options.EnableVirtualSpace);
        }

        /// <summary>
        /// Gets the text view position from the specified visual position.
        /// If the position is inside a character, the position in front of the character is returned.
        /// </summary>
        /// <param name="visualPosition">The position in WPF device-independent pixels relative
        /// to the top left corner of the document.</param>
        /// <returns>The logical position, or null if the position is outside the document.</returns>
        public TextViewPosition? GetPositionFloor(Point visualPosition)
        {
            VerifyAccess();
            if (this.Document == null)
                throw ThrowUtil.NoDocumentAssigned();
            VisualLine line = GetVisualLineFromVisualTop(visualPosition.Y);
            if (line == null)
                return null;
            return line.GetTextViewPositionFloor(visualPosition, Options.EnableVirtualSpace);
        }
        #endregion

        #region Service Provider
        readonly ServiceContainer services = new ServiceContainer();

        /// <summary>
        /// Gets a service container used to associate services with the text view.
        /// </summary>
        /// <remarks>
        /// This container does not provide document services -
        /// use <c>TextView.GetService()</c> instead of <c>TextView.Services.GetService()</c> to ensure
        /// that document services can be found as well.
        /// </remarks>
        public ServiceContainer Services
        {
            get { return services; }
        }

        /// <summary>
        /// Retrieves a service from the text view.
        /// If the service is not found in the <see cref="Services"/> container,
        /// this method will also look for it in the current document's service provider.
        /// </summary>
        public virtual object GetService(Type serviceType)
        {
            object instance = services.GetService(serviceType);
            if (instance == null && document != null)
            {
                instance = document.ServiceProvider.GetService(serviceType);
            }
            return instance;
        }

        void ConnectToTextView(object obj)
        {
            if (obj is ITextViewConnect)
            {
                ITextViewConnect c = obj as ITextViewConnect;
                c.AddToTextView(this);
            }
        }

        void DisconnectFromTextView(object obj)
        {
            if (obj is ITextViewConnect)
            {
                ITextViewConnect c = obj as ITextViewConnect;
                c.RemoveFromTextView(this);
            }
        }
        #endregion

        #region MouseHover
        /// <summary>
        /// The PreviewMouseHover event.
        /// </summary>
        public static readonly RoutedEvent PreviewMouseHoverEvent =
            EventManager.RegisterRoutedEvent("PreviewMouseHover", RoutingStrategy.Tunnel,
                                             typeof(MouseEventHandler), typeof(TextView));
        /// <summary>
        /// The MouseHover event.
        /// </summary>
        public static readonly RoutedEvent MouseHoverEvent =
            EventManager.RegisterRoutedEvent("MouseHover", RoutingStrategy.Bubble,
                                             typeof(MouseEventHandler), typeof(TextView));

        /// <summary>
        /// The PreviewMouseHoverStopped event.
        /// </summary>
        public static readonly RoutedEvent PreviewMouseHoverStoppedEvent =
            EventManager.RegisterRoutedEvent("PreviewMouseHoverStopped", RoutingStrategy.Tunnel,
                                             typeof(MouseEventHandler), typeof(TextView));
        /// <summary>
        /// The MouseHoverStopped event.
        /// </summary>
        public static readonly RoutedEvent MouseHoverStoppedEvent =
            EventManager.RegisterRoutedEvent("MouseHoverStopped", RoutingStrategy.Bubble,
                                             typeof(MouseEventHandler), typeof(TextView));


        /// <summary>
        /// Occurs when the mouse has hovered over a fixed location for some time.
        /// </summary>
        public event MouseEventHandler PreviewMouseHover
        {
            add { AddHandler(PreviewMouseHoverEvent, value); }
            remove { RemoveHandler(PreviewMouseHoverEvent, value); }
        }

        /// <summary>
        /// Occurs when the mouse has hovered over a fixed location for some time.
        /// </summary>
        public event MouseEventHandler MouseHover
        {
            add { AddHandler(MouseHoverEvent, value); }
            remove { RemoveHandler(MouseHoverEvent, value); }
        }

        /// <summary>
        /// Occurs when the mouse had previously hovered but now started moving again.
        /// </summary>
        public event MouseEventHandler PreviewMouseHoverStopped
        {
            add { AddHandler(PreviewMouseHoverStoppedEvent, value); }
            remove { RemoveHandler(PreviewMouseHoverStoppedEvent, value); }
        }

        /// <summary>
        /// Occurs when the mouse had previously hovered but now started moving again.
        /// </summary>
        public event MouseEventHandler MouseHoverStopped
        {
            add { AddHandler(MouseHoverStoppedEvent, value); }
            remove { RemoveHandler(MouseHoverStoppedEvent, value); }
        }

        MouseHoverLogic hoverLogic;

        void RaiseHoverEventPair(MouseEventArgs e, RoutedEvent tunnelingEvent, RoutedEvent bubblingEvent)
        {
            var mouseDevice = e.MouseDevice;
            var stylusDevice = e.StylusDevice;
            int inputTime = Environment.TickCount;
            var args1 = new MouseEventArgs(mouseDevice, inputTime, stylusDevice)
            {
                RoutedEvent = tunnelingEvent,
                Source = this
            };
            RaiseEvent(args1);
            var args2 = new MouseEventArgs(mouseDevice, inputTime, stylusDevice)
            {
                RoutedEvent = bubblingEvent,
                Source = this,
                Handled = args1.Handled
            };
            RaiseEvent(args2);
        }
        #endregion

        /// <summary>
        /// Collapses lines for the purpose of scrolling. <see cref="DocumentLine"/>s marked as collapsed will be hidden
        /// and not used to start the generation of a <see cref="VisualLine"/>.
        /// </summary>
        /// <remarks>
        /// This method is meant for <see cref="VisualLineElementGenerator"/>s that cause <see cref="VisualLine"/>s to span
        /// multiple <see cref="DocumentLine"/>s. Do not call it without providing a corresponding
        /// <see cref="VisualLineElementGenerator"/>.
        /// If you want to create collapsible text sections, see <see cref="Folding.FoldingManager"/>.
        /// 
        /// Note that if you want a VisualLineElement to span from line N to line M, then you need to collapse only the lines
        /// N+1 to M. Do not collapse line N itself.
        /// 
        /// When you no longer need the section to be collapsed, call <see cref="CollapsedLineSection.Uncollapse()"/> on the
        /// <see cref="CollapsedLineSection"/> returned from this method.
        /// </remarks>
        public CollapsedLineSection CollapseLines(DocumentLine start, DocumentLine end)
        {
            VerifyAccess();
            if (heightTree == null)
                throw ThrowUtil.NoDocumentAssigned();
            return heightTree.CollapseText(start, end);
        }

        /// <summary>
        /// Gets the height of the document.
        /// </summary>
        public double DocumentHeight
        {
            get
            {
                // return 0 if there is no document = no heightTree
                return heightTree != null ? heightTree.TotalHeight : 0;
            }
        }

        /// <summary>
        /// Gets the document line at the specified visual position.
        /// </summary>
        public DocumentLine GetDocumentLineByVisualTop(double visualTop)
        {
            VerifyAccess();
            if (heightTree == null)
                throw ThrowUtil.NoDocumentAssigned();
            return heightTree.GetLineByVisualPosition(visualTop);
        }

        /// <inheritdoc/>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (TextFormatterFactory.PropertyChangeAffectsTextFormatter(e.Property))
            {
                // first, create the new text formatter:
                RecreateTextFormatter();
                // changing text formatter requires recreating the cached elements
                RecreateCachedElements();
                // and we need to re-measure the font metrics:
                InvalidateDefaultTextMetrics();
            }
            else if (e.Property == Control.ForegroundProperty
                     || e.Property == TextView.NonPrintableCharacterBrushProperty
                     || e.Property == TextView.LinkTextBackgroundBrushProperty
                     || e.Property == TextView.LinkTextForegroundBrushProperty
                     || e.Property == TextView.LinkTextUnderlineProperty)
            {
                // changing brushes requires recreating the cached elements
                RecreateCachedElements();
                Redraw();
            }
            if (e.Property == Control.FontFamilyProperty
                || e.Property == Control.FontSizeProperty
                || e.Property == Control.FontStretchProperty
                || e.Property == Control.FontStyleProperty
                || e.Property == Control.FontWeightProperty)
            {
                // changing font properties requires recreating cached elements
                RecreateCachedElements();
                // and we need to re-measure the font metrics:
                InvalidateDefaultTextMetrics();
                Redraw();
            }
            if (e.Property == ColumnRulerPenProperty)
            {
                columnRulerRenderer.SetRuler(this.Options.ColumnRulerPosition, this.ColumnRulerPen);
            }
            if (e.Property == CurrentLineBorderProperty)
            {
                currentLineHighlighRenderer.BorderPen = this.CurrentLineBorder;
            }
            if (e.Property == CurrentLineBackgroundProperty)
            {
                currentLineHighlighRenderer.BackgroundBrush = this.CurrentLineBackground;
            }
        }

        /// <summary>
        /// The pen used to draw the column ruler.
        /// <seealso cref="TextEditorOptions.ShowColumnRuler"/>
        /// </summary>
        public static readonly DependencyProperty ColumnRulerPenProperty =
            DependencyProperty.Register("ColumnRulerBrush", typeof(Pen), typeof(TextView),
                                        new FrameworkPropertyMetadata(CreateFrozenPen(Brushes.LightGray)));

        static Pen CreateFrozenPen(SolidColorBrush brush)
        {
            Pen pen = new Pen(brush, 1);
            pen.Freeze();
            return pen;
        }

        /// <summary>
        /// Gets/Sets the pen used to draw the column ruler.
        /// <seealso cref="TextEditorOptions.ShowColumnRuler"/>
        /// </summary>
        public Pen ColumnRulerPen
        {
            get { return (Pen)GetValue(ColumnRulerPenProperty); }
            set { SetValue(ColumnRulerPenProperty, value); }
        }

        /// <summary>
        /// The <see cref="CurrentLineBackground"/> property.
        /// </summary>
        public static readonly DependencyProperty CurrentLineBackgroundProperty =
            DependencyProperty.Register("CurrentLineBackground", typeof(Brush), typeof(TextView));

        /// <summary>
        /// Gets/Sets the background brush used by current line highlighter.
        /// </summary>
        public Brush CurrentLineBackground
        {
            get { return (Brush)GetValue(CurrentLineBackgroundProperty); }
            set { SetValue(CurrentLineBackgroundProperty, value); }
        }

        /// <summary>
        /// The <see cref="CurrentLineBorder"/> property.
        /// </summary>
        public static readonly DependencyProperty CurrentLineBorderProperty =
            DependencyProperty.Register("CurrentLineBorder", typeof(Pen), typeof(TextView));

        /// <summary>
        /// Gets/Sets the background brush used for the current line.
        /// </summary>
        public Pen CurrentLineBorder
        {
            get { return (Pen)GetValue(CurrentLineBorderProperty); }
            set { SetValue(CurrentLineBorderProperty, value); }
        }

        /// <summary>
        /// Gets/Sets highlighted line number.
        /// </summary>
        public int HighlightedLine
        {
            get { return this.currentLineHighlighRenderer.Line; }
            set { this.currentLineHighlighRenderer.Line = value; }
        }
    }
}