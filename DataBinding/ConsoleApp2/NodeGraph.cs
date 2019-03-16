using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL.Astar
{
    /// <summary>
    /// 节点类，包括点的坐标、父节点和其他相关信息
    /// </summary>
    public class Node
    {
        #region Properties

        public int X { get; set; }                     //节点X坐标
        public int Y { get; set; }                     //节点Y坐标
        public int Index { get; set; }                 //节点编号
        public Node NParent { get; set; }              //父节点
        public Node NGoal { get; set; }                //目标节点
        public int CurrentCost { get; set; }           //累积代价

        public int EstimateCost                        //当前点到目标点的预计代价
        {
            get
            {
                return CalEstimateCost();
            }
        }
        public int TotalCost                           //总代价 = 累积代价 + 到目标点预计代价
        {
            get
            {
                return CurrentCost + EstimateCost;
            }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// 初始化节点类.
        /// </summary>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
        /// <param name="nParent">父节点</param>
        /// <param name="nGoal">目标点</param>
        public Node(int index, int x, int y, Node nGoal)
        {
            Index = index;
            X = x;
            Y = y;
            NGoal = nGoal;
        }

        public Node(int index, int x, int y)
        {
            Index = index;
            X = x;
            Y = y;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// 确定当前点是否为目标点
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is goal; otherwise, <c>false</c>.
        /// </returns>
        public bool IsGoal()
        {
            if (this == null)
            {
                return false;
            }
            else if (X == NGoal.X && Y == NGoal.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 计算到目标点预计代价
        /// </summary>
        public int CalEstimateCost()
        {
            int w = 2;                //比例因子
            int xd = X - NGoal.X;     //当前点到目标点在X方向上的距离                                       
            int yd = Y - NGoal.Y;     //当前点到目标点在Y方向上的距离

            // 曼哈顿距离（适用于AGV运动路径只在水平和垂直方向）
            int cost = w * (Math.Abs(xd) + Math.Abs(yd));

            //// 欧几里得距离（适用于AGV运动路径可以为任意方向）
            //EstimateCost = w * (int)(Math.Sqrt(xd * xd + yd * yd));

            //// 对角距离（适用于AGV运动路径为八方向）
            //EstimateCost = w * Math.Max(Math.Abs(xd), Math.Abs(yd));

            return cost;
        }

        /// <summary>
        /// 获取节点信息
        /// </summary>
        public string GetNodeInfo()
        {
            return $"节点编号:{Index}\tX坐标:{X}\tY坐标:{Y}\t累计代价:{CurrentCost}\t" +
                $"预计代价:{EstimateCost}\t总代价:{TotalCost}\t目标点编号:{NGoal.Index}\t";
        }

        /// <summary>
        /// 判断节点是否存在父节点
        /// </summary>
        /// <returns></returns>
        public bool HasParent()
        {
            if (NParent != null)
            {
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Public Overriden Methods

        /// <summary>
        /// 重写Equals方法
        /// </summary>
        public override bool Equals(object obj)
        {
            Node node = (Node)obj;
            if(node.Index == Index)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 重写GetHashCode方法
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }




    /// <summary>拓扑图类</summary>
    /// <remarks>
    /// 创建邻接矩阵并检查节点数和边数的有效性
    /// </remarks>
    public class Graph
    {
        #region Properties

        public int Nodes { get; set; }          //节点数
        public int Edges { get; set; }          //边数

        private int[,] _Matrix;                 //邻接矩阵
        public int[,] Matrix
        {
            get
            {
                CreateMatrix();
                return _Matrix;
            }
        }
        public DataSet DS                       //路径和节点信息表
        {
            get
            {
                string selectnode = "select id,x,y from node";                        //获取节点信息                       
                string selectpath = "select bnX,bnY,enX,enY from path";      //获取路径信息
                DataSet ds = new DataSet();
                DataTable dt;
                dt = DAL.SQLHelper.SelectSQLReturnDataTable(selectnode).Copy();
                dt.TableName = "NodeInfo";
                ds.Tables.Add(dt);
                dt = DAL.SQLHelper.SelectSQLReturnDataTable(selectpath).Copy();
                dt.TableName = "PathInfo";
                ds.Tables.Add(dt);
                return ds;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 初始化图类.
        /// </summary>
        /// <param name="nodes">图中的节点数</param>
        /// <param name="edges">图中的边数</param>
        public Graph()
        {
            Nodes = DS.Tables["NodeInfo"].Rows.Count;
            Edges = DS.Tables["PathInfo"].Rows.Count;
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// 创建邻接矩阵
        /// </summary>
        /// <returns>
        /// 创建成功返回0；节点数和边数错误，返回1；路径起点、终点或权重错误，返回2
        /// </returns>
        public int CreateMatrix()
        {
            int result = 0;
            if (!CheckNodesEdges())
            {
                result = 1;                                                         //节点数和边数错误，返回1
            }
            
            DataTable dt = DS.Tables[1];                                            //获取节点信息表

            _Matrix = new int[Nodes, Nodes];                                        //创建 节点数×节点数 大小的二维数组
            foreach (DataRow row in dt.Rows)                                        //将查询到的数据填入矩阵
            {
                int beginNode = (int)row[0];                                        //起始节点编号
                int endNode = (int)row[1];                                          //终止节点编号
                int weight = (int)row[2];                                           //路径权重
                if (CheckPathValue(beginNode, endNode, weight))
                {
                    _Matrix[beginNode - 1, endNode - 1] = weight;                   //邻接矩阵[起点,终点] = 路径权重
                    _Matrix[endNode - 1, beginNode - 1] = weight;                   //邻接矩阵[终点,起点] = 路径权重（无向图）
                }
                else
                {
                    result = 2;                                                     //路径起点、终点或权重错误，返回2
                }
            }
            return result;
        }

        // 输出展示创建的邻接矩阵（控制台输出）
        public void DisplayMatrix()
        {
            for (int i = 0; i < 22; i++)
            {
                for (int j = 0; j < 22; j++)
                {
                    if (Matrix[i, j] != 0)
                    {
                        Console.Write("{0,-7}", Matrix[i, j]);
                    }
                    else
                    {
                        Console.Write("{0,-7}", "max");
                    }
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// 检查路径起点编号、终点编号及路径权重是否合法
        /// </summary>
        /// <param name="beginNode">路径起点编号</param>
        /// <param name="endNode">路径终点编号</param>
        /// <param name="weight">路径权重</param>
        private bool CheckPathValue(int beginNode, int endNode, int weight)
        {
            if (beginNode < 1 || endNode < 1 || beginNode > Nodes || endNode > Nodes || weight < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        // 验证节点数和边数是否满足关系
        private bool CheckNodesEdges()
        {
            if (Nodes < 0 || Edges < 0 || (Nodes - 1) * Nodes < Edges)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
    }
}
