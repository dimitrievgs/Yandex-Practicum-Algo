﻿/*
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
*/

public class Solution<TValue>
{
    public static void Solve(Node<TValue> head)
    {
        // Your code
        // ヽ(´▽`)/
    }

    private static void Test()
    {
        var node3 = new Node<string>("node3", null);
        var node2 = new Node<string>("node2", node3);
        var node1 = new Node<string>("node1", node2);
        var node0 = new Node<string>("node0", node1);
        Solve(node0);
        /*
        Output is:
        node0
        node1
        node2
        node3
        */
    }
}
