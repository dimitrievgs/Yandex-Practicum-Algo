using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            for (int i = 0; i < n; i++)
            {
                int[] edge = ReadInts();
                graph.AddEdge(edge[0], edge[1], edge[2]);
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
        private int size;
        private List<Node<int>> Nodes { get; set; }
        private List<Edge<int>> Edges { get; set; }

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
            Edges.Add(edge);
        }

        List<Edge<int>> maxSpanningTreeEdges;
        BinaryHeap<Edge<int>> edgesToConsider;
        //List<Node<int>> nodesAdded;
        List<Node<int>> nodesNotAdded;

        List<bool> visited;
        List<Node<int>> previous;

        public void GetMaximumSpanningTree()
        {
            maxSpanningTreeEdges = new List<Edge<int>>();
            edgesToConsider = new BinaryHeap<Edge<int>>();
            //nodesAdded = new List<Node<int>>();
            nodesNotAdded = Nodes.ToList();

            visited = new List<bool>(new bool[this.size]);
            previous = new List<Node<int>>(new Node<int>[this.size]);

            Node<int> v = Nodes[0];
            previous[0] = null;

            foreach (var edge in v.Edges)
                edgesToConsider.Add(edge);

            while (nodesNotAdded.Count > 0 && edgesToConsider.Size > 0)
            {
                var edge = edgesToConsider.PopMax();
                var edgeNodeOne = edge.Nodes[0];
                var edgeNodeTwo = edge.Nodes[1];
                if (visited[edgeNodeOne.Index] && visited[edgeNodeTwo.Index])
                    continue;
                Node<int> nextNode = null;
                if (visited[edgeNodeOne.Index])
                    nextNode = edgeNodeOne;
                else if (visited[edgeNodeTwo.Index])
                    nextNode = edgeNodeTwo;

                visited[nextNode.Index] = true;
                previous[nextNode.Index] = v;

                foreach (var cEdge in v.Edges)
                {
                    cEdge.Nodes.Find(n => n != )
                    edgesToConsider.Add(cEdge);
                }
            }
        }

        private void AddVertexToSpanningTree(Node<int> node)
        {
            nodesAdded.Add(node);
            nodesNotAdded.Remove(node);
            foreach (var cEdge in node.Edges)
            {
                var otherNode = cEdge.Nodes.Find(n => n != node);
                if (otherNode.Index)
                    edgesToConsider.Add(cEdge);
            }
            //...
        }
    }

    class Edge<T> : IComparable
    {
        public List<Node<T>> Nodes;
        public int Weight { get; set; }

        public Edge(Node<T> nodeOne, Node<T> nodeTwo, int weight)
        {
            this.Nodes = new List<Node<T>> { nodeOne, nodeTwo };
            this.Weight = weight;
            nodeOne.Edges.Add(this);
            nodeTwo.Edges.Add(this);
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

    /*class Intern : IComparable
    {
        private string login;
        private int solvedTasksNr;
        private int penalty;

        public string Login
        {
            get { return login; }
        }

        public int SolvedTasksNr
        {
            get { return solvedTasksNr; }
        }

        public int Penalty
        {
            get { return penalty; }
        }

        public Intern(string login, int solvedTasksNr, int penalty)
        {
            this.login = login;
            this.solvedTasksNr = solvedTasksNr;
            this.penalty = penalty;
        }

        public int CompareTo(object obj)
        {
            int result = 0;
            if (obj == null)
                result = 1;
            else
            {
                Intern otherIntern = obj as Intern;
                if (otherIntern != null)
                {
                    if (this.SolvedTasksNr != otherIntern.SolvedTasksNr)
                        result = otherIntern.SolvedTasksNr.CompareTo(this.SolvedTasksNr);
                    else if (this.Penalty != otherIntern.Penalty)
                        result = this.Penalty.CompareTo(otherIntern.Penalty);
                    else
                        result = this.Login.CompareTo(otherIntern.Login);
                }
                else
                    throw new ArgumentException("Object is not an Intern");
            }
            return result;
        }
    }*/
}
