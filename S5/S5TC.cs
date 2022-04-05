using System;
using System.Collections.Generic;

namespace S5TC
{
    class Solution
    {
        public static void Main(string[] args)
        {
            Test();
        }

        public static bool Solve(Node root)
        {
            return CheckAnagram(root, root);
        }

        private static bool CheckAnagram(Node node1, Node node2)
        {
            if ((node1 == null && node2 != null) || (node1 != null && node2 == null))
                return false;
            else if (node1 == null && node2 == null)
                return true;
            else
            {
                var s1 = (node1.Value == node2.Value) && CheckAnagram(node1.Left, node2.Right) 
                    && CheckAnagram(node1.Right, node2.Left);
                return s1;
            }
        }

        private static void Test()
        {
            var node1 = new Node(3);
            var node2 = new Node(4);
            var node3 = new Node(4);
            var node4 = new Node(3);
            var node5 = new Node(2)
            {
                Left = node1,
                Right = node2,
            };

            var node6 = new Node(2)
            {
                Left = node3,
                Right = node4
            };

            var node7 = new Node(1)
            {
                Left = node5,
                Right = node6
            };

            Console.WriteLine(Solve(node7));
            node3.Value = 7;
            Console.WriteLine(Solve(node7));
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