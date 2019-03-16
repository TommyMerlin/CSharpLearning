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
            Graph graph = new Graph();
            DataTable dt = graph.DS.Tables["PathInfo"];
            DataTable dtNode = graph.DS.Tables["NodeInfo"];
            Path path = new Path();
            path.Stroke = Brushes.Black;
            path.StrokeThickness = 3;
            GeometryGroup g = new GeometryGroup();
            //foreach (DataRow row in dt.Rows)
            //{
            //    Point sp = new Point((double)row["bnX"], (double)row["bnY"]);
            //    Point ep = new Point((double)row["enX"], (double)row["enX"]);
            //    LineGeometry line = new LineGeometry(sp, ep);
            //    g.Children.Add(line);
            //}

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double bnX = (Convert.ToDouble(dt.Rows[i][0])) / 200;
                double bnY = (Convert.ToDouble(dt.Rows[i][1])) / 200;
                double enX = (Convert.ToDouble(dt.Rows[i][2])) / 200;
                double enY = (Convert.ToDouble(dt.Rows[i][3])) / 200;
                Point sp = new Point(bnX, bnY);
                Point ep = new Point(enX, enY);
                LineGeometry line = new LineGeometry(sp, ep);
                g.Children.Add(line);
            }

            for (int i = 0; i < dtNode.Rows.Count; i++)
            {
                double X = (Convert.ToDouble(dtNode.Rows[i][1])) / 200;
                double Y = (Convert.ToDouble(dtNode.Rows[i][2])) / 200;
                Point p = new Point(X, Y);
                EllipseGeometry ellipse = new EllipseGeometry(p, 10, 10);
                g.Children.Add(ellipse);
            }

            path.Data = g;
            canvas1.Children.Add(path);
            grid1.ItemsSource = dt.DefaultView;
            //MessageBox.Show(((double)dt.Rows[0][0]).ToString());
        }
    }
}
