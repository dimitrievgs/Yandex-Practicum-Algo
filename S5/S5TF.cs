using System;

namespace S5TF
{
    class Solution
    {
        public static void Main(string[] args)
        {
            Test();
        }

        public static int Solve(Node root)
        {
            return GetMaxPathWithDFS(root);
        }

        private static int GetMaxPathWithDFS(Node node)
        {
            if (node == null)
                return 0;
            int max = Math.Max(GetMaxPathWithDFS(node.Left), GetMaxPathWithDFS(node.Right)) + 1;
            return max;
        }

        private static void Test()
        {
            var node1 = new Node(1);
            var node2 = new Node(4);
            var node3 = new Node(3)
            {
                Left = node1,
                Right = node2
            };
            var node4 = new Node(8);
            var node5 = new Node(5)
            {
                Left = node3,
                Right = node4
            };
            int value = Solve(node5);
            Console.WriteLine(value == 3);
        }
    }

    
    // закомментируйте перед отправкой
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }
}