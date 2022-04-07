/*
ID 66936024
отчёт https://contest.yandex.ru/contest/24810/run-report/66936024/
задача https://contest.yandex.ru/contest/24810/problems/A/

-- ПРИНЦИП РАБОТЫ --
В задаче нужно реализовать сортировку массива с помощью бинарной кучи.
Для этого сначала создаётся бинарная куча, на основе массива. 
Туда последовательно добавляются элементы из входящего массива.
В процессе каждого добавления реализуется просеивание вверх, чтобы восстановить свойство,
согласно которому ключи дочерних узлов не больше родительских.
Затем создаётся новый динамический массив, куда последовательно складываются
отсортированные элементы.
Элементы в новый массив складываются путём удаления элемента с максимальным ключом
из кучи. После каждого удаления корня кучи осуществляется просеивание вниз,
для восстановления свойства кучи, что ключи дочерних узлов не больше родительских.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Поскольку мы реализуем кучу, в которой ключи дочерних узлов не больше родительских,
то в корне кучи должен храниться элемент в максимальным ключом. 
Поэтому последовательно удаляя корень бинарной кучи, мы сортируем элементы
по невозрастанию.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Создание бинарной кучи - O(1), т.к. выделяем память под динамический массив.
Далее последовательная вставка n элементов в кучу с просеиванием вверх:
за O(log 1) + O(log 2) + ... + O(log N) ~ O(N log N)
Далее извлечение элементов из корня с просеиванием вниз:
за O(log N) + ... + O(log 1) + O(log 2) ~ O(N log N)
Таким образом, сложность алгоритма O(N log N)
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Для реализации алгоритма пирамидальной сортировки требуется выделить
O(N) памяти для промежуточного хранения элементов в бинарной куче
и O(N) памяти для итогового массива с отсортированными элементами.
Поэтому пространственная сложность O(N).
-- ПРАВКИ --
Не знаю, верно ли я понял комментарии. Да, сейчас увидел, что там выделялся дополнительный 
массив для считывания экземпляров Intern. Убрал это, теперь считывается прямо в кучу. 
Так что до этого было по пространственной сложности формально 3 O(n) ~ O(n). 
Поэтому я оставил блок "Пространственная сложность" без изменения, 
вроде бы, сейчас он корректен.
Также подправил для класса Intern метод int CompareTo(object obj):
там пришлось вернуть else перед throw (замечание к одной из предыдущих финальных задач), 
чтобы логику не поломать. Лучше ли так, как сейчас сделано?
*/

using System;
using System.Collections.Generic;

namespace S5FA
{
    class Solution
    {
        public static void Main(string[] args)
        {
            int n = ReadInt();
            List<Intern> interns = InternsHeapSort(n);
            foreach (var intern in interns)
                Console.WriteLine(intern.Login);
        }

        private static int ReadInt()
        {
            return int.Parse(Console.ReadLine());
        }

        private static Intern ReadIntern()
        {
            string[] fields = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string login = fields[0];
            int solvedTasksNr = int.Parse(fields[1]);
            int penalty = int.Parse(fields[2]);
            return new Intern(login, solvedTasksNr, penalty);
        }

        private static List<Intern> InternsHeapSort(int n)
        {
            BinaryHeap<Intern> heap = new BinaryHeap<Intern>();

            for (int i = 0; i < n; i++)
                heap.Add(ReadIntern());

            List<Intern> sortedArray = new List<Intern>();
            while (heap.Size > 0)
                sortedArray.Add(heap.PopMax());
            return sortedArray;
        }
    }

    class BinaryHeap<T> where T : IComparable
    {
        private List<T> array;
        private int size;

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public BinaryHeap()
        {
            array = new List<T> { default(T) }; //нумерация с 1го элемента
            size = 0;
        }

        public void Add(T item)
        {
            this.size += 1;
            int index = this.size;
            array.Add(item);
            SiftUp(index);
        }

        private void SiftUp(int index)
        {
            if (index == 1)
                return;
            int parentIndex = index / 2;
            if (array[parentIndex].CompareTo(array[index]) > 0)
            {
                (array[index], array[parentIndex]) = (array[parentIndex], array[index]);
                SiftUp(parentIndex);
            }
            return;
        }

        public T PopMax()
        {
            T result = this.array[1];
            if (this.size > 1)
            {
                this.array[1] = this.array[this.size];
                this.array.RemoveAt(this.size);
            }
            this.size -= 1;
            SiftDown(1);
            return result;
        }

        private void SiftDown(int index)
        {
            int leftIndex = 2 * index;
            int rightIndex = 2 * index + 1;

            int indexOfLargest = index;
            if (this.size < leftIndex)
                return;
            else if (this.size >= rightIndex && array[leftIndex].CompareTo(array[rightIndex]) > 0)
                indexOfLargest = rightIndex;
            else
                indexOfLargest = leftIndex;

            if (array[index].CompareTo(array[indexOfLargest]) > 0)
            {
                (array[index], array[indexOfLargest]) = (array[indexOfLargest], array[index]);
                SiftDown(indexOfLargest);
            }
        }
    }

    class Intern : IComparable
    {
        private string login;
        private int solvedTasksNr;
        private int penalty;

        public string Login
        {
            get { return login; }
        }

        public int SolvedTasksNr
        {
            get { return solvedTasksNr; }
        }

        public int Penalty
        {
            get { return penalty; }
        }

        public Intern(string login, int solvedTasksNr, int penalty)
        {
            this.login = login;
            this.solvedTasksNr = solvedTasksNr;
            this.penalty = penalty;
        }

        public int CompareTo(object obj)
        {
            int result = 0;
            if (obj == null)
                result = 1;
            else
            {
                Intern otherIntern = obj as Intern;
                if (otherIntern != null)
                {
                    if (this.SolvedTasksNr != otherIntern.SolvedTasksNr)
                        result = otherIntern.SolvedTasksNr.CompareTo(this.SolvedTasksNr);
                    else if (this.Penalty != otherIntern.Penalty)
                        result = this.Penalty.CompareTo(otherIntern.Penalty);
                    else
                        result = this.Login.CompareTo(otherIntern.Login);
                }
                else
                    throw new ArgumentException("Object is not an Intern");
            }
            return result;
        }
    }
}
