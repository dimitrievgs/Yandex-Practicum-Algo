using System;
using System.Linq;
using System.IO;

namespace S8TB
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            var nameOne = reader.ReadLine();
            var nameTwo = reader.ReadLine();
            int i = 0;
            int j = 0;
            int diffCount = 0;
            while (i < nameOne.Length && j < nameTwo.Length)
            {
                if (nameOne[i] == nameTwo[j])
                {
                    i++;
                    j++;
                }
                else
                {
                    diffCount++;
                    if (j + 1 < nameTwo.Length && nameOne[i] == nameTwo[j + 1])
                    {
                        i++;
                        j += 2;
                    }
                    else if (i + 1 < nameOne.Length && nameOne[i + 1] == nameTwo[j])
                    {
                        i += 2;
                        j++;
                    }
                    else
                    {
                        i++;
                        j++;
                    }
                }
                if (diffCount >= 2)
                    break;
            }
            if (i < nameOne.Length || j < nameTwo.Length) //последний символ у какой-то из строк не был сравнён
                diffCount++;
            writer.WriteLine(diffCount <= 1 ? "OK" : "FAIL");

            writer.Close();
            reader.Close();
        }
    }
}