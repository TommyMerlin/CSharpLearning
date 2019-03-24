using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Data;
using BLL.Astar;

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
        private Path BeginNode;
        private Path EndNode;
        private StoreHeap NodeList = new StoreHeap();
        private List<Path> finalPaths = new List<Path>();
        

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
                path.Name = "路径_" + dt.Rows[i][0].ToString();
                path.ToolTip = "路径" + dt.Rows[i][0].ToString();
                //path.MouseDown += Path_MouseDown;
                pathPaths.Add(path);
                canvas1.Children.Add(path);
            }

            for (int i = 0; i < dtNode.Rows.Count; i++)
            {
                Path path = new Path
                {
                    Fill = Brushes.LightBlue
                };
                double X = (Convert.ToDouble(dtNode.Rows[i][1])) / 200;
                double Y = (Convert.ToDouble(dtNode.Rows[i][2])) / 200;
                Point p = new Point(X, Y);
                //RadioButton radiobtn = new RadioButton();
                //radiobtn.PointToScreen(p);
                //canvas1.Children.Add(radiobtn);
                EllipseGeometry ellipse = new EllipseGeometry(p, 10, 10);
                path.Data = ellipse;
                path.Name = "节点_" + dt.Rows[i][0].ToString();
                path.ToolTip = "节点" + dt.Rows[i][0].ToString();
                path.MouseDown += Path_MouseDown;
                pathPaths.Add(path);
                canvas1.Children.Add(path);
            }
        }

        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (cmdSelectBeginNode.IsChecked == true)
            {
                if (BeginNode != null)
                {
                    BeginNode.Stroke = Brushes.Black;
                    BeginNode.Fill = Brushes.LightBlue;
                }
                Path path = sender as Path;
                path.Stroke = Brushes.Red;
                path.Fill = Brushes.Red;
                BeginNode = path;
                txtblockStartNode.Text = path.Name.Split('_')[1];
            }
            else if(cmdSelectEndNode.IsChecked == true)
            {
                if (EndNode != null)
                {
                    EndNode.Stroke = Brushes.Black;
                    EndNode.Fill = Brushes.LightBlue;
                }
                Path path = sender as Path;
                path.Stroke = Brushes.Green;
                path.Fill = Brushes.Green;
                EndNode = path;
                txtblockGoalNode.Text = path.Name.Split('_')[1];
            }
        }

        private void BtnPathPlanning_Click(object sender, RoutedEventArgs e)
        {
            Graph graph = new Graph();
            AStar aStar = new AStar(graph);
            int snId = Convert.ToInt32(txtblockStartNode.Text);
            int gnId = Convert.ToInt32(txtblockGoalNode.Text);
            txtblockFinalPath.Text = aStar.GetFinalPath(snId, gnId);
            StoreHeap heap = aStar.PathPanning(snId, gnId);
            if(finalPaths.Count > 0)
            {
                foreach(var p in finalPaths)
                {
                    canvas1.Children.Remove(p);
                }
                finalPaths.Clear();
            }
            NodeList = heap;

            for(int i = 0; i < NodeList.Count-1; i++)
            {
                Path path = new Path();
                double bnX = (Convert.ToDouble(NodeList.NodeList[i].X)) / 200;
                double bnY = (Convert.ToDouble(NodeList.NodeList[i].Y)) / 200;
                double enX = (Convert.ToDouble(NodeList.NodeList[i+1].X)) / 200;
                double enY = (Convert.ToDouble(NodeList.NodeList[i+1].Y)) / 200;
                Point sp = new Point(bnX, bnY);
                Point ep = new Point(enX, enY);
                LineGeometry line = new LineGeometry(sp, ep);
                path.Data = line;
                path.Stroke = Brushes.Orange;
                path.Fill = Brushes.Orange;
                finalPaths.Add(path);
                canvas1.Children.Add(path);
            }
        }
    }
}
