using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using WPFSoundVisualizationLib;
using System.Windows.Data;

namespace Sample_BASS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Un4seen.Bass.BassNet.Registration("technoapurva@gmail.com", "2X1823322152222");
            InitializeComponent();
            
            BassEngine bassEngine = BassEngine.Instance;
            BassEngine1 bassEngine1 = BassEngine1.Instance;

            bassEngine.PropertyChanged += BassEngine_PropertyChanged;
            UIHelper.Bind(bassEngine, "CanStop", StopButton, Button.IsEnabledProperty);
            UIHelper.Bind(bassEngine, "CanPlay", PlayButton, Button.IsEnabledProperty);
            UIHelper.Bind(bassEngine, "CanPause", PauseButton, Button.IsEnabledProperty);
            UIHelper.Bind(bassEngine, "SelectionBegin", repeatStartTimeEdit, TimeEditor.ValueProperty, BindingMode.TwoWay);
            UIHelper.Bind(bassEngine, "SelectionEnd", repeatStopTimeEdit, TimeEditor.ValueProperty, BindingMode.TwoWay);

            bassEngine1.PropertyChanged += BassEngine_PropertyChanged1;
            UIHelper.Bind(bassEngine1, "CanStop1", StopButton1, Button.IsEnabledProperty);
            UIHelper.Bind(bassEngine1, "CanPlay1", PlayButton1, Button.IsEnabledProperty);
            UIHelper.Bind(bassEngine1, "CanPause1", PauseButton1, Button.IsEnabledProperty);
            UIHelper.Bind(bassEngine1, "SelectionBegin1", repeatStartTimeEdit1, TimeEditor.ValueProperty, BindingMode.TwoWay);
            UIHelper.Bind(bassEngine1, "SelectionEnd1", repeatStopTimeEdit1, TimeEditor.ValueProperty, BindingMode.TwoWay);            



            spectrumAnalyzer.RegisterSoundPlayer(bassEngine);
            waveformTimeline.RegisterSoundPlayer(bassEngine);
            

            spectrumAnalyzer1.RegisterSoundPlayer(bassEngine1);
            waveformTimeline1.RegisterSoundPlayer(bassEngine1);

            LoadExpressionDarkTheme();
        }

        #region Bass Engine Events

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
                                    albumArtPanel1.AlbumArtImage = albumImage;
                                }
                                catch (NotSupportedException)
                                {
                                    albumArtPanel1.AlbumArtImage = null;
                                    // System.NotSupportedException:
                                    // No imaging component suitable to complete this operation was found.
                                }
                                albumArtworkMemStream.Close();
                            }
                        }
                        else
                        {
                            albumArtPanel1.AlbumArtImage = null;
                        }
                    }
                    else
                    {
                        albumArtPanel1.AlbumArtImage = null;
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
                                    albumArtPanel.AlbumArtImage = albumImage;
                                }
                                catch (NotSupportedException)
                                {
                                    albumArtPanel.AlbumArtImage = null;
                                    // System.NotSupportedException:
                                    // No imaging component suitable to complete this operation was found.
                                }
                                albumArtworkMemStream.Close();
                            }
                        }
                        else
                        {
                            albumArtPanel.AlbumArtImage = null;
                        }                        
                    }
                    else
                    {
                        albumArtPanel.AlbumArtImage = null;
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
            txt_BPM.Text = BassEngine.Instance.GetBPM().ToString();
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
            txt_BPM.Text = BassEngine.Instance.GetBPM().ToString();
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
            txt_BPM.Text = BassEngine.Instance.GetBPM().ToString();
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
            txt_BPM.Text = BassEngine.Instance.GetBPM().ToString();
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
                BassEngine.Instance.OpenFile(openDialog.FileName);
                FileText.Text = openDialog.FileName;
            }
        }

        private void OpenFile1()
        {
            Microsoft.Win32.OpenFileDialog openDialog = new Microsoft.Win32.OpenFileDialog();
            openDialog.Filter = "(*.mp3, *.m4a)|*.mp3;*.m4a";
            if (openDialog.ShowDialog() == true)
            {
                BassEngine1.Instance.OpenFile(openDialog.FileName);
                FileText1.Text = openDialog.FileName;
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
            BassEngine.Instance.SetVolume((float)(1 - crossFeeder.Value));
            BassEngine1.Instance.SetVolume((float)(crossFeeder.Value));
        }
    }
}
