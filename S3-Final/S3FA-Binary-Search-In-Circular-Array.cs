using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace S3FA
{
    class Solution
    {
        public static int BrokenSearch(List<int> list, int el)
        {
            CircularSortedArray cArray = new CircularSortedArray(list);
            int kIndex = cArray.FindIndex(el);
            return kIndex;
        }

        public static void Main(string[] args)
        {
            //ProcessConsoleData();
            Test.Do(BrokenSearch);
        }

        private static void ProcessConsoleData()
        {
            int n = ReadInt();
            int k = ReadInt();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int kIndex = BrokenSearch(ReadInts(), k);
            Console.WriteLine(kIndex);
            stopwatch.Stop();
            Console.WriteLine($"Elapsed time = {stopwatch.Elapsed.TotalSeconds:F3} sec.");
        }

        private static int ReadInt()
        {
            return int.Parse(Console.ReadLine());
        }

        private static List<int> ReadInts()
        {
            return Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
    }

    class CircularSortedArray
    {
        private List<int> array;

        public List<int> Array
        {
            get { return array; }
        }

        private int size;

        public int Size
        {
            get { return size; }
        }

        private int head;
        private int tail;

        public CircularSortedArray(List<int> arrayIn)
        {
            this.size = arrayIn.Count;
            array = arrayIn; //just take it to avoid O(N) copying
            //var headTail0 = GetHeadAndTailLinearly();
            int[] headTail = GetHeadAndTailByBinarySearch();
            head = headTail[0];
            tail = headTail[1];
        }

        /// <summary>
        /// Get Head and Tail with O(N)
        /// </summary>
        /// <returns></returns>
        private int[] GetHeadAndTailLinearly()
        {
            int head = 0, tail = 0;
            for (int i = 0; i < size; i++)
            {
                if (array[i] < array[(i - 1 + size) % size])
                {
                    head = i;
                    tail = (i - 1 + size) % size;
                    break;
                }
            }
            return new int[] { head, tail };
        }

        public int[] GetHeadAndTailByBinarySearch()
        {
            int tail = GetTailByBinarySearch(0, size - 1);
            int head = (tail + 1) % size;
            return new int[] { head, tail };
        }

        public int GetTailByBinarySearch(int left, int right)
        {
            if (left >= right - 1) 
            {
                if (array[left] > array[right])
                    return left;
                else 
                    return right;
            }
            int mid = (left + right) / 2;
            //check where the violation of non-decreasing is
            if (array[mid] < array[left])
                return GetTailByBinarySearch(left, mid);
            else if (array[right] < array[mid])
                return GetTailByBinarySearch(mid, right);
            else return left;
        }

        public int FindIndex(int element)
        {
            return FindIndex(head, tail, element);
        }

        /// <summary>
        /// Переводим реальный индекс в "виртульный", словно голова в нуле, 
        /// а хвост в (size-1)
        /// </summary>
        /// <param name="realIndex"></param>
        /// <returns></returns>
        private int ToVirtualIndex(int realIndex)
        {
            int num = realIndex - head;
            if (num < 0)
                num += size;
            return num;
        }

        /// <summary>
        /// Переводим виртуальный индекс (для которого словно голова в нуле, 
        /// а хвост в (size-1)) в реальный
        /// </summary>
        /// <param name="virtualIndex"></param>
        /// <returns></returns>
        private int ToRealIndex(int virtualIndex)
        {
            int num = virtualIndex + head;
            if (num > size - 1)
                num -= size;
            return num;
        }

        /// <summary>
        /// Здесь границы left и right - включая: [left, right]
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public int FindIndex(int left, int right, int element)
        {
            int vLeft = ToVirtualIndex(left);
            int vRight = ToVirtualIndex(right);
            if ((vLeft == vRight && array[vLeft] != element) || vLeft > vRight) //1 элемент в массиве
                return -1;
            int mid = ToRealIndex((vLeft + vRight) / 2);
            if (array[mid] == element)
                return mid;
            else if (array[mid] > element)
                return FindIndex(left, mid, element);
            else
                return FindIndex(mid + 1, right, element);
        }
    }

    class Test
    {
        public delegate int BrokenSearchDelegate(List<int> list, int el);

        public static void Do(BrokenSearchDelegate BrokenSearch)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("n?");
            Console.ForegroundColor = ConsoleColor.White;
            int n = int.Parse(Console.ReadLine());
            int k;
            List<int> testData = GenerateTestData(n, out k);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int kIndex = BrokenSearch(testData, k);
            stopwatch.Stop();
            Console.WriteLine(kIndex);
            Console.WriteLine("Elapsed time = " + stopwatch.Elapsed.TotalSeconds.ToString("F3") + " sec.");
        }

        private static List<int> GenerateTestData(int n, out int k)
        {
            Random rand = new Random();
            List<int> list = new List<int>();
            int lastElement = rand.Next(0, n / 10);
            for (int i = 0; i < n; i++)
            {
                lastElement += rand.Next(1, 5);
                list.Add(lastElement);
            }
            int separator = rand.Next(0, n);
            List<int> brokenList = list.GetRange(separator, n - separator);
            brokenList.AddRange(list.GetRange(0, separator));
            int kIndex = rand.Next(0, n);
            k = brokenList[kIndex];
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(string.Join(" ", brokenList));
            Console.WriteLine("separator index = " + separator);
            Console.WriteLine("k = " + k + ", kIndex = " + kIndex);
            Console.ForegroundColor = ConsoleColor.White;
            return brokenList;
        }
    }
}