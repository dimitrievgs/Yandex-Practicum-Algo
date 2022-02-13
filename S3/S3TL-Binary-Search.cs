using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3TL
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
            int[] array = ReadInts();
            int s = ReadInt();

            int priceOnce = BinarySearchEqualOrHigher(array, 0, array.Length, s);
            if (priceOnce != -1)
                priceOnce = Math.Min(array.Length, priceOnce + 1);
            int priceTwice = BinarySearchEqualOrHigher(array, 0, array.Length, s * 2);
            if (priceTwice != -1)
                priceTwice = Math.Min(array.Length, priceTwice + 1);
            writer.WriteLine(priceOnce + " " + priceTwice);

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

        private static int BinarySearchEqualOrHigher(int[] array, int left, int right, int price)
        {
            if (left == right || (left + 1 == right && array[left] < price))
            {
                if (right < array.Length && array[right] >= price)
                    return right;
                else return -1;
            }

            int mid = (left + right) / 2;
            if (array[mid] >= price)
                return BinarySearchEqualOrHigher(array, left, mid, price);
            else
                return BinarySearchEqualOrHigher(array, mid, right, price); 
        }

        private static int BinarySearch(int[] array, int left, int right, int element)
        {
            if (left >= right)
                return -1;
            int mid = (left + right) / 2;
            if (array[mid] == element)
                return mid;
            else if (array[mid] < element)
                return BinarySearch(array, mid, right, element);
            else
                return BinarySearch(array, left, mid, element);
        }
    }
}
