   M S B A M L   `   `   `  ����E  Aw3tools.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:w3tools.UI.Views.Dialogsw3tools.UI.Views.Dialogs  5      2    ,w3tools.UI.Views.Dialogs.ExceptionDialogView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:w3tools.UI.Views.Dialogs     5      
�������$	��500��5	        � 	Resources  5
         exceptionIcon L� Drawing � Brush � Geometry��%>  &	        L� 5   
    5      I� 5      ��5      �  �	#00FFFFFF+ �� $�  $� �     $� �        � $�     6<   � 5       �	#FFF6F6F6+ ��     $� � $�  $� � $�     �        6<   � 5       �	#FFEFEFF0s ��@T� @T� ���j @T� ���j  	= �@T�  	= �@T�  � ���j  � ���j ��� �@T� ��� ��� ��� р�� ��� р�� �� р� �� 6<   � 5       �	#FF424242� ��@T� ��� ���j ��� ���j  � �@T�  � �@T�  	= ���j  	= ���j @T� �@T� @T� ��� ��� р�� ��� р�� �� р� �� ���� ��� �@B ��� �@B @B ���� @B 6<   5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   .+�5"   
   � $��0��6   $��10q�6    � ColumnDefinitions 5#      �� 5$      $
��auto �6#   �� 5%      $��* �6#   .+�5(      �� $��0��5)      $��40��5*      $��40��5+      $��Center=�5,      $��0 0 10 0q�5-      #��[  5.      �� 50      $��1��6   .+�51      �� $
��Wrap=�6   ��60   �� *	Message+�� 52      $
��Wrap=�6   $��0 5 0 0q�60   ��6A   �� *Exception.Message+53      55      �� 57   
   $	��0.5��6   $��
10 0 10 10q�6!   $��1��65   9� 59   
   $��Expand for more details��6   $��2��64   $
��10 0q�6A   .��5:      �� $��0 10 0 0q�6   $��1q�6'   ���	#FFABADB3._�5;      �� $	��200��6   $
��Auto=�6,   $
��Auto=�6Q   .��5<      �� $��0q�5=       �� IsReadOnlyCaretVisible .   �� 
IsReadOnly . ��5<      �� *ExceptionString+ �� Mode$ OneWay=�5?      5@      5A      5B      �� 5D   
   $	��0.5��6   $��
10 10 10 0q�6!   $��3��65   �� 5F   
   $��4��6   $��10q�6"   	 �� Orientation$	 
Horizontal=�6.   $��Right=�6G     5G      
 �� 
TargetType �� Property �� Value��%�   '��       �� 5H      "
 ��6   .�5I      ��  � ��$ 75��6/   �� 5J       � ��$ 25��6/   �� 5K       � ��$ 10 0 0 0q�6/   5L      5M      .+�5N      �� $��Report��69   ��6   �� *ReportCommand+69   �� 5O      $
��Quit��69   ��6   �� *QuitCommand+69   �� 5P      $��Continue��69   ��6   �� *ContinueCommand+69   5Q      5S      5T      