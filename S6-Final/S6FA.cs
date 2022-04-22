/*
ID 67663119
отчёт https://contest.yandex.ru/contest/25070/run-report/67663119/
задача https://contest.yandex.ru/contest/25070/problems/A/

-- ПРИНЦИП РАБОТЫ --
По условиям задачи нужно связать компьютеры в единую сеть, построив максимальное остовное дерево
в неориентированном графе.
Для этого граф хранится в виде списка смежности, используется алгоритм Прима.
При этом на каждом шаге рассматриваемые рёбра вокруг очередной вершины заносятся в бинарную кучу, 
откуда извлекается элемент в корне (максимальный по весу, поскольку мы строим 
максимальное остовное дерево).
Реализация бинарной кучи взята из задачи S5FA https://contest.yandex.ru/contest/24810/run-report/66936024/
Если ребро с максимальным весом на очередном шаге содержит вершину, которая ещё не была добавлена
(добавленные отмечаем в массиве bool), то добавляем это ребро в остовное дерево.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Посколько на каждом шаге мы подбираем максимальное ребро из возможных, то по аналогии с
алгоритмом Прима на очереди с приоритетами, где строится минимальное остовное дерево, это
позволяет нам построить максимальное остовное дерево.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Благодаря приоритетной очереди сложность алгоритма Прима O(∣E∣⋅log⁡∣V∣), 
где |E| — количество рёбер в графе, а |V| — количество вершин.
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Мы храним граф в списках смежности, поэтому пространственная сложность O(|V|+|E|):
для хранения вершин в массиве O(|V|), для хранения рёбер как отдельных, связанных с вершинами,
сущностей O(|E|). Список рассмотренных вершин bool[] nodesAdded занимает O(|V|). 
Список рёбер maxSpanningTreeEdges, извлекаемой из приоритетной очереди - O(|E|).
Список ещё не добавленных в остовное дерево вершин nodesNotAdded - O(|V|).
Хранение рёбер в бинарной куче edgesToConsider - O(|E|).
Итого пространственная сложность O(|V|+|E|).
-- ПРАВКИ --
Убрал else перед "return maxSpanningTreeEdges;", теперь он в конце метода.
Вместо списка Nodes ввёл NodeOne и NodeTwo. Не могу заранее сказать, 
какой из них Source, какой Destination, потому просто ввожу NodeOne, NodeTwo.
Убрал динамический массив, заменил на дву поля, и да, теперь
325ms / 29.18Mb -> 262ms / 18.63Mb :)
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace S6FA
{
    class Solution
    {
        public static void Main(string[] args)
        {
            int[] sizes = ReadInts();
            int n = sizes[0];
            int m = sizes[1];

            Graph graph = new Graph(n);
            for (int i = 0; i < m; i++)
            {
                int[] edge = ReadInts();
                graph.AddEdge(edge[0], edge[1], edge[2]);
            }

            var maxSpanningTree = graph.GetMaximumSpanningTree();
            if (maxSpanningTree != null)
            {
                int totalWeight = graph.GetTotalEdgesWeight(maxSpanningTree);
                Console.WriteLine(totalWeight);
            }
            else
                Console.WriteLine("Oops! I did it again");
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
        private int size;
        private List<Node<int>> Nodes { get; set; }

        public Graph(int size)
        {
            this.size = size;
            Nodes = new List<Node<int>>();
            for (int i = 0; i < size; i++)
                Nodes.Add(new Node<int>(i, i + 1));
        }

        public void AddEdge(int nodeOneValue, int nodeTwoValue, int weight)
        {
            Node<int> nodeOne = Nodes[nodeOneValue - 1];
            Node<int> nodeTwo = Nodes[nodeTwoValue - 1];
            Edge<int> edge = new Edge<int>(nodeOne, nodeTwo, weight);
            nodeOne.Edges.Add(edge);
            nodeTwo.Edges.Add(edge);
        }

        List<Edge<int>> maxSpanningTreeEdges;
        BinaryHeap<Edge<int>> edgesToConsider;
        List<Node<int>> nodesNotAdded;
        List<bool> nodesAdded;

        public int GetTotalEdgesWeight(List<Edge<int>> edges)
        {
            int weight = 0;
            foreach (var edge in edges)
                weight += edge.Weight;
            return weight;
        }

        public List<Edge<int>> GetMaximumSpanningTree()
        {
            maxSpanningTreeEdges = new List<Edge<int>>();
            edgesToConsider = new BinaryHeap<Edge<int>>();
            nodesNotAdded = Nodes.ToList();
            nodesAdded = new List<bool>(new bool[this.size]);

            Node<int> v = Nodes[0];
            AddVertexToSpanningTree(v);
            while (nodesNotAdded.Count > 0 && edgesToConsider.Size > 0)
            {
                var edge = edgesToConsider.PopMax();
                Node<int> nextNode = null;
                if (!nodesAdded[edge.NodeOne.Index])
                    nextNode = edge.NodeOne;
                else if (!nodesAdded[edge.NodeTwo.Index])
                    nextNode = edge.NodeTwo;
                if (nextNode == null)
                    continue;
                maxSpanningTreeEdges.Add(edge);
                AddVertexToSpanningTree(nextNode);
            }
            if (nodesNotAdded.Count > 0)
                return null;
            return maxSpanningTreeEdges;
        }

        private void AddVertexToSpanningTree(Node<int> node)
        {
            nodesAdded[node.Index] = true;
            nodesNotAdded.Remove(node);
            foreach (var cEdge in node.Edges)
            {
                var otherNode = (cEdge.NodeOne != node) ? cEdge.NodeOne : cEdge.NodeTwo;
                if (otherNode != null && nodesAdded[otherNode.Index] == false)
                    edgesToConsider.Add(cEdge);
            }
        }
    }

    class Edge<T> : IComparable
    {
        public Node<T> NodeOne { get; }
        public Node<T> NodeTwo { get; }
        public int Weight { get; set; }

        public Edge(Node<T> nodeOne, Node<T> nodeTwo, int weight)
        {
            NodeOne = nodeOne;
            NodeTwo = nodeTwo;
            this.Weight = weight;
        }

        public int CompareTo(object obj)
        {
            int result = 0;
            if (obj == null)
                result = 1;
            else
            {
                Edge<T> otherEdge = obj as Edge<T>;
                if (otherEdge != null)
                {
                    if (this.Weight != otherEdge.Weight)
                        result = otherEdge.Weight.CompareTo(this.Weight);
                }
                else
                    throw new ArgumentException("Object is not an Edge");
            }
            return result;
        }
    }

    class Node<T>
    {
        public int Index { get; set; }
        public T Value { get; set; }
        public List<Edge<T>> Edges { get; set; }

        public Node(int index, T value)
        {
            this.Index = index;
            this.Value = value;
            this.Edges = new List<Edge<T>>();
        }
    }

    /// <summary>
    /// Реализация из задачи S5FA
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class BinaryHeap<T> where T : IComparable
    {
        private List<T> array;
        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public BinaryHeap()
        {
            array = new List<T> { default(T) }; //нумерация с 1го элемента
            size = 0;
        }

        public void Add(T item)
        {
            this.size += 1;
            int index = this.size;
            array.Add(item);
            SiftUp(index);
        }

        private void SiftUp(int index)
        {
            if (index == 1)
                return;
            int parentIndex = index / 2;
            if (array[parentIndex].CompareTo(array[index]) > 0)
            {
                (array[index], array[parentIndex]) = (array[parentIndex], array[index]);
                SiftUp(parentIndex);
            }
            return;
        }

        public T PopMax()
        {
            T result = this.array[1];
            if (this.size > 1)
            {
                this.array[1] = this.array[this.size];
                this.array.RemoveAt(this.size);
            }
            this.size -= 1;
            SiftDown(1);
            return result;
        }

        private void SiftDown(int index)
        {
            int leftIndex = 2 * index;
            int rightIndex = 2 * index + 1;

            int indexOfLargest = index;
            if (this.size < leftIndex)
                return;
            else if (this.size >= rightIndex && array[leftIndex].CompareTo(array[rightIndex]) > 0)
                indexOfLargest = rightIndex;
            else
                indexOfLargest = leftIndex;

            if (array[index].CompareTo(array[indexOfLargest]) > 0)
            {
                (array[index], array[indexOfLargest]) = (array[indexOfLargest], array[index]);
                SiftDown(indexOfLargest);
            }
        }
    }
}
