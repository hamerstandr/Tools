   M S B A M L   `   `   `  ����C  ?Calendar, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:CalendarCalendar  5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35q mTelerik.Windows.Controls.ImageEditor, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7r nTelerik.Windows.Controls.ScheduleView, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.RibbonView, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7f bTelerik.Windows.Documents, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7m iTelerik.Windows.Controls.Docking, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7a	 ]Telerik.Windows.Data, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e
 aTelerik.Windows.Controls, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7p lTelerik.Windows.Controls.Navigation, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Documents.Core, Version=2019.2.612.40, Culture=neutral, PublicKeyToken=5803cfa389c90ce7s oTelerik.Windows.Controls.RichTextBoxUI, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'    !Calendar.CategoryToBrushConverter   CategoryToBrushConverter  RedCategoryBrush  �� Color  GreenCategoryBrush  BlueCategoryBrush  FreeMarkerBrush  BusyMarkerBrush  TentativeMarkerBrush  OutOfOfficeMarkerBrush  CategoryHeaderTemplate 	 TimeMarkerHeaderTemplate �� 
TargetType 
 CalendarFooterSlider �� Property �� Value: 
 4Telerik.Windows.Controls.Office2013ResourceExtension� 
 �Telerik.Windows.Controls.ThemeResourceExtension`1[[Telerik.Windows.Controls.Office2013ResourceKey, Telerik.Windows.Controls, Version=2019.2.618.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7]]   ResourceKeyA 
 ;Telerik.Windows.Controls.Office2013ResourceKeyTypeConverter9   3Calendar.OrientedGroupHeaderContentTemplateSelector & "GroupHeaderContentTemplateSelector   HorizontalTemplate   VerticalTemplate@ 
 :Telerik.Windows.Controls.Primitives.LayoutTransformControl   LayoutTransform �� Angle!	   HorizontalResourceTemplate
 �� 	Converter   VerticalResourceTemplate�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    Stelerik1http://schemas.telerik.com/2008/xaml/presentation      	 
     5       localclr-namespace:Calendar  5      :x,http://schemas.microsoft.com/winfx/2006/xaml     5      %   &	        &	      &	 0     &	 O     &	 m     &	 �     &	 �     &	 �     &	 �     &		 ?    &	
 �    &	 d    7   7      5      6%   �� 5      $	  Red��6/   �� 5	      $  Green��61   �� 5
      $
  Blue��60   �� 5      $  	#FF309B46��6.   �� 5      $  	#FFE61E26��6.   �� 5      $  	#FF41229B��63   �� 5      $  	#FFF1C700��65   �� 5      .R�5   
   �� ��6   �� *DisplayName+5      �� 5      .R�5   
   �� ��6   �� *TimeMarkerName+5      �� 5      " ��6   .�5   
   ��  � ��$ 33��6#   �� 5   
    � �� 6'     $ AccentBrush �� 5   
    � �� 6(     $ 
BasicBrush �� 5   
    � ��$ 0 1 0 0q�6,   5        5        5"   
   �� 5#      .R�5$      �� $��16��6E   $��4q�6Q   ��6#   �� *FormattedName+6Q   5%       5'   
   �� 5(      .R�5)        $	��Top=�61    5*      �� 5+      $	 -90Q�6*   .��5-      �� $��4q�6I   $��16��6T   ��6'   �� *FormattedName+6T   5.      5/      	 51   
   �� 52      .R�53      � ��6   �� *FormattedName+8
   .+�54      �� $
��Bold"�6?   $��6 5 0 0q�6Q   ��6    �� *FormattedName+6Q   55      56       58   
   �� 59      .R�5:      � ��6   �� *FormattedName+8
  .+�5;        $��Center=�65    5<      �� 5=      $	 -90Q�6.   .��5?      �� $��4q�6C   $
��Bold"�6N   ��6$   �� *FormattedName+6N   5@      5A      5B      5D      5E      