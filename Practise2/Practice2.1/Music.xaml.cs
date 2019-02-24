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

namespace Practice2._1
{
    /// <summary>
    /// Music.xaml 的交互逻辑
    /// </summary>
    public partial class Music : UserControl
    {
        public Music()
        {
            InitializeComponent();
        }
        private void Btnbgm_Click(object sender, RoutedEventArgs e)
        {
            if (sound1.LoadedBehavior == MediaState.Pause || sound1.LoadedBehavior == MediaState.Stop)
            {
                sound1.LoadedBehavior = MediaState.Play;
                imgbgm.Source = new BitmapImage(new Uri("Images/pause2.png", UriKind.Relative));
            }
            else
            {
                sound1.LoadedBehavior = MediaState.Pause;
                imgbgm.Source = new BitmapImage(new Uri("Images/start2.png", UriKind.Relative));
            }

        }

        private void BtnbgmStop_Click(object sender, RoutedEventArgs e)
        {
            sound1.LoadedBehavior = MediaState.Stop;
            imgbgm.Source = new BitmapImage(new Uri("Images/start2.png", UriKind.Relative));
        }
    }
}
