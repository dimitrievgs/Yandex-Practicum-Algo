﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace S0TB
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
            var numbers1 = ReadList();
            var numbers2 = ReadList();

            string s = "";
            for (var i = 0; i < n; i++)
            {
                if (i > 0) s += " ";
                s += numbers1[i] + " ";
                s += numbers2[i];
            }
            writer.Write(s);

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