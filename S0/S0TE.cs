using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace S0TE
{
    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            var n = ReadInt();
            var numbers = ReadList();
            var k = ReadInt();

            int left = 0, right = n - 1;
            int i1 = 0, i2 = 0;
            bool success = false;
            while (left != right)
            {
                if (numbers[left] + numbers[right] == k)
                {
                    i1 = left;
                    i2 = right;
                    success = true;
                    break;
                }
                else if (numbers[left] + numbers[right] < k)
                    left += 1;
                else
                    right -= 1;
            }
            if (success)
                writer.Write(numbers[i1] + " " + numbers[i2]);
            else
                writer.Write("None");

            reader.Close();
            writer.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static List<int> ReadList()
        {
            return reader.ReadLine()
                .Split(new[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }
    }
}