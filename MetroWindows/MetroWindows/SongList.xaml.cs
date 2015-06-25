using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MetroWindows
{
    /// <summary>
    /// Interaction logic for SongList.xaml
    /// </summary>
    public partial class SongList : UserControl
    {
        public SongList()
        {
            InitializeComponent();
        }

        public string SongName
        {
            get
            {
                return songName.Text;
            }
            set
            {
                songName.Text = value;
            }
        }

        public string ArtistName
        {
            get
            {
                return artistName.Text;
            }
            set
            {
                artistName.Text = value;
            }
        }

        public ImageSource AlbumArt
        {
            get
            {
                return null;
            }
            set
            {
                albumArt.Source = value;
            }
            
        }

        public string SongPath
        {
            get
            {
                return songPath.Text;
            }
            set
            {
                songPath.Text = value;
            }

        }
    }
}
