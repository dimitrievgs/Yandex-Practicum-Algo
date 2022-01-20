using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int a = ReadInt();
        writer.WriteLine(IsFourDegree(a));

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static bool IsFourDegree(int a)
    {
        int div, rem;
        int pool = a;
        if (a == 1)
            return true;
        while (pool > 1)
        {
            div = pool / 4;
            rem = pool % 4;
            if (rem > 0)
                return false;
            pool = div;
        }
        return true;
    }
}
