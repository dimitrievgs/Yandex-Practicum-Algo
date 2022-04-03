using System;
using System.Collections.Generic;

namespace S5TE
{
    class Solution
    {
        public static void Main(string[] args)
        {
            Test();
        }

        public static bool Solve(Node root)
        {
            List<int> treeValuesArray = new List<int>();
            LMR(root, treeValuesArray);
            bool ordered = CheckOrder(treeValuesArray);
            return ordered;
        }

        private static void LMR(Node node, List<int> treeValuesArray)
        {
            if (node.Left != null)
                LMR(node.Left, treeValuesArray);
            treeValuesArray.Add(node.Value);
            if (node.Right != null)
                LMR(node.Right, treeValuesArray);
        }

        private static bool CheckOrder(List<int> treeValuesArray)
        {
            int nr = treeValuesArray.Count;
            if (nr > 1)
            {
                for (int i = 0; i < nr - 1; i++)
                {
                    if (treeValuesArray[i] >= treeValuesArray[i + 1])
                        return false;
                }
            }
            return true;
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
            Console.WriteLine(Solve(node5));
            node2.Value = 5;
            Console.WriteLine(Solve(node5));
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