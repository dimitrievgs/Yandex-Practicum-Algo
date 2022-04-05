using System;
using System.Collections.Generic;

namespace S5TD
{
    class Solution
    {
        public static void Main(string[] args)
        {
            Test();
        }

        public static bool Solve(Node left, Node right)
        {
            return CheckTwins(left, right);
        }

        private static bool CheckTwins(Node node1, Node node2)
        {
            if ((node1 == null && node2 != null) || (node1 != null && node2 == null))
                return false;
            else if (node1 == null && node2 == null)
                return true;
            else
            {
                var s1 = (node1.Value == node2.Value) && CheckTwins(node1.Left, node2.Left) 
                    && CheckTwins(node1.Right, node2.Right);
                return s1;
            }
        }

        private static void Test()
        {
            var node1 = new Node(1);
            var node2 = new Node(2);
            var node3 = new Node(3)
            {
                Left = node1,
                Right = node2
            };

            var node4 = new Node(1);
            var node5 = new Node(2);
            var node6 = new Node(3)
            {
                Left = node4,
                Right = node5
            };

            Console.WriteLine(Solve(node3, node6));
            node5.Value = 77;
            Console.WriteLine(Solve(node3, node6));
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