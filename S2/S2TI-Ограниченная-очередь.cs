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

        int n = ReadInt();
        int size = ReadInt();
        CircleQueue<int> queue = new CircleQueue<int>(size);
        for (int i = 0; i < n; i++)
            ReadCommand(queue);

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static void ReadCommand(CircleQueue<int> queue)
    {
        string[] commandParts = reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string command = commandParts[0];
        int parameter = (commandParts.Length > 1) ? int.Parse(commandParts[1]) : 0;
        string el = "";
        switch (command)
        {
            case "push":
                el = queue.push(parameter);
                if (!String.IsNullOrEmpty(el))
                    writer.WriteLine(el);
                break;
            case "pop":
                el = queue.pop();
                writer.WriteLine(el);
                break;
            case "peek":
                el = queue.peek();
                writer.WriteLine(el);
                break;
            case "size":
                int size = queue.size();
                writer.WriteLine(size);
                break;
        }
    }
}

public class CircleQueue<TValue>
{
    private TValue[] array;
    private int head;
    private int tail;
    private int arSize;
    private int max_n;

    public CircleQueue(int n)
    {
        array = new TValue[n];
        max_n = n;
        head = 0;
        tail = 0;
        arSize = 0;
    }

    public string push(TValue x)
    {
        if (arSize < max_n)
        {
            array[tail] = x;
            tail = (tail + 1) % max_n;
            arSize += 1;
            return "";
        }
        else
            return "error";
    }

    public string pop()
    {
        if (isEmpty())
            return "None";
        TValue oldValue = array[head];
        array[head] = default(TValue);
        head = (head + 1) % max_n;
        arSize -= 1;
        return oldValue.ToString();
    }

    public string peek()
    {
        if (isEmpty())
            return "None";
        else
            return array[head].ToString();
    }

    public int size()
    {
        return arSize;
    }

    public bool isEmpty()
    {
        return arSize == 0;
    }
}

