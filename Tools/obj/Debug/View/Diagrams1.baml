   M S B A M L   `   `   `  ����@  <Tools, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Tools.View
Tools.View  5      L HQuickStart.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulltLclr-namespace:Telerik.Windows.Controls.QuickStart;assembly=QuickStart.Common#Telerik.Windows.Controls.QuickStart 5      c6clr-namespace:Telerik.Windows.Examples.Diagrams.Common(Telerik.Windows.Examples.Diagrams.Common  5
      n jTelerik.Windows.Controls.Diagrams, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7�eclr-namespace:Telerik.Windows.Controls.Diagrams.Primitives;assembly=Telerik.Windows.Controls.Diagrams,Telerik.Windows.Controls.Diagrams.Primitives 5      j fTelerik.Windows.Diagrams.Core, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7tRclr-namespace:Telerik.Windows.Diagrams.Core;assembly=Telerik.Windows.Diagrams.CoreTelerik.Windows.Diagrams.Core 5          Tools.View.Diagrams1   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      *localclr-namespace:Tools.View     5      k gTelerik.Windows.Documents.Core, Version=2019.2.612.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7m	 iTelerik.Windows.Controls.Docking, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e
 aTelerik.Windows.Controls, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.Expressions, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7| xTelerik.Windows.Controls.FixedDocumentViewersUI, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Documents, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.ImageEditor, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7z vTelerik.Windows.Controls.FixedDocumentViewers, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.FileDialogs, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7y uTelerik.Windows.Controls.Diagrams.Extensions, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7]telerik1http://schemas.telerik.com/2008/xaml/presentation  	 
              5      �telerikQuickStartLclr-namespace:Telerik.Windows.Controls.QuickStart;assembly=QuickStart.Common  	 
              5      acommon6clr-namespace:Telerik.Windows.Examples.Diagrams.Common  	 
              5
      �
primitiveseclr-namespace:Telerik.Windows.Controls.Diagrams.Primitives;assembly=Telerik.Windows.Controls.Diagrams  	 
              5      �declarativesRclr-namespace:Telerik.Windows.Diagrams.Core;assembly=Telerik.Windows.Diagrams.Core  	 
              5        � 	Resources  5       �� MergedDictionaries �� Source* 
 $Telerik.Windows.Controls.IconSources   	IconPaths   LightBasePath   DarkBasePath�� 5   
    5      �� 5      $' !Diagrams/ToolboxItemTemplate.xaml?�6%   �� 5      $ Diagrams/Resources.xaml?�6%   %�   &	          5      $2 ,/Tools;component/View/Diagrams/Images/Light/��64   $1 +/Tools;component/View/Diagrams/Images/Dark/��5   4   5      .��5      � -   5         �Name$ 
LayoutRoot��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   ,  &Telerik.Windows.Controls.RadRibbonView  -   5          Name$ Ribbon��5          TitleBarVisibility$ 	Collapsed=�60   	   MinimizeButtonVisibility$	 Visible=�5    A   !
   QuickAccessToolBarPosition$
 	NotHosted=�5!      7  1Telerik.Windows.Controls.RibbonView.KeyTipService   IsKeyTipsEnabled .    BackstageClippingElement 5       ��  �� ElementName$ 
LayoutRoot��   ApplicationButtonContent 5"      �� 5#      $
��Menu��6   $
��10 2q�6(      	Backstage 5%      1  +Telerik.Windows.Controls.RadRibbonBackstage  5&      .@�5'      5  /Telerik.Windows.Controls.RadRibbonBackstageItem  $��Simple��65      	IsDefault . .��5(      �� -   5         ��Name$ SamplesList��5(   "   $��0q�67   $��Center=�6K   $��Center=�5)   	     SamplesTemplate#��[ 6$     UniformGridPanelTemplate#��[ 5*   	   ��6@   �� *	Samples+ �� SelectedItem 5,   	   �� *SelectedSample+ �� Mode$ TwoWay=�5-      5.      .@�50      +  %Telerik.Windows.Controls.RadRibbonTab  $
��Home��6#      
IsSelected .    
AccessText$ 1��6C   .@�51      -  'Telerik.Windows.Controls.RadRibbonGroup  $��General��6)   (  "Telerik.Windows.Controls.ScreenTip   Description$ Generic actions.��6:      Title$ General��52      .@�53      .	  (Telerik.Windows.Controls.RadRibbonButton	  -   5         	 Name$ 	NewButton��53   .   $#��telerik:DiagramCommands.Clear��6A    	  Size$ Large=�55   m    	  Text$	 New��6z   ��54      �� $ diagram�� 	  
LargeImage 55      4
 
 .Telerik.Windows.Controls.IconResourceExtension
   
  IconRelativePath$ new.png�� 
  IconSources# [  6z   	  56      -   5        $ 
OpenButton��56   .   $"��telerik:DiagramCommands.Open��58   .   $ Large=�5:   n   $
 Open��6{   ��59      �� $
 root�� 5:      
  $ open.png��# [  6{   	  5;      -   5        $ 
SaveButton��5;   .   $"��telerik:DiagramCommands.Save��5=   .   $ Large=�5?   n   $
 Save��6{   ��5>      �� $
 root�� 5?      
  $ save.png��# [  6{   2  ,Telerik.Windows.Controls.RadCollapsiblePanel  5@      .+�5A      3  -Telerik.Windows.Controls.RadRibbonSplitButton  - 
 'Telerik.Windows.Controls.RadSplitButton   Command$" telerik:DiagramCommands.Undo��67      CollapseToSmall$ WhenGroupIsSmall=�5B   7       
LargeImage$  Diagrams/Images/Redo.png��5C   	   !   Size$! Medium=�6/   "   Text$
" Undo��6=   #   CommandTarget# 5B   	   �� $ diagram��$   DropDownContent$ 5D      �� 5E   "   -   5        $	��500��5E   *   $��0q�6:     UndoItemTemplate#��[ 5G      ��5E   N   �� *	UndoStack+5G      5I        5J      $" telerik:DiagramCommands.Redo��67   $ WhenGroupIsSmall=�5K   7   $  Diagrams/Images/Redo.png��5L   	   $! Medium=�6/   $
" Redo��6=   # 5K   	   �� $ diagram��$ 5M      �� 5N   "   -   5        $	��500��5N   *   $��0q�6:   #��[ 5P      ��5N   N   �� *	RedoStack+5P      5R      5S      5T        5U      $��	Clipboard��6)   $ FO��6<   $&  Show the Clipboard Task options.��5V      $ 	Clipboard��5W      .@�5X      	  -	   5        $ PasteButton��5X   .   $#��telerik:DiagramCommands.Paste��6C   $ Large=�5Z   o   % 	  
SmallImage$!% Diagrams/Images/paste16.png��5[      $ V��61   $* $Paste the contents of the Clipboard.��5\      $ Paste��5]      $ Paste��6(   ��5Y      �� $ diagram�� 5Z      
  $ 	paste.png��# [  5]   (     5^      .+�5_      	  -
   5        $ 	CutButton��5_   2   $!��telerik:DiagramCommands.Cut��6E   & 	  CollapseToSmall$& WhenGroupIsSmall=�5`   7   $% Diagrams/Images/cut.png��5a   	   $ X��6.   $F @Cut the selection from the document and put it on the Clipboard.��5b   	   $	 Cut��5c   	   $	 Cut��6'   ��5`   	   �� $ diagram��5c   '   	  5d      -   5        $ 
