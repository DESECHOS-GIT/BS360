   M S B A M L   `   `   `  ����@  <BS360, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BS360.CustomControlsBS360.CustomControls  5   	   '    !BS360.CustomControls.MyMessageBox   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TFontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10fFfa=http://schemas.awesome.incremented/wpf/xaml/fontawesome.sharp  5   	   .local"clr-namespace:BS360.CustomControls  5   	     � Title$  
MessageBox��5	   	   $	��230��6   $	��550��6)    � WindowStyle$
 None=�5
   	    � WindowStartupLocation$ CenterScreen=�5   	    � Topmost$
 True��5   	    � AllowsTransparency$
 True��5   	   
������  � SizeToContent$ WidthAndHeight=�5   	    � 	Resources 5         HoverButtonStyle �� 
TargetType ��Name	 �� CornerRadius 
 Color4
 ��Name �� Triggers��%�   &	        7  �� 5   
   " ��63   .R�5      �� $ Border��6   #��z��5      $	 6��5      8��  $��1q�5      ._�5      �� $
 
TextButton��6#   $��Center=�5   #   $��Center=�5   #   5       5      5#      5$      .��5&      �� $	 10��6    
 Color1#��[ 5'      #��[ 5(      $��2q�5)      ._�5+   
   �  � RowDefinitions 5,      �� 5-      $��25 �6    �� 5.      $
��auto �6    �� 5/      �� 50      $
��auto �6    .+�53      �� -   5         ��Name$ 
pnlControl��53      
������  � Cursor$
 Hand��55      $��0��56       �� Orientation$ 
Horizontal=�57      $��RightToLeft=�58      $��0,5,5,0q�59      .+�5=      �� -   5         ��Name$ btnClose��5=        controlButton#��[ 5>       
 Color2
 � Tag# [ 5?      .��5A      !  FontAwesome.Sharp.IconImage  �  �FontAwesome.Sharp.IconImageBase`1[[FontAwesome.Sharp.IconChar, FontAwesome.Sharp, Version=6.3.0.0, Culture=neutral, PublicKeyToken=d16d1e4e568ec10f]]   Icon$ XmarkCircle=�6#   $��15��66   $��15��6A     controlButtonIcon#��[ 6M   5B      5D      �� 5F      -   5         ��Name$ TBLOCK_Title��5F      $��1��5G      $��Center=�5H      $��Title��5I      $��10q�5J      $
��Bold"�5K      $��30(�5L        primaryFont#��[ 5M      5N      �� 5P      -   5        $ TBLOCK_Message��5S      $��WrapWithOverflow=�5P      $��Content��5Q      $��Center=�5R      $��10q�5T      $��2��5U      $��20(�5V      #��[ 5W      �� 5Y      -   5        $ SP_ContainsButton��5Y      $��3��5Z      $ 
Horizontal=�5[      $��Center=�5\      $
��0 20q�5]      .+�5_      �� -   5        $ BTN_OK��5_      $
 Hand��5a        ColorSucess#��[ 5b      
�������$��	10 0 10 6q�5d      $	��100��5e      $��28��5f      $��85��5g      $��10q�5h      #��[  5i      .��5k      �� -   5        $ 
TextButton��5k   !   $��OK��5l   !   $��Center=�5m   !   $��Center=�5n   !   #��[ 5o   !   5p      �� 5r      -   5        $ BTN_YES��5r      #��[ 5s      #��[ 5t      $
 Hand��5v      $��	10 0 10 6q�5w      $	��100��5x      $��28��5y      $��85��5z      $��10q�5{      ��5}      �� 5~      " ��6*   .R�5      �� #��z��6%   $	 6��5�   %   ._�5�   "   �� $��SI��6,   $��Center=�5�   ,   $��Center=�5�   ,   #��[ 5�   ,   5�      5�      5�      �� 5�      -	   5        $ BTN_NO��5�       
 Color5#��[ 5�      #��[ 5�      $
 Hand��5�      $��	10 0 10 6q�5�      $	��100��5�      $��28��5�      $��85��5�      $��10q�5�      ��5�      �� 5�      " ��6*   .R�5�      �� #��z��6%   $	 6��5�   %   ._�5�   "   �� $��NO��6,   $��Center=�5�   ,   $��Center=�5�   ,   #��[ 5�   ,   5�      5�      5�      �� 5�      -
   5        $ 
BTN_CANCEL��5�      $
 Hand��5�       	 
ColorError#��[	 5�      
�������$��	10 0 10 6q�5�      $	��100��5�      $��28��5�      $��85��5�      $��10q�5�      ��5�      �� 5�      " ��6*   .R�5�      �� #��z��6%   $	 6��5�   %   ._�5�   "   �� $��CANCEL��6,   $��Center=�5�   -   $��Center=�5�   -   #��[ 5�   -   5�      5�      5�      5�      5�      5�      5�      