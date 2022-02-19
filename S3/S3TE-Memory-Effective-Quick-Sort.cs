using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S3TE
{
    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            List<int> nK = ReadInts();
            int n = nK[0]; //number of houses
            int k = nK[1]; //total budget
            List<int> housesCosts = ReadInts(); //costs of houses
            //List<int> housesCosts = TestHousesCost(n, k);
            QuickSort(housesCosts);
            int houseIndex = 0;
            int remainedBudget = k;
            while (houseIndex < n)
            {
                int houseCost = housesCosts[houseIndex];
                if (houseCost <= remainedBudget)
                {
                    remainedBudget -= houseCost;
                    houseIndex++;
                }
                else
                    break;
            }
            writer.WriteLine(houseIndex);

            reader.Close();
            writer.Close();
        }

        private static List<int> ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();
        }

        private static List<int> TestHousesCost(int n, int houseCost)
        {
            List<int> list = new List<int>();
            Random rand = new Random();
            for (int i = 0; i < n; i++)
                list.Add(rand.Next(1, houseCost * 3));
            return list;
        }

        private static Random rand = new Random();

        private static void QuickSort(List<int> array)
        {
            QuickSort(array, 0, array.Count - 1);
        }

        /// <summary>
        /// Границы беру включительно: [left, right]
        /// </summary>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static void QuickSort(List<int> array, int left, int right)
        {
            //база рекурсии
            if (left >= right)
                return;
            else if (left == right - 1)
            {
                if (array[left] > array[right])
                    (array[left], array[right]) = (array[right], array[left]);
                return;
            }
            //шаг рекурсии
            int pivot = array[rand.Next(left, right + 1)];
            int leftCursor = left;
            int rightCursor = right;
            while (leftCursor < rightCursor) //повторяем, пока курсоры не столкнутся
            {
                while (array[leftCursor] < pivot) //двигаем левый указатель вправо до тех пор, пока он указывает на элемент, меньший опорного
                    leftCursor++;
                while (array[rightCursor] > pivot) //двигаем правый указатель влево, пока он стоит на элементе, превосходящем опорный
                    rightCursor--;
                if (leftCursor < rightCursor) //Элементы, на которых стоят указатели, нарушают порядок. Поменяем их местами (в большинстве языков программирования используется функция swap()) и продвинем указатели на следующие элементы.
                {
                    (array[leftCursor], array[rightCursor]) = (array[rightCursor], array[leftCursor]);
                    leftCursor++;
                    rightCursor--;
                }
            }
            QuickSort(array, left, rightCursor);
            QuickSort(array, leftCursor, right);
        }
    }
}