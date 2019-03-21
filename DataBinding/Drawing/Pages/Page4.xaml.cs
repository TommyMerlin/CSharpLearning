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
    /// Page4.xaml 的交互逻辑
    /// </summary>
    public partial class Page4 : Page
    {
        private Pen pen = new Pen(Brushes.Black, 3);

        private DrawingVisual drawedBeginNode;
        private DrawingVisual drawedEndNode;

        public Page4()
        {
            InitializeComponent();
            LoadNode();
        }

        private void LoadNode()
        {
            Graph graph = new Graph();
            DataTable dt = graph.DS.Tables["PathInfo"];
            DataTable dtNode = graph.DS.Tables["NodeInfo"];

            for (int i = 0; i < dtNode.Rows.Count; i++)
            {
                double X = (Convert.ToDouble(dtNode.Rows[i][1])) / 200;
                double Y = (Convert.ToDouble(dtNode.Rows[i][2])) / 200;
                Point p = new Point(X, Y);

                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext dc = visual.RenderOpen())
                {
                    Geometry geo = new EllipseGeometry(p, 10, 10);
                    dc.DrawGeometry(Brushes.AliceBlue, pen, geo);
                }
                drawingSurface.AddVisual(visual);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double bnX = (Convert.ToDouble(dt.Rows[i][2])) / 200;
                double bnY = (Convert.ToDouble(dt.Rows[i][3])) / 200;
                double enX = (Convert.ToDouble(dt.Rows[i][5])) / 200;
                double enY = (Convert.ToDouble(dt.Rows[i][6])) / 200;
                Point sp = new Point(bnX, bnY);
                Point ep = new Point(enX, enY);

                DrawingVisual visual = new DrawingVisual();
                using (DrawingContext dc = visual.RenderOpen())
                {
                    Geometry geo = new LineGeometry(sp, ep);
                    
                    dc.DrawGeometry(Brushes.AliceBlue, pen, geo);
                }
                drawingSurface.AddVisual(visual);
            }
        }

        private void DrawingSurface_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(drawingSurface);

            
            if(cmdSelectBeginNode.IsChecked == true)
            {
                DrawingVisual visual = drawingSurface.GetVisual(p);
                if (visual != null)
                {
                    Point topLeft = new Point(visual.ContentBounds.TopLeft.X + 10.0 + pen.Thickness/2, visual.ContentBounds.TopLeft.Y + 10.0 + pen.Thickness / 2);
                    DrawBeginNode(topLeft);
                }
            }
            else if(cmdSelectEndNode.IsChecked == true)
            {
                DrawingVisual visual = drawingSurface.GetVisual(p);
                if (visual != null)
                {
                    Point topLeft = new Point(visual.ContentBounds.TopLeft.X + 10.0 + pen.Thickness / 2, visual.ContentBounds.TopLeft.Y + 10.0 + pen.Thickness / 2);
                    DrawEndNode(topLeft);
                }
            }
        }

        /// <summary>
        /// 绘制选定的初始点
        /// </summary>
        /// <param name="p">初始点位置</param>
        public void DrawBeginNode(Point p)
        {
            if (drawedBeginNode != null)
            {
                drawingSurface.RemoveVisual(drawedBeginNode);
            }
            drawedBeginNode = new DrawingVisual();
            using(DrawingContext dc = drawedBeginNode.RenderOpen())
            {
                Geometry g = new EllipseGeometry(p, 10, 10);
                dc.DrawGeometry(Brushes.Red, pen, g);
            }
            drawingSurface.AddVisual(drawedBeginNode);
        }

        /// <summary>
        /// 绘制选定的目标点
        /// </summary>
        /// <param name="p">目标点位置</param>
        public void DrawEndNode(Point p)
        {
            if (drawedEndNode != null)
            {
                drawingSurface.RemoveVisual(drawedEndNode);
            }
            drawedEndNode = new DrawingVisual();
            using (DrawingContext dc = drawedEndNode.RenderOpen())
            {
                Geometry g = new EllipseGeometry(p, 10, 10);
                dc.DrawGeometry(Brushes.Green, pen, g);
            }
            drawingSurface.AddVisual(drawedEndNode);
        }
    }
}
