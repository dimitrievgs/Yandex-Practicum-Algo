using System;
using System.Collections.Generic;

namespace S5FB_2
{
    class Solution
    {
        public static void Main(string[] args)
        {
            Test();
        }

        public static Node Remove(Node root, int key)            // корень поддерева, удаляемый ключ
        {
            if (root == null)
                return root;
            if (key < root.Value)
                root.Left = Remove(root.Left, key);
            else if (key > root.Value)
                root.Right = Remove(root.Right, key);
            else if (root.Left != null && root.Right != null)
            {
                root.Value = minimum(root.Right).Value;
                root.Right = Remove(root.Right, root.Value);
            }
            else
            {
                if (root.Left != null)
                    root = root.Left;
                else if (root.Right != null)
                    root = root.Right;
                else
                    root = null;
            }
            return root;
        }

        private static Node minimum(Node x)
        {
            if (x.Left == null)
                return x;
            return minimum(x.Left);
        }

        /*public static Node Remove(Node root, int key)
        {
            // Your code
            // “ヽ(´▽｀)ノ”

            Node current = root;
            Node currentParent = null;
            bool currentIsLeftChild = false;
            while (current.Value != key)
            {
                if (key < current.Value)
                {
                    currentParent = current;
                    current = current.Left;
                    currentIsLeftChild = true;
                }
                else if (key > current.Value)
                {
                    currentParent = current;
                    current = current.Right;
                    currentIsLeftChild = false;
                }
                if (current == null) //нет такого узла
                    return null;
            }
            //сюда попадаем, если нашли узел: current.Value == key
            if (current.Left == null && current.Right == null) //нет детей у узла, т.е. лист
            {
                if (!currentIsLeftChild)
                    currentParent.Right = null;
                else
                    currentParent.Left = null;
                //remove the edge from current.parent to current
            }
            else if (current.Left == null || current.Right == null) //у узла 1 дочерний
            {
                bool hasLeftChild = current.Left != null;
                if (!currentIsLeftChild)
                    currentParent.Right = hasLeftChild ? current.Left : current.Right;
                else
                    currentParent.Left = hasLeftChild ? current.Left : current.Right;
                //have current.parent point to current’s child instead of to current
            }
            else //у узла 2 дочерних
            {
                Node successor = null;
                if (successor == )
            }
        }*/

        /// <summary>
        /// Функция преемника (successor) узла бинарного дерева поиска
        /// находит «преемника» — следующего по величине узла в BST.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /*private static Node Successor(Node node)
        {
            Node current = null;
            if (node.Right != null) //случай 1: есть дочерний узел справа
            {
                current = node.Right;
                while (current.Left != null)
                    current = current.Left;
                return current;
            }
            else //случай 2: node не имеет дочернего узла справа
            {

            }
        }*/

        /*public static Node Insert(Node root, int key)
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
        }*/

        public static void Test()
        {
            var node1 = new Node(2);
            var node2 = new Node(3)
            {
                Left = node1
            };

            var node3 = new Node(1)
            {
                Right = node2
            };

            var node4 = new Node(6);
            var node5 = new Node(8)
            {
                Left = node4
            };

            var node6 = new Node(10)
            {
                Left = node5
            };

            var node7 = new Node(5)
            {
                Left = node3,
                Right = node6
            };

            var newHead = Remove(node7, 10);

            Console.WriteLine(newHead.Value == 5);
            Console.WriteLine(newHead.Right == node5);
            Console.WriteLine(newHead.Right.Value == 8);
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