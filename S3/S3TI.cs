using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class S3TI
{
    private static TextReader reader;
    private static TextWriter writer;

    public static void Main(string[] args)
    {
        reader = new StreamReader(Console.OpenStandardInput());
        writer = new StreamWriter(Console.OpenStandardOutput());

        int n = ReadInt();
        int[] universitiesIDs = ReadInts();
        int k = ReadInt();

        Array.Sort(universitiesIDs);
        List<University> universities = new List<University>();
        int i = 0, j = 0;
        int lastUniversityID = int.MinValue;
        University lastUniversity = null;
        while (i < n)
        {
            int universityID = universitiesIDs[i];
            if (lastUniversityID != universityID)
            {
                lastUniversity = new University(universityID, 1);
                universities.Add(lastUniversity);
                lastUniversityID = universityID;
            }
            else
                lastUniversity.StudentsNr += 1;
            i++;
        }
        universities.Sort(new IComparer());
        universities = universities.Take(k).ToList();
        foreach (University u in universities)
            writer.Write($"{u.ID} ");

        reader.Close();
        writer.Close();
    }

    private class IComparer : IComparer<University>
    {
        public int Compare(University uOne, University uTwo)
        {
            if (uTwo.StudentsNr != uOne.StudentsNr)
                return uTwo.StudentsNr.CompareTo(uOne.StudentsNr);
            else
                return uOne.ID.CompareTo(uTwo.ID);
        }
    }

    public static int ReadInt()
    {
        return int.Parse(reader.ReadLine());
    }

    public static int[] ReadInts()
    {
        return reader.ReadLine()
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
    }

    private class University
    {
        private int id;
        private int studentsNr;

        public int ID
        {
            get { return id; }
            private set { id = value; }
        }

        public int StudentsNr
        {
            get { return studentsNr; }
            set { studentsNr = value; }
        }

        public University(int id, int studentsNr)
        {
            ID = id;
            StudentsNr = studentsNr;
        }
    }
}
