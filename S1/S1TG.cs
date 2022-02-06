using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class S1TG
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        var n = ReadInt();
        writer.WriteLine(ToBinary(n));

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static string ToBinary(int n)
    {
        string outBin = "";
        int pool = n;
        int div = 1, rem = 0;
        while (div > 0)
        {
            div = pool / 2;
            rem = pool % 2;
            outBin = rem + outBin;
            pool = div;
        }
        return outBin;
    }
}
