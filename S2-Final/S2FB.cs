/*
ID 64247257
отчёт https://contest.yandex.ru/contest/22781/run-report/64247257/
задача https://contest.yandex.ru/contest/22781/problems/B/

-- ПРИНЦИП РАБОТЫ --
Для реализации стека использую односвязный список. Храню в стеке операторы и операнды. 
В остальном как в задании: если идёт число, то помещаю в стек как будущий операнд, 
если следующий элемент - это знак операции, то достаю два последних операнда, 
осуществляю операцию с ними в порядке их добавления в стек, результат помещаю в стек. 
В конце получаю результат, просто забирая верхний элемент из стека.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Поскольку на вход подаётся выражение в обратной польской нотации, то в ней по определению
уже операнды расположены перед знаками операций, а порядок задаётся порядком следования 
знаков операций. Поэтому не нужно беспокоиться о приоритетах, и выражение уже адаптировано
для вычисления с использованием стека.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Поскольку мы взаимодействуем только с головой односвязного списка, то это гарантирует 
в данном контексте арифметическую сложность O(1) методов stack.Pop(...) и stack.Push(...).
Определение размера здесь не требуется, но оно тоже O(1), т.к. для этого используется
вспомогательная переменная типа int.
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Пространственная сложность O(n), т.к. на n входящих операндов мы храним в стеке в каждом 
узле значение и ссылку на следующий узел. Остальные переменные занимают константный объём памяти.
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
                switch (s)
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
                        operationResult = Divide(operands[0], operands[1]);
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

    private static int Divide(int dividend, int divider)
    {
        if (divider != 0)
            return (int)Math.Floor(dividend / (double)divider);
        else
        {
            if (dividend == 0)
                return 0; //т.к. в int нет NaN, здесь любое число
            else
                return int.MaxValue * GetSign(dividend);
        }
    }

    private static int GetSign(int value)
    {
        return (Math.Sign(value) >= 0) ? +1 : -1;
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