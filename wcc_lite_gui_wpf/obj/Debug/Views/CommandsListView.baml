   M S B A M L   `   `   `  ����E  Aw3tools.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:w3tools.UI.Viewsw3tools.UI.Views  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R8clr-namespace:System.ComponentModel;assembly=WindowsBaseSystem.ComponentModel 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35X@clr-namespace:System.Windows.Data;assembly=PresentationFrameworkSystem.Windows.Data 5      '    !w3tools.UI.Views.CommandsListView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:w3tools.UI.Views     5      Hscm8clr-namespace:System.ComponentModel;assembly=WindowsBase     5      Pdat@clr-namespace:System.Windows.Data;assembly=PresentationFramework     5         MaterialDesignBody#���   5        MaterialDesignPaper#���  5        � 	Resources  5        	BoolToVis 
 cvs_cs �� IsLiveGroupingRequested �� IsLiveFilteringRequested �� IsLiveSortingRequested �� Source �� Mode �� SortDescriptions+  %System.ComponentModel.SortDescription   PropertyName �� GroupDescriptions2  ,System.Windows.Data.PropertyGroupDescription	   PropertyName  ContainerStyle
 �� 
TargetType �� Property �� Value 9� 
IsExpanded��%�  &	       &	      &	 �     �� 5   
   6'   �� 5   
    .  .  .  5      �� *	Toolbox+$ TwoWay=� 5        5      $ Category��6&     5       $
 Name��6&    5$        5%      $	 Category��6/   5(      �� 5*   
   "
 ��6'   .�5+      ��  � �� 5,      �� 5-      .R�5.      9�  . ��6#   �� *Name+.��5/      �� 50      51      53      54      58      .��5:      �  � RowDefinitions 5;   
   �� 5<      $
��auto �6   �� 5=      $��* �6   .+�5A   
   � $��0��6    � ColumnDefinitions 5B      �� 5D      $��* �6#   �� 5F      $
��auto �6#   .+�5J      �� $��0��6   ��6&   �� *
SearchText+$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=��� 5M      $��1��6   ��6%   �� *ClearCommand+.��5N      �� $��16��6:   ��6   �� *SearchBoxImage+6:   5O      5P      �� 5S   
   -   5         ��Name$ _commandsListView��5S       $��1��6   ��5T      ��  �� Source# [ $ PropertyChanged=� �� SelectedItem 5U      �� *ActiveCommand+ J� InputBindings 5X      d� 5Y       d� Gesture$ LeftDoubleClicka�6    �� Command 69   �� *
AddCommand+ � ContextMenu 5]      �� 5^      .@�5_      m� $��Add Command to Workflow��6    m� Command 5`      �� *
AddCommand+ m� CommandParameter 5a      ��  �� RelativeSource ��  �� AncestorType$ ContextMenuL� �� Path$" PlacementTarget.SelectedItem�5b      5d       �� 
GroupStyle 5h      (  "System.Windows.Controls.GroupStyle  5i         ContainerStyle# [ 6   ��5n      �� 5o        MaterialDesignListBoxItem �� BasedOn# [ 6   "
 ��6M   .�5p      ��  � y� 63   �� *	IsVisible+  �� 	Converter#  [ 5q      5u      5v      5w      