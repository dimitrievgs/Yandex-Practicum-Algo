using System;

//namespace S5TB
//{
    class Solution
    {
        /*public static void Main(string[] args)
        {
            Test();
        }*/

        public static bool Solve(Node root)
        {
            return CheckBalance(root);
        }

        private static bool CheckBalance(Node root)
        {
            balanced = true;
            CalculateHeight(root);
            return balanced;
        }

        private static bool balanced;

        private static int CalculateHeight(Node node)
        {
            if (node == null)
                return 0;
            int h1 = CalculateHeight(node.Left);
            int h2 = CalculateHeight(node.Right);
            if (Math.Abs(h1 - h2) > 1)
                balanced = false;
            return Math.Max(h1, h2) + 1;
        }

        private static void Test()
        {
            var node1 = new Node(1);
            var node2 = new Node(-5);
            var node3 = new Node(3)
            {
                Left = node1,
                Right = node2
            };
            var node4 = new Node(10);
            var node5 = new Node(2)
            {
                Left = node3,
                Right = node4
            };
            Console.WriteLine(Solve(node5));
        }
    }

    // закомментируйте перед отправкой
    /*public class Node
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
    }*/
//}