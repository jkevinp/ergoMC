   M S B A M L   `   `   `  ����
  ErgoMc:    4ProjectK.ErgoMC.Assessment.UserContent.view.Recovery   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  ERGOMC��5   	   $	��390��6   $	��300��6(   $	��300��68   $	��500��6E    � WindowStartupLocation$ CenterScreen=�6Q      KinectBluishBG#���   6v    � WindowStyle$ 
ToolWindow=�6�    � 	Resources 5        MediumGreyBrush �� Color  KinectPurpleBrush  KinectBlueBrush  KinectBluish  KinectBlack��%  &	       &	 #     &	 F     &	 i     &	  �     &	 �     �� 5   
   $ 	#ff6e6e6e��62   �� 5   
   $ 	#ff52318f��64   �� 5	   
   $ 	#ff00BCF2��62   �� 5
   
   $ 	#FF0095D4��6/   �� 5   
   $ 	#FFF1F5F8��61   �� 5   
   $ #213035��6.   5      .��5      � $��0q�6   .+�5   
   �� .+�5      �� $��ErgoMC��6   $��Center=�6&   $��Center=�6C   $��40(�6g   $��	ExtraBold"�6u   $	��492��6�   #���  6�   
�������6�   �� 5      $��Forgot Password��6   $��Center=�6/   $��20(�6L   $��	ExtraBold"�6Z   
�������$	��492��6�   #���  6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5      $��0,20,0,0q�6   .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��	Username:��6   $��16(�60   $	��120��6>   $��Bottom=�6M   $��Center=�6o   �� 5      -   5        $��txt_username��5      $	��400��6d   $	��350��6s   $
��Left=�6�   $��20(�6�    �� TextWrapping$
 Wrap=�6�   $��0,0,0,3q�6�   $��0,10,0,5q�6�   #���  6�   $��Center=�6    �� 	MaxLength$ 16��6Z  $��45��6i  $��45��6x  ��62   �� 	 �� Path$	 User.UserName�
 �� Mode$
 OneWay=���6D  Z� 6x  5      �� 5      -   5        $��Hidden��5   4   $y�	Collapsed=�6   .+�5      �� $ 
Horizontal=�6!   .+�5      �� $��	Question:��6    $��16(�64   $	��120��6B   $��Bottom=�6Q   $��Center=�6s   �� 5      -   5        $��txt_forgot_question��5   "   $	��400��6h   $	��350��6w   $
��Left=�6�   $��20(�6�   $��0,0,0,3q�6�   $��0,10,0,5q�6�   $	��321��6�   $��0q�6�   #���  6�   $��Center=�6(  $ 16��6b  $��45��6q  $��45��6�  ��6=   �� $	 User.Forgot_question���6L  Z� 6�  5       �� 5!      $ 
Horizontal=�6!   .+�5"      �� $��Answer:��6    $��16(�62   $	��120��6@   $��Bottom=�6O   $��Center=�6q   �� 5#      -   5        $��txt_forgot_answer��5#   "   $	��400��6;   $	��350��6J   $
��Left=�6Y   $��20(�6v   $��0,0,0,3q�6�   $��0,10,0,5q�6�   $	��321��6�   $��0q�6�   #���  6�   $��Center=�6�   $ 16��65  $��45��6D  $��45��6S  ��6  Z� 6S  5$      5%      �� 5&      .+�5'      �� -   5        $��Verify��5'      $��Center=�6.   $	��500��6K   #���  6W   $��20,0,0,0q�6�   #���  6�   
�������$��16(�6�   $	��1,5q�6�   $��
10,20,10,0q�6�   6  �� 5(      -   5        $��Cancel��5(      $��Center=�6/   $	��500��6L   #���  6X   $��20,0,0,0q�6�   #���  6�   
�������$��16(�6�   $	��1,5q�6�   $��	10,7,10,0q�6�   6  5)      5*      5+      5,      5-      