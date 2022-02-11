using System.Collections.Generic;

public class Solution
{
    /*private static TextReader reader;
    private static TextWriter writer;
    private static Random random = new Random();

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int n = int.Parse(reader.ReadLine());
        List<int> array = RandomArray(n);
        writer.WriteLine(string.Join(" ", array));
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        MergeSort(array, 0, n);
        stopwatch.Stop();
        writer.WriteLine(string.Join(" ", array));
        writer.WriteLine("-----------------------------");
        writer.WriteLine(stopwatch.Elapsed.TotalSeconds);

        reader.Close();
        writer.Close();
    }

    private static List<int> RandomArray(int n)
    {
        List<int> array = new List<int>();
        for (int i = 0; i < n; i++)
            array.Add(random.Next(0, 890000));
        return array;
    }
    */
    public static void MergeSort(List<int> array, int left, int right)
    {
        if (right - left <= 1)
            return;
        int mid = (left + right) / 2;
        MergeSort(array, left, mid);
        MergeSort(array, mid, right);
        array = Merge(array, left, mid, right);
    }

    public static List<int> Merge(List<int> array2, int left2, int mid2, int right2)
    {
        //Copying part of list in the array to get O(1) for getting elements
        int[] array = array2.GetRange(left2, right2 - left2).ToArray();
        int[] result = (int[])array.Clone();
        int left = 0;
        int mid = mid2 - left2;
        int right = right2 - left2;

        int leftArPointer = left;
        int rightArPointer = mid;
        int resultPointer = left;

        while (leftArPointer < mid && rightArPointer < right)
        {
            int leftEl = array[leftArPointer];
            int rightEl = array[rightArPointer];
            if (leftEl < rightEl)
            {
                result[resultPointer] = leftEl;
                resultPointer++;
                leftArPointer++;
            }
            else
            {
                result[resultPointer] = rightEl;
                resultPointer++;
                rightArPointer++;
            }
        }

        while (leftArPointer < mid)
        {
            result[resultPointer] = array[leftArPointer];
            resultPointer++;
            leftArPointer++;
        }

        while (rightArPointer < right)
        {
            result[resultPointer] = array[rightArPointer];
            resultPointer++;
            rightArPointer++;
        }

        for (int i = left; i < right; i++)
            array2[i + left2] = result[i];

        return array2;
    }
}