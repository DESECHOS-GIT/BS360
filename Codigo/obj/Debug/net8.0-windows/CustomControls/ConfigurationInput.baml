   M S B A M L   `   `   `  ����@  <BS360, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BS360.CustomControlsBS360.CustomControls  5      -    'BS360.CustomControls.ConfigurationInput   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:BS360.CustomControls     5      .��5      � 
������ $
��Auto��6$   $	��Top=�62     � RowDefinitions  5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ TextBlockLabel��5   #   
������� K� ZIndex$ 3��6;   $
��Bold"�6L   $
��Left=�5      $��2��6,   $��	29,0,0,38q�6=   $��12(�5      ��5      �� *	LabelText+ �� RelativeSource ��  �� AncestorType UserControl��5      �� *LabelForeground+ ��  UserControl5      �� 5   
   -   5         ��Name$ BorderInputTextBox��5      $��1��6-   
������� 
  Color2#��[  5      $��2q�5       �� CornerRadius$ 8��5      $��5,5,25,5q�5      ._�5      �� -   5         ��Name$ InputTextBox��5      $��Medium"�5      $��12(�5       $��0q�5!      $��10,0,0,0q�5#      $��17��5$       �� TextAlignment$
 Left=�5&      $��Stretch=�5'        titleColor5#��[ 5(      	 �� TextWrapping$
	 Wrap=�5,      ��5"      �� *	InputText+ ��  UserControl
 �� Mode$
 TwoWay=�5,      5.      �� 50   
   -   5         ��Name$ ErrorTextBlock��52      
������ $��2��51      $#��El campo no puede estar vacio��53        
ColorError#��[ 54      $��5,0,0,5q�55      $
��Bold"�56      $y�	Collapsed=�57      58      59      