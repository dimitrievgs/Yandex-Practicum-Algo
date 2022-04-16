﻿/*
ID 66924869
отчёт https://contest.yandex.ru/contest/24810/run-report/66924869/
задача https://contest.yandex.ru/contest/24810/problems/B/

-- ПРИНЦИП РАБОТЫ --
В задаче требуется найти и удалить узел с заданным ключом в бинарной дереве поиска.
Задача решается рекурсивно, на каждом шаге рекурсии имеем 3 возможных ситуации:
1) Корень поддерева больше ключа, значит, искомый узел должен быть в левом поддереве
корня текущего поддерева (если искомый узел есть), тут рекурсивно ищем узел в левом поддереве.
2) Корень поддерева меньше ключа, рекурсивно ищем узел в правом поддереве корня текущего поддерева.
3) Нашли узел, тогда 3 варианта:
а) Найденный узел имеет 2 дочерних узла. Тогда заменяем ключ минимальным ключом из правого поддерева
и удаляем узел с минимальным ключом из правого поддерева.
б) Если найденный узел имеет один дочерний узел, то заменяем его потомком.
в) Если найденный узел не имеет дочерних узлов, то просто удаляем его.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Алгоритм описан выше. Использование рекурсии основано на свойстве бинарного дерева поиска, что
все узлы в левом поддереве текущего узла имеют ключи меньше ключа корня текущего поддерева,
а все узлы в правом поддереве - больше ключа корня текущего поддерева.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Поскольку данные в бинарном дереве поиска отсортированы в любой момент времени,
и на каждом шаге мы перемещаемся из поддерева в поддерево, то временная сложность 
поиска и удаления искомого узла O(log N) в лучшем случае (сбалансированное дерево)
и O(N) в худшем случае (дерево вырождается в список).
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Поскольку при решении используется рекурсия, на каждом шаге которой переменные 
занимают константный объём памяти, то пространственная сложность аналогично временной: 
O(log N) в лучшем случае (сбалансированное дерево) и O(N) в худшем случае 
(несбалансированное дерево, дерево вырождено в список).
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