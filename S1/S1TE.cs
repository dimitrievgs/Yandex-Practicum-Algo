using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class S1TE
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        var L = ReadInt();
        var words = ReadList();

        var wordsNr = words.Length;
        int maxWordLength = 0;
        string maxWord = "";
        for (int i = 0; i < wordsNr; i++)
        {
            string word = words[i];
            int wordLength = word.Length;
            if (wordLength > maxWordLength)
            {
                maxWord = word;
                maxWordLength = wordLength;
            }
        }

        writer.WriteLine(maxWord);
        writer.WriteLine(maxWordLength);

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static string[] ReadList()
    {
        return reader.ReadLine()
        .Split(new char[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
        .ToArray();
    }
}