// ***********************************************************************
// Author     ：HuYe
// Function   ：存储列表类，提供A*算法运算所需的节点存储列表
// CreateTime ：2019/3/25 13:41:57
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketService
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
        public int PathWidth { get; set; }             //节点携带的路径宽度信息
        public int Direction { get; set; } = 0;        //在该节点处的行进方向(0:直行 1:左转 2:右转 3:停止)

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
        /// <param name="index">节点编号</param>
        /// <param name="x">X坐标</param>
        /// <param name="y">Y坐标</param>
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

        public Node(int index, int x, int y, int direction)
        {
            Index = index;
            X = x;
            Y = y;
            Direction = direction;
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
            int w = 1;                //比例因子
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
            if (node.Index == Index)
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

    /// <summary>
    /// 节点列表类
    /// </summary>
    /// <remarks>
    /// 包含对节点的添加、删除、替换，路径列表的复制、反转、查找等操作
    /// </remarks>
    public class StoreHeap
    {
        #region Field & Properties

        public List<Node> NodeList;         //节点列表
        public int Count                    //节点列表所包含的节点个数
        {
            get
            {
                return NodeList.Count;
            }
        }
        #endregion

        #region Constructor

        public StoreHeap()
        {
            NodeList = new List<Node>();
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// 判断节点是否存在于列表中
        /// </summary>
        /// <param name="nodeCheck">待检查的节点</param>
        /// <returns>
        ///   <c>true</c> if the specified node check is exist; otherwise, <c>false</c>.
        /// </returns>
        public bool IsExist(Node nodeCheck)
        {
            foreach(var node in NodeList)
            {
                if (node.Index == nodeCheck.Index)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 删除指定的节点
        /// </summary>
        /// <param name="node">待删除的节点</param>
        public void Remove(Node node)
        {
            //Node findedNode = NodeList.Find(n => n.Index == node.Index);
            //if (findedNode != null)
            //{
            //    NodeList.Remove(findedNode);
            //}
            if (!NodeList.Remove(node))
            {
                return;
            }
        }

        /// <summary>
        /// 删除指定编号的节点
        /// </summary>
        /// <param name="index">待删除节点的编号</param>
        public void RemoveIndex(int index)
        {
            Node findedNode = NodeList.Find(n => n.Index == index);
            if (findedNode != null)
            {
                NodeList.Remove(findedNode);
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 添加节点到节点列表
        /// </summary>
        /// <param name="node">待添加的节点</param>
        public void Add(Node node)
        {
            //if (this.IsExist(node))
            //{
            //    return;
            //}
            //else
            //{
                NodeList.Add(node);
            //}
        }

        /// <summary>
        /// 复制一个当前的对象
        /// </summary>
        public object Clone()
        {
            StoreHeap heap = new StoreHeap();
            heap.NodeList = NodeList;
            return heap;
        }

        /// <summary>
        /// 根据编号返回节点列表中的节点
        /// </summary>
        /// <param name="index">节点编号</param>
        public Node FindNode(int index)
        {
            Node node = NodeList.Find(n => n.Index == index);
            if (node != null)
            {
                return node;
            }
            else
                return null;
        }

        /// <summary>
        /// 替换指定节点
        /// </summary>
        /// <param name="node">需要替换的节点</param>
        public void ReplaceNode(Node node)
        {
            Node nodeToChange = FindNode(node.Index);
            Remove(nodeToChange);
            Add(node);
        }

        /// <summary>
        /// 找到当前节点列表中总代价最小的节点
        /// </summary>
        /// <returns>总代价最小的节点</returns>
        public Node GetMinCostNode()
        {
            Node minCostNode = NodeList[0];
            int minCost = NodeList[0].TotalCost;
            foreach(Node node in NodeList)
            {
                if (minCost > node.TotalCost)
                {
                    minCostNode = node;
                    minCost = node.TotalCost;
                }
            }
            return minCostNode;
        }

        /// <summary>
        /// 返回位于NodeList末尾的点
        /// </summary>
        public Node Pop()
        {
            Node node = NodeList[NodeList.Count - 1];
            return node;
        }

        /// <summary>
        /// 将NodeList中的元素进行反转
        /// </summary>
        public void Reverse()
        {
            NodeList.Reverse();
        }

        #endregion

        #region Interface Implement

        /// <summary>
        /// 实现返回循环访问StoreHeap方法
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            return NodeList.GetEnumerator();
        }
        #endregion
    }
}
