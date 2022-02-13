using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3TP
{
    class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            Console.ForegroundColor = ConsoleColor.Green;
            int n = ReadInt();
            int[] array = ReadInts();
            Console.ForegroundColor = ConsoleColor.White;

            //O(N) - go through array and tack maximums from 0 index to each element
            ValueAndLeftMax[] valuesWithMaximums = new ValueAndLeftMax[n];
            int max = int.MinValue;
            for (int i = 0; i < n; i++)
            {
                int el = array[i];
                max = Math.Max(max, el);
                valuesWithMaximums[i] = new ValueAndLeftMax(el, max);
            }

            //O(N^2) - for each element El check if there is no element to the right that is < El.Max
            //actually looks like we need simpler algo - for element just track maximum
            //from 0 index to it and minimum from last index to it, but I stopped with this
            List<ValueAndLeftMax> rightBlockBorders = new List<ValueAndLeftMax>();
            for (int i = 0; i < n; i++)
            {
                var el = valuesWithMaximums[i];
                var curMax = el.LeftMax;
                bool lessExistToTheRight = false;
                for (int j = i + 1; j < n; j++)
                {
                    if (valuesWithMaximums[j].Value < curMax)
                    {
                        lessExistToTheRight = true;
                        i = j - 1; //to get j at next i-for-loop cycle
                        break;
                    }
                }
                if (!lessExistToTheRight)
                    rightBlockBorders.Add(valuesWithMaximums[i]);
            }

            writer.WriteLine(rightBlockBorders.Count);

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
    }

    class ValueAndLeftMax
    {
        private int value;

        public int Value
        {
            get => value;
            set => this.value = value;
        }

        private int leftMax;

        public int LeftMax
        {
            get => leftMax; 
            set => leftMax = value;
        }

        public ValueAndLeftMax(int value, int leftMax)
        {
            this.value = value;
            this.leftMax = leftMax;
        }
    }
}
