using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Astar
{
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
            Node findedNode = NodeList.Find(n => n.Index == node.Index);
            if (findedNode != null)
            {
                NodeList.Remove(findedNode);
            }
            else
                return;
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
            if (this.IsExist(node))
            {
                return;
            }
            else
            {
                NodeList.Add(node);
            }
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
