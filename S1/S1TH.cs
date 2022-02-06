using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class S1TH
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        string a = reader.ReadLine();
        string b = reader.ReadLine();
        writer.WriteLine(BinarySum(a, b));

        reader.Close();
        writer.Close();
    }

    private static string BinarySum(string a, string b)
    {
        string result = "";
        int aL = a.Length;
        int bL = b.Length;
        int L = Math.Max(aL, bL);
        int zeroCode = (int)'0';
        int div = 0, rem = 0;
        int i = 0;
        while (i < L || div > 0)
        {
            int cA = (i <= aL - 1) ? (int)((a[aL - i - 1]) - zeroCode) : 0;
            int cB = (i <= bL - 1) ? (int)((b[bL - i - 1]) - zeroCode) : 0;
            int sum = cA + cB + div;
            rem = sum % 2;
            div = sum / 2;
            result = rem + result;
            i++;
        }
        return result;
    }
}
