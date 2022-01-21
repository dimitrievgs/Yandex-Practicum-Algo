/*
ID 
отчёт 
задача https://contest.yandex.ru/contest/22781/problems/A/

-- ПРИНЦИП РАБОТЫ --
Поскольку нужно, чтобы операциb выполнялись за O(1), для реализации дека
использую закольцованный массив. При реализации закольцованного массива 
для единообразия PushFront/PushBack и PopFront/PopBack у меня head указывает 
на первый левый заполненный элемент, а tail на последний правый заполненный 
элемент.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Поскольку это реализация Дека, то мы 1 к 1 переводим команды со входа в операции
на Деке. В основе абстрактного типа данных Дек использован закольцованный массив.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Использование массива позволяет обращаться к любому элементу, если известно 
его положение, за O(1). А закольцованность позволит избежать проблем при 
вставке элементов в начало или середину массива (не понадобится сдвигать 
все, что справа). Дополнительно O(1) гарантируется условиями задачи: 
согласно им, не нужно кратно увеличивать размер массива при его заполнении, 
достаточно выводить error.
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Пространственная сложность линейная O(N), поскольку обрабатывая команды, мы 
поочередно помещаем и извлекаем элементы из массива. В пределе это просто N
если на вход подаётся только push(x). Остальные переменные занимают константный
объём памяти.
*/

using System;
using System.IO;

public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;
    private static Deque<int> deque;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int n = ReadInt();
        int m = ReadInt();
        deque = new Deque<int>(m);
        for (int i = 0; i < n; i++)
        {
            string commandLine = reader.ReadLine();
            ParseCommand(deque, commandLine);
        }

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static void ParseCommand(Deque<int> deque, string commandLine)
    {
        string[] commandParts = commandLine
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        string command = commandParts[0];
        int parameter = (commandParts.Length > 1) ? int.Parse(commandParts[1]) : 0;
        string result;
        int value;
        switch (command)
        {
            case "push_front":
                result = deque.PushFront(parameter);
                if (!string.IsNullOrEmpty(result))
                    writer.WriteLine(result);
                break;
            case "push_back":
                result = deque.PushBack(parameter);
                if (!string.IsNullOrEmpty(result))
                    writer.WriteLine(result);
                break;
            case "pop_front":
                result = deque.PopFront(out value);
                if (!string.IsNullOrEmpty(result))
                    writer.WriteLine(result);
                else
                    writer.WriteLine(value);
                break;
            case "pop_back":
                result = deque.PopBack(out value);
                if (!string.IsNullOrEmpty(result))
                    writer.WriteLine(result);
                else
                    writer.WriteLine(value);
                break;
        }
    }
}

public class Deque<TValue>
{
    private TValue[] array;
    private int nMax;
    private int head; //для удобства head указывает на первую заполненную ячейку
    private int tail; //а tail на последнюю заполненную, как предлагается на stepik
    private int filledCells;

    private const string errorMessage = "error";

    public Deque(int n)
    {
        array = new TValue[n];
        nMax = n;
        head = 0;
        tail = 0;
        filledCells = 0;
    }

    public string PushFront(TValue value)
    {
        string result = "";
        if (IsFull())
            result = errorMessage;
        else
        {
            if (!IsEmpty())
                head = (head - 1 + nMax) % nMax;
            array[head] = value;
            filledCells += 1;
        }
        return result;
    }

    public string PushBack(TValue value)
    {
        string result = "";
        if (IsFull())
            result = errorMessage;
        else
        {
            if (!IsEmpty())
                tail = (tail + 1) % nMax;
            array[tail] = value;
            filledCells += 1;
        }
        return result;
    }

    public string PopFront(out TValue value)
    {
        string result = "";
        if (IsEmpty())
        {
            value = default(TValue);
            result = errorMessage;
        }
        else
        {
            value = array[head];
            array[head] = default(TValue);
            if (filledCells > 1)
                head = (head + 1) % nMax;
            filledCells -= 1;
        }
        return result;
    }

    public string PopBack(out TValue value)
    {
        string result = "";
        if (IsEmpty())
        {
            value = default(TValue);
            result = errorMessage;
        }
        else
        {
            value = array[tail];
            array[tail] = default(TValue);
            if (filledCells > 1)
                tail = (tail - 1 + nMax) % nMax;
            filledCells -= 1;
        }
        return result;
    }

    private bool IsFull()
    {
        return filledCells == nMax;
    }

    private bool IsEmpty()
    {
        return filledCells == 0;
    }
}