CopyButton��5d   2   $"��telerik:DiagramCommands.Copy��6F   $& WhenGroupIsSmall=�5e   7   $ C��5f   o   $5 /Copy the selection and put it on the Clipboard.��5g   	   $
 Copy��5h   	   $
 Copy��6(   ��5e   	   �� $ diagram��% 5f   	   
  $ copy.png��# [  5h   (   	  5i      -   5        $ DeleteButton��5i   2   $$��telerik:DiagramCommands.Delete��6H   $& WhenGroupIsSmall=�5j   7   $(% "Diagrams/Images/delete_disable.png��5k   	   $ D��5l   	   $! Deletes the selected items.��5m   	   $ Delete��5n   	   $ Delete��6*   ��5j   	   �� $ diagram��5n   *   5o      5p        5q      $��Tools��6)   $ TO��68   $ Show the Tools options.��5r      $ Tools��6?   .@�5s        .+�5t      3  -Telerik.Windows.Controls.RadRibbonRadioButton  -   5        '  Name$' 
TextButton��5t   7   (   
SmallImage$( Diagrams/Images/Text.png��5u   	   $ T��6/   $) #Changes the main tool to Text Tool.��5v   	   $
 Text��5w   	   )   Text$
) Text��6(     5x      -   5        $' 
PathButton��5x   7   $#( Diagrams/Images/path_tool.png��5y   	   $ P��5z   	   $) #Changes the main tool to Text Tool.��5{   	   $ 	Path Tool��5|   	   $
) Path��6-     5}      -   5        $' PencilButton��5}   7   $%( Diagrams/Images/pencil_tool.png��5~   	   $ P��5   	   $) #Changes the main tool to Text Tool.��5�   	   $ Pencil Tool��5�   	   $) Pencil��6/     5�      -   5        $' PointerButton��5�   7   * d� 	IsChecked$
* TrueY�6f   $!( Diagrams/Images/Pointer.png��5�   	   $ P��5�   	   $, &Changes the main tool to Pointer Tool.��5�   	   $ Point��5�   	   $) Pointer��6)     5�      -   5        $' ConnectionButton��5�   7   $ C��5�   	   $/ )Changes the main tool to Connection Tool.��5�   	   $ 
Connection��5�   	   $) 	Connector��6.   ( 5�   	   
  $ Connection.png��# [  5�   .   5�      5�        5�      $��Export��6)   .@�5�      	  $$��telerik:DiagramCommands.Export��6.   $	��Png��6W   $ Large=�5�   s   $ Ex��5�      $ Export to Image��6.   ��5�      �� $ diagram�� 5�      
  $ 32/export.png��# [  5�   .   	  5�      -   5        $ Large=�5�   w   $	 EHt��5�      $ Export to Html��6/    5�      
  $ 32/exportHtml.png��# [  5�   /   	  5�      $,��&telerik:DiagramExtensionCommands.Print��6.   $ Pr��5�   6   $ Large=�5�   r   $ Print��6   ��5�      �� $ diagram�� 5�      
  $ 32/print.png��# [  6   5�        5�      $��Grouping��6)   .@�5�      	  -   5        $ GroupButton��5�   .   $#��telerik:DiagramCommands.Group��6C   $ Diagrams/Images/Group.png��5�      $ Large=�6/   $ Gr��5�      $  Groups the selected items.��5�      $ Group��6C   $ Group��5�      ��5�      �� $ diagram��5�      	  5�      -   5        $ UngroupButton��5�   .   $%��telerik:DiagramCommands.Ungroup��6E   $! Diagrams/Images/Ungroup.png��5�      $ Large=�61   $	 Ugr��5�      $# Ungroups the selected groups.��5�      $ Ungroup��5�      $ Ungroup��6*   ��5�      �� $ diagram��5�   *   5�        5�      $��Arrangement��6)   $ AR��6>   $ Arrange��5�      .@�5�      	  -   5        $ BackwardButton��5�   .   $*��$telerik:DiagramCommands.SendBackward��6F   $+ %Diagrams/Images/32/bring_backward.png��5�      $ Large=�6;   $ BF��5�      $ Send Backward��6.   $ Send Backward��5�      ��5�      �� $ diagram��5�      	  5�      -   5        $ ForwardButton��5�   .   $*��$telerik:DiagramCommands.BringForward��6E   $* $Diagrams/Images/32/bring_Forward.png��5�      $ Large=�6:   $ BB��5�      $ Bring Forward��6.   $ Bring Forward��5�      ��5�      �� $ diagram��5�      	  5�      -   5        $ ToFrontButton��5�   .   $*��$telerik:DiagramCommands.BringToFront��6E   $&  Diagrams/Images/32/bring_top.png��5�      $ Large=�66   $ BF��5�      $( "Brings the selected item to front.��5�      $ Bring to Top��5�      $ Bring to Top��6/   ��5�      �� $ diagram��5�   /   	  5�      -   5        $ ToBackButton��5�   .   $(��"telerik:DiagramCommands.SendToBack��6D   $&  Diagrams/Images/32/send_back.png��5�      $ Large=�66   $ BB��5�      $' !Brings the selected item to back.��5�      $ Send to Back��5�      $ Send to Back��6/   ��5�      �� $ diagram��5�   /   5�        5�      $��	Alignment��6)   .@�5�      	  -   5        $ AlignLeftButton��5�   .   $#��telerik:DiagramCommands.Align��6G   $
��Left��5�      $# Diagrams/Images/32/align1.png��5�      $ Large=�63   $ 
Align Left��6@   ��5�       �� $ diagram��5�   @   	  5�      -   5        $ AlignBottomButton��5�   .   $#��telerik:DiagramCommands.Align��6I   $��Bottom��5�      $# Diagrams/Images/32/align3.png��5�      $ Large=�63   $ Align Bottom��6@   ��5�   "   �� $ diagram��5�   @   	  5�      -   5        $ AlignTopButton��5�   .   $#��telerik:DiagramCommands.Align��6F   $	��Top��5�      $# Diagrams/Images/32/align2.png��5�      $ Large=�63   $ 	Align Top��6@   ��5�      �� $ diagram��5�   @   	  5�      -   5        $ AlignRightButton��5�   .   $#��telerik:DiagramCommands.Align��6H   $��Right��5�      $# Diagrams/Images/32/align6.png��5�      $ Large=�63   $ Align Right��6@   ��5�   !   �� $ diagram��5�   @   5�        5�      $��Layout��6)   .@�5�      	  -   5        $ LayoutButton��5�   .   $# Diagrams/Images/32/layout.png��5�      $ Large=�63   $ L��5�      $* $Automatically organizes the diagram.��5�      $ Layout��5�      $ Layout��6)   5�      5�        5�      $��Settings��6#   .@�5�        $
��Page��6)   $
 Page��67   .@�5�      6  0Telerik.Windows.Controls.RadRibbonDropDownButton  -   5        +  Name$+ BackgroundColorButton��5�   6   $	��5,0q�5�   q   ,   Size$, Large=�6   -   Text$- Background Color��5�      .   
