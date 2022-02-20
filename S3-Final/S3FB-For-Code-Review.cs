/*
ID 65349921
отчёт https://contest.yandex.ru/contest/23815/run-report/65349921/
задача https://contest.yandex.ru/contest/23815/problems/B/

-- ПРИНЦИП РАБОТЫ --
На вход подаётся информация об интернах, из логинах, количестве решённых задач и штрафе при решении.
Формирую динамический массив List<Intern> interns. Сортирую его QuickSort'ом, но модифицированным, 
как в условиях задачи: на каждом шаге рекурсии использую два указателя leftCursor и rightCursor,
которые двигаются от изначальных краёв отрезка на каждом шаге рекурсии к противоположному краю, 
попутно сравнивая элементы с опорным pivot, положение которого выбирается случайным образом между left и right.
Левый указатель проверяет, что каждый следующий элемент меньше опорного, а правый - что больше.
Если оба указателя находят элементы, которые нарушают это правило, то элементы меняются местами в массиве.
Дальше алгоритм повторяется, пока указатели на текущем отрезке не столкнутся.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
За исключеним того, что вместо дополнительных массивов используются указатели, алгоритм аналогичен 
классическому QuickSort'у, поскольку в результате мы получаем пространственное разделение элементов 
меньших (левее leftCursor) и больших (правее rightCursor) опорного.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Временная сложность аналогична классическому QuickSort'у и в среднем составляет O(N * log N), в худшем O(N^2).
Поскольку операция разделения массива на две части на каждом шаге рекурсии занимает в среднем O(log N),
а в худшем случае O(N), если каждое последующее разделение делит входной отрезок на 1 и n-1 (например,
если в качестве опорного выбирается наименьший или наибольший на данном шаге). На каждом уровне рекурсии 
выполняется O(N) операций, поскольку все элементы на текущем отрезке текущего шага рекурсии сравниваются 
с текущим опорным.
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Пространственная сложность сортировки определяется глубиной рекурсии * локальные параметры на каждом шаге 
(left, right), которые занимают O(1). Поскольку глубина рекурсии в среднем составляет O(log N), а в худшем
случае O(N), то пространственная сложность O(log N) в среднем и O(N) в худшем случае.
-- ПРАВКИ --
Исправил последовательность writer.Close() / reader.Close(); убрал ReadInt(). 
Исключение ArgumentException("Object is not an Intern") теперь выбрасывается по умолчанию 
в конце блока кода метода, без else.
*/

using System;
using System.Collections.Generic;
using System.IO;

namespace S3FB
{
    class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = int.Parse(reader.ReadLine());
            List<Intern> interns = ReadInternsInfo(n);
            MemoryEffectiveQuickSort<Intern>.Sort(interns);
            PrintInternsLogins(interns);

            writer.Close();
            reader.Close();
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
        /// По условиям задачи, при сравнении двух участников выше будет идти тот, у которого решено больше задач. 
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
                if (this.SolvedTasksNr != otherIntern.SolvedTasksNr)
                    return otherIntern.SolvedTasksNr.CompareTo(this.SolvedTasksNr);
                else if (this.Penalty != otherIntern.Penalty)
                    return this.Penalty.CompareTo(otherIntern.Penalty);
                else
                    return this.Login.CompareTo(otherIntern.Login);
            }
            throw new ArgumentException("Object is not an Intern");
        }
    }
}
