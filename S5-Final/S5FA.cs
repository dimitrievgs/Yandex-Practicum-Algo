/*
ID 
отчёт 
задача 

-- ПРИНЦИП РАБОТЫ --

-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --

-- ВРЕМЕННАЯ СЛОЖНОСТЬ --

-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
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
            List<Intern> interns = new List<Intern>();
            for (int i = 0; i < n; i++)
                interns.Add(ReadIntern());

            interns = HeapSort(interns);

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

        private static List<Intern> HeapSort(List<Intern> array)
        {
            BinaryHeap<Intern> heap = new BinaryHeap<Intern>();

            foreach (var item in array)
                heap.Add(item);

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
