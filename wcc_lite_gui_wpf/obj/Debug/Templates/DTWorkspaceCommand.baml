   M S B A M L   `   `   `  ����E  Aw3tools.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:w3tools.UI.Templatesw3tools.UI.Templates  5      3clr-namespace:w3tools.UI.Viewsw3tools.UI.Views  5      -    'w3tools.UI.Templates.DTWorkspaceCommand   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:w3tools.UI.Templates     5      X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      *Viewsclr-namespace:w3tools.UI.Views  5      .��5      �� 
����d�$��2q�6*   ._�5   
   � $��5,5,5,5q�6     � RowDefinitions  5      �� 5      $��20 �6    �� 5      $��* �6    �� 5      $��20 �6    .+�5      � $��1,1,1,1q�6    � ColumnDefinitions 5      �� 5      $
��auto �6'   �� 5      $
��auto �6'   �� 5      $��* �6'   �� 5      $
��auto �6'   .+�5       �� -   5         ��Name$ _expandIcon��5    (   $��0��6   $W��Q/w3tools.UI;component/Resources/Icons/CollapseChevronDown_bold_blueNoHalo_16x.png��6=   $��0,0,0,0q�6�   5#      �� 5$      $��1��6   $��10,0,0,0q�6A   ��6(   �� *Image+6A   �� 5%      $��2��6   $
��Bold"�6B   $��10,0,0,0q�6T   ��6,   �� *Name+6T   �� 5&      -   5        $��3��5&      $9��3/w3tools.UI;component/Resources/Icons/Close_16x.png��6(   $��0,0,0,0q�6e    � DataContext 5*      ��  �� Mode$ OneWay=� �� RelativeSource �� *FindAncestor+ �� AncestorType#   w3tools.UI.Views.DocumentView"  5+      1  +Xceed.Wpf.Toolkit.PropertyGrid.PropertyGrid  5.      -   5          Name$ _propertyGridMain��5/      $��1��5.       $	��100��52      $	��200��53         ShowSearchBox .  	   ShowSortOptions	 .  
   ShowSummary
 .     	ShowTitle .  $��1,5,1,1q�56      $��3q�57         SelectedObject 50      �� y�51      �� *PropertyGridVisibility+57      � 5<      $��1,1,1,1q�6   $��2��6$    5>      �� 5?      $��50 �6'   �� 5@      $
��auto �6'   �� 5A      $��20 �6'   �� 5B      $
��auto �6'   �� 5C      $��* �6'   .+�5F      �� $��1��6   $��Results��6)   
������ 
���   �
������ $
��Bold"�5K      ��5I      �� *ResultsCommand+5K      �� 5L      $��3��6   $��Options��6)   
������ 
���   �
������ $
��Bold"�5Q      ��5O      �� *ResultsCommand+5Q      5S      5U      5W      5X      