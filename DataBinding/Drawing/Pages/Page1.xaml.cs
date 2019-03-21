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
using BLL.Astar;
using System.Data;

namespace Drawing.Pages
{
    /// <summary>
    /// Page1.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            
        }

        private List<Path> pathPaths = new List<Path>();
        private List<Path> nodePaths = new List<Path>();

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Graph graph = new Graph();
            DataTable dt = graph.DS.Tables["PathInfo"];
            DataTable dtNode = graph.DS.Tables["NodeInfo"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Path path = new Path();
                double bnX = (Convert.ToDouble(dt.Rows[i][2])) / 200;
                double bnY = (Convert.ToDouble(dt.Rows[i][3])) / 200;
                double enX = (Convert.ToDouble(dt.Rows[i][5])) / 200;
                double enY = (Convert.ToDouble(dt.Rows[i][6])) / 200;
                Point sp = new Point(bnX, bnY);
                Point ep = new Point(enX, enY);
                LineGeometry line = new LineGeometry(sp, ep);
                path.Data = line;
                path.Name = "路径" + dt.Rows[i][0].ToString();
                path.MouseDown += Path_MouseDown;
                pathPaths.Add(path);
                canvas1.Children.Add(path);
            }

            for (int i = 0; i < dtNode.Rows.Count; i++)
            {
                Path path = new Path();
                path.Fill = Brushes.LightBlue;
                double X = (Convert.ToDouble(dtNode.Rows[i][1])) / 200;
                double Y = (Convert.ToDouble(dtNode.Rows[i][2])) / 200;
                Point p = new Point(X, Y);
                //RadioButton radiobtn = new RadioButton();
                //radiobtn.PointToScreen(p);
                //canvas1.Children.Add(radiobtn);
                EllipseGeometry ellipse = new EllipseGeometry(p, 10, 10);
                path.Data = ellipse;
                path.Name = "节点" + dt.Rows[i][0].ToString();
                path.MouseDown += Path_MouseDown;
                pathPaths.Add(path);
                canvas1.Children.Add(path);
            }
            grid1.ItemsSource = dt.DefaultView;
        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Path path = sender as Path;
            if(path.Stroke == Brushes.Red)
            {
                path.Stroke = Brushes.Black;
                txtblockSelectedName.Text = path.Name;
            }
            else
            {
                path.Stroke = Brushes.Red;
                txtblockSelectedName.Text = path.Name;
            }
        }
    }
}
