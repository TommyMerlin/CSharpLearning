// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/4/22 17:21:27
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DependencyLearning
{
    class ReportTimeEventArgs : RoutedEventArgs
    {
        public DateTime ClickTime { get; set; }

        public ReportTimeEventArgs(RoutedEvent routedEvent, object source) : base(routedEvent, source) { }
    }

    class TimeButton : Button
    {


        public event RoutedEventHandler ReportTime
        {
            add { AddHandler(ReportTimeEvent, value); }
            remove { RemoveHandler(ReportTimeEvent, value); }
        }

        public static readonly RoutedEvent ReportTimeEvent = EventManager.RegisterRoutedEvent(
        "ReportTime", RoutingStrategy.Bubble, typeof(EventHandler<ReportTimeEventArgs>), typeof(TimeButton));

        protected override void OnClick()
        {
            base.OnClick();

            ReportTimeEventArgs args = new ReportTimeEventArgs(ReportTimeEvent, this)
            {
                ClickTime = DateTime.Now
            };
            this.RaiseEvent(args);
        }

    }
}
