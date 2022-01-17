/*
    Я поправил логику, но получилось, что теперь в одной функции и подсчёт дистанции, и вывод в консоль.
    Другая проблема в том, что нет видимого улучшения, по времени дольше, по памяти чуть меньше.
    Где-то дублируются массивы?
    Ведь int[] houses в CalcDistances по ссылке передаётся, не копируется? 
    Не понимаю. Передал distances по ссылке, не помогло (вроде и должно передаваться по ссылке и без ref?).

    Вот сейчас:
	ID 63978957	
    Время 1.051s
    Память 94.85Mb
    отчёт https://contest.yandex.ru/contest/22450/run-report/63978957/

    А вот было:
    ID 63940244
    Время 1.043s
    Память 97.94Mb
    отчёт https://contest.yandex.ru/contest/22450/run-report/63940244/
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Solution
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int n = ReadInt();
        int[] houses = ReadArray();
        int housesNr = houses.Length;

        int[] distances = new int[housesNr];
        CalcDistances(ref distances, houses, housesNr, housesNr - 1, 0, -1);
        CalcDistances(ref distances, houses, housesNr, 0, housesNr - 1, +1, false, true);

        reader.Close();
        writer.Close();
    }

    private static void CalcDistances(ref int[] distances, int[] houses, int housesNr, int startIndex, int stopIndex, int shift,
        bool firstPass = true, bool writeToConsole = false)
    {
        int lastEmptyLotDistance = int.MaxValue;
        int i = startIndex;
        while (i != stopIndex + shift)
        {
            if (houses[i] == 0)
                lastEmptyLotDistance = 0;
            else
            {
                if (lastEmptyLotDistance < int.MaxValue)
                    lastEmptyLotDistance++;
            }
            distances[i] = firstPass ? lastEmptyLotDistance : Math.Min(lastEmptyLotDistance, distances[i]);
            if (writeToConsole)
                writer.Write("{0} ", distances[i]);
            i += shift;
        }
    }

    private static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    private static int[] ReadArray()
    {
        return reader.ReadLine()
            .Split(new char[] { ' ', '\t', }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }
}