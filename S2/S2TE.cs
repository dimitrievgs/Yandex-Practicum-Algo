using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace S2TE
{
    // закомментируйте перед отправкой
    public class Node<TValue>
    {
        public TValue Value { get; private set; }
        public Node<TValue> Next { get; set; }
        public Node<TValue> Prev { get; set; }

        public Node(TValue value, Node<TValue> next, Node<TValue> prev)
        {
            Value = value;
            if (next != null)
            {
                Next = next;
                next.Prev = this;
            }
            if (prev != null)
            {
                Prev = prev;
                prev.Next = this;
            }
        }
    }

    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static Node<string> Solve(Node<string> head)
        {
            Node<string> node = head;
            while (node.Next != null)
            {
                Node<string> prevNode = node.Prev, nextNode = node.Next;
                node.Prev = nextNode;
                node.Next = prevNode;
                node = nextNode;
            }
            node.Next = node.Prev;
            node.Prev = null;
            return node;
        }
    }
}