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

namespace GridGame
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        Random random = new Random();
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 10; i++)
            {
                ColumnDefinition colDef = new ColumnDefinition();
                RowDefinition rowDef = new RowDefinition();
                GridGame.ColumnDefinitions.Add(colDef);
                GridGame.RowDefinitions.Add(rowDef);
            }
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 10; j++)
                {
                    //Button btn = new Button();
                    //btn.Content = i + "," + j;
                    //Grid.SetRow(btn, i);
                    //Grid.SetColumn(btn, j);
                    //GridGame.Children.Add(btn);

                    Image img = new Image();
                    int imgName = random.Next(1, 8);
                    img.Source = new BitmapImage(new Uri("Images/"+imgName+".jpg", UriKind.Relative));
                    Grid.SetColumn(img, j);
                    Grid.SetRow(img, i);
                    GridGame.Children.Add(img);
                }
            }
        }
    }
}
