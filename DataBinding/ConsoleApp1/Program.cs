using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        public static void PrintN(int N)
        {
            if (N != 0)
            {
                PrintN(N - 1);
                Console.WriteLine(N);
            }
        }

        public static void PrintNLoop(int N)
        {
            for(int i=1;i<=N;i++)
            {
                Console.WriteLine(i);
            }
        }

        public static double F(int n,double[] a,double x)
        {
            double p = a[n];
            for(int i=n;i>n;i--)
            {
                p = a[i - 1] + x * p;
            }
            return p;
        }


        static void Main(string[] args)
        {
            //PrintNLoop(100000);

            int[,,] numsMatrix = new int[3, 4, 2]
            {
                {{1, 2 },{1, 2 },{1, 2 },{1, 2 } },
                {{1, 2 },{1, 2 },{1, 2 },{1, 2 } },
                {{1, 2 },{1, 2 },{1, 2 },{1, 2 } }
            };
            int d1Length = numsMatrix.GetLength(0);
            int d2Length = numsMatrix.GetLength(1);
            int d3Length = numsMatrix.GetLength(2);

            Console.WriteLine("D1:{0} D2:{1} D3:{2}",d1Length,d2Length,d3Length);

            Console.Read();
        }
    }

    class MaxtrixOp
    {
        public int[,] Add(int[,] matrix1,int[,] matrix2)
        {
            if(matrix1.GetLength(1)!=matrix2.GetLength(1) || matrix1.GetLength(0) != matrix2.GetLength(0))
            {
                return default(int[,]);
            }
            else
            {
                int[,] matrix = new int[matrix1.GetLength(0), matrix2.GetLength(1)];
                for(int i = 0; i < matrix1.GetLength(0); i++)
                {
                    for(int j = 0; j < matrix2.GetLength(1); j++)
                    {
                        matrix[i, j] = matrix1[i, j] + matrix2[i, j];
                    }
                }
                return matrix;
            }
        }

        
    }
}
