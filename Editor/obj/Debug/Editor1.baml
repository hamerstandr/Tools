   M S B A M L   `   `   `  ����A  =Editor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:EditorEditor  5          Editor.Editor1   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:Editor     5      q mTelerik.Windows.Controls.ImageEditor, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7m iTelerik.Windows.Controls.Docking, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a ]Telerik.Windows.Data, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7q mTelerik.Windows.Controls.FileDialogs, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k	 gTelerik.Windows.Controls.Input, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e
 aTelerik.Windows.Controls, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Mtelerik1http://schemas.telerik.com/2008/xaml/presentation     	 
   5        J� CommandBindings  5
      )  #System.Windows.Input.CommandBinding  5   
   -   5           Editor.Editor1   	MyCommand   Command# Z 5      6F    � 	Resources 5         
HeaderPane �� Orientation �� Mode$ 
 Telerik.Windows.Controls.Label( 
 "Telerik.Windows.Controls.RadButton  Name   InnerCornerRadius F�Name��%^  &	        �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5      �� $��*��6   
���  ��y�6%   �� *Change+$ TwoWay=�6b     5      ��6    �� *Header+  5      $ CloseButton��6%   ��.  $
��5,.5q�6L   $��23��6Z   $��23��6f   $ 0��5   
   ��5   	   �� *CloseCommand+.��5      F� $ ForegroundIcon��6   �����        р�     р� @B ���- @B ���- �� �@KL �� �@KL @B р�[ @B р�[     � z     � z @B ���j @B ���j �� р�[ �� р�[ ��- �@KL ��- �@KL  	= р�[  	= р�[ @KL р�[ @KL ���j @KL ���j ��[ � z ��[ � z ��j р�[ ��j р�[ ��[ �@KL ��[ �@KL @KL ���- @KL ���- ��[ р� ��[ р� ��j �    ��j �    ��[ �@B ��[ �@B @KL р� @KL р�  	= ���-  	= ���- ��- р� ��- р� �� �@B �� �@B @B �    @B 	
���   �$��7��6�   $	��0,1q�6  $��8��6  5>      5?      5A      5F      .��5G      � 	 � RowDefinitions	 5H   
   �� 5I      $��* �6   �� 5J      $
��Auto �6   .+�5L   
   )  #Telerik.Windows.Controls.RadDocking  $��0��6   .@�5M      0  *Telerik.Windows.Controls.RadSplitContainer  
   InitialPosition$
 
DockedLeft=�6(   .@�5N      +  %Telerik.Windows.Controls.RadPaneGroup  $��Stretch=�6(   $��Stretch=�6F   + 
 %Telerik.Windows.Controls.StyleManager   Theme-	 
 'Telerik.Windows.Controls.ThemeConverter$ Fluent	 6b   .@�5O      &
   Telerik.Windows.Controls.RadPane
  $��Solution Explorer��6&    
  CanUserClose .   
  CanDockInDocumentHost .   
  CanFloat .  .��5P      � $��Stretch=�6   $��Stretch=�6=   	 5Q      �� 5R   "   $
��Auto �60   �� 5S   "   $��* �60   .+�5U        -   5        $��0��5U   0   $��10q�6=   $��35��6I   $��Open Folder��6U   $ Fluent	 6�   *  $Telerik.Windows.Controls.RadTreeView  5W      -   5          Name$ treeView��5W   2   $��1��6D      IsLoadOnDemandEnabled . $ Fluent	 5[   W   5^      5_      5`      5a         DocumentHost 5b        5c      .@�5d        -   5          Name$ 
PaneGroup1��5d   +   $ Fluent	 6@   5k      5l      5o      �� 5p   
   $��1��6   $��24��6"   $ 
Horizontal=�6.   ���	#ffca5100.+�5q      - 
 'Telerik.Windows.Controls.RadProgressBar  -   5        $��ProgressBar1��5q   %   $��16��69   $	��100��6E   $��	10,0,10,0q�6Q   5r      5s      5t      