using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using WPFSoundVisualizationLib;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;
using System.Windows.Markup;
using System.Xml;
using System.Collections.ObjectModel;
using System.Threading;
using Microsoft.VisualBasic.Devices;
using Microsoft.VisualBasic;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Windows.Media.Animation;

namespace MetroWindows
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        public static bool deck1DownloadRead=false;
        public static bool deck2DownloadRead=false;
        private string deck1File = "";
        private string deck2File = "";
        public static bool left = true;
        static public Cloud c = new Cloud();
        BitmapImage pause_red_light = new BitmapImage(new Uri("/Images/pause red light.png", UriKind.Relative));
        BitmapImage pause_red = new BitmapImage(new Uri("/Images/pause red.png", UriKind.Relative));
        BitmapImage pause_white = new BitmapImage(new Uri("/Images/pause white.png", UriKind.Relative));
        BitmapImage pause_blue_light = new BitmapImage(new Uri("/Images/pause blue light.png", UriKind.Relative));
        BitmapImage pause_blue = new BitmapImage(new Uri("/Images/pause blue.png", UriKind.Relative));

        BitmapImage play_red_light = new BitmapImage(new Uri("/Images/play red light.png", UriKind.Relative));
        BitmapImage play_red = new BitmapImage(new Uri("/Images/play red.png", UriKind.Relative));
        BitmapImage play_white = new BitmapImage(new Uri("/Images/play white.png", UriKind.Relative));
        BitmapImage play_blue_light = new BitmapImage(new Uri("/Images/play blue light.png", UriKind.Relative));
        BitmapImage play_blue = new BitmapImage(new Uri("/Images/play blue.png", UriKind.Relative));

        BitmapImage stop_red_light = new BitmapImage(new Uri("/Images/stop red light.png", UriKind.Relative));
        BitmapImage stop_red = new BitmapImage(new Uri("/Images/stop red.png", UriKind.Relative));
        BitmapImage stop_white = new BitmapImage(new Uri("/Images/stop white.png", UriKind.Relative));
        BitmapImage stop_blue_light = new BitmapImage(new Uri("/Images/stop blue light.png", UriKind.Relative));
        BitmapImage stop_blue = new BitmapImage(new Uri("/Images/stop blue.png", UriKind.Relative));

        BitmapImage cue_red_light = new BitmapImage(new Uri("/Images/cue red light.png", UriKind.Relative));
        BitmapImage cue_red = new BitmapImage(new Uri("/Images/cue red.png", UriKind.Relative));
        BitmapImage cue_white = new BitmapImage(new Uri("/Images/cue white.png", UriKind.Relative));
        BitmapImage cue_blue_light = new BitmapImage(new Uri("/Images/cue blue light.png", UriKind.Relative));
        BitmapImage cue_blue = new BitmapImage(new Uri("/Images/cue blue.png", UriKind.Relative));

        BitmapImage sync_red_light = new BitmapImage(new Uri("/Images/sync red light.png", UriKind.Relative));
        BitmapImage sync_red = new BitmapImage(new Uri("/Images/sync red.png", UriKind.Relative));
        BitmapImage sync_white = new BitmapImage(new Uri("/Images/sync white.png", UriKind.Relative));
        BitmapImage sync_blue_light = new BitmapImage(new Uri("/Images/sync blue light.png", UriKind.Relative));
        BitmapImage sync_blue = new BitmapImage(new Uri("/Images/sync blue.png", UriKind.Relative));

        BitmapImage browse_red_light = new BitmapImage(new Uri("/Images/browse red light.png", UriKind.Relative));
        BitmapImage browse_red = new BitmapImage(new Uri("/Images/browse red.png", UriKind.Relative));
        BitmapImage browse_white = new BitmapImage(new Uri("/Images/browse white.png", UriKind.Relative));
        BitmapImage browse_blue_light = new BitmapImage(new Uri("/Images/browse blue light.png", UriKind.Relative));
        BitmapImage browse_blue = new BitmapImage(new Uri("/Images/browse blue.png", UriKind.Relative));

        BitmapImage record_red = new BitmapImage(new Uri("/Images/record.png", UriKind.Relative));
        BitmapImage record_red_light = new BitmapImage(new Uri("/Images/record light.png", UriKind.Relative));
        BitmapImage save_blue_light = new BitmapImage(new Uri("/Images/save blue light.png", UriKind.Relative));
        BitmapImage save_blue = new BitmapImage(new Uri("/Images/save blue.png", UriKind.Relative));


        bool isPlay1 = false;
        bool isPlay2 = false;

        public static string bpmDisplay1 = "";

        public static string bpmDisplay2 = "";

        static double redAngle = 0;
        static double blueAngle = 0;
        DispatcherTimer redTimer;
        DispatcherTimer blueTimer;

        public bool[] cueFlag = { false, false, false, false, false, false };
        public double[] cueValue = { 0, 0, 0, 0, 0, 0 };
        private int cueSelected = 0;

        private bool recordFlag = false;
        public MainWindow()
        {
           
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += new EventHandler(someEventHandler);
            timer.Start();

            redTimer = new DispatcherTimer();
            redTimer.Interval = TimeSpan.FromMilliseconds(1);
            redTimer.Tick += new EventHandler(redEventHandler);
            //rotateTimer.Start();

            blueTimer = new DispatcherTimer();
            blueTimer.Interval = TimeSpan.FromMilliseconds(1);
            blueTimer.Tick += new EventHandler(blueEventHandler);

            Un4seen.Bass.BassNet.Registration("technoapurva@gmail.com", "2X1823322152222");
            InitializeComponent();

            BassEngine bassEngine = BassEngine.Instance;
            BassEngine1 bassEngine1 = BassEngine1.Instance;

            bassEngine.PropertyChanged += BassEngine_PropertyChanged;
            /*UIHelper.Bind(bassEngine, "CanStop", StopButton, Button.IsEnabledProperty);
            UIHelper.Bind(bassEngine, "CanPlay", PlayButton, Button.IsEnabledProperty);
            UIHelper.Bind(bassEngine, "CanPause", PauseButton, Button.IsEnabledProperty);*/
            UIHelper.Bind(bassEngine, "SelectionBegin", repeatStartTimeEdit, TimeEditor.ValueProperty, BindingMode.TwoWay);
            UIHelper.Bind(bassEngine, "SelectionEnd", repeatStopTimeEdit, TimeEditor.ValueProperty, BindingMode.TwoWay);

            bassEngine1.PropertyChanged += BassEngine_PropertyChanged1;
            /*UIHelper.Bind(bassEngine1, "CanStop1", StopButton1, Button.IsEnabledProperty);
            UIHelper.Bind(bassEngine1, "CanPlay1", PlayButton1, Button.IsEnabledProperty);
            UIHelper.Bind(bassEngine1, "CanPause1", PauseButton1, Button.IsEnabledProperty);*/
            UIHelper.Bind(bassEngine1, "SelectionBegin", repeatStartTimeEdit1, TimeEditor.ValueProperty, BindingMode.TwoWay);
            UIHelper.Bind(bassEngine1, "SelectionEnd", repeatStopTimeEdit1, TimeEditor.ValueProperty, BindingMode.TwoWay);



            spectrumAnalyzer.RegisterSoundPlayer(bassEngine);
            waveformTimeline.RegisterSoundPlayer(bassEngine);


            spectrumAnalyzer1.RegisterSoundPlayer(bassEngine1);
            waveformTimeline1.RegisterSoundPlayer(bassEngine1);

            LoadExpressionDarkTheme();
            //redRotate = (Storyboard)FindResource("redRotation");
            try
            {
                c.ConnectToCloud("leap", "JNY/5fguNeNTyktSAR9gnbUN7G9tks3Ec9ic0H0SCy01DJDAuB7UHop0Mrp0rDWCaBiNcq2SM0lCrQnXUNSaRg==");

            }
            catch (Exception exe)
            { }

            
        }

        
        #region Bass Engine Events


        private void someEventHandler(Object sender, EventArgs args)
        {
            try
            {
                bpmText1.Text = bpmDisplay1;
                bpmText2.Text = bpmDisplay2;
                if (deck1DownloadRead == true)
                {
                    deck1DownloadRead = false;
                    isPlay1 = false;
                    string path = @"Cache\" + deck1File;
                    BassEngine.Instance.OpenFile(path);
                    BassEngine.Instance.SetVolume((float)((1 - crossFeeder.Value) * volumeSlider1.Value));
                    TagLib.File tagFile = TagLib.File.Create(path);
                    string artist = tagFile.Tag.FirstAlbumArtist;
                    string album = tagFile.Tag.Album;
                    string title = tagFile.Tag.Title;
                    songName1.Text = title;
                    albumName1.Text = artist + ", " + album;
                    //Cloud.downloadThread.Suspend();
                }
                else if (deck2DownloadRead == true)
                {
                    deck2DownloadRead = false;
                    isPlay2 = false;
                    string path = @"Cache\" + deck1File;
                    BassEngine1.Instance.OpenFile(path);
                    BassEngine1.Instance.SetVolume((float)((1 - crossFeeder.Value) * volumeSlider1.Value));
                    TagLib.File tagFile = TagLib.File.Create(path);
                    string artist = tagFile.Tag.FirstAlbumArtist;
                    string album = tagFile.Tag.Album;
                    string title = tagFile.Tag.Title;
                    songName1.Text = title;
                    albumName1.Text = artist + ", " + album;
                    // Cloud.downloadThread.Suspend();
                }
            }
            catch (Exception exe)
            {
                MessageBox.Show("Could not connect to cloud");
            }
        }

        private void redEventHandler(Object sender, EventArgs args)
        {
            redAngle += 1;
            redDisc.RenderTransform = new RotateTransform(redAngle);
        }

        private void blueEventHandler(Object sender, EventArgs args)
        {
            blueAngle += 1;
            blueDisc.RenderTransform = new RotateTransform(blueAngle);
        }

        private void BassEngine_PropertyChanged1(object sender, PropertyChangedEventArgs e)
        {
            BassEngine1 engine = BassEngine1.Instance;
            switch (e.PropertyName)
            {
                case "FileTag":
                    if (engine.FileTag != null)
                    {
                        TagLib.Tag tag = engine.FileTag.Tag;
                        if (tag.Pictures.Length > 0)
                        {
                            using (MemoryStream albumArtworkMemStream = new MemoryStream(tag.Pictures[0].Data.Data))
                            {
                                try
                                {
                                    BitmapImage albumImage = new BitmapImage();
                                    albumImage.BeginInit();
                                    albumImage.CacheOption = BitmapCacheOption.OnLoad;
                                    albumImage.StreamSource = albumArtworkMemStream;
                                    albumImage.EndInit();
                                    //albumArtPanel1.AlbumArtImage = albumImage;
                                }
                                catch (NotSupportedException)
                                {
                                    //albumArtPanel1.AlbumArtImage = null;
                                    // System.NotSupportedException:
                                    // No imaging component suitable to complete this operation was found.
                                }
                                albumArtworkMemStream.Close();
                            }
                        }
                        else
                        {
                            //albumArtPanel1.AlbumArtImage = null;
                        }
                    }
                    else
                    {
                        //albumArtPanel1.AlbumArtImage = null;
                    }
                    break;
                case "ChannelPosition":
                    clockDisplay1.Time = TimeSpan.FromSeconds(engine.ChannelPosition);
                    break;
                default:
                    // Do Nothing
                    break;
            }

        }

        private void BassEngine_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            BassEngine engine = BassEngine.Instance;
            switch (e.PropertyName)
            {
                case "FileTag":
                    if (engine.FileTag != null)
                    {
                        TagLib.Tag tag = engine.FileTag.Tag;
                        if (tag.Pictures.Length > 0)
                        {
                            using (MemoryStream albumArtworkMemStream = new MemoryStream(tag.Pictures[0].Data.Data))
                            {
                                try
                                {
                                    BitmapImage albumImage = new BitmapImage();
                                    albumImage.BeginInit();
                                    albumImage.CacheOption = BitmapCacheOption.OnLoad;
                                    albumImage.StreamSource = albumArtworkMemStream;
                                    albumImage.EndInit();
                                    //albumArtPanel.AlbumArtImage = albumImage;
                                }
                                catch (NotSupportedException)
                                {
                                    //albumArtPanel.AlbumArtImage = null;
                                    // System.NotSupportedException:
                                    // No imaging component suitable to complete this operation was found.
                                }
                                albumArtworkMemStream.Close();
                            }
                        }
                        else
                        {
                            //albumArtPanel.AlbumArtImage = null;
                        }
                    }
                    else
                    {
                        //albumArtPanel.AlbumArtImage = null;
                    }
                    break;
                case "ChannelPosition":
                    clockDisplay.Time = TimeSpan.FromSeconds(engine.ChannelPosition);
                    break;
                default:
                    // Do Nothing
                    break;
            }

        }
        #endregion

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (BassEngine.Instance.CanPlay)
                BassEngine.Instance.Play();
        }

        private void PlayButton_Click1(object sender, RoutedEventArgs e)
        {
            if (BassEngine1.Instance.CanPlay)
                BassEngine1.Instance.Play();

        }
        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (BassEngine.Instance.CanPause)
                BassEngine.Instance.Pause();

        }

        private void PauseButton_Click1(object sender, RoutedEventArgs e)
        {
            if (BassEngine1.Instance.CanPause)
                BassEngine1.Instance.Pause();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if (BassEngine.Instance.CanStop)
                BassEngine.Instance.Stop();
        }

        private void StopButton_Click1(object sender, RoutedEventArgs e)
        {
            if (BassEngine1.Instance.CanStop)
                BassEngine1.Instance.Stop();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }
        private void BrowseButton_Click1(object sender, RoutedEventArgs e)
        {
            OpenFile1();
        }

        private void LoadDefaultTheme()
        {
            DefaultThemeMenuItem.IsChecked = true;
            DefaultThemeMenuItem.IsEnabled = false;
            ExpressionDarkMenuItem.IsChecked = false;
            ExpressionDarkMenuItem.IsEnabled = true;
            ExpressionLightMenuItem.IsChecked = false;
            ExpressionLightMenuItem.IsEnabled = true;

            Resources.MergedDictionaries.Clear();
        }

        private void LoadExpressionDarkTheme()
        {
            DefaultThemeMenuItem.IsChecked = false;
            DefaultThemeMenuItem.IsEnabled = true;
            ExpressionDarkMenuItem.IsChecked = true;
            ExpressionDarkMenuItem.IsEnabled = false;
            ExpressionLightMenuItem.IsChecked = false;
            ExpressionLightMenuItem.IsEnabled = true;

            Resources.MergedDictionaries.Clear();
            ResourceDictionary themeResources = Application.LoadComponent(new Uri("ExpressionDark.xaml", UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(themeResources);
        }

        private void LoadExpressionLightTheme()
        {
            DefaultThemeMenuItem.IsChecked = false;
            DefaultThemeMenuItem.IsEnabled = true;
            ExpressionDarkMenuItem.IsChecked = false;
            ExpressionDarkMenuItem.IsEnabled = true;
            ExpressionLightMenuItem.IsChecked = true;
            ExpressionLightMenuItem.IsEnabled = false;

            Resources.MergedDictionaries.Clear();
            ResourceDictionary themeResources = Application.LoadComponent(new Uri("ExpressionLight.xaml", UriKind.Relative)) as ResourceDictionary;
            Resources.MergedDictionaries.Add(themeResources);
        }

        private void DefaultThemeMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            LoadDefaultTheme();
        }

        private void ExpressionDarkMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            LoadExpressionDarkTheme();
        }

        private void ExpressionLightMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            LoadExpressionLightTheme();
        }

        private void OpenFile()
        {
            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.Filter = "(*.mp3, *.m4a)|*.mp3;*.m4a";
            if (openDialog.ShowDialog() == true)
            {
                //fileSelectedFlag1 = true;
                isPlay1 = false;
                BassEngine.Instance.OpenFile(openDialog.FileName);
                BassEngine.Instance.SetVolume((float)((1 - crossFeeder.Value) * volumeSlider1.Value));
                TagLib.File tagFile = TagLib.File.Create(openDialog.FileName);
                string artist = tagFile.Tag.FirstAlbumArtist;
                string album = tagFile.Tag.Album;
                string title = tagFile.Tag.Title;
                songName1.Text = title;
                albumName1.Text = artist + ", " + album;
                //Calculate_BPM1();
            }
        }

        private void OpenFile1()
        {
            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.Filter = "(*.mp3, *.m4a)|*.mp3;*.m4a";
            if (openDialog.ShowDialog() == true)
            {
                isPlay2 = false;
                BassEngine1.Instance.OpenFile(openDialog.FileName);
                BassEngine1.Instance.SetVolume((float)(crossFeeder.Value * volumeSlider2.Value));
                TagLib.File tagFile = TagLib.File.Create(openDialog.FileName);
                string artist = tagFile.Tag.FirstAlbumArtist;
                string album = tagFile.Tag.Album;
                string title = tagFile.Tag.Title;
                songName2.Text = title;
                albumName2.Text = artist + ", " + album;
                //Calculate_BPM2();
            }
        }

        private void Calculate_BPM1()
        {
            double bpmTemp = (BassEngine.Instance.ChannelLength * 60 / 256) + 60;
            if (bpmTemp < 100)
            {
                //bpmText1.Text = bpmTemp.ToString().Substring(0, 5);
            }
            else
            {
                //bpmText1.Text = bpmTemp.ToString().Substring(0, 6);
            }
        }

        private void Calculate_BPM2()
        {
            double bpmTemp = (BassEngine1.Instance.ChannelLength * 60 / 256) + 60;
            if (bpmTemp < 100)
            {
                bpmText2.Text = bpmTemp.ToString().Substring(0, 5);
            }
            else
            {
                bpmText2.Text = bpmTemp.ToString().Substring(0, 6);
            }
        }

        private void OpenFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
        }

        private void CloseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void crossFeeder_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                BassEngine.Instance.SetVolume((float)((1 - crossFeeder.Value) * volumeSlider1.Value));
                BassEngine1.Instance.SetVolume((float)(crossFeeder.Value * volumeSlider2.Value));
            }
            catch (Exception exe)
            { }
        }
        private void PART_TITLEBAR_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void PART_CLOSE_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PART_MAXIMIZE_RESTORE_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }

        private void PART_MINIMIZE_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        private void Image_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void browseButton1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFile();
            browseButton1.Source = browse_white;
            if (isPlay1 == false)
            {
                playButton1.Source = play_white;
            }
            try
            {
                redTimer.Stop();
            }
            catch (Exception exe)
            { }
        }

        private void playButton1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (BassEngine.Instance.CanPlay)
            {
                float[] values = equalizer.EqualizerValues;
                for (int i = 0; i < 3; i++)
                {
                    BassEngine.Instance.UpdateEQ(i, values[i] * 15f);
                    BassEngine1.Instance.UpdateEQ(i, values[i + 4] * 15f);
                }
                BassEngine.Instance.Play();
                playButton1.Source = play_red;
                isPlay1 = true;
                redTimer.Start();
            }

        }

        private void pauseButton1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (BassEngine.Instance.CanPause)
            {
                BassEngine.Instance.Pause();
                isPlay1 = false;
                playButton1.Source = play_white;
                redTimer.Stop();
            }
            pauseButton1.Source = pause_white;
            
        }

        private void stopButton1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (BassEngine.Instance.CanStop)
            {
                BassEngine.Instance.Stop();
                isPlay1 = false;
                playButton1.Source = play_white;
                redTimer.Stop();
            }
            stopButton1.Source = stop_white;
        }

        private void pauseButton1_MouseEnter(object sender, MouseEventArgs e)
        {
            pauseButton1.Source = pause_red_light;
        }

        private void pauseButton1_MouseLeave(object sender, MouseEventArgs e)
        {
            pauseButton1.Source = pause_white;
        }

        private void pauseButton1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pauseButton1.Source = pause_red;
        }

        private void browseButton1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            browseButton1.Source = browse_red;
        }

        private void browseButton1_MouseLeave(object sender, MouseEventArgs e)
        {
            browseButton1.Source = browse_white;
        }

        private void browseButton1_MouseEnter(object sender, MouseEventArgs e)
        {
            browseButton1.Source = browse_red_light;
        }

        private void stopButton1_MouseLeave(object sender, MouseEventArgs e)
        {
            stopButton1.Source = stop_white;
        }

        private void stopButton1_MouseEnter(object sender, MouseEventArgs e)
        {
            stopButton1.Source = stop_red_light;
        }

        private void stopButton1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            stopButton1.Source = stop_red;
        }

        private void syncButton1_MouseEnter(object sender, MouseEventArgs e)
        {
            syncButton1.Source = sync_red_light;
        }

        private void syncButton1_MouseLeave(object sender, MouseEventArgs e)
        {
            syncButton1.Source = sync_white;
        }

        private void syncButton1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            syncButton1.Source = sync_white;
        }

        private void syncButton1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            syncButton1.Source = sync_red;
        }

        private void cueButton1_MouseLeave(object sender, MouseEventArgs e)
        {
            cueButton1.Source = cue_white;
        }

        private void cueButton1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cueButton1.Source = cue_white;
            BassEngine.Instance.Pause();
            isPlay1 = false;
            redTimer.Stop();
            if (cueSelected == 0)
            {
                BassEngine.Instance.ChannelPosition = 0;
            }
            else
            {
                BassEngine.Instance.ChannelPosition = cueValue[cueSelected - 1];
            }
        }

        private void cueButton1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cueButton1.Source = cue_red;
            if (cueSelected == 0)
            {
                BassEngine.Instance.ChannelPosition = 0;
            }
            else
            {
                BassEngine.Instance.ChannelPosition = cueValue[cueSelected - 1];
            }
            if (BassEngine.Instance.CanPlay)
            {
                float[] values = equalizer.EqualizerValues;
                for (int i = 0; i < 3; i++)
                {
                    BassEngine.Instance.UpdateEQ(i, values[i] * 15f);
                    BassEngine1.Instance.UpdateEQ(i, values[i + 4] * 15f);
                }
                BassEngine.Instance.Play();
                isPlay1 = true;
                redTimer.Start();
            }
            playButton1.Source = play_white;
        }

        private void cueButton1_MouseEnter(object sender, MouseEventArgs e)
        {
            cueButton1.Source = cue_red_light;
        }

        private void playButton1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isPlay1 == false)
            {
                playButton1.Source = play_white;
            }
        }

        private void playButton1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isPlay1 == false)
            {
                playButton1.Source = play_red_light;
            }
        }

        private void playButton1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isPlay1 == false)
            {
                playButton1.Source = play_red;
            }
        }

        private void browseButton2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFile1();
            browseButton2.Source = browse_white;
            if (isPlay2 == false)
            {
                playButton2.Source = play_white;
            }
            try
            {
                blueTimer.Stop();
            }
            catch (Exception exe)
            { }
        }

        private void browseButton2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            browseButton2.Source = browse_blue;
        }

        private void browseButton2_MouseLeave(object sender, MouseEventArgs e)
        {
            browseButton2.Source = browse_white;
        }

        private void browseButton2_MouseEnter(object sender, MouseEventArgs e)
        {
            browseButton2.Source = browse_blue_light;
        }

        private void syncButton2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            syncButton2.Source = sync_white;
        }

        private void syncButton2_MouseLeave(object sender, MouseEventArgs e)
        {
            syncButton2.Source = sync_white;
        }

        private void syncButton2_MouseEnter(object sender, MouseEventArgs e)
        {
            syncButton2.Source = sync_blue_light;
        }

        private void syncButton2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            syncButton2.Source = sync_blue;
        }

        private void cueButton2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            cueButton2.Source = cue_white;
            BassEngine1.Instance.Pause();
            isPlay2 = false;
            blueTimer.Stop();
            if (cueSelected == 0)
            {
                BassEngine1.Instance.ChannelPosition = 0;
            }
            else
            {
                BassEngine1.Instance.ChannelPosition = cueValue[cueSelected - 1];
            }
        }

        private void cueButton2_MouseLeave(object sender, MouseEventArgs e)
        {
            cueButton2.Source = cue_white;
        }

        private void cueButton2_MouseEnter(object sender, MouseEventArgs e)
        {
            cueButton2.Source = cue_blue_light;
        }

        private void cueButton2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cueButton2.Source = cue_blue;
            if (cueSelected == 0)
            {
                BassEngine1.Instance.ChannelPosition = 0;
            }
            else
            {
                BassEngine1.Instance.ChannelPosition = cueValue[cueSelected - 1];
            }
            if (BassEngine1.Instance.CanPlay)
            {
                float[] values = equalizer.EqualizerValues;
                for (int i = 0; i < 3; i++)
                {
                    BassEngine.Instance.UpdateEQ(i, values[i] * 15f);
                    BassEngine1.Instance.UpdateEQ(i, values[i + 4] * 15f);
                }
                BassEngine1.Instance.Play();
                isPlay2 = true;
                blueTimer.Start();
            }
            playButton2.Source = play_white;
        }

        private void pauseButton2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (BassEngine1.Instance.CanPause)
            {
                BassEngine1.Instance.Pause();
                isPlay2 = false;
                playButton2.Source = play_white;
                blueTimer.Stop();
            }
            pauseButton2.Source = pause_white;
        }

        private void pauseButton2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            pauseButton2.Source = pause_blue;
        }

        private void pauseButton2_MouseEnter(object sender, MouseEventArgs e)
        {
            pauseButton2.Source = pause_blue_light;
        }

        private void pauseButton2_MouseLeave(object sender, MouseEventArgs e)
        {
            pauseButton2.Source = pause_white;
        }

        private void stopButton2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (BassEngine1.Instance.CanStop)
            {
                BassEngine1.Instance.Stop();
                isPlay2 = false;
                playButton2.Source = play_white;
                blueTimer.Stop();
            }
            stopButton2.Source = stop_white;
        }

        private void stopButton2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            stopButton2.Source = stop_blue;
        }

        private void stopButton2_MouseLeave(object sender, MouseEventArgs e)
        {
            stopButton2.Source = stop_white;
        }

        private void stopButton2_MouseEnter(object sender, MouseEventArgs e)
        {
            stopButton2.Source = stop_blue_light;
        }

        private void playButton2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (BassEngine1.Instance.CanPlay)
            {
                float[] values = equalizer.EqualizerValues;
                for (int i = 0; i < 3; i++)
                {
                    BassEngine.Instance.UpdateEQ(i, values[i] * 15f);
                    BassEngine1.Instance.UpdateEQ(i, values[i + 4] * 15f);
                }
                BassEngine1.Instance.Play();
                playButton2.Source = play_blue;
                isPlay2 = true;
                blueTimer.Start();
            }
        }

        private void playButton2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (isPlay2 == false)
            {
                playButton2.Source = play_blue;
            }
        }

        private void playButton2_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isPlay2 == false)
            {
                playButton2.Source = play_white;
            }
        }

        private void playButton2_MouseEnter(object sender, MouseEventArgs e)
        {
            if (isPlay2 == false)
            {
                playButton2.Source = play_blue_light;
            }
        }

        private void pitchSlider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BassEngine.Instance.changePitch((float)pitchSlider1.Value);
        }

        private void equalizer_MouseMove(object sender, MouseEventArgs e)
        {
            float[] values = equalizer.EqualizerValues;
            for (int i = 0; i < 3; i++)
            {
                BassEngine.Instance.UpdateEQ(i, values[i] * 15f);
                BassEngine1.Instance.UpdateEQ(i, values[i + 4] * 15f);
            }
        }

        private void equalizer_MouseLeave(object sender, MouseEventArgs e)
        {
            float[] values = equalizer.EqualizerValues;
            for (int i = 0; i < 3; i++)
            {
                BassEngine.Instance.UpdateEQ(i, values[i] * 15f);
                BassEngine1.Instance.UpdateEQ(i, values[i + 4] * 15f);
            }
        }

        private void volumeSlider1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //BassEngine.Instance.SetVolume((float)(
        }

        private void volumeSlider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private object dummyNode = null;

        public string SelectedImagePath { get; set; }


        void folder_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            if (item.Items.Count == 1 && item.Items[0] == dummyNode)
            {
                item.Items.Clear();
                try
                {
                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        TreeViewItem subitem = new TreeViewItem();
                        subitem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        subitem.Tag = s;
                        subitem.FontWeight = FontWeights.Normal;
                        subitem.Foreground = Brushes.Red;
                        subitem.Items.Add(dummyNode);
                        subitem.Expanded += new RoutedEventHandler(folder_Expanded);
                        item.Items.Add(subitem);
                    }
                }
                catch (Exception) { }
            }
        }

        private void foldersItem_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView tree = (TreeView)sender;
            TreeViewItem temp = ((TreeViewItem)tree.SelectedItem);

            if (temp == null)
                return;
            SelectedImagePath = "";
            string temp1 = "";
            string temp2 = "";
            while (true)
            {
                temp1 = temp.Header.ToString();
                if (temp1 == "Cloud")
                {
                    cloudGrid.Visibility = Visibility.Visible;
                    fileList.Visibility = Visibility.Hidden;
                }
                else
                {
                    cloudGrid.Visibility = Visibility.Hidden;
                    fileList.Visibility = Visibility.Visible;
                    cloudList.Visibility = Visibility.Hidden;
                    uploadGrid.Visibility = Visibility.Hidden;
                }
                if (temp1.Contains(@"\"))
                {
                    temp2 = "";
                }
                SelectedImagePath = temp1 + temp2 + SelectedImagePath;
                if (temp.Parent.GetType().Equals(typeof(TreeView)))
                {
                    break;
                }
                temp = ((TreeViewItem)temp.Parent);
                temp2 = @"\";
            }
            //show user selected path
            try
            {
                fileList.Items.Clear();
                
                foreach (string command in Directory.GetFiles(SelectedImagePath.Substring(9) , "*.mp3"))
                {
                    //load commands by OS compatibility
                    ListBoxItem item = new ListBoxItem();
                    SongList songData = new SongList();
                    TagLib.File tagFile = TagLib.File.Create(command);
                    songData.SongName = tagFile.Tag.Title;
                    songData.ArtistName = "Album : "+tagFile.Tag.Album+"   Artist : "+tagFile.Tag.FirstAlbumArtist;
                    songData.SongPath = command;
                    try
                    {
                        TagLib.IPicture pic = tagFile.Tag.Pictures[0];
                        MemoryStream ms = new MemoryStream(pic.Data.Data);
                        ms.Seek(0, SeekOrigin.Begin);

                        // ImageSource for System.Windows.Controls.Image
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = ms;
                        bitmap.EndInit();

                        songData.AlbumArt = bitmap;
                    }
                    catch (Exception exe)
                    { }

                    item.Content = songData;
                    fileList.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            //MessageBox.Show(SelectedImagePath.Substring(9));
        }

        private void foldersItem_Loaded(object sender, RoutedEventArgs e)
        {
            TreeViewItem item1 = new TreeViewItem();
            item1.Header = "Computer";
            item1.Foreground = Brushes.Red;
            item1.IsExpanded = true;
            foldersItem.Items.Add(item1);
            foreach (string s in Directory.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem();
                item.Header = s;
                item.Tag = s;
                item.FontWeight = FontWeights.Normal;
                item.Foreground = Brushes.Red;
                item.Items.Add(dummyNode);                
                item.Expanded += new RoutedEventHandler(folder_Expanded);
                item1.Items.Add(item);

            }
            item1 = new TreeViewItem();
            item1.Header = "Cloud";
            item1.Foreground = Brushes.Red;
            item1.IsExpanded = false;
            foldersItem.Items.Add(item1);
        }

        private void fileList_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }


        private void fileList_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if (e.LeftButton == MouseButtonState.Pressed &&
                   (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    // Get the dragged ListViewItem
                    ListBox listView = sender as ListBox;
                    ListBoxItem listViewItem =
                        FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);

                    // Find the data behind the ListViewItem
                    SongList contact = (SongList)listViewItem.Content;
                    /*listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem);*/

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("myFormat", contact);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
            }
            catch (Exception exe)
            { }
        }

        private static T FindAnchestor<T>(DependencyObject current)
    where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private Point startPoint;

        private void Border_DragEnter_1(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("myFormat")  ||
        sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
            if (!e.Data.GetDataPresent("cloudFormat") ||
        sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void Border_Drop_1(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                SongList contact = e.Data.GetData("myFormat") as SongList;

                if (contact.SongPath.Length>0)
                {
                    isPlay1 = false;
                    BassEngine.Instance.OpenFile(contact.SongPath);
                    BassEngine.Instance.SetVolume((float)((1 - crossFeeder.Value) * volumeSlider1.Value));
                    TagLib.File tagFile = TagLib.File.Create(contact.SongPath);
                    string artist = tagFile.Tag.FirstAlbumArtist;
                    string album = tagFile.Tag.Album;
                    string title = tagFile.Tag.Title;
                    songName1.Text = title;
                    albumName1.Text = artist + ", " + album;
                    if (isPlay1 == false)
                    {
                        playButton1.Source = play_white;
                    }
                    try
                    {
                        redTimer.Stop();
                    }
                    catch (Exception exe)
                    { }
                    //fileSelectedFlag1 = true;
                    //Calculate_BPM1();
                }
            }
            else if (e.Data.GetDataPresent("cloudFormat"))
            {
                string contact = e.Data.GetData("cloudFormat") as string;
                deck1File = contact;
                c.DownloadThread(contact);
                left = true;
            }
        }

        private void Border_Drop_2(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                SongList contact = e.Data.GetData("myFormat") as SongList;

                if (contact.SongPath.Length >0)
                {
                    isPlay2 = false;
                    BassEngine1.Instance.OpenFile(contact.SongPath);
                    BassEngine1.Instance.SetVolume((float)(crossFeeder.Value * volumeSlider2.Value));
                    TagLib.File tagFile = TagLib.File.Create(contact.SongPath);
                    string artist = tagFile.Tag.FirstAlbumArtist;
                    string album = tagFile.Tag.Album;
                    string title = tagFile.Tag.Title;
                    songName2.Text = title;
                    albumName2.Text = artist + ", " + album;
                    if (isPlay2 == false)
                    {
                        playButton2.Source = play_white;
                    }
                    try
                    {
                        blueTimer.Stop();
                    }
                    catch (Exception exe)
                    { }
                    //Calculate_BPM2();
                }
            }
            else if (e.Data.GetDataPresent("cloudFormat"))
            {
                string contact = e.Data.GetData("cloudFormat") as string;
                c.DownloadThread(contact);
                left = false;
                deck2File = contact;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            {
                saveFileDialog.Filter = "WAV Sounds|*.wav";
                saveFileDialog.OverwritePrompt = true;
                if (saveFileDialog.ShowDialog() == true)
                {
                    string saveCommand = string.Format("save recsound {0}",
                        saveFileDialog.FileName);
                    mciSendString(saveCommand, "", 0, 0);
                    mciSendString("close recsound", "", 0, 0);
                }
            }

        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                redTimer.Stop();
            }
            catch (Exception exe)
            { }
            System.Windows.Input.Mouse.Capture(redDisc);
        }

        private void Image_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            scratchDisc1.Stop();
            scratchFlag = false;
            try
            {
                if (isPlay1 == true)
                {
                    redTimer.Start();
                }
            }
            catch (Exception exe)
            { }
            System.Windows.Input.Mouse.Capture(null);
        }
        private bool scratchFlag=false;
        private bool scratchFlag1 = false;
        private void Image_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (System.Windows.Input.Mouse.Captured == redDisc)
            {
                // Get the current mouse position relative to the volume control
                Point currentLocation = System.Windows.Input.Mouse.GetPosition(redDiscGrid);

                // We want to rotate around the center of the knob, not the top corner
                Point knobCenter = new Point(redDiscGrid.ActualHeight/2, redDiscGrid.ActualWidth / 2);

                // Calculate an angle
                double radians = Math.Atan((currentLocation.Y - knobCenter.Y) /
                                           (currentLocation.X - knobCenter.X));
                double angle=radians * 180 / Math.PI;
                redDisc.RenderTransform = new RotateTransform(angle);
                redAngle = angle;
                Console.WriteLine(angle);
                // Apply a 180 degree shift when X is negative so that we can rotate
                // all of the way around
                if (currentLocation.X - knobCenter.X < 0)
                {
                    redDisc.RenderTransform = new RotateTransform(angle+180);
                    redAngle = angle + 180;
                }
                if (scratchFlag == false)
                {
                    scratchDisc1.Source = new Uri(@"Sounds\scratch.wav", UriKind.Relative);
                    scratchDisc1.Play();
                }
                scratchFlag = true;
            }
        }

        private void blueDisc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                blueTimer.Stop();
            }
            catch (Exception exe)
            { }
            System.Windows.Input.Mouse.Capture(blueDisc);
        }

        private void blueDisc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scratchDisc2.Stop();
            scratchFlag1 = false;
            try
            {
                if (isPlay2 == true)
                {
                    blueTimer.Start();
                }
            }
            catch (Exception exe)
            { }
            System.Windows.Input.Mouse.Capture(null);
        }

        private void blueDisc_MouseMove(object sender, MouseEventArgs e)
        {
            if (System.Windows.Input.Mouse.Captured == blueDisc)
            {
                // Get the current mouse position relative to the volume control
                Point currentLocation = System.Windows.Input.Mouse.GetPosition(blueDiscGrid);

                // We want to rotate around the center of the knob, not the top corner
                Point knobCenter = new Point(blueDiscGrid.ActualHeight / 2, blueDiscGrid.ActualWidth / 2);

                // Calculate an angle
                double radians = Math.Atan((currentLocation.Y - knobCenter.Y) /
                                           (currentLocation.X - knobCenter.X));
                double angle = radians * 180 / Math.PI;
                blueDisc.RenderTransform = new RotateTransform(angle);
                blueAngle = angle;
                Console.WriteLine(angle);
                // Apply a 180 degree shift when X is negative so that we can rotate
                // all of the way around
                if (currentLocation.X - knobCenter.X < 0)
                {
                    blueDisc.RenderTransform = new RotateTransform(angle + 180);
                    blueAngle = angle + 180;
                }
                if (scratchFlag1 == false)
                {
                    scratchDisc2.Source = new Uri(@"Sounds\scratch.wav", UriKind.Relative);
                    scratchDisc2.Play();
                }
                scratchFlag1 = true;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (isPlay1 == true)
            {
                if (cueFlag[0] == false)
                {
                    cueValue[0] = BassEngine.Instance.ChannelPosition;
                    cue1.Background = Brushes.Red;
                    cueFlag[0] = true;
                }
                else
                {
                    BassEngine.Instance.ChannelPosition = cueValue[0];
                }
            }
            if (cueFlag[0] == true)
            {
                cueSelected = 1;
            }

        }

        private void cue2_Click(object sender, RoutedEventArgs e)
        {
            if (isPlay1 == true && cueFlag[0]==true)
            {
                if (cueFlag[1] == false)
                {
                    cueValue[1] = BassEngine.Instance.ChannelPosition;
                    cue2.Background = Brushes.Red;
                    cueFlag[1] = true;
                }
                else
                {
                    BassEngine.Instance.ChannelPosition = cueValue[1];
                }
            }
            if (cueFlag[1] == true)
            {
                cueSelected = 2;
            }
        }

        private void cue3_Click(object sender, RoutedEventArgs e)
        {
            if (isPlay1 == true && cueFlag[0] == true && cueFlag[1] == true)
            {
                if (cueFlag[2] == false)
                {
                    cueValue[2] = BassEngine.Instance.ChannelPosition;
                    cue3.Background = Brushes.Red;
                    cueFlag[2] = true;
                }
                else
                {
                    BassEngine.Instance.ChannelPosition = cueValue[2];
                }
            }
            if (cueFlag[2] == true)
            {
                cueSelected = 3;
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                cueFlag[i] = false;
            }
            cue1.Background = Brushes.Gray;
            cue2.Background = Brushes.Gray;
            cue3.Background = Brushes.Gray;
        }

        private void cue1_MouseEnter(object sender, MouseEventArgs e)
        {
            //cue1.Background = Brushes.Red;
        }

        private void cue1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (cueFlag[0] == false)
            {
                //cue1.Background = Brushes.Gray;
            }
        }

        private void cue1_MouseMove(object sender, MouseEventArgs e)
        {
            //cue1.Background = Brushes.Red;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            for (int i = 3; i < 6; i++)
            {
                cueFlag[i] = false;
            }
            cue4.Background = Brushes.Gray;
            cue5.Background = Brushes.Gray;
            cue6.Background = Brushes.Gray;
        }

        private void cue4_Click(object sender, RoutedEventArgs e)
        {
            if (isPlay2 == true)
            {
                if (cueFlag[3] == false)
                {
                    cueValue[3] = BassEngine1.Instance.ChannelPosition;
                    cue4.Background = Brushes.Blue;
                    cueFlag[3] = true;
                }
                else
                {
                    BassEngine1.Instance.ChannelPosition = cueValue[3];
                }
            }
            if (cueFlag[3] == true)
            {
                cueSelected = 4;
            }
        }

        private void cue5_Click(object sender, RoutedEventArgs e)
        {
            if (isPlay2 == true && cueFlag[3] == true)
            {
                if (cueFlag[4] == false)
                {
                    cueValue[4] = BassEngine1.Instance.ChannelPosition;
                    cue5.Background = Brushes.Blue;
                    cueFlag[4] = true;
                }
                else
                {
                    BassEngine1.Instance.ChannelPosition = cueValue[4];
                }
            }
            if (cueFlag[4] == true)
            {
                cueSelected = 5;
            }
        }

        private void cue6_Click(object sender, RoutedEventArgs e)
        {
            if (isPlay2 == true && cueFlag[3] == true && cueFlag[4] == true)
            {
                if (cueFlag[5] == false)
                {
                    cueValue[5] = BassEngine1.Instance.ChannelPosition;
                    cue6.Background = Brushes.Blue;
                    cueFlag[5] = true;
                }
                else
                {
                    BassEngine1.Instance.ChannelPosition = cueValue[5];
                }
            }
            if (cueFlag[5] == true)
            {
                cueSelected = 6;
            }
        }

        private void ClearLoopButtons()
        {
            rLoop1.Background = Brushes.Gray;
            rLoop2.Background = Brushes.Gray;
            rLoop4.Background = Brushes.Gray;
            rLoop8.Background = Brushes.Gray;
            rLoop16.Background = Brushes.Gray;
            rLoop32.Background = Brushes.Gray;
        }

        private void ClearLoopButtons1()
        {
            bLoop1.Background = Brushes.Gray;
            bLoop2.Background = Brushes.Gray;
            bLoop4.Background = Brushes.Gray;
            bLoop8.Background = Brushes.Gray;
            bLoop16.Background = Brushes.Gray;
            bLoop32.Background = Brushes.Gray;
        }
        private void rLoop1_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition+1) / 60;
            repeatStopTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition+1) % 60;
            ClearLoopButtons(); 
            rLoop1.Background = Brushes.Red;
            
        }

        private void rLoop2_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition + 2) / 60;
            repeatStopTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition + 2) % 60;
            ClearLoopButtons(); 
            rLoop2.Background = Brushes.Red;
            
        }

        private void rLoop4_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition + 4) / 60;
            repeatStopTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition + 4) % 60;
            ClearLoopButtons(); 
            rLoop4.Background = Brushes.Red;
            
        }

        private void rLoop8_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition + 8) / 60;
            repeatStopTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition + 8) % 60;
            ClearLoopButtons(); 
            rLoop8.Background = Brushes.Red;
            
        }

        private void rLoop16_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition + 16) / 60;
            repeatStopTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition + 16) % 60;
            ClearLoopButtons(); 
            rLoop16.Background = Brushes.Red;
            
        }

        private void rLoop32_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit.Minutes = Convert.ToInt32(BassEngine.Instance.ChannelPosition + 32) / 60;
            repeatStopTimeEdit.Seconds = Convert.ToInt32(BassEngine.Instance.ChannelPosition + 32) % 60;
            ClearLoopButtons(); 
            rLoop32.Background = Brushes.Red;
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ClearLoopButtons();
            repeatStopTimeEdit.Minutes = 0;
            repeatStopTimeEdit.Seconds = 0;
        }

        private void bLoop1_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 1) / 60;
            repeatStopTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 1) % 60;
            ClearLoopButtons1();
            bLoop1.Background = Brushes.Blue;
        }

        private void bLoop2_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 2) / 60;
            repeatStopTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 2) % 60;
            ClearLoopButtons1();
            bLoop2.Background = Brushes.Blue;
        }

        private void bLoop4_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 4) / 60;
            repeatStopTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 4) % 60;
            ClearLoopButtons1();
            bLoop4.Background = Brushes.Blue;
        }

        private void bLoop8_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 8) / 60;
            repeatStopTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 8) % 60;
            ClearLoopButtons1();
            bLoop8.Background = Brushes.Blue;
        }

        private void bLoop16_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 16) / 60;
            repeatStopTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 16) % 60;
            ClearLoopButtons1();
            bLoop16.Background = Brushes.Blue;
        }

        private void bLoop32_Click(object sender, RoutedEventArgs e)
        {
            repeatStartTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) / 60;
            repeatStartTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition) % 60;
            repeatStopTimeEdit1.Minutes = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 32) / 60;
            repeatStopTimeEdit1.Seconds = Convert.ToInt32(BassEngine1.Instance.ChannelPosition + 32) % 60;
            ClearLoopButtons1();
            bLoop32.Background = Brushes.Blue;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ClearLoopButtons1();
            repeatStopTimeEdit1.Minutes = 0;
            repeatStopTimeEdit1.Seconds = 0;
        }

        private void recordButton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (recordFlag == false)
            {
                recordButton.Source = record_red_light;
            }
            else
            {
                recordButton.Source = save_blue_light;
            }
        }

        private void recordButton_MouseLeave(object sender, MouseEventArgs e)
        {
            if (recordFlag == false)
            {
                recordButton.Source = record_red;
            }
            else
            {
                recordButton.Source = save_blue;
            }
        }

        private void recordButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (recordFlag == false && (isPlay1 == true || isPlay2 == true))
            {
                recordButton.Source = save_blue;
            }
            else
            {
                recordButton.Source = record_red;
            }
        }

        private void recordButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (isPlay1 == true || isPlay2 == true)
            {
                if (recordFlag == false)
                {
                    recordButton.Source = save_blue;
                    recordFlag = true;
                    mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
                    mciSendString("record recsound", "", 0, 0);
                }
                else
                {
                    recordButton.Source = record_red;
                    recordFlag = false;
                    SaveFileDialog sf = new SaveFileDialog();
                    sf.Filter = "(*.mp3, *.m4a)|*.mp3;*.m4a";
                    if (sf.ShowDialog() == true)
                    {
                        string command = "save recsound " + sf.FileName;
                        mciSendString(command, "", 0, 0);
                        mciSendString("close recsound ", "", 0, 0);
                    }
                }
            }
        }

        private void Rectangle_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            LoadDefaultTheme();
        }

        private void Rectangle_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            LoadExpressionLightTheme();
        }

        private void Rectangle_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            LoadExpressionDarkTheme();
        }

        private void effectCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BassEngine.Instance.PlayEffects(effectCombo.SelectedIndex);
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            if (tb3.Text == "" || pb1.Password == "")
            {
                messageLabel.Visibility = Visibility.Visible;
            }
            else if (!(MainWindow.c.MatchUname(tb3.Text)))
            {
                messageLabel.Visibility = Visibility.Visible;

            }
            else if (!(MainWindow.c.MatchPassword(tb3.Text, pb1.Password)))
            {
                messageLabel.Visibility = Visibility.Visible;
            }
            else
            {
                //this.Visibility = Visibility.Hidden;

                c.Connect("leap", "JNY/5fguNeNTyktSAR9gnbUN7G9tks3Ec9ic0H0SCy01DJDAuB7UHop0Mrp0rDWCaBiNcq2SM0lCrQnXUNSaRg==", tb3.Text);
                cloudGrid.Visibility = Visibility.Hidden;
                fileList.Visibility = Visibility.Hidden;
                uploadGrid.Visibility = Visibility.Visible;
                cloudList.Visibility = Visibility.Visible;
                cloudList.Items.Clear();
                foreach (var blobItem in Cloud.container.ListBlobs())
                {
                    cloudList.Items.Add(blobItem.Uri.ToString().Split('/')[blobItem.Uri.ToString().Split('/').Length - 1]);
                }
                
                //createWindow();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            tb3.Text = "";
            pb1.Password = "";
        }

        private void cloudList_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Point mousePos = e.GetPosition(null);
                Vector diff = startPoint - mousePos;

                if (e.LeftButton == MouseButtonState.Pressed &&
                   (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    // Get the dragged ListViewItem
                    ListBox listView = sender as ListBox;
                    ListBoxItem listViewItem =
                        FindAnchestor<ListBoxItem>((DependencyObject)e.OriginalSource);

                    // Find the data behind the ListViewItem
                    string contact = (string)listViewItem.Content;
                    /*listView.ItemContainerGenerator.
                        ItemFromContainer(listViewItem);*/

                    // Initialize the drag & drop operation
                    DataObject dragData = new DataObject("cloudFormat", contact);
                    DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                }
            }
            catch (Exception exe)
            { }
        }

        private void effectCombo1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BassEngine1.Instance.PlayEffects(effectCombo1.SelectedIndex);
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.Filter = "(*.mp3, *.m4a)|*.mp3;*.m4a";
            string name, path;

            if (openDialog.ShowDialog() == true)
            {
                path = openDialog.FileName;
                name = path.Split('\\')[path.Split('\\').Length - 1];
                c.UploadThread(name, path);
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            cloudList.Items.Clear();
            foreach (var blobItem in Cloud.container.ListBlobs())
            {
                cloudList.Items.Add(blobItem.Uri.ToString().Split('/')[blobItem.Uri.ToString().Split('/').Length - 1]);
            }
        }

        private void Refresh_Copy_Click(object sender, RoutedEventArgs e)
        {
            c.DeleteBlob(cloudList.SelectedItem.ToString());
        }


     

        

        


    }
}
