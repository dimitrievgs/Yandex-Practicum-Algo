using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3TM
{
    class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = ReadInt();
            int m = ReadInt();
            int[] nArray = ReadInts();
            int[] mArray = ReadInts();

            double median = GetMedian(nArray, mArray, n, m);
            writer.WriteLine(median.ToString("0.#", CultureInfo.InvariantCulture));

            reader.Close();
            writer.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static int[] ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        public static double GetMedian(int[] nArray, int[] mArray, int n, int m)
        {
            int leftMiddle = (n + m + 1) / 2; //первый элемент левее середины, начиная с единицы, в общем массиве n + m
            int rightMiddle = (n + m + 2) / 2; //первый элемент правее середины, начиная с единицы, в общем массиве n + m
            double avg = (GetCustomMedian(nArray, mArray, 0, 0, leftMiddle) + GetCustomMedian(nArray, mArray, 0, 0, rightMiddle)) / 2.0;
            return avg;
        }
        public static int GetCustomMedian(int[] nArray, int[] mArray, int nIndex, int mIndex, int medianIndex)
        {
            if (nIndex >= nArray.Length) 
                return mArray[mIndex + medianIndex - 1];
            if (mIndex >= mArray.Length) 
                return nArray[nIndex + medianIndex - 1];
            if (medianIndex == 1) 
                return Math.Min(nArray[nIndex], mArray[mIndex]);
            int nMiddleValue = (nIndex + medianIndex / 2 - 1 < nArray.Length) ? nArray[nIndex + medianIndex / 2 - 1] : int.MaxValue;
            int mMiddleValue = (mIndex + medianIndex / 2 - 1 < mArray.Length) ? mArray[mIndex + medianIndex / 2 - 1] : int.MaxValue;
            if (nMiddleValue < mMiddleValue)
                return GetCustomMedian(nArray, mArray, nIndex + medianIndex / 2, mIndex, medianIndex - medianIndex / 2);
            else
                return GetCustomMedian(nArray, mArray, nIndex, mIndex + medianIndex / 2, medianIndex - medianIndex / 2);
        }
    }
}
