using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MySql.Data.MySqlClient;

namespace Practice2._1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MySqlCommand cmd = null;
        private MySqlConnection conn = null;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            btnSystemColor.Foreground = SystemColors.WindowTextBrush;
            timer.Tick += new EventHandler(timer_Tick);
            //timer.Interval = TimeSpan.FromSeconds(0.1);   //设置刷新的间隔时间
            timer.Start();
            txtbox1.Text = "ChangeLog:\nV1: Implement the function of Dijkstra Algorithm.\nV2: Add MySQL connection.\nV3.0: Rewrite the codes.";
        }

        void timer_Tick(object sender, EventArgs e)
        {
            Title = string.Concat("TimerWindow  ", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            double Seconds = Math.Round(sound1.Position.TotalSeconds, 0);
            int Minutes = (int)(Seconds/60);
            Seconds = Seconds - 60 * Minutes;
            txtblockBgmPosition.Text = Minutes.ToString() + " min " + Seconds.ToString() + " s";
        }

        private void Txtbox1_SelectionChanged(object sender, RoutedEventArgs e)
        {
            txtblock1.Text = txtbox1.SelectedText;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            txtblockSelectedDate.Text = calendar1.SelectedDate.ToString();
        }

        private void Datepicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            txtblockSelectedDate.Text = datepicker1.SelectedDate.ToString();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.showSomething();
        }

        public void showSomething()
        {
            MessageBox.Show("show something");
        }

        private void Btnbgm_Click(object sender, RoutedEventArgs e)
        {
            if(sound1.LoadedBehavior == MediaState.Pause || sound1.LoadedBehavior == MediaState.Stop)
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

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("New Command Triggered by " + e.Source.ToString());
        }

        private void BtnRandomName_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Random rand = new Random();
            int randnum = rand.Next(0, 255);
            btn.Content = randnum.ToString();
            btnStyle.Style = (Style)FindResource("BigFontButtonStyle");
            btn.Background = new SolidColorBrush(Color.FromRgb((byte)randnum,0,0));
        }

        private void BtnAddRecord_Click(object sender, RoutedEventArgs e)
        {
            string connstr = "Server = localhost; Database = agv; Uid = root; Pwd = zjuhuye";
            conn = new MySqlConnection(connstr);
            conn.Open();
            int id = int.Parse(txtbox_id.Text);
            int x = int.Parse(txtbox_x.Text);
            int y = int.Parse(txtbox_y.Text);
            string insert = $"insert into node(id,x,y)values({id},{x},{y})";
            cmd = new MySqlCommand(insert, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            txtblock_addinfo.Text = "id:" + id.ToString() + "x:" + x.ToString() + "y:" + y.ToString() + "has been added to the table(agv.node)";
        }

        private void element_MouseEnter(object sender,MouseEventArgs e)
        {
            ((TextBlock)sender).Background = new SolidColorBrush(Colors.Blue);
        }
        private void element_MouseLeave(object sender,MouseEventArgs e)
        {
            ((TextBlock)sender).Background = null;
        }
    }

}
