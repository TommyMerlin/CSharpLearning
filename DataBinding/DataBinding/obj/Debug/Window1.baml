   M S B A M L   `   `   `  ����D  @Panuon.UI, Version=0.1.0.2, Culture=neutral, PublicKeyToken=null8*clr-namespace:Panuon.UI;assembly=Panuon.UI	Panuon.UI  5   	    DataBinding   DataBinding.Window1   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   9pu*clr-namespace:Panuon.UI;assembly=Panuon.UI     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	      Panuon.UI.PUWindow    Header$  hello��5   	      AnimationStyle$ Gradual=�6    � Title$ Window1��5   	   $	��450��6   $	��800��6&    � WindowStartupLocation$ CenterScreen=�62    � 	Resources 5	       �� 
TargetType �� Property �� Value��%�   '��       �� 5
   
   " ��6   .�5      ��  � ��$ 15(�6)   �� 5       � ��$ 	10 5 10 5q�6'   �� 5       � ��$ 3q�6(   5      5      .��5      � .+�5   
   �� -   5        $	��top��5   l   
�������$��5q�6$   
��� d � �� CornerRadius$ 10,10,10,10��6Q   ._�5      � 	 � ColumnDefinitions	 5      �� 5      $	��120 �6'   �� 5      $��5* �6'   .+�5      �� $��0��6   .+�5      �� -   5        

 � Tag$
 Pages/page1.xaml��5      .��6I   Page16P   �� 5      -   5        $
 Pages/page2.xaml��5      .��6I   Page26P   d� 5      $��10q�6#   .��6/   Page366      Panuon.UI.PUButton  5      -   5           ButtonStyle$ Hollow=�5   "   $��2q�6N   $	��100��6b   $��3q�6n   .��6z   确定6~   �� 5      -   5         ��Name$
 btn1��5      .��6>   Click6E   5      �� 5       � 5      -   5          �Name$ frame1��5      $��1��6(     � NavigationUIVisibility$ Hidden=�68   
����ح�$��3q�6p   5       5!      5"      5#      