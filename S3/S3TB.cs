using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

public class S3TB
{
    private static TextReader reader;
    private static TextWriter writer;

    private static Dictionary<int, char[]> telButtonSymbols =
        new Dictionary<int, char[]> {
            { 2, new char[] { 'a', 'b', 'c' } },
            { 3, new char[] { 'd', 'e', 'f' } },
            { 4, new char[] { 'g', 'h', 'i' } },
            { 5, new char[] { 'j', 'k', 'l' } },
            { 6, new char[] { 'm', 'n', 'o' } },
            { 7, new char[] { 'p', 'q', 'r', 's' } },
            { 8, new char[] { 't', 'u', 'v' } },
            { 9, new char[] { 'w', 'x', 'y', 'z' } }
        };

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        List<int> buttonsNumbers = ReadButtons();
        GetCombinations(buttonsNumbers, "");

        reader.Close();
        writer.Close();
    }

    private static List<int> ReadButtons()
    {
        char[] chars = reader.ReadLine().ToCharArray();
        List<int> numbers = new List<int>();
        foreach (char c in chars)
            numbers.Add(int.Parse(c.ToString()));
        return numbers;
    }

    private static void GetCombinations(List<int> buttonsNumbers, string word)
    {
        int remainedNumbersCount = buttonsNumbers.Count;
        if (remainedNumbersCount == 0)
        {
            writer.Write(word + " ");
            return;
        }

        int number = buttonsNumbers.First();
        List<int> remainedButtonsNumbers = buttonsNumbers.GetRange(1, remainedNumbersCount - 1);
        char[] correspondingSymbols = telButtonSymbols[number];
        foreach (char symbol in correspondingSymbols)
            GetCombinations(remainedButtonsNumbers, word + symbol);
    }
}