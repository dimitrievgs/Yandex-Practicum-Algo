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
            ushort n = ReadInt(); // количество городов

            Graph graph = new Graph(n);
            for (int i = 0; i < n - 1; i++)
            {
                char[] edges = ReadChars();
                for (int j = 0; j < n - i - 1; j++)
                    graph.AddEdge(i, i + j + 1, edges[j]);
            }

            bool noCycle = graph.DFS();
            string mapIsOptimal = noCycle ? "YES" : "NO";
            Console.WriteLine(mapIsOptimal);
        }

        private static ushort ReadInt()
        {
            return ushort.Parse(Console.ReadLine());
        }

        private static char[] ReadChars()
        {
            return Console.ReadLine()
                .ToCharArray();
        }
    }

    class Graph
    {
        private ushort size;
        private List<Node> Nodes { get; set; }

        public Graph(ushort size)
        {
            this.size = size;
            Nodes = new List<Node>();
            for (ushort i = 0; i < size; i++)
                Nodes.Add(new Node(i));
        }

        public void AddEdge(int nodeOneIndex, int nodeTwoIndex, char type)
        {
            Node nodeOne = Nodes[nodeOneIndex];
            Node nodeTwo = Nodes[nodeTwoIndex];
            if (type == 'R')
            {
                nodeOne.ConnectedNodes.Add(nodeTwo);
            }
            else
            {
                nodeTwo.ConnectedNodes.Add(nodeOne);
            }
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
                    for (int i = v.ConnectedNodes.Count - 1; i >= 0; i--)
                    {
                        var w = v.ConnectedNodes[i];
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

        public enum NodeColor : byte
        {
            White,
            Gray,
            Black
        }
    }

    class Node
    {
        public ushort Index { get; }
        public List<Node> ConnectedNodes { get; set; }

        public Node(ushort index)
        {
            this.Index = index;
            this.ConnectedNodes = new List<Node>();
        }
    }
}
