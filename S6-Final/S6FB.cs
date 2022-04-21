using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S6FB
{
    class Solution
    {
        public static void Main(string[] args)
        {
            int n = ReadInt(); // количество городов

            Graph graph = new Graph(n);
            for (int i = 0; i < n - 1; i++)
            {
                char[] edges = ReadChars();
                for (int j = 0; j < n - i - 1; j++)
                    graph.AddEdge(i, i + j + 1, edges[j]);
            }

            bool noCycle = graph.DFS();
            string mapIsOptimal = noCycle ? "YES": "NO";
            Console.WriteLine(mapIsOptimal);
        }

        private static int ReadInt()
        {
            return int.Parse(Console.ReadLine());
        }

        private static char[] ReadChars()
        {
            return Console.ReadLine()
                .ToCharArray();
        }
    }

    class Graph
    {
        private int size;
        private List<Node> Nodes { get; set; }
        private List<Edge> Edges { get; set; }

        public Graph(int size)
        {
            this.size = size;
            Nodes = new List<Node>();
            for (int i = 0; i < size; i++)
                Nodes.Add(new Node(i));
            Edges = new List<Edge>();
        }

        public void AddEdge(int nodeOneIndex, int nodeTwoIndex, char type)
        {
            Node nodeOne = Nodes[nodeOneIndex];
            Node nodeTwo = Nodes[nodeTwoIndex];
            Edge edge = null;
            if (type == 'R')
            {
                edge = new Edge(nodeOne, nodeTwo);
                nodeOne.Edges.Add(edge);
            }
            else
            {
                edge = new Edge(nodeTwo, nodeOne);
                nodeTwo.Edges.Add(edge);
            }
            Edges.Add(edge);
        }

        private List<NodeColor> color;

        public bool DFS()
        {
            color = new List<NodeColor>(new NodeColor[this.size]);
            for (int i = 0; i < this.size; i++)
                if (color[i] == NodeColor.White)
                    if (!DFS(Nodes[i]))
                        return false;
            return true;
        }

        public bool DFS(Node node)
        {
            var stack = new Stack<Node>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var v = stack.Pop();
                if (color[v.Index] == NodeColor.White)
                {
                    color[v.Index] = NodeColor.Gray;
                    stack.Push(v);
                    for (int i = v.Edges.Count - 1; i >= 0; i--)
                    {
                        var w = v.Edges[i].NodeTo; 
                        if (color[w.Index] == NodeColor.White)
                        {
                            stack.Push(w);
                        }
                        else if (color[w.Index] == NodeColor.Gray)
                            return false;
                    }
                }
                else if (color[v.Index] == NodeColor.Gray)
                    color[v.Index] = NodeColor.Black;
            }
            return true;
        }

        public enum NodeColor
        {
            White,
            Gray,
            Black
        }
    }

    class Edge
    {
        private Node nodeFrom, nodeTo;

        public Node NodeFrom
        {
            get => nodeFrom;
        }

        public Node NodeTo
        {
            get => nodeTo;
        }

        public Edge(Node nodeFrom, Node nodeTo)
        {
            this.nodeFrom = nodeFrom;
            this.nodeTo = nodeTo;
        }
    }

    class Node
    {
        public int Index { get; }
        public List<Edge> Edges { get; set; }

        public Node(int index/*, T value*/)
        {
            this.Index = index;
            this.Edges = new List<Edge>();
        }
    }
}
