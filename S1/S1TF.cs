using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class S1TF
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        string line = reader.ReadLine();
        List<char> correctChars = new List<char>();
        foreach (char c in line)
        {
            char corrChar;
            bool correctChar = CorrectChar(c, out corrChar);
            if (correctChar)
                correctChars.Add(corrChar);
        }
        char[] correctCharsArray = correctChars.ToArray();
        int charsNr = correctCharsArray.Length;
        int middleCharIndex = (int)Math.Ceiling(charsNr / 2.0);
        bool isPalindrome = true;
        int left = 0;
        int right = charsNr - 1;
        while (right - left >= 1)
        {
            if (correctCharsArray[left] != correctCharsArray[right])
            {
                isPalindrome = false;
                break;
            }
            right--;
            left++;
        }
        string result = isPalindrome ? "True" : "False";
        writer.WriteLine(result);

        reader.Close();
        writer.Close();
    }

    private static bool CorrectChar(char c, out char corrChar)
    {
        bool correctChar = Char.IsNumber(c) || Char.IsLetter(c);
        if (correctChar)
        {
            corrChar = Char.ToLower(c);
            return true;
        }
        else
        {
            corrChar = '.';
            return false;
        }
    }
}