LargeImage. 5�      
  $ bgColor.png��# [  0 
 *Telerik.Windows.Controls.RadDropDownButton/   DropDownContent/ 5�      /  )Telerik.Windows.Controls.RadColorSelector  5�      -   5        0   MainPalette$0 Metro=�5�   7    1   StandardPaletteVisibility$1 	Collapsed=�6K   2   MainPaletteHeaderText$2 Metro Palette��5�   
   3   NoColorVisibility$3 	Collapsed=�60   5�   
   5�      5�        5�      $
��Grid��6)   $
 Grid��67   .@�5�      4  .Telerik.Windows.Controls.RadRibbonToggleButton  -    5        4  Name$4 ShowGridToggle��5�   4   $2��,telerik:DiagramCommands.SwitchGridVisibility��5�      $
* TrueY�5�   6   5   Size$5 Large=�5�   r   6   Text$6 	Show Grid��5�   r   ��5�      �� $ diagram��7   
LargeImage7 5�      
  $ showGrid.png��# [  8   
SmallImage8 5�      
  $ showGrid.png��# [  6r     5�      -!   5        $+ GridColorButton��5�   6   $, Large=�5�   s   $- 
Grid Color��6�   . 6   
  $ GridColor.png��# [  / 5�        5�      -"   5        $0 Metro=�5�   7   $1 	Collapsed=�6K   $2 Metro Palette��5�   
   $3 	Collapsed=�60   5�   
   5�      � 5�       5�      �� 5�      �� 5�      9 � ColumnDefinitions9 5�      �� 5�      �� 5�      .+�5�      �� $��0��6$   $��0��61   $��0 0 5 0q�6A   $��Center=�6R   $��
