using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace S1FA
{
    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = ReadInt();
            int[] houses = ReadArray();
            int housesNr = houses.Length;

            int[] distances = new int[housesNr];
            CalcDistances(ref distances, houses, housesNr, housesNr - 1, 0, -1);
            CalcDistances(ref distances, houses, housesNr, 0, housesNr - 1, +1, false, true);

            reader.Close();
            writer.Close();
        }

        private static void CalcDistances(ref int[] distances, int[] houses, int housesNr, int startIndex, int stopIndex, int shift,
            bool firstPass = true, bool writeToConsole = false)
        {
            int lastEmptyLotDistance = int.MaxValue;
            int i = startIndex;
            while (i != stopIndex + shift)
            {
                if (houses[i] == 0)
                    lastEmptyLotDistance = 0;
                else
                {
                    if (lastEmptyLotDistance < int.MaxValue)
                        lastEmptyLotDistance++;
                }
                distances[i] = firstPass ? lastEmptyLotDistance : Math.Min(lastEmptyLotDistance, distances[i]);
                if (writeToConsole)
                    writer.Write("{0} ", distances[i]);
                i += shift;
            }
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static int[] ReadArray()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }
}