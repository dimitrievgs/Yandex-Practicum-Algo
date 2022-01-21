using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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