   M S B A M L   `   `   `  ����   Drawing    Drawing.Pages.Page2   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008       N� Title$  Page2�� � 	Resources  
  ComGeo �� GeometryCombineMode �� 	Geometry1 �� Rect �� 	Geometry2  
ButtonClip � FillRule @� Center @� RadiusX	 @� RadiusY  personDataTem
 �� CornerRadius � RowDefinitions �� Path �� Triggers P� 
SourceName P� Property P� Value �� 
TargetName �� Property �� Value��%�  &	        &	 E     &	 �     �� $	 Xor=� �� $ 0 0 200 100� �� $ 0 0 100 200�� $ Nonzero=�.��@� $ 30 30&�$ 30Q�$	 20Q�@� $ 30 30&�$ 20Q�$	 30Q��� .R��� $��
blueBorder��$��3q�$��3q�
����  �$
 5��._�� $��3q� �� �� .+��� $��nametxt��$
��Bold"����� $
 Name��� $��1������ $	 Age� P� $ 
blueBorder�� �@J�IsMouseOver . . ��� $ 
blueBorder�� � ��
 ������� $ nametxt�� � ��$ 20(�.���  �� �� .+��� -    ��Name$ canvas1��$��0��.+��� -    ��Name$
 rec1�� �� Left$	 600��
 �� Top$	 100��$	��100��$��50��
����ح�
���  ��$��3��F� $ 10��$ 10��#��[  F� $	 220��$ 10��  ForbidShape#��[ F� $	 350��$ 10����B� .��E�  E� 
StartPoint$ 00 50&� E� IsClosed . .����  �� Point$ 100 50&��� $ 100 0&�E� $ 0 100&� E� IsFilled . .����  �� SweepDirection$ 	Clockwise=� �� Size$ 50 50�� �� Point$ 0 200&��� $ 0 100&�F� $	 450��$ 10����B� $ ��M 10,0 L 100,0 L 100,100 ZC��� $	 600��$ 10��$��65��$��65�� J� Clip# [ .��Button�� $��1��.+��� -     buttonStyle#��[ .��Print�� -     ��Name$  lbInfo��#��[ 