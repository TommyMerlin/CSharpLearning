using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BLL.Astar
{
    /// <summary>
    /// A*算法路径规划类
    /// </summary>
    public sealed class AStar
    {
        #region Properties

        public Graph Graph { get; set; }                                 //拓扑图
        public StoreHeap NodeList { get; private set; }                  //节点列表，保存拓扑图中的所有节点
        public StoreHeap OpenSet { get; set; } = new StoreHeap();        //开放集合，存储待检验的节点
        public StoreHeap ClosedSet { get; set; } = new StoreHeap();      //关闭结合，存储已检验的节点

        #endregion

        #region Constructor
        public AStar(Graph graph)
        {
            Graph = graph;
            DataTable dt = Graph.DS.Tables["NodeInfo"];
            NodeList = new StoreHeap();
            foreach (DataRow row in dt.Rows)
            {
                Node node = new Node((int)row[0], (int)row[1], (int)row[2]);
                NodeList.Add(node);
            }
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// 路径规划
        /// </summary>
        /// <param name="snId">起始点编号</param>
        /// <param name="gnId">目标点编号</param>
        /// <returns>规划所得路径的节点列表</returns>
        public StoreHeap PathPanning(int snId,int gnId)
        {
            if(snId != gnId)
            {
                Node startNode = NodeList.FindNode(snId);      //起始点
                Node goalNode = NodeList.FindNode(gnId);       //目标点

                // 设置起始点和目标点的累积代价为0
                startNode.CurrentCost = goalNode.CurrentCost = 0;

                foreach (Node node1 in NodeList)
                {
                    node1.NGoal = goalNode;
                }

                // 添加起始点到开放集合
                OpenSet.Add(startNode);
                DataTable dt = Graph.DS.Tables["NodeInfo"];

                while (true)
                {
                    // 当前点为开放集合中启发代价最小的点
                    Node currentNode = OpenSet.GetMinCostNode();

                    // 如果当前点为目标点，则循环结束
                    if (currentNode.Index == goalNode.Index)
                    {
                        // 设置目标点的父节点为关闭集合中的最后一个点
                        goalNode.NParent = ClosedSet.Pop();
                        break;
                    }

                    // 将当前点从开放集合中删去并添加到关闭集合
                    OpenSet.Remove(currentNode);
                    ClosedSet.Add(currentNode);

                    for (int i = 0; i < Graph.Nodes; i++)
                    {
                        if (Graph.Matrix[currentNode.Index - 1, i] != 0)
                        {
                            Node node = NodeList.FindNode((int)dt.Rows[i]["id"]);
                            node.CurrentCost = currentNode.CurrentCost + Graph.Matrix[currentNode.Index - 1, i];

                            // 如果节点存在于关闭集合，则跳过后续步骤，直接下一个循环
                            if (ClosedSet.IsExist(node))
                            {
                                continue;
                            }

                            // 如果节点不在开放集合，则将节点加入到开放集合
                            else if (!OpenSet.IsExist(node))
                            {
                                node.NParent = currentNode;
                                OpenSet.Add(node);
                            }
                            else
                            {
                                // 如果当前点到查询到的点的代价比现在的大，则更新节点的父节点
                                if (OpenSet.FindNode(node.Index).CurrentCost >= node.CurrentCost)
                                {
                                    node.NParent = currentNode;
                                    OpenSet.ReplaceNode(node);
                                }
                            }
                        }
                    }
                }


                StoreHeap finalPath = new StoreHeap();       //最终路径的节点列表
                Node recallnode = goalNode;
                finalPath.Add(recallnode);

                // 只要当前点还存在父节点，则将其都加入到最终路径的节点列表
                while (recallnode.HasParent())
                {
                    recallnode = recallnode.NParent;
                    finalPath.Add(recallnode);
                }
                finalPath.Reverse();
                return finalPath;
            }

            // 如果起始点和目标点为同一点，则返回一个包含起始点的节点列表
            else
            {
                StoreHeap result = new StoreHeap();
                result.Add(NodeList.FindNode(snId));
                return result;
            }
        }

        /// <summary>
        /// 获取规划所得路径
        /// </summary>
        /// <param name="snId">起始节点</param>
        /// <param name="gnId">目标节点</param>
        /// <returns></returns>
        public string GetFinalPath(int snId, int gnId)
        {
            StoreHeap finalPath = PathPanning(snId, gnId);

            string result = "";

            result += finalPath.NodeList[0].Index.ToString();

            for(int i = 1; i < finalPath.Count; i++)
            {
                result += " => " + finalPath.NodeList[i].Index.ToString();
            }

            return result;
        }

        #endregion
    }
}
