/*
 * Через список смежности.
 */

using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace S6TD
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        private static Graph graph;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            ReadGraph();
            //GenerateGraph(100000, 99999);

            graph.BFS();

            Console.WriteLine(graph.sB);

            writer.Close();
            reader.Close();
        }

        private static void ReadGraph()
        {
            int[] sizes = ReadInts();
            int n = sizes[0];
            int m = sizes[1];

            graph = new Graph(n);

            for (int j = 0; j < m; j++)
            {
                int[] ends = ReadInts();
                Node nodeOne = graph.Nodes[ends[0] - 1];
                Node nodeTwo = graph.Nodes[ends[1] - 1];
                nodeOne.ConnectedNodes.Add(nodeTwo);
                nodeTwo.ConnectedNodes.Add(nodeOne);
            }
            for (int k = 0; k < n; k++)
                graph.Nodes[k].ConnectedNodes
                    = graph.Nodes[k].ConnectedNodes.OrderBy(o => o.Value).ToList();
            graph.SetStartNode(ReadInt());
        }

        private static Random rand = new Random();

        private static void GenerateGraph(int n, int m)
        {
            graph = new Graph(n);

            for (int j = 0; j < m; j++)
            {
                int start, end;
                Node nodeOne, nodeTwo;
                bool connected;
                do
                {
                    start = rand.Next(1, n + 1);
                    end = rand.Next(1, n + 1);
                    nodeOne = graph.Nodes[start - 1];
                    nodeTwo = graph.Nodes[end - 1];
                    connected = nodeOne.ConnectedNodes.Contains(nodeTwo);
                }
                while (start == end || connected);
                nodeOne.ConnectedNodes.Add(nodeTwo);
                nodeTwo.ConnectedNodes.Add(nodeOne);
            }
            graph.SetStartNode(rand.Next(1, n + 1));
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static int[] ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }

    public enum NodeColor
    {
        White,
        Gray,
        Black
    }

    public class Graph
    {
        private int nodesNr;
        public List<Node> Nodes { get; set; }

        private Node startNode;

        private List<NodeColor> color;
        private List<Node> previous;
        private List<int> distance;

        public Graph(int n)
        {
            this.nodesNr = n;
            Nodes = new List<Node>();
            for (int i = 1; i <= n; i++)
                Nodes.Add(new Node(i));
        }

        public void SetStartNode(int nodeValue)
        {
            startNode = Nodes.Find(o => o.Value == nodeValue);
        }

        private int dFSSortedNodesCount;
        public StringBuilder sB;

        public void BFS()
        {
            BFS(startNode);
        }

        public void BFS(Node node)
        {
            color = new List<NodeColor>(new NodeColor[this.nodesNr]);
            previous = new List<Node>(new Node[this.nodesNr]);
            distance = new List<int>(new int[this.nodesNr]);
            sB = new StringBuilder();

            var queue = new Queue<Node>();
            previous[node.Value - 1] = null;
            distance[node.Value - 1] = 0;
            color[node.Value - 1] = NodeColor.Gray;
            queue.Enqueue(node);

            while (queue.Count > 0)
            {
                var v = queue.Dequeue();
                PrintNodeValue(v);
                for (int i = 0; i < v.ConnectedNodes.Count; i++)
                {
                    var w = v.ConnectedNodes[i];
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
        }

        private void PrintNodeValue(Node node)
        {
            dFSSortedNodesCount++;
            if (dFSSortedNodesCount > 1)
                sB.Append(' ');
            sB.Append(node.Value);
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public List<Node> ConnectedNodes { get; set; }

        public Node(int value)
        {
            this.Value = value;
            this.ConnectedNodes = new List<Node>();
        }
    }
}