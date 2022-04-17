using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S6TE
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
                int[] edge = ReadInts();
                var nodeOne = graph.GetNode(edge[0]);
                var nodeTwo = graph.GetNode(edge[1]);
                nodeOne.AddEdge(nodeTwo);
                nodeTwo.AddEdge(nodeOne);
            }

            var connectivityComponents = graph.GetConnectivityComponents();
            Console.WriteLine(connectivityComponents.Nr);
            foreach (var nodes in connectivityComponents.Nodes)
            {
                var sB = new StringBuilder();
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (i > 0)
                        sB.Append(' ');
                    sB.Append(nodes[i].Value);
                }
                Console.WriteLine(sB);
            }
        }

        private static int[] ReadInts()
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
        public int Size { get; set; }

        public Graph(int size)
        {
            this.Size = size;
            this.nodes = new List<Node<int>>();
            for (int i = 1; i <= size; i++)
            {
                nodes.Add(new Node<int>(i));
            }
        }

        public Node<int> GetNode(int Value)
        {
            return nodes[Value - 1];
        }

        private List<NodeColor> color;
        private List<int> connComponent;
        private int connComponentIndex;

        public (int Nr, List<List<Node<int>>> Nodes) GetConnectivityComponents()
        {
            BFS();
            var connectivityComponents =
                new List<List<Node<int>>>();
            for (int c = 0; c <= connComponentIndex; c++)
                connectivityComponents.Add(new List<Node<int>>());
            for (int i = 0; i < this.Size; i++)
            {
                connectivityComponents[connComponent[i]].Add(nodes[i]);
            }
            return (connComponentIndex + 1, connectivityComponents);
        }

        public void BFS()
        {
            color = new List<NodeColor>(new NodeColor[this.Size]);
            connComponent = new List<int>(new int[this.Size]);
            connComponentIndex = -1;
            for (int i = 0; i < Size; i++)
            {
                if (color[i] == NodeColor.White)
                {
                    connComponentIndex++;
                    BFS(nodes[i], connComponentIndex);
                }
            }
        }

        private void BFS(Node<int> node, int connComponentIndex)
        {
            var queue = new Queue<Node<int>>();
            color[node.Value - 1] = NodeColor.Gray;
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                var v = queue.Dequeue();
                connComponent[v.Value - 1] = connComponentIndex;
                for (int i = 0; i < v.ConnectedNodes.Count; i++)
                {
                    var w = v.ConnectedNodes[i];
                    if (color[w.Value - 1] == NodeColor.White)
                    {
                        color[w.Value - 1] = NodeColor.Gray;
                        queue.Enqueue(w);
                    }
                }
                color[v.Value - 1] = NodeColor.Black;
            }
        }
    }

    public enum NodeColor
    {
        White,
        Gray,
        Black
    }

    public class Node<T>
    {
        public T Value { get; set; }
        public List<Node<T>> ConnectedNodes { get; set; }

        public Node(T value)
        {
            this.Value = value;
            this.ConnectedNodes = new List<Node<T>>();
        }

        public void AddEdge(Node<T> otherNode)
        {
            if (!this.ConnectedNodes.Contains(otherNode))
                this.ConnectedNodes.Add(otherNode);
        }
    }
}
