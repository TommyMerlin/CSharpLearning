// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/4/23 9:04:31
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
    class ReportDateEventArgs : RoutedEventArgs
    {
        public DateTime ClickDate;

        public ReportDateEventArgs(RoutedEvent e,object source) : base(e, source) { }
    }

    class DateButton : Button
    {


        public event RoutedEventHandler ReportDate
        {
            add { AddHandler(ReportDateEvent, value); }
            remove { RemoveHandler(ReportDateEvent, value); }
        }

        public static readonly RoutedEvent ReportDateEvent = EventManager.RegisterRoutedEvent(
        "ReportDate", RoutingStrategy.Bubble, typeof(EventHandler<ReportDateEventArgs>), typeof(DateButton));

        protected override void OnClick()
        {
            base.OnClick();

            ReportDateEventArgs arg = new ReportDateEventArgs(ReportDateEvent, this)
            {
                ClickDate = DateTime.Now
            };
            RaiseEvent(arg);
        }

    }
}
