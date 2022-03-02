/*
ID 65697013
отчёт https://contest.yandex.ru/contest/24414/run-report/65697013/
задача https://contest.yandex.ru/contest/24414/problems/B/

-- ПРИНЦИП РАБОТЫ --
По условиям задачи нужно реализовать структуру HashMap с методами void Put(uint key, uint value),
uint Get(uint key) и uint Delete(uint key). 
Реализую через массив и хеш-функцию, которая берётся как остаток от деления ключа на размер массива
(метод деления). Для разрешение коллизий используется метод открытой адресации, квадратичное пробирование.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Беру фактор заполнения 0.5, принимая во внимание, что 0.75 - предельный адекватный фактор заполнения, 
при бОльших значениях ожидаемое количество операций для put/get/delete резко возрастает.
По условиям задачи предполагается, что при обработке входных данных не случится переполнение массива.
Поэтому я беру фиксированный размер массива как (максимальное_кол-во_ключей / фактор_заполнения)
и ищу ближайшее простое число выше этого значения, чтобы избежать ненужных корреляций и добиться
максимально равномерного заполнения массива HashMap'а.
Рехеширование и масштабирование HashMap делать по условиям задачи не требуется, потому я беру 
этот размер массива как фиксированный. 
Также для квадратичного пробирования для констант c1 и c2 я беру простые числа, сопоставимые с
размером массива, избегая общих множителей для максимального распределения ключей.
Функция определения индекса массива для записи пары ключ-значение корректна, т.к. обладает всеми
основными свойствами: 
- Детерминизмом (для одних и тех же данных функция всегда возвращает одно и то же значение).
- Эффективностью (быстро вычисляется, дополнительно для подсчёта квадрата использую step*step вместо Math.Pow(...))
- Ограниченностью (результат вычисления функции принадлежит диапазону от 0 до M-1, где M — размер массива, 
используемого для реализации HashMap).
- Равномерностью (данные в хеш-таблице распределяются равномерно. Это достигается подбором простых M, c1, c2, 
для уменьшения кластеризации константы c1 и c2 выбраны сопоставимыми по значению M).
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Временная сложность операций в худщем случае (квадратичное пробирование):
Put / Get / Delete: O(n) — Если все ключи сопоставляются одним и тем же индексам массива, мы должны осуществлять пробирование для всех n ключей
Временная сложность операций в среднем
Put / Get / Delete: O(1 + alpha), где alpha = N / M (N - кол-во пар ключ-значение, добавленных в таблицу, M - размер массива).
Поскольку для эффективной работы HashMap alpha стараются брать в ограниченном диапазоне < 0.75, то можно сказать,
что средняя временная сложность O(1).
Временная сложность операций в лучшем случае
Put / Get / Delete: O(1) - нет коллизий
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Пространственная сложность (квадратичное пробирование):
O(n) — размер массива HashMap'а принято брать как M, деленную на константу "фактор заполнения", для открытой адресации её выбирают 
в определённом дипазоне, например между 0.3 и 0.75.
*/

using System;
using System.IO;

