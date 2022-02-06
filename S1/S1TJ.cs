using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class S1TJ
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int n = ReadInt();
        writer.WriteLine(string.Join(" ", Factorization(n)));

        reader.Close();
        writer.Close();
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static List<int> Factorization(int n)
    {
        List<int> numbers = new List<int>();
        int pool = n;
        for (int i = 2; i * i < n; i++)
            while (pool % i == 0)
            {
                numbers.Add(i);
                pool = pool / i;
            }
        if (pool > 1)
            numbers.Add(pool);
        return numbers;
    }
}
