using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace S3FB_2
{
    class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = ReadInt();
            List<Intern> interns = ReadInternsInfo(n);
            MemoryEffectiveQuickSort<Intern>.Sort(interns);
            PrintInternsLogins(interns);

            reader.Close();
            writer.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static List<Intern> ReadInternsInfo(int n)
        {
            List<Intern> interns = new List<Intern>();
            for (int i = 0; i < n; i++)
            {
                string[] elements = reader.ReadLine().Split(" ");
                string login = elements[0];
                int solvedTaskNr = int.Parse(elements[1]);
                int penalty = int.Parse(elements[2]);
                interns.Add(new Intern(login, solvedTaskNr, penalty));
            }
            return interns;
        }

        private static void PrintInternsLogins(List<Intern> interns)
        {
            foreach (var intern in interns)
                Console.WriteLine(intern.Login);
        }
    }

    class MemoryEffectiveQuickSort<T> where T : IComparable
    {
        private static Random rand = new Random();

        public static void Sort(List<T> array)
        {
            Sort(array, 0, array.Count - 1);
        }

        /// <summary>
        /// Границы беру включительно: [left, right]
        /// </summary>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        private static void Sort(List<T> array, int left, int right)
        {
            //база рекурсии
            if (left >= right)
                return;
            else if (left == right - 1)
            {
                if (array[left].CompareTo(array[right]) > 0)
                    (array[left], array[right]) = (array[right], array[left]);
                return;
            }
            //шаг рекурсии
            T pivot = array[rand.Next(left, right + 1)];
            int leftCursor = left;
            int rightCursor = right;
            while (leftCursor < rightCursor) //повторяем, пока курсоры не столкнутся
            {
                while (array[leftCursor].CompareTo(pivot) < 0) //двигаем левый указатель вправо до тех пор, пока он указывает на элемент, меньший опорного
                    leftCursor++;
                while (array[rightCursor].CompareTo(pivot) > 0) //двигаем правый указатель влево, пока он стоит на элементе, превосходящем опорный
                    rightCursor--;
                if (leftCursor < rightCursor) //Элементы, на которых стоят указатели, нарушают порядок. Поменяем их местами (в большинстве языков программирования используется функция swap()) и продвинем указатели на следующие элементы.
                {
                    (array[leftCursor], array[rightCursor]) = (array[rightCursor], array[leftCursor]);
                    leftCursor++;
                    rightCursor--;
                }
            }
            Sort(array, left, rightCursor);
            Sort(array, leftCursor, right);
        }
    }

    class Intern : IComparable
    {
        private string login;

        public string Login
        {
            get => login;
            set => login = value;
        }

        private int solvedTasksNr;

        public int SolvedTasksNr
        {
            get => solvedTasksNr;
            set => solvedTasksNr = value;
        }

        private int penalty;

        public int Penalty
        {
            get => penalty;
            set => penalty = value;
        }

        public Intern(string login, int solvedTasksNr, int penalty)
        {
            this.login = login;
            this.solvedTasksNr = solvedTasksNr;
            this.penalty = penalty;
        }

        /// <summary>
        /// При сравнении двух участников выше будет идти тот, у которого решено больше задач. 
        /// При равенстве числа решённых задач первым идёт участник с меньшим штрафом. 
        /// Если же и штрафы совпадают, то первым будет тот, у которого логин 
        /// идёт раньше в алфавитном (лексикографическом) порядке.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Intern otherIntern = obj as Intern;
            if (otherIntern != null)
            {
                //return this.temperatureF.CompareTo(otherIntern.temperatureF);
                if (this.SolvedTasksNr != otherIntern.SolvedTasksNr)
                    return otherIntern.SolvedTasksNr.CompareTo(this.SolvedTasksNr);
                else if (this.Penalty != otherIntern.Penalty)
                    return this.Penalty.CompareTo(otherIntern.Penalty);
                else
                    return this.Login.CompareTo(otherIntern.Login);
            }
            else
                throw new ArgumentException("Object is not an Intern");
        }
    }

    class Test
    {
        private static Random rand = new Random();

        public static List<int> RandomData()
        {
            List<int> array = new List<int>();
            int n = 0;
            while (n == 0)
            {
                Console.WriteLine("n?");
                int.TryParse(Console.ReadLine(), out n);
            }
            for (int i = 0; i < n; i++)
                array.Add(rand.Next(0, 3 * n));
            return array;
        }

        public static bool CheckIfSorted(List<int> array)
        {
            List<int> sortedArray = array.ToList();
            sortedArray.Sort();
            return array.SequenceEqual(sortedArray);
        }
    }
}
