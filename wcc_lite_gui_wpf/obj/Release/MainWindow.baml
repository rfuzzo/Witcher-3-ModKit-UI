   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  K Gwcc_lite_gui_wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:wcc_lite_gui_wpf.Formswcc_lite_gui_wpf.Forms I)clr-namespace:wcc_lite_gui_wpf.ViewModelswcc_lite_gui_wpf.ViewModels 3clr-namespace:wcc_lite_gui_wpfwcc_lite_gui_wpf !   wcc_lite_gui_wpf.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    >d2http://schemas.microsoft.com/expression/blend/2008    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9System&clr-namespace:System;assembly=mscorlib     g cXceed.Wpf.AvalonDock.Themes.Aero, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4i eXceed.Wpf.AvalonDock.Themes.VS2010, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4h dXceed.Wpf.AvalonDock.Themes.Metro, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4i	 eXceed.Wpf.AvalonDock.Themes.VS2013, Version=2.9.0.1, Culture=neutral, PublicKeyToken=3e4669d2f30244f4[
 WXceed.Wpf.AvalonDock, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4=ad,http://schemas.xceed.com/wpf/xaml/avalondock    	 
 7frms$clr-namespace:wcc_lite_gui_wpf.Forms    	 
 B
ViewModels)clr-namespace:wcc_lite_gui_wpf.ViewModels    	 
 2localclr-namespace:wcc_lite_gui_wpf    	 
 -   $��	Wcc_thicc��  � Title$  	Wcc_thicc��$	��900��$
��1600�� � Icon$1 +Resources/Icons/400751097372868608_icon.ico��.���  � RowDefinitions �� $
��auto ��� $
��auto ��� $��* ��� $
��auto �.+�o� -    o�Name$ _menu�� o� 
IsMainMenu . $��3q�$��0��.@�m� $��_File��`� -    `�Name$ _toolbar��$��1��.�c�  � 	Resources  �� 
TargetType �� Property	 �� Value
 �� Triggers P� Property P� Value��%t   '��       �� " ��.���  �@^�ShowOnDisabled	 . 
 P�  � {� .  . ���  �@J�Opacity$		 0.5Q�.@���  � ToolTip$ Previous��.���� $B��</wcc_lite_gui_wpf;component/Resources/Icons/Backward_16x.png���� $
 Next��.���� $A��;/wcc_lite_gui_wpf;component/Resources/Icons/Forward_16x.png���� �� $ New File��.���� $A��;/wcc_lite_gui_wpf;component/Resources/Icons/NewFile_16x.png���� $ 	Open File��.���� $B��</wcc_lite_gui_wpf;component/Resources/Icons/OpenFile_16x.png���� $
 Save������ *SaveFileCommand+.���� $>��8/wcc_lite_gui_wpf;component/Resources/Icons/Save_16x.png���� $ Save All��.���� $A��;/wcc_lite_gui_wpf;component/Resources/Icons/SaveAll_16x.png���� �� $
 Undo��$��ApplicationCommands.Undo��.���� $>��8/wcc_lite_gui_wpf;component/Resources/Icons/Undo_16x.png���� $
 Redo��$��ApplicationCommands.Redo��.���� $>��8/wcc_lite_gui_wpf;component/Resources/Icons/Redo_16x.png���� $	 Cut��$��ApplicationCommands.Cut��.���� $=��7/wcc_lite_gui_wpf;component/Resources/Icons/Cut_16x.png���� $
 Copy��$��ApplicationCommands.Copy��.���� $>��8/wcc_lite_gui_wpf;component/Resources/Icons/Copy_16x.png���� $ Paste��$��ApplicationCommands.Paste��.���� $?��9/wcc_lite_gui_wpf;component/Resources/Icons/Paste_16x.png���� �� $	 Run������ *RunWccCmdCommand+.���� $=��7/wcc_lite_gui_wpf;component/Resources/Icons/Run_16x.png��) 
 #Xceed.Wpf.AvalonDock.DockingManager  -     Name$ _manager��$��2��   ShowSystemMenu .    AllowMixedOrientation .    Theme 1 	 +Xceed.Wpf.AvalonDock.Themes.Vs2013DarkTheme     Layout. , 
 &Xceed.Wpf.AvalonDock.Layout.LayoutRoot  -   	_root��   LeftSide 2 
 ,Xceed.Wpf.AvalonDock.Layout.LayoutAnchorSide  � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorGroup, Xceed.Wpf.AvalonDock, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 3 
 -Xceed.Wpf.AvalonDock.Layout.LayoutAnchorGroup  � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable, Xceed.Wpf.AvalonDock, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 2 
 ,Xceed.Wpf.AvalonDock.Layout.LayoutAnchorable     AutoHideMinWidth$	 200Q�/	 
 )Xceed.Wpf.AvalonDock.Layout.LayoutContent 	  Title$ Commands�� 	  Content. '
  !wcc_lite_gui_wpf.Forms.UCCommands
  -    
 Name$ Commands��   	RootPanel. - 
 'Xceed.Wpf.AvalonDock.Layout.LayoutPanel     Orientation$ Vertical=�� 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutPanelElement, Xceed.Wpf.AvalonDock, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children. 4 
 .Xceed.Wpf.AvalonDock.Layout.LayoutDocumentPane  � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutGroup`1[[Xceed.Wpf.AvalonDock.Layout.LayoutContent, Xceed.Wpf.AvalonDock, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]   Children.    	  	ContentId$ 	workspace��$ 	Workspace��. +  %wcc_lite_gui_wpf.Forms.UCWorkflowView  -     Name$ 	Workspace��  � 
 �Xceed.Wpf.AvalonDock.Layout.LayoutPositionableGroup`1[[Xceed.Wpf.AvalonDock.Layout.ILayoutPanelElement, Xceed.Wpf.AvalonDock, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4]]    
DockHeight$	  150 �$ 
Horizontal=�. 6 
 0Xceed.Wpf.AvalonDock.Layout.LayoutAnchorablePane  .   $ errors��$ 
Error List��. %  wcc_lite_gui_wpf.Forms.UCErrors    $ output��$ Console��. &   wcc_lite_gui_wpf.Forms.UCConsole  -   !  Name$! Console���� -	   " ��Name$" _status��$��3�����#007acc.@��� .���� $
��test��