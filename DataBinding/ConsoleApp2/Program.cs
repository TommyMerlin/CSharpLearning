// ***********************************************************************
// Author     ：HuYe
// Function   ：
// CreateTime ：2019/3/16 21:20:35
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Astar;
using System.Data;

namespace Drawing
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph();
            DataTable dt = graph.DS.Tables["PathInfo"];
            DataTable dtNode = graph.DS.Tables["NodeInfo"];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                double bnX = (Convert.ToDouble(dt.Rows[i][0])) / 100;
                double bnY = (Convert.ToDouble(dt.Rows[i][1])) / 100;
                double enX = (Convert.ToDouble(dt.Rows[i][2])) / 100;
                double enY = (Convert.ToDouble(dt.Rows[i][3])) / 100;

                //double bnX = Convert.ToDouble(dt.Rows[i][0]);
                //double bnY = Convert.ToDouble(dt.Rows[i][1]);
                //double enX = Convert.ToDouble(dt.Rows[i][2]);
                //double enY = Convert.ToDouble(dt.Rows[i][3]);


                Console.WriteLine("{0,-10}{1,-10}{2,-10}{3,-10}", bnX, bnY, enX, enY);
            }

            for (int i = 0; i < dtNode.Rows.Count; i++)
            {
                double X = (Convert.ToDouble(dtNode.Rows[i][1])) / 100;
                double Y = (Convert.ToDouble(dtNode.Rows[i][2])) / 100;

                //double X = Convert.ToDouble(dt.Rows[i][1]);
                //double Y = Convert.ToDouble(dt.Rows[i][2]);

                Console.WriteLine("{0,-10}{1,-10}", X, Y);
            }

            Console.Read();
        }
    }
}
