using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace S6TA
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        private static List<Node> nodes;
        private static List<Edge> edges;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int[] sizes = ReadInts();
            int n = sizes[0];
            int m = sizes[1];

            nodes = new List<Node>();
            for (int i = 1; i <= n; i++)
                nodes.Add(new Node(i));

            edges = new List<Edge>();
            for (int j = 0; j < m; j++)
            {
                int[] ends = ReadInts();
                Node fromNode = nodes.Find(o => o.Value == ends[0]);
                Node toNode = nodes.Find(o => o.Value == ends[1]);
                Edge edge = new Edge(fromNode, toNode);
                fromNode.OutgoingEdges.Add(edge);
                edges.Add(edge);
            }

            for (int i = 0; i < n; i++)
            {
                List<int> outgoingNodes = new List<int>(), valuesToWrite;
                foreach(var edge in nodes[i].OutgoingEdges)
                {
                    outgoingNodes.Add(edge.ToNode.Value);
                }
                valuesToWrite = new List<int> { outgoingNodes.Count };
                if (outgoingNodes.Count > 0)
                {
                    outgoingNodes.Sort();
                    valuesToWrite.AddRange(outgoingNodes);
                }
                writer.WriteLine(string.Join(' ', valuesToWrite));
            }

            writer.Close();
            reader.Close();
        }

        private static int[] ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }

    public class Node
    {
        public int Value { get; set; }
        public List<Edge> OutgoingEdges { get; set; }

        public Node(int value)
        {
            this.Value = value;
            this.OutgoingEdges = new List<Edge>();
        }
    }

    public class Edge
    {
        public Node FromNode { get; set; }
        public Node ToNode { get; set; }

        public Edge(Node fromNode, Node toNode)
        {
            this.FromNode = fromNode;
            this.ToNode = toNode;
        }
    }
}