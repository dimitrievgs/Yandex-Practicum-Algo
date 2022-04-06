using System;
using System.Collections.Generic;

namespace S5TJ
{
    class Solution
    {
        public static void Main(string[] args)
        {
            Test();
        }

        public static Node Insert(Node root, int key)
        {
            InsertNode(root, key);
            return root;
        }

        public static void InsertNode(Node root, int key)
        {
            if (key >= root.Value)
            {
                if (root.Right != null)
                    InsertNode(root.Right, key);
                else
                {
                    Node node = new Node(key);
                    root.Right = node;
                }
            }
            else
            {
                if (root.Left != null)
                    InsertNode(root.Left, key);
                else
                {
                    Node node = new Node(key);
                    root.Left = node;
                }
            }
        }

        private static void Test()
        {
            var node1 = new Node(7);
            var node2 = new Node(8)
            {
                Left = node1
            };

            var node3 = new Node(7)
            {
                Right = node2
            };
            var newHead = Insert(node3, 6);
            Console.WriteLine(newHead == node3);
            Console.WriteLine(newHead.Left.Value == 6);
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