using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

public class S2TG
{
    private static TextReader reader;
    private static TextWriter writer;
    private static StackMaxEffective stack;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        stack = new StackMaxEffective();
        int commandNr = ReadInt();
        for (int i = 0; i < commandNr; i++)
        {
            string command = reader.ReadLine();
            ParseCommand(command);
        }

        reader.Close();
        writer.Close();
    }

    private static void ParseCommand(string command)
    {
        string[] commandParts = command.Split(' ');
        int parameter = commandParts.Length > 1 ? int.Parse(commandParts[1]) : 0;
        switch (commandParts[0])
        {
            case "get_max":
                stack.get_max();
                break;
            case "push":
                stack.push(parameter);
                break;
            case "pop":
                stack.pop();
                break;
        }
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static int[] ReadArray()
    {
        return reader.ReadLine()
            .Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}

public class StackMaxEffective
{
    Stack<int> stack;
    Stack<ListNode<int>> maxStack;

    public StackMaxEffective()
    {
        stack = new Stack<int>();
        maxStack = new Stack<ListNode<int>>();
    }

    public void push(int x)
    {
        ListNode<int> node = stack.push(x);
        ListNode<int> maxValue = maxStack.peekValue();
        if (maxValue == null || maxValue.Value < x)
            maxStack.push(node);
    }

    public void pop()
    {
        ListNode<int> oldHead = stack.pop();
        ListNode<int> maxTopNode = maxStack.peekValue();
        if (oldHead != null && oldHead == maxTopNode)
        {
            maxStack.pop();
        }
    }

    public void get_max()
    {
        ListNode<int> maxTopNode = maxStack.peekValue();
        string result = (maxTopNode != null) ? maxTopNode.Value.ToString() : "None";
        Console.WriteLine(result);
    }
}

public class Stack<TValue>
{
    ListNode<TValue> head;

    public Stack()
    {
        head = null;
    }

    public ListNode<TValue> push(TValue x)
    {
        ListNode<TValue> node = new ListNode<TValue>(x, head);
        head = node;
        return node;
    }

    public void push(ListNode<TValue> node)
    {
        node.Next = head;
        head = node;
    }

    public ListNode<TValue> pop()
    {
        if (head != null)
        {
            ListNode<TValue> oldHead = head;
            head = head.Next;
            return oldHead;
        }
        else
        {
            Console.WriteLine("error");
            return null;
        }
    }

    public TValue peekValue()
    {
        TValue value = (head != null) ? head.Value : default(TValue);
        return value;
    }

    public ListNode<TValue> peekNode()
    {
        return head;
    }

    /*public void get_max()
    {
        ListNode<TValue> node = head;
        int maxEl = int.MinValue;
        while (node != null)
        {
            maxEl = Math.Max(maxEl, node.Value);
            node = node.Next;
        }
        string result = (maxEl > int.MinValue) ? maxEl.ToString() : "None";
        Console.WriteLine(result);
    }*/
}

public class ListNode<TValue>
{
    public TValue Value;
    public ListNode<TValue> Next;

    public ListNode(TValue value, ListNode<TValue> next)
    {
        Value = value;
        Next = next;
    }
}
