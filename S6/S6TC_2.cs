/*
 * Через матрицу смежности.
 */

using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace S6TC_2
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        private static Graph graph;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            //writer = new StreamWriter(Console.OpenStandardOutput());

            DateTime startTime = DateTime.Now;

            ReadGraph();
            //GenerateGraph(100000, 99999);
            graph.DFS();

            /*foreach (var nodeIndex in graph.dFSSortedNodes)
                Console.Write($" {nodeIndex}");*/
            Console.WriteLine("\n\n");
            Console.WriteLine((DateTime.Now - startTime).TotalMilliseconds);

            //writer.Close();
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
                graph.SetNonOrientedEdge(ends[0], ends[1]);
            }
            graph.SetStartNode(ReadInt());
        }

        private static Random rand = new Random();

        private static void GenerateGraph(int n, int m)
        {
            graph = new Graph(n);

            for (int j = 0; j < m; j++)
            {
                int start, end;
                bool connected;
                do
                {
                    start = rand.Next(1, n + 1);
                    end = rand.Next(1, n + 1);
                    connected = graph.GetNonOrientedEdge(start, end);
                    if (start == end || connected)
                        ;
                }
                while (start == end || connected);
                graph.SetNonOrientedEdge(start, end);
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

    /// <summary>
    /// Внутри нумерация узлов от 0, наружу выводим в нумерации от 1
    /// Храним только нижнюю диагональ в матрице смежности
    /// </summary>
    public class Graph
    {
        private int nodesNr;
        private List<List<bool>> adjacencyMatrix;
        private List<NodeColor> nodesColors;
        private int startNode;

        public Graph(int nodesNr)
        {
            this.nodesNr = nodesNr;
            adjacencyMatrix = new List<List<bool>>();
            for (int i = 0; i < nodesNr; i++)
            {
                List<bool> row = new List<bool>();
                for (int j = 0; j < i; j++)
                    row.Add(false);
                adjacencyMatrix.Add(row);
            }
        }

        public void SetNonOrientedEdge(int nodeOneIndex, int nodeTwoIndex)
        {
            if (nodeOneIndex != nodeTwoIndex)
            {
                List<int> indeces = new List<int> { nodeOneIndex - 1, nodeTwoIndex - 1 };
                indeces.Sort();
                adjacencyMatrix[indeces[1]][indeces[0]] = true;
            }
        }

        public bool GetNonOrientedEdge(int nodeOneIndex, int nodeTwoIndex)
        {
            if (nodeOneIndex != nodeTwoIndex)
            {
                List<int> indeces = new List<int> { nodeOneIndex - 1, nodeTwoIndex - 1 };
                indeces.Sort();
                return adjacencyMatrix[indeces[1]][indeces[0]];
            }
            else
                return true;
        }

        public void SetStartNode(int startNode)
        {
            this.startNode = startNode - 1;
        }

        public List<int> dFSSortedNodes { get; set; }

        public void DFS()
        {
            dFSSortedNodes = new List<int>();
            nodesColors = new List<NodeColor>(new NodeColor[this.nodesNr]);
            DFSRecursive(startNode);
        }

        private void DFSRecursive(int start_vertex)
        {
            Stack<int> stack = new Stack<int>();
            stack.Push(start_vertex);
            while (stack.Count > 0)
            {
                // Получаем из стека очередную вершину.
                // Это может быть как новая вершина, так и уже посещённая однажды.
                int v = stack.Pop();
                if (nodesColors[v] == NodeColor.White)
                {
                    // Красим вершину в серый. И сразу кладём её обратно в стек:
                    // это позволит алгоритму позднее вспомнить обратный путь по графу.
                    nodesColors[v] = NodeColor.Gray;
                    stack.Push(v);
                    PrintNodeValue(v);
                    // Теперь добавляем в стек все непосещённые соседние вершины,
                    // вместо вызова рекурсии
                    //для каждого исходящего ребра(v, w):
                    //возьмём вершину w
                    //если color[w] == white:
                    //stack.push(w)                    
                    for (int i = nodesNr - 1; i >= 0; i--)
                    {
                        if (i != v && nodesColors[i] == NodeColor.White &&
                            GetNonOrientedEdge(v + 1, i + 1) == true)
                            stack.Push(i);
                    }
                }
                else if (nodesColors[v] == NodeColor.Gray)
                {
                    // Серую вершину мы могли получить из стека только на обратном пути.
                    // Следовательно, её следует перекрасить в чёрный.
                    nodesColors[v] = NodeColor.Black;
                }
            }
        }

        private void PrintNodeValue(int nodeIndex)
        {
            dFSSortedNodes.Add(nodeIndex);
            if (dFSSortedNodes.Count > 1)
                Console.Write(' ');
            Console.Write(nodeIndex + 1);
        }
    }
}