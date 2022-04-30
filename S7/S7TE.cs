using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S7TE
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int x = ReadInt();
            int k = ReadInt();
            int[] denominations = ReadInts();
            Array.Sort(denominations, new Comparison<int>((i1, i2) => -1 * i1.CompareTo(i2)));

            var minBanknotesNumber = GetMinBanknotesNumber(x, denominations);
            writer.Write(minBanknotesNumber);

            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static int[] ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }

        private static int GetMinBanknotesNumber(int totalMoneyAmount, int[] denominations)
        {
            //введём динамику minBanknotesForMoney[i]: в каждой ячейке минимальное кол-во купюр для суммы (i + 1)
            int[] minBanknotesForMoney = new int[totalMoneyAmount];
            for (int considMoneyValue = 1; considMoneyValue <= totalMoneyAmount; considMoneyValue++)
            {
                minBanknotesForMoney[considMoneyValue - 1] = -1;
                for (int j = 0; j < denominations.Length; j++) //по каждому номиналу банкнот
                {
                    int banknoteValue = denominations[j];
                    if (banknoteValue == considMoneyValue)
                    {
                        minBanknotesForMoney[considMoneyValue - 1] = 1;
                        break;
                    }
                    int curDenominationsBanknotesMax = (int)Math.Ceiling(considMoneyValue / (double)banknoteValue);
                    for (int k = 1; k <= curDenominationsBanknotesMax; k++)
                    {
                        int diff = considMoneyValue - k * banknoteValue;
                        if (diff < 0) //сликшом много вычли
                            break;
                        int diffBanknotesNr = diff > 0 ? minBanknotesForMoney[diff - 1] : 0;
                        if (diffBanknotesNr < 0)
                            continue; //оставшуюся сумму не набрать банкнотами, попробуем взять ещё одну такую же купюру
                        //если уже получали кол-во банкнот для данной ячейки (суммы) и текущее k больше, то дальше смотреть смысла нет
                        if (minBanknotesForMoney[considMoneyValue - 1] > 0 && k >= minBanknotesForMoney[considMoneyValue - 1])
                            break;
                        //если мы уже считали кол-во банкнот для данной ячейки, то сравним и оставим наименьшее
                        minBanknotesForMoney[considMoneyValue - 1] =
                            (minBanknotesForMoney[considMoneyValue - 1] > 0) ?
                            Math.Min(k + diffBanknotesNr, minBanknotesForMoney[considMoneyValue - 1]) :
                            k + diffBanknotesNr;
                    }
                }
            }
            return minBanknotesForMoney[totalMoneyAmount - 1]; //в последней ячейке массива - решение
        }
    }
}