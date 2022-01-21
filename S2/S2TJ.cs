using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        ListQueue<int> listQueue = new ListQueue<int>();
        int n = ReadInt();
        for (int i = 0; i < n; i++)
        {
            ParseCommand(listQueue);
        }

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static void ParseCommand(ListQueue<int> listQueue)
    {
        string[] commandParts = reader.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string command = commandParts[0];
        int parameter = (commandParts.Length > 1) ? int.Parse(commandParts[1]) : 0;
        string el = "";
        switch (command)
        {
            case "get":
                el = listQueue.get();
                writer.WriteLine(el);
                break;
            case "put":
                listQueue.put(parameter);
                break;
            case "size":
                writer.WriteLine(listQueue.size());
                break;
        }
    }
}

public class ListQueue<TValue>
{
    private Node<TValue> head;
    private Node<TValue> tail;
    private int listSize;

    public ListQueue()
    {
        head = null;
        tail = null;
        listSize = 0;
    }

    public string get()
    {
        if (isEmpty())
            return "error";
        else
        {
            TValue value = head.Value;
            head = head.Next;
            listSize -= 1;
            if (listSize == 0)
                tail = null;
            return value.ToString();
        }
    }

    public void put(TValue x)
    {
        Node<TValue> node = new Node<TValue>(x, null);
        if (isEmpty())
            head = node;
        else
            tail.Next = node;
        tail = node;
        listSize += 1;
    }

    public int size()
    {
        return listSize;
    }

    public bool isEmpty()
    {
        return listSize == 0;
    }
}

public class Node<TValue>
{
    public TValue Value;
    public Node<TValue> Next;

    public Node(TValue value, Node<TValue> next)
    {
        Value = value;
        Next = next;
    }
}