using System;
using System.Collections.Generic;
using System.Linq;

namespace S5TL
{
    class Solution
    {
        public static void Main(string[] args)
        {
            Test();
        }

        public static int SiftDown(List<int> array, int idx)
        {
            //Console.WriteLine(String.Join(" ", array));
            int leftIndex = 2 * idx;
            int rightIndex = 2 * idx + 1;

            int arrayLength = array.Count;
            int largestIndex = idx;
            if (arrayLength == leftIndex + 1)
            {
                if (array[leftIndex] > array[idx])
                    largestIndex = leftIndex;
            }
            else if (arrayLength >= rightIndex + 1)
            {
                List<int> indeces = new List<int> { leftIndex, rightIndex };
                int temp = array[idx];
                foreach (int n in indeces)
                {
                    if (array[n] > temp)
                    {
                        temp = array[n];
                        largestIndex = n;
                    }
                }
            }

        //Console.WriteLine(idx + " -> " + largestIndex);

        if (idx != largestIndex)
            {
                int temp = array[idx];
                array[idx] = array[largestIndex];
                array[largestIndex] = temp;
                return SiftDown(array, largestIndex);
            }
            else
                return largestIndex;
        }

        private static void Test()
        {
            //var sample = new List<int> { -1, 12, 1, 8, 3, 4, 7 };
            //System.Console.WriteLine(SiftDown(sample, 2) == 5);
            var sample = new List<int> { 0, 0, 9, 6, 2, 4, 1 };
            System.Console.WriteLine(SiftDown(sample, 1));
        }
    }
}