   M S B A M L   `   `   `  ����E  Aw3tools.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:w3tools.UI.Views.Dialogsw3tools.UI.Views.Dialogs  5      7    1w3tools.UI.Views.Dialogs.UnsavedChangesDialogView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:w3tools.UI.Views.Dialogs     5      $	��400��5      
�������.��5
      �   � RowDefinitions  5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��10q�6   ._�5      �� $
��Wrap=�6   ��6,   �� *	Message+5      � 5   
   $
��10 0q�6   $	��200��6   $��1��6*   .+�5      ��  �� SelectionMode$ Single=�6   
���������6A   �� *	Documents+��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6%   .+�5      �� $��16��6$   $��16��6/   $��0 0 5 0q�6;   ��6L   �� *
IconSource+�� 5      ��6(   �� *
FilePath+5      5      ��5      �� 5        �� 
TargetType" ��6   .�5!      ��  �� Property �@��
IsSelected �� Value 67   �� *
IsSelected+5"      5$      5%      �� 5'   
   $��10q�6   $��Right=�6!   $ 
Horizontal=�6=   $��2��6V    � 	Resources 5(      ��%�   '��       �� 5)      " ��6   .�5*      ��  � ��$ 75��6/   �� 5+       � ��$ 25��6/   �� 5,       � ��$ 10 0 0 0q�6/   5-      5.      .+�5/      �� $	��Yes��67   ��6   �� *
YesCommand+67   �� 50      $��No��67   ��6   �� *	NoCommand+67   �� 51      $��Cancel��67   ��6   �� *CancelCommand+67   52      54      55      