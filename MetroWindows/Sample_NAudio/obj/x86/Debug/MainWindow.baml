   M S B A M L   `   `   `  ����Z  VWPFSoundVisualizationLib, Version=0.4.4975.42657, Culture=neutral, PublicKeyToken=nulleHclr-namespace:WPFSoundVisualizationLib;assembly=WPFSoundVisualizationLibWPFSoundVisualizationLib  5
   	    Sample_NAudio   Sample_NAudio.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5	   	   XsvlHclr-namespace:WPFSoundVisualizationLib;assembly=WPFSoundVisualizationLib     5
   	     � Title$  WPFSVL Sample (NAudio)��5   	   $	��482��5   	   $	��681��5   	   .��5      �    WindowBackgroundBrush#���   6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $	��100 �6   �� 5      $��* �6   �� 5      $	��100 �6   �� 5      $��40 �6    � ColumnDefinitions 5   
   �� 5      $	��30* �6   �� 5      $	��60* �6   .+�5   
   o� $��0��6   $��2��5      .@�5      m� $
��File��6   .@�5      m� -   5        $��OpenFileMenuItem��5      $��	Open File��5      5      �� 5       m� 5!      -   5        $��CloseMenuItem��5"      $��Close��5!      5#      5$      m� 5%      $��Theme��6   .@�5&      m� -   5        $��DefaultThemeMenuItem��5&      $��Default (No Theme)��5'       m� IsCheckable . 5)      m� 5*      -   5        $��ExpressionDarkMenuItem��5*      $��Expression Dark��5+       . 5-      m� 5.      -   5        $��ExpressionLightMenuItem��5.      $��Expression Light��5/       . 51      52      53      +   %WPFSoundVisualizationLib.DigitalClock  54   
   -   5          Name$ clockDisplay��56      $��1��54      $��2��55         	ShowHours .     ShowSubSeconds . $��Center=�59      $��5q�5:      .   (WPFSoundVisualizationLib.AlbumArtDisplay  5;   
   -   5          Name$ albumArtPanel��5=      $��0��5;      $��2��5<      $��5q�5>      � 5?   
   $��1��6   $��2��5@       5A      �� 5B      $��* �6#   �� 5C      $��* �6#   .+�5E      /   )WPFSoundVisualizationLib.SpectrumAnalyzer  -   5          Name$ spectrumAnalyzer��5E   #   $��5q�5F   #   	   BarCount$	 16��5G   #   (   "WPFSoundVisualizationLib.Equalizer  5H      
   NumberOfBands$
 7��6   $��1��5I      5J      � 5K   
   $��3��6   $��2��5L       5M      �� 5N      $��* �6#   �� 5O      $	��100 �6#   .+�5Q      /   )WPFSoundVisualizationLib.WaveformTimeline  -	   5          Name$ waveformTimeline��5Q   #   $��5q�5R   #   $��1q�5S   #   �� 5T       �� Orientation$ Vertical=�6   $��Center=�5U      $��1��5V      .+�5W      )   #WPFSoundVisualizationLib.TimeEditor  -
   5        $��repeatStartTimeEdit��5X   !   $��5q�5W   !      
IsReadOnly . 5Y   !     5Z      -   5        $��repeatStopTimeEdit��5[   !   $��5q�5Z   !    . 5\   !   5]      5^      � 5_   
   $��4��6   $��2��5`      $��5q�5a       5b      �� 5c      $��* �6#   �� 5d      $
��Auto �6#   .+�5f      �� -   5        $��FileText��5k      $��Center=�5f      $��0��5g      $��Stretch=�5h      $��5q�5i       �� 
IsReadOnly . 5k      �� 5l      $��Right=�6   $ 
Horizontal=�5m      $��1��5n      .+�5o      �� -   5        $��BrowseButton��5t      $��Browse��5o      $��24��5p      $��50��5q      $��Center=�5r      $��2,2,10,2q�5s      $��Center=�5u      5v      �� 5w      -   5        $��
PlayButton��5w      $��Center=�5x      $��50��5y      $��24��5z      $��2q�5{      {�.  $
��Play��5~      �� 5      -   5        $��PauseButton��5      $��Center=�5�      $��50��5�      $��24��5�      $��2q�5�      {�.  $��Pause��5�      �� 5�      -   5        $��
StopButton��5�      $��Center=�5�      $��50��5�      $��24��5�      $��2q�5�      {�.  $
��Stop��5�      5�      5�      5�      5�      