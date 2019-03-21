// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/3/21 12:03:20
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Drawing.Pages
{
    public class DrawingCanvas : Canvas
    {
        private List<Visual> visuals = new List<Visual>();

        protected override int VisualChildrenCount
        {
            get
            {
                return visuals.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return visuals[index];
        }

        public void AddVisual(Visual visual)
        {
            visuals.Add(visual);

            base.AddLogicalChild(visual);
            base.AddVisualChild(visual);
        }

        public void RemoveVisual(Visual visual)
        {
            visuals.Remove(visual);

            base.RemoveLogicalChild(visual);
            base.RemoveVisualChild(visual);
        }

        public DrawingVisual GetVisual(Point p)
        {
            HitTestResult hitResult = VisualTreeHelper.HitTest(this, p);
            return hitResult.VisualHit as DrawingVisual;
        }

        public void ClearAllVisuals()
        {
            visuals.Clear();
        }
    }
}
