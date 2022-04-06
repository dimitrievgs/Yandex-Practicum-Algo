using System;
using System.Collections.Generic;
using System.Linq;

namespace S5TM
{
    class Solution
    {
        public static void Main(string[] args)
        {
            Test();
        }

        public static int SiftUp(List<int> array, int idx)
        {
            //Console.WriteLine(String.Join(" ", array));
            if (idx == 1)
                return idx;
            int parentIndex = idx / 2;
            if (array[parentIndex] < array[idx])
            {
                int temp = array[idx];
                array[idx] = array[parentIndex];
                array[parentIndex] = temp;
                return SiftUp(array, parentIndex);
            }
            //Console.WriteLine(idx + " -> " + largestIndex);
            return idx;
        }

        private static void Test()
        {
            var sample = new List<int> { -1, 12, 6, 8, 3, 15, 7 };
            System.Console.WriteLine(SiftUp(sample, 5) == 1);
        }
    }
}