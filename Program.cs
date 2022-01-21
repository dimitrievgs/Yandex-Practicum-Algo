/*
-- ПРИНЦИП РАБОТЫ --
Для реализации стека использую односвязный список. Поскольку мы взаимодействуем 
только с его головой, то это гарантирует в данном контексте арифметическую
сложность O(1). Храню в стеке операторы и операнды. В остальном как в
задании: Если идёт числа, то помещаю в стек как будущие операнды, если следующий
элемент - это занк операции, то достаю два последних операнда, осуществляю операцию
с ними в порядке их добавления в стек, результат помещаю в стек. В конце получаю
результат, просто забирая верхний элемент из стека.
*/

using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;
    private static Stack<int> stack;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        string[] input = reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        stack = new Stack<int>();
        foreach (string s in input)
        {
            if (char.IsDigit(s[s.Length - 1])) //операнды
                stack.Push(int.Parse(s));
            else //операторы
            {
                int[] operands = new int[2];
                for (int i = 1; i >= 0; i--)
                    stack.Pop(out operands[i]);
                int operationResult = 0;
                switch(s)
                {
                    case "+":
                        operationResult = operands[0] + operands[1];
                        break;
                    case "-":
                        operationResult = operands[0] - operands[1];
                        break;
                    case "*":
                        operationResult = operands[0] * operands[1];
                        break;
                    case "/":
                        operationResult = (int)Math.Floor(operands[0] / (double)operands[1]);
                        break;
                }
                stack.Push(operationResult);
            }
        }
        int result;
        stack.Pop(out result);
        writer.WriteLine(result);

        reader.Close();
        writer.Close();
    }
}

public class Stack<TValue>
{
    Node<TValue> head;
    int size;

    public Stack()
    {
        size = 0;
    }

    public void Push(TValue value)
    {
        Node<TValue> node = new Node<TValue>(value, head);
        head = node;
        size += 1;
    }

    public bool Pop(out TValue value)
    {
        if (head != null)
        {
            value = head.Value;
            head = head.Next;
            size -= 1;
            return true;
        }
        else
        {
            value = default(TValue);
            return false;
        }
    }

    public int Size()
    {
        return size;
    }
}

public class Node<TValue>
{
    public TValue Value;
    public Node<TValue> Next;

    public Node(TValue value, Node<TValue> next = null)
    {
        Value = value;
        Next = next;
    }
}