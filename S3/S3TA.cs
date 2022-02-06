using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

class S3TA
{
    public static TextReader textReader;
    public static TextWriter textWriter;

    public static void Main(string[] args)
    {
        textReader = new StreamReader(Console.OpenStandardInput());
        textWriter = new StreamWriter(Console.OpenStandardOutput());

        int n = ReadInt();

        GetBracketSequence(2 * n, 2 * n, 0, 0, new List<char>());

        textReader.Close();
        textWriter.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(textReader.ReadLine());
    }

    private static void GetBracketSequence(int totalSymbols, int remainedSymbols,
        int openBrackets, int closeBrackets, List<char> sequence)
    {
        if (remainedSymbols == 0)
        {
            textWriter.WriteLine(string.Join("", sequence));
            return;
        }

        if (openBrackets < totalSymbols / 2)
        {
            List<char> newSequence = sequence.ToList();
            newSequence.Add('(');
            GetBracketSequence(totalSymbols, remainedSymbols - 1, openBrackets + 1, closeBrackets, newSequence);
        }

        if (openBrackets - closeBrackets > 0)
        {
            List<char> newSequence = sequence.ToList();
            newSequence.Add(')');
            GetBracketSequence(totalSymbols, remainedSymbols - 1, openBrackets, closeBrackets + 1, newSequence);
        }
    }
}