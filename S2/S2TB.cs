using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace S2TB
{
    public class Solution<TValue>
    {
        public static void Solve(Node<TValue> head)
        {
            var node = head;
            do
            {
                Console.WriteLine(node.Value);
                node = node.Next;
            }
            while (node != null);
        }
    }

    // закомментируйте перед отправкой
    public class Node<TValue>
    {
        public TValue Value { get; private set; }
        public Node<TValue> Next { get; set; }

        public Node(TValue value, Node<TValue> next)
        {
            Value = value;
            Next = next;
        }
    }
}