Cell Width��5�   	   /  )Telerik.Windows.Controls.RadNumericUpDown  5�      -#   5        :  Name$: CellWidthSpinner��5�   3   $��0��6M   $��1��6Z   $
��Left=�5�   	   $��Center=�6$   ;   
IsEditable; .  + 
 %Telerik.Windows.Controls.RadRangeBase<   Maximum$	< 100Q�6R   =   Minimum$= 5Q�5�   	   >   NumberDecimalDigits$> 0��6   ?   Value$? 20X�5�   	   �� 5      $��1��6$   $��0��61   $��0 0 5 0q�6A   $��Center=�6R   $��Cell Height��5  	     5     -$   5        $: CellHeightSpinner��5  3   $��1��6N   $��1��6[   $
��Left=�5  	   $��Center=�6$   ; .  $	< 100Q�6R   $= 5Q�5  	   $> 0��6   $? 20X�5  	   5       5     $5 Large=�5	  {   $6 Snap to Items��5
     ��5  4   �� $ diagram��* 5     �� *IsSnapToItemsEnabled+$ diagram��$ TwoWay=�7 5	     
  $ Snap_to_items_big.png��# [  5
       5     $5 Large=�5  z   $6 Snap to Grid��5     ��5  4   �� $ diagram��* 5     �� *IsSnapToGridEnabled+$ diagram��$ TwoWay=�7 5     
  $ Snap_to_grid_big.png��# [  5     � 5      5     �� 5     �� 5     9 5     �� 5     �� 5     .+�5     �� $��0��6$   $��0��61   $��0 0 5 0q�6A   $��Center=�6R   $��Snap X��5  	     5     $��0��63   $��1��6@   $
��Left=�6P   $��Center=�5  	   ; .  $	< 100Q�67   $= 5Q�6E   $> 0��5  	   ? 6!   �� $ diagram��@ �� Path$@ SnapX�$ TwoWay=��� 5     $��1��6$   $��0��61   $��0 0 5 0q�6A   $��Center=�6R   $��Snap Y��5  	     5     $��1��63   $��1��6@   $
��Left=�6P   $��Center=�5   	   ; .  $	< 100Q�67   $= 5Q�6E   $> 0��5!  	   ? 6!   �� $ diagram��$@ SnapY�$ TwoWay=�5"     5#       5$     $
��Zoom��6)   $
 Zoom��67   .@�5%     � $��Right=�6   $��Center=�67    5&     �� 5'     $	��.5* �6,   �� 5(     $	��.5* �6,   .+�5*     �� A �� Orientation$A 
Horizontal=�6%   .+�5+     �� $��0,0,5,0q�6(   $��Center=�69   $
��Zoom��6T     5,     -%   5        $: ZoomSpinner��5,  7   $
��Left=�6L   $��Center=�5-  
   ; .  $	< 500Q�68   $= 10Q�6F   $> 0��5.  
   ? 5/  
   �� *Zoom+$ TwoWay=�  PercentConverterB �� 	Converter#B [ $ diagram��50     	  51     -&   5        $ FitToWindowButton��51  2   $��1��6M   $��0,5,0,0q�6Z   $%��telerik:DiagramCommands.AutoFit��52  	   $ Fit to Window��53  v   ��52  3   �� $ diagram��% 53  	   
  $ zoom_to_fit.png��# [  6v   54     55       56     $��
Properties��6)   .@�57       .+�58       $(8 "Diagrams/Images/Rotate_Enabled.png��5:  	   $6 Rotation��69   * 59  	   �� *IsRotationEnabled+$ diagram��$ TwoWay=�5:  9     5;     $+8 %Diagrams/Images/Transform_Enabled.png��5=  	   $6 Resizing��6<   * 5<  	   �� *IsResizingEnabled+$ diagram��$ TwoWay=�5=  <     5>     $ 8 Diagrams/Images/cursor.png��5@  	   $6 Dragging��61   * 5?  	   �� *IsDraggingEnabled+$ diagram��$ TwoWay=�5@  1   0  *Telerik.Windows.Controls.RadRibbonComboBox  5A     -'   5        C  Name$C SelectionModeCombo��5A  4   D �� SelectedIndex$D 1��6P   $ Selection Mode��5B  	   .@�5C     1  +Telerik.Windows.Diagrams.Core.SelectionMode 
Single=�6A    5D     Extended=�6C    5E     Multiple=�6C    5F     None=�6?   5G     5H     5I     5J     5K     � 5L  
   $��1��6   
�������$��0 4 0 0q�6/   9 5M     �� 5N     �� 5O     $
��Auto �6#   .+�5Q     D  >Telerik.Windows.Controls.Diagrams.Extensions.RadDiagramToolbox  -(   5        E  Name$E toolBox��5Q  (   $��1��69   $��5 0 0 0q�6I   F   Title$F Gallery��6[   G   Header$G ToolBox��6k   H   IsOpenH .  6|   )  #Telerik.Windows.Controls.RadDiagram  5R     -)   5        I  Name$I diagram��5R  !   $	��500��5S  	   $��1q�5T  	     RadDiagramStyle#��[ 5U  	   J   SelectionModeJ 5V  	   �� *SelectedItem+$ SelectionModeCombo��$ TwoWay=�I  CTelerik.Windows.Controls.Diagrams.Primitives.ItemInformationAdornerK   AdditionalContentK 5W     ?  9Telerik.Windows.Controls.Diagrams.Extensions.SettingsPane  5X     C  =Telerik.Windows.Controls.Diagrams.Extensions.SettingsPaneBaseL   DiagramL 6+   �� $ diagram��5Z     5[     5]     4  .Telerik.Windows.Controls.QuickStart.QuickStartM   ConfigurationPanelM 5^     �� 5_  
   .+�5`     �� $��	Samples: ��6   $��2 0 0 2q�6)   ) 
 #Telerik.Windows.Controls.RadListBox  5a     -*   5        N  Name$N SamplesList1��5a  !   
�������$	��2 0q�5b  ;   ��5a  7   �� *	Samples+> 
 8Telerik.Windows.Controls.Primitives.ItemsControlSelectorO   SelectedItemO 5b     �� *SelectedSample+$ TwoWay=���5c     �� 5d     .R�5e     �� $	��5 2q�6;   ��6$   �� *Title+6;   5f     5h     5i     5k     