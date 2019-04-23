// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/4/22 13:48:00
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DependencyLearning
{
    public class Student : DependencyObject
    {
        public static readonly DependencyProperty NameProperty = 
            DependencyProperty.Register("Name", typeof(string), typeof(Student));

        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        public BindingExpressionBase SetBingding(DependencyProperty dp,BindingBase binding)
        {
            return BindingOperations.SetBinding(this, dp, binding);
        }

        public int Age
        {
            get { return (int)GetValue(AgeProperty); }
            set { SetValue(AgeProperty, value); }
        }

        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.Register("Age", typeof(int), typeof(Student),new PropertyMetadata(10));
    }

    public class School : DependencyObject
    {


        public static int GetGrade(DependencyObject obj)
        {
            return (int)obj.GetValue(GradeProperty);
        }

        public static void SetGrade(DependencyObject obj, int value)
        {
            obj.SetValue(GradeProperty, value);
        }

        // Using a DependencyProperty as the backing store for ClassName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GradeProperty =
            DependencyProperty.RegisterAttached("Grade", typeof(int), typeof(School), new PropertyMetadata(0));


    }

    public class Human : Button
    {
        
    }
}
