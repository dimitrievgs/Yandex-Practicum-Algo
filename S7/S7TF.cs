using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S7TF
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int[] parameters = ReadInts();
            int stepsCount = parameters[0];
            int maxStepsJump = parameters[1];

            int waysToReachLastStep = CountWaysToReachLastStep(stepsCount, maxStepsJump);
            writer.Write(waysToReachLastStep);

            writer.Close();
            reader.Close();
        }

        private static int[] ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        private const int divider = 1_000_000_007;

        private static int CountWaysToReachLastStep(int stepsCount, int maxStepsJump)
        {
            int[] dp = new int[stepsCount]; //введём массив для динамического решения
            dp[0] = 1;
            for (int i = 1; i < stepsCount; i++)
            {
                int firstReachableStep = Math.Max(i - maxStepsJump, 0);
                int lastReachableStep = Math.Max(i - 1, 0);
                int nrOfWays = 0;
                for (int j = firstReachableStep; j <= lastReachableStep; j++)
                    nrOfWays = (nrOfWays + dp[j]) % divider;
                dp[i] = nrOfWays;
            }
            return dp[stepsCount - 1];
        }
    }
}