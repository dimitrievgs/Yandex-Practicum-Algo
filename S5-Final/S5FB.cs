/*
ID 
отчёт 
задача 

-- ПРИНЦИП РАБОТЫ --

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --

-- ВРЕМЕННАЯ СЛОЖНОСТЬ --

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
*/

using System;

namespace S5FB
{
    class Solution
    {
        public static void Main(string[] args)
        {
            Test();
        }

        /// <summary>
        /// Рекурсивное удаление узла из бинарного дерева.
        /// Время работы алгоритма — O(h).
        /// </summary>
        /// <param name="root"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Node Remove(Node root, int key)
        {
            if (root == null)
                return root;
            if (key < root.Value) // удаляемый элемент находится в левом поддереве текущего поддерева
                root.Left = Remove(root.Left, key); // нужно рекурсивно удалить элемент из нужного поддерева
            else if (key > root.Value) // удаляемый элемент находится в правом поддереве текущего поддерева
                root.Right = Remove(root.Right, key); // нужно рекурсивно удалить элемент из нужного поддерева
            else if (root.Left != null && root.Right != null)
            {
                // Если удаляемый элемент находится в корне текущего поддерева и имеет два дочерних узла,
                // то нужно заменить его минимальным элементом из правого поддерева
                // и рекурсивно удалить этот минимальный элемент из правого поддерева
                root.Value = Minimum(root.Right).Value;
                root.Right = Remove(root.Right, root.Value);
            }
            else // если удаляемый элемент имеет один дочерний узел, нужно заменить его потомком
            {
                if (root.Left != null)
                    root = root.Left;
                else if (root.Right != null)
                    root = root.Right;
                else // удаляемый элемент не имеет дочерних узлов
                    root = null;
            }
            return root;
        }

        /// <summary>
        /// Чтобы найти минимальный элемент в бинарном дереве поиска, нужно следовать 
        /// указателям .Left от корня дерева, пока не встретится значение null. 
        /// Время ее работы O(log(n)), где log(n) — высота дерева.
        /// <para/>
        /// Если у вершины есть левое поддерево, то по свойству бинарного дерева поиска в нем хранятся все элементы с меньшим ключом. 
        /// Если его нет, значит эта вершина и есть минимальная. 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private static Node Minimum(Node x)
        {
            if (x.Left == null)
                return x;
            return Minimum(x.Left);
        }

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