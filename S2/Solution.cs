using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

// закомментируйте перед отправкой

/*public class Node<TValue>
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
}*/


public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;

    /*public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int n = ReadInt();
        Node<string> node = null, head = null;
        for (int i = 0; i < n; i++)
        {
            string s = reader.ReadLine();
            node = new Node<string>(s, null, node);
            if (i == 0)
                head = node;
        }
        //writer.WriteLine("---");
        var newNode = Solve(head);


        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }
    */
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

        /*Node<string> curr = node;
        while (curr != null)
        {
            Console.WriteLine(curr.Value);
            curr = curr.Next;
        }*/

        //Console.WriteLine("---");
        return node;
    }
}