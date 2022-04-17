using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S6TF
{
    class Solution
    {
        public static void Main(string[] args)
        {
            var sizes = ReadInts();
            int n = sizes[0];
            int m = sizes[1];

            Graph graph = new Graph(n);
            for (int i = 0; i < m; i++)
            {
                var edge = ReadInts();
                graph.AddEdge(edge[0], edge[1]);
            }

            var pathEnds = ReadInts();
            var startNodeValue = pathEnds[0];
            var endNodeValue = pathEnds[1];
            var minDistance = graph.GetMinDistance(startNodeValue, endNodeValue);
            Console.WriteLine(minDistance);
        }

        public static int[] ReadInts()
        {
            return Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }

    class Graph
    {
        private List<Node<int>> nodes;
        private int size;

        public Graph(int size)
        {
            this.size = size;
            this.nodes = new List<Node<int>>();
            for (int i = 0; i < size; i++)
            {
                nodes.Add(new Node<int>(i + 1));
            }
        }

        public void AddEdge(int nodeOneValue, int nodeTwoValue)
        {
            var nodeOne = this.nodes[nodeOneValue - 1];
            var nodeTwo = this.nodes[nodeTwoValue - 1];
            nodeOne.AddEdge(nodeTwo);
            nodeTwo.AddEdge(nodeOne);
        }

        public int GetMinDistance(int startNodeValue, int endNodeValue)
        {
            Node<int> startNode = this.nodes[startNodeValue - 1];
            Node<int> endNode = this.nodes[endNodeValue - 1];
            return GetMinDistance(startNode, endNode);
        }

        private List<NodeColor> color;
        private List<Node<int>> previous;
        private List<int> distance;

        private int GetMinDistance(Node<int> startNode, Node<int> endNode)
        {
            if (endNode == startNode)
                return 0;

            color = new List<NodeColor>(new NodeColor[this.size]);
            previous = new List<Node<int>>(new Node<int>[this.size]);
            distance = new List<int>(new int[this.size]);

            var queue = new Queue<Node<int>>();
            previous[startNode.Value - 1] = null;
            distance[startNode.Value - 1] = 0;
            color[startNode.Value - 1] = NodeColor.Gray;
            queue.Enqueue(startNode);
            while (queue.Count > 0)
            {
                var v = queue.Dequeue();

                if (v == endNode)
                    return distance[v.Value - 1];

                foreach (var w in v.ConnectedNodes)
                {
                    if (color[w.Value - 1] == NodeColor.White)
                    {
                        previous[w.Value - 1] = v;
                        distance[w.Value - 1] = distance[v.Value - 1] + 1;
                        color[w.Value - 1] = NodeColor.Gray;
                        queue.Enqueue(w);
                    }
                }
                color[v.Value - 1] = NodeColor.Black;
            }
            return -1;
        }
    }

    class Node<T>
    {
        public T Value { get; set; }
        public List<Node<T>> ConnectedNodes { get; set; }

        public Node(T value)
        {
            this.Value = value;
            ConnectedNodes = new List<Node<T>>();
        }

        public void AddEdge(Node<T> otherNode)
        {
            if (!ConnectedNodes.Contains(otherNode))
                ConnectedNodes.Add(otherNode);
        }
    }

    public enum NodeColor
    {
        White,
        Gray,
        Black
    }
}
