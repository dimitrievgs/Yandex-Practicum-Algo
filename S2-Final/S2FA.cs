/*
ID 64266916
отчёт https://contest.yandex.ru/contest/22781/run-report/64266916/
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
-- ПРАВКИ --
Выправил сигнатуры pop<...>() и push<...>(x)
*/

using System;
using System.IO;

namespace S2FA
{
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
            int value;
            try
            {
                switch (command)
                {
                    case "push_front":
                        deque.PushFront(parameter);
                        break;
                    case "push_back":
                        deque.PushBack(parameter);
                        break;
                    case "pop_front":
                        value = deque.PopFront();
                        writer.WriteLine(value);
                        break;
                    case "pop_back":
                        value = deque.PopBack();
                        writer.WriteLine(value);
                        break;
                }
            }
            catch (InvalidOperationException e)
            {
                writer.WriteLine(e.Message);
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

        public void PushFront(TValue value)
        {
            if (IsFull())
                throw new InvalidOperationException(errorMessage);
            else
            {
                if (!IsEmpty())
                    head = (head - 1 + nMax) % nMax;
                array[head] = value;
                filledCells += 1;
            }
        }

        public void PushBack(TValue value)
        {
            if (IsFull())
                throw new InvalidOperationException(errorMessage);
            else
            {
                if (!IsEmpty())
                    tail = (tail + 1) % nMax;
                array[tail] = value;
                filledCells += 1;
            }
        }

        public TValue PopFront()
        {
            TValue value;
            if (IsEmpty())
            {
                value = default(TValue);
                throw new InvalidOperationException(errorMessage);
            }
            else
            {
                value = array[head];
                array[head] = default(TValue);
                if (filledCells > 1)
                    head = (head + 1) % nMax;
                filledCells -= 1;
            }
            return value;
        }

        public TValue PopBack()
        {
            TValue value;
            if (IsEmpty())
            {
                value = default(TValue);
                throw new InvalidOperationException(errorMessage);
            }
            else
            {
                value = array[tail];
                array[tail] = default(TValue);
                if (filledCells > 1)
                    tail = (tail - 1 + nMax) % nMax;
                filledCells -= 1;
            }
            return value;
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
}