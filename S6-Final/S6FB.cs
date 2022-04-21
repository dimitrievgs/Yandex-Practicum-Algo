/*
ID 67637541
отчёт https://contest.yandex.ru/contest/25070/run-report/67637541/
задача https://contest.yandex.ru/contest/25070/problems/B/

-- ПРИНЦИП РАБОТЫ --
По условиям задачи нужно определить, является ли карта железных дорог оптимальной,
т.е., что из одного узла в другой можно проехать только дорогами одного типа.
Задачу можно переформулировать как то, что двум цветам рёбер (R и B) соответствуют
противоположные направления рёбер.
Тогда оптимальность карты будет следовать из отсутствия в её графе циклов.
Циклы ищем, запуская DFS по графу. Если в окрестностях узла есть серые узлы, значит, 
мы наткнулись на цикл.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Равносильность двух цветов (типов) дорог и противоположной направленности исходит из
того, что две дороги (типа R и B) можно свести к двум дорогам, где в одном случае дорога
идёт из A в B, а в другом из B в A. Таким образом мы получаем цикл. Равносильность?
1) => Существование двух путей разного цвета позволяет всегда построить цикл.
2) <= Если у нас существует цикл, то это означает, что есть путь из A в C (где C > A) и путь 
из C в A. Допустим, ребро из меньшего узла в больший это R, а из большего в меньший это B.
Тогда если дорога приводит в узел с бОльшим номером, то там должна быть хотя бы одно ребро R,
иначе (если она состоит только из B) она смогла бы привести только в узел с меньшим номером.
Аналогично, если дорога ведёт в узел с меньшим номером, то там должна быть хотя бы одно
ребро B, иначе бы она могла привести только в узел с большим номером. А поскольку у нас есть,
как минимум, одно ребро R (путь 1) и одно ребро B (путь 2), то либо есть два пути, 
один только из R, а второй только из B, либо один из этих путей (допустим, из A в C) 
содержит N путей R и K путей B. В любой случае мы получаем "не оптимальный" граф.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Определение временной и пространственной сложности сводится к определению п. и в. сложности
алгоритма DFS по матрице смежности. 
В случае представления графа в виде матрицы смежности, чтобы найти соседние вершины, 
нужно просмотреть целиком всю строку матрицы, соответствующую этой вершине. 
Для каждой из ∣V∣ вершин необходимо выполнить ∣V∣ операций. 
Тогда временная сложность O(|V|^2).
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Матрица смежности занимает O(|V|^2), массив цветов вершин O(|V|).
Отсюда получаем пространственную сложность O(|V|^2).
*/

using System;
using System.Collections.Generic;

namespace S6FB
{
    class Solution
    {
        public static void Main(string[] args)
        {
            ushort n = ReadInt();

            Graph graph = new Graph(n);
            for (int i = 0; i < n - 1; i++)
            {
                char[] edges = ReadChars();
                for (int j = 0; j < n - i - 1; j++)
                    graph.AddEdge(i, i + j + 1, edges[j]);
            }

            bool noCycle = graph.DFS();
            string mapIsOptimal = noCycle ? "YES" : "NO";
            Console.WriteLine(mapIsOptimal);
        }

        private static ushort ReadInt()
        {
            return ushort.Parse(Console.ReadLine());
        }

        private static char[] ReadChars()
        {
            return Console.ReadLine()
                .ToCharArray();
        }
    }

    class Graph
    {
        private ushort size;
        private bool[,] adjacencyMatrix;

        public Graph(ushort size)
        {
            this.size = size;
            adjacencyMatrix = new bool[size, size];
        }

        public void AddEdge(int nodeOneIndex, int nodeTwoIndex, char type)
        {
            if (type == 'R')
            {
                adjacencyMatrix[nodeOneIndex, nodeTwoIndex] = true;
            }
            else
            {
                adjacencyMatrix[nodeTwoIndex, nodeOneIndex] = true;
            }
        }

        private NodeColor[] color;

        public bool DFS()
        {
            color = new NodeColor[this.size];
            for (int i = 0; i < this.size; i++)
                if (color[i] == NodeColor.White)
                    if (!DFS(i))
                        return false;
            return true;
        }

        public bool DFS(int nodeIndex)
        {
            var stack = new Stack<int>();
            stack.Push(nodeIndex);
            while (stack.Count > 0)
            {
                var v = stack.Pop();
                if (color[v] == NodeColor.White)
                {
                    color[v] = NodeColor.Gray;
                    stack.Push(v);
                    for (int w = size - 1; w >= 0; w--) 
                    {
                        if (adjacencyMatrix[v, w] == true)
                        {
                            if (color[w] == NodeColor.White)
                            {
                                stack.Push(w);
                            }
                            else if (color[w] == NodeColor.Gray)
                                return false;
                        }
                    }
                }
                else if (color[v] == NodeColor.Gray)
                    color[v] = NodeColor.Black;
            }
            return true;
        }

        public enum NodeColor : byte
        {
            White,
            Gray,
            Black
        }
    }
}
