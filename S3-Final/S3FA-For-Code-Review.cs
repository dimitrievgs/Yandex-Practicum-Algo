/*
ID 65331648
отчёт https://contest.yandex.ru/contest/23815/run-report/65331648/
задача https://contest.yandex.ru/contest/23815/problems/A/

-- ПРИНЦИП РАБОТЫ --
По условиям задачи на вход подаётся "сломанный" круговой динамический массив.
Восстаналиваю его, т.е. передаю в класс CircularSortedArray, где нахожу положение Head и Tail с помощью бинарного поиска за O(log N).
Затем с помощью бинарного поиска ищу элемент.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Первый бинарный поиск ищет последовательно отрезок, где происходит нарушение неубывания. По условиям, массив чисел в кольцевом буфере был изначально отсортирован по возрастанию.
Второй бинарный поиск, зная положения Head и Tail, работает с индексами так, словно это не кольцевой массив, а обычный, напрямую пересчитывая индексы относительно Head.
За этим исключением, это обычный бинарный поиск.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Временная сложность 2 * O(log N) -> O(log N), т.к. последовательно осуществляются два бинарных поиска.
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Пространственная сложность O(log N), т.к. используется рекурсия, и, не считая динамического массива, который берётся как есть, без копирования, 
поля CircularSortedArray (size, head, tail,..) занимают O(1); переменные left, right занимают O(1) на каждом шаге рекурсии, а т.к.
глубина рекурсии O(log N), то эти переменные, будучи складывемые в ходе рекурсии в стек, занимают O(log N).
*/

using System.Collections.Generic;

class Solution
{
    public static int BrokenSearch(List<int> list, int el)
    {
        CircularSortedArray cArray = new CircularSortedArray(list);
        int kIndex = cArray.FindIndex(el);
        return kIndex;
    }
}

class CircularSortedArray
{
    private List<int> array;

    public List<int> Array
    {
        get { return array; }
    }

    private int size;

    public int Size
    {
        get { return size; }
    }

    private int head;
    private int tail;

    public CircularSortedArray(List<int> arrayIn)
    {
        this.size = arrayIn.Count;
        array = arrayIn; //берём как есть, чтобы избежать O(N) копирования
        int[] headTail = GetHeadAndTailByBinarySearch(); //компилятор для этой задачи не поддерживает кортежи, потому использую массив из 2-х элементов
        head = headTail[0];
        tail = headTail[1];
    }

    public int[] GetHeadAndTailByBinarySearch()
    {
        int tail = GetTailByBinarySearch(0, size - 1);
        int head = (tail + 1) % size;
        return new int[] { head, tail };
    }

    public int GetTailByBinarySearch(int left, int right)
    {
        if (left >= right - 1)
        {
            if (array[left] > array[right])
                return left;
            else
                return right;
        }
        int mid = (left + right) / 2;
        //проверим, где происходит нарушение неубывания
        if (array[mid] < array[left])
            return GetTailByBinarySearch(left, mid);
        else if (array[right] < array[mid])
            return GetTailByBinarySearch(mid, right);
        else return left;
    }

    public int FindIndex(int element)
    {
        return FindIndex(head, tail, element);
    }

    /// <summary>
    /// Переводим реальный индекс в "виртульный", словно голова в нуле, 
    /// а хвост в (size-1)
    /// </summary>
    /// <param name="realIndex"></param>
    /// <returns></returns>
    private int ToVirtualIndex(int realIndex)
    {
        int num = realIndex - head;
        if (num < 0)
            num += size;
        return num;
    }

    /// <summary>
    /// Переводим виртуальный индекс (для которого словно голова в нуле, 
    /// а хвост в (size-1)) в реальный
    /// </summary>
    /// <param name="virtualIndex"></param>
    /// <returns></returns>
    private int ToRealIndex(int virtualIndex)
    {
        int num = virtualIndex + head;
        if (num > size - 1)
            num -= size;
        return num;
    }

    /// <summary>
    /// Здесь границы left и right - включая: [left, right]
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="element"></param>
    /// <returns></returns>
    public int FindIndex(int left, int right, int element)
    {
        int vLeft = ToVirtualIndex(left);
        int vRight = ToVirtualIndex(right);
        if ((vLeft == vRight && array[vLeft] != element) || vLeft > vRight) //1 элемент в массиве
            return -1;
        int mid = ToRealIndex((vLeft + vRight) / 2);
        if (array[mid] == element)
            return mid;
        else if (array[mid] > element)
            return FindIndex(left, mid, element);
        else
            return FindIndex(mid + 1, right, element);
    }
}