// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/3/14 21:39:24
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public delegate void GreetingDelegate(string name);

    class GreetingManager
    {
        public event GreetingDelegate MakeGreet;

        public void GreetingPeople(string name)
        {
            MakeGreet?.Invoke(name);
        }
    }
}
