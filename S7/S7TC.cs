using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S7TC
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int M = ReadInt();
            int n = ReadInt();
            List<GoldenPile> piles = new List<GoldenPile>();
            for (int i = 0; i < n; i++)
            {
                long[] pileParameters = ReadInts();
                piles.Add(new GoldenPile(pileParameters[0], pileParameters[1]));
            }
            piles.Sort();

            long maxValueOfGold = GetMaxValueOfGold(piles, M);
            writer.Write(maxValueOfGold);

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static long[] ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(long.Parse)
                .ToArray();
        }

        private static long GetMaxValueOfGold(List<GoldenPile> piles, int M)
        {
            long maxValueOfGold = 0;
            long leftWeight = M;
            int pileIndex = 0;
            while (leftWeight > 0 && pileIndex < piles.Count)
            {
                long weightToPick = Math.Min(piles[pileIndex].Weight, leftWeight);
                leftWeight -= weightToPick;
                maxValueOfGold += weightToPick * piles[pileIndex].Price;
                pileIndex += 1;
            }
            return maxValueOfGold;
        }
    }

    class GoldenPile : IComparable
    {
        /// <summary>
        /// c_i
        /// </summary>
        public long Price { get; set; }
        public long Weight { get; set; }

        public GoldenPile(long price, long weight)
        {
            this.Price = price;
            this.Weight = weight;
        }

        public int CompareTo(object obj)
        {
            int result = 0;
            if (obj == null)
                result = 1;
            else
            {
                GoldenPile otherPile = obj as GoldenPile;
                if (otherPile != null)
                {
                    if (this.Price.CompareTo(otherPile.Price) != 0)
                        return this.Price.CompareTo(otherPile.Price) * (-1); //по убыванию цены
                    else if (this.Weight.CompareTo(otherPile.Weight) != 0)
                        return this.Weight.CompareTo(otherPile.Weight) * (-1); //по убыванию веса
                }
                else throw new ArgumentException("Object is not a GoldenPile.");
            }
            return result;
        }
    }
}