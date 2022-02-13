// It should be sent as the Solution.cs file to be compiled by the Make compiler, without namespace.
// Signatures of MergeSort(List<int> listArray, int left, int right) and Merge(List<int> listArray, int left, int mid, int right) are fixed by the task.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace S3TK
{
    public class Solution
    {
        /*private static TextReader reader;
        private static TextWriter writer;
        private static Random random = new Random();

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = int.Parse(reader.ReadLine());
            List<int> array = RandomArray(n);
            writer.WriteLine(string.Join(" ", array));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            MergeSort(array, 0, n);
            stopwatch.Stop();
            writer.WriteLine(string.Join(" ", array));
            writer.WriteLine("-----------------------------");
            writer.WriteLine(stopwatch.Elapsed.TotalSeconds);

            reader.Close();
            writer.Close();
        }

        private static List<int> RandomArray(int n)
        {
            List<int> array = new List<int>();
            for (int i = 0; i < n; i++)
                array.Add(random.Next(0, 890000));
            return array;
        }*/
        
        public static void MergeSort(List<int> listArray, int left, int right)
        {
            if (right - left <= 1)
                return;
            int mid = (left + right) / 2;
            MergeSort(listArray, left, mid);
            MergeSort(listArray, mid, right);
            listArray = Merge(listArray, left, mid, right);
        }

        public static List<int> Merge(List<int> listArray, int left, int mid, int right)
        {
            //Copying part of list in the array to get O(1) for getting elements //otherwise I get "TL": for list with 53548 it's more than 2 sec on Yandex Contest. This algo takes 32 ms at my local pc.
            int[] array = listArray.GetRange(left, right - left).ToArray();
            int[] result = (int[])array.Clone();
            int resLeft = 0;
            int resMid = mid - left;
            int resRight = right - left;
            int leftArPointer = resLeft;
            int rightArPointer = resMid;
            int resultPointer = resLeft;

            while (leftArPointer < resMid && rightArPointer < resRight)
            {
                int leftEl = array[leftArPointer];
                int rightEl = array[rightArPointer];
                if (leftEl < rightEl)
                {
                    result[resultPointer] = leftEl;
                    resultPointer++;
                    leftArPointer++;
                }
                else
                {
                    result[resultPointer] = rightEl;
                    resultPointer++;
                    rightArPointer++;
                }
            }

            while (leftArPointer < resMid)
            {
                result[resultPointer] = array[leftArPointer];
                resultPointer++;
                leftArPointer++;
            }

            while (rightArPointer < resRight)
            {
                result[resultPointer] = array[rightArPointer];
                resultPointer++;
                rightArPointer++;
            }

            //looks like here access to listArray is not O(1), should be done in better way
            for (int i = resLeft; i < resRight; i++)
                listArray[i + left] = result[i];

            return listArray;
        }
    }
}