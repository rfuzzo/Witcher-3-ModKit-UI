   M S B A M L   `   `   `  ����E  Aw3tools.UI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:w3tools.UI.Viewsw3tools.UI.Views  5   	        w3tools.UI.Views.AboutView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   0localclr-namespace:w3tools.UI.Views     5   	   $	��250��5	   	   $	��300��6   ���	#FF24292E  � 	Resources  5         FocusVisual �� Property �� Value J� SnapsToDevicePixels �� StrokeDashArray  Button.Static.Background �� Color  Button.Static.Border  Button.MouseOver.Background  Button.MouseOver.Border  Button.Pressed.Background  Button.Pressed.Border  Button.Disabled.Background  Button.Disabled.Border 	 Button.Disabled.Foreground 
 ButtonStyle1 �� 
TargetType �� 
TargetType ��Name	 ��Name
 �� Triggers P� Property P� Value �� 
TargetName��%  &	        &	 �     &	 �     &	 �     &	     &	 2    &	 U    &	 x    &	 �    &		 �    &	
 �    7   7  7  7  7  7  7  7  7 	 �� 5   
   .�5      ��  � �� 5      �� 5      .R�5      �� $��2q�6$    . #��� ��6J   $��1��6�   $	 1 2R�6�   5      5      5      �� 5   
   $ 	#FF2F363D��6;   �� 5   
   $ Black��67   �� 5   
   $ 	#FFBEE6FD��6>   �� 5   
   $ 	#FF3C7FB1��6:   �� 5   
   $ 	#FFC4E5F6��6<   �� 5   
   $ 	#FF2C628B��68   �� 5   
   $ 	#FFF4F4F4��6=   �� 5   
   $ 	#FFADB2B5��69   �� 5   
   $ 	#FF838383��6=   �� 5   
   " ��6%   .�5       ��  �@�FocusVisualStyle8   61   �� 5!       � ��8  6+   �� 5"       � ��8  6,   �� 5#       � ��# � ��6+   �� 5$       � ��$ 1q�60   �� 5%       � ��$ Center=�6;   �� 5&       � ��$ Center=�69   �� 5'       � ��$ 1q�6(   �� 5(       � �� 5)      �� 5*      " ��6&   .R�5+      �� $ border��6!   #��z��61   #��z��6]   #��z��6�    . ._�5,      �� $	 contentPresenter��6/   |�.  #��z��6[   #��z��6�   ��. # z 6�   #��z��6  5-      
 5.      P� 5/       �@��IsDefaulted . . �50   "   �� $ border��6@   # � ��6T    � ��51      P� 52       �@J�IsMouseOver . . �53   "   �� $ border��6@   8   � ��6T   54      P� 55       � �� . . �56   "   �� $ border��6?   8   � ��6S   �� 57   "   $ border��6@   8   � ��6T   58      P� 59       � {� .  . �5:   "   �� $ border��6?   8   � ��6S   �� 5;   "   $ border��6@   8   � ��6T   �� 5<   "   $ contentPresenter��6K   8   � ��6i   5=      5?      5A      5B      5C      .��5F      �  � RowDefinitions 5G   
   �� 5H      $��* �6   �� 5I      $
��Auto �6   �� 5J      $��50 �6   .+�5M   
   �� $��0��6    �� Orientation$ Vertical=�6"   .+�5N      �� $6��0/w3tools.UI;component/Resources/modkitUIIcon.ico��6   $
��Fill=�5O      $��50��5P      $��50��5Q      $��5,30,5,5q�5R      �� 5T      $��	ModKit UI��6   $��Center=�6)   $
��Bold"�5U      
�������$��5,5,5,5q�5W      $��14(�5X      �� 5Z      $��Center=�6   $��1,1,1,1q�65   �� 5[      $��12(�6    �� TextDecorations$  {�6*   .�6=   .�5\      �� ��6    �� *	Version+ �� StringFormat$ Version: {0}��5]      5^      �� 5_      $��Center=�6   �� 5`      $��12(�6_   $  {�6m    �� Command 6   �� *OpenGithubCommand+ �� CommandParameter 6B   �� .�6�   .�5a      �� ��6    �� *	URLGithub+5b      5c      5e      �� 5g   
   $��1��6   
���   �6!   �� 5i   
   $��2��6   .+�5j      �� $��Right=�6   $��	0,13,10,0q�5k      $��80��5l      $��25��5m      
�������#��� 
 5o      ��5p      �� *	OKCommand+.��5q      Close6   5r      5t      5u      