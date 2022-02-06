using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

public class S3TC
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        char[] s = reader.ReadLine().ToCharArray();
        char[] t = reader.ReadLine().ToCharArray();

        int sCursor = 0;
        int sLength = s.Length;
        int tLength = t.Length;

        bool contains = false;
        if (sLength == 0)
            contains = true;
        else
        {
            for (int i = 0; i < tLength; i++)
            {
                if (s[sCursor] == t[i])
                {
                    sCursor++;
                    if (sCursor == sLength)
                    {
                        contains = true;
                        break;
                    }
                }
            }
        }

        writer.Write(contains);

        reader.Close();
        writer.Close();
    }
}