using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4TG
{
    class Solution
    {
        public static void Main(string[] args)
        {
            int n = ReadInt();
            int[] results = null;
            if (n > 0)
                results = ReadInts();
            int maxLengthOfDrawResult = 0;
            if (n > 1)
            {
                // 0 -> -1
                for (int i = 0; i < n; i++)
                    if (results[i] == 0)
                        results[i] = -1;

                // префиксные суммы
                Dictionary<int, List<int>> dict = new Dictionary<int, List<int>>();
                dict.Add(0, new List<int> { -1 }); // -1-й элемент имеет префиксную сумму 0, нужно для правильного расчёта
                int trackSum = 0;
                for (int i = 0; i < n; i++)
                {
                    trackSum += results[i];
                    dict.TryGetValue(trackSum, out List<int> list);
                    if (list != null)
                        list.Add(i);
                    else
                        dict.Add(trackSum, new List<int> { i });
                }

                // смотрим максимальное расстояние между двумя одинаковыми элементами в массиве префиксных сумм
                foreach (var el in dict)
                {
                    int f = el.Value.First();
                    int l = el.Value.Last();
                    int newMaxLength = l - f;
                    maxLengthOfDrawResult = Math.Max(newMaxLength, maxLengthOfDrawResult);
                }
            }
            else
                maxLengthOfDrawResult = 0;
            Console.WriteLine(maxLengthOfDrawResult);
        }

        private static int ReadInt()
        {
            return int.Parse(Console.ReadLine());
        }

        private static int[] ReadInts()
        {
            return Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }
}