namespace S4FB
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = int.Parse(reader.ReadLine());
            HashMap hashMap = new HashMap();
            for (int i = 0; i < n; i++)
                ParseCommand(hashMap, reader.ReadLine());

            writer.Close();
            reader.Close();
        }

        private static void ParseCommand(HashMap hashMap, string commandLine)
        {
            string[] commandParts = commandLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string command = commandParts[0];
            uint key, value;
            switch (command)
            {
                case "put":
                    try
                    {
                        key = uint.Parse(commandParts[1]);
                        value = uint.Parse(commandParts[2]);
                        hashMap.Put(key, value);
                    }
                    catch (InvalidOperationException e)
                    {
                        writer.WriteLine(e.Message);
                    }
                    break;
                case "get":
                    key = uint.Parse(commandParts[1]);
                    value = hashMap.Get(key);
                    if (value != uint.MaxValue)
                        writer.WriteLine(value);
                    else
                        writer.WriteLine(HashMap.keyNotFoundMessage);
                    break;
                case "delete":
                    key = uint.Parse(commandParts[1]);
                    value = hashMap.Delete(key);
                    if (value != uint.MaxValue)
                        writer.WriteLine(value);
                    else
                        writer.WriteLine(HashMap.keyNotFoundMessage);
                    break;
            }
        }
    }

    /// <summary>
    /// Коллизии разрешаются методом открытой адресации
    /// </summary>
    class HashMap
    {
        /// <summary>
        /// 0.75 - предельный адекватный фактор заполнения, при бОльших значениях 
        /// ожидаемое количество операций при поиске (например) резко возрастает
        /// </summary>
        private double loadFactor = 0.5;
        private uint backingArraySize;
        public KeyValue[] array;
        public const string keyNotFoundMessage = "None";

        private int c1 = (int)GetNearestLargerPrimeNumber(20_000), c2 = (int)GetNearestLargerPrimeNumber(100_000);

        public HashMap(int keysCapacity = 100_000)
        {
            backingArraySize = GetNearestLargerPrimeNumber((uint)(keysCapacity / loadFactor));
            array = new KeyValue[backingArraySize];
        }

        private static uint GetNearestLargerPrimeNumber(uint n)
        {
            uint i = n;
            while (!IsPrime(i))
                i++;
            return i;
        }

        private static bool IsPrime(uint n)
        {
            if (n == 1)
                return false;
            int i = 2;
            while (i * i <= n)
            {
                if ((n % i) == 0)
                    return false;
                i = i + 1;
            }
            return true;
        }

        /// <summary>
        /// Используем квадратичное пробирование
        /// </summary>
        /// <param name="key"></param>
        /// <param name="step"></param>
        /// <returns></returns>
        private int GetArrayIndex(uint key, int step)
        {
            return (int)((key + c1 * step + c2 * step * step) % backingArraySize);
        }

        public void Put(uint key, uint value)
        {
            int step = 0;
            int startIndex = GetArrayIndex(key, step);
            int index = startIndex;
            while (true)
            {
                if (array[index] == null || array[index].Deleted) // эта ячейка пустая
                {
                    array[index] = new KeyValue(key, value);
                    return;
                }
                else if (array[index].Key == key) //перезаписываем значение
                {
                    array[index].Value = value;
                    return;
                }
                else // коллизия
                {
                    step++;
                    index = GetArrayIndex(key, step);
                }
                if (index == startIndex) //пришли снова в стартовый индекс
                {
                    throw new InvalidOperationException("No empty slots");
                }
            }
        }

        public uint Get(uint key)
        {
            int step = 0;
            int startIndex = GetArrayIndex(key, step);
            int index = startIndex;
            while (true)
            {
                if (array[index] == null) // не смогли найти
                {
                    return uint.MaxValue;
                }
                else if (array[index].Key == key)
                {
                    return array[index].Value;
                }
                else // коллизия
                {
                    step++;
                    index = GetArrayIndex(key, step);
                }
                if (index == startIndex) //пришли снова в стартовый индекс
                {
                    return uint.MaxValue;
                }
            }
            return uint.MaxValue;
        }

        public uint Delete(uint key)
        {
            int step = 0;
            int startIndex = GetArrayIndex(key, step);
            int index = startIndex;
            while (true)
            {
                if (array[index] == null) // не смогли найти
                {
                    return uint.MaxValue;
                }
                else if (array[index].Key == key)
                {
                    uint value = array[index].Value;
                    array[index] = new KeyValue(true);
                    return value;
                }
                else // коллизия
                {
                    step++;
                    index = GetArrayIndex(key, step);
                }
                if (index == startIndex) //пришли снова в стартовый индекс
                {
                    return uint.MaxValue;
                }
            }
            return uint.MaxValue;
        }

        public class KeyValue
        {
            private uint key;

            public uint Key
            {
                get => key;
            }

            private uint iValue;

            public uint Value
            {
                get => iValue;
                set => iValue = value;
            }

            private bool deleted;

            public bool Deleted
            {
                get => deleted;
            }

            public KeyValue(uint key, uint value)
            {
                this.key = key;
                this.iValue = value;
                this.deleted = false;
            }

            public KeyValue(bool deleted)
            {
                this.key = uint.MaxValue;
                this.iValue = uint.MaxValue;
                this.deleted = deleted;
            }
        }
    }
}