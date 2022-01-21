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

        List<char> s = reader.ReadLine().ToCharArray().ToList();
        List<char> t = reader.ReadLine().ToCharArray().ToList();
        foreach (char c in s)
            t.Remove(c);
        writer.WriteLine(string.Join(" ", t));

        reader.Close();
        writer.Close();
    }
}