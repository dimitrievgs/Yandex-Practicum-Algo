using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace S3TN
{
    class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            Console.ForegroundColor = ConsoleColor.Green;
            int n = ReadInt();
            CultivatedLandSegments[] segments = new CultivatedLandSegments[n];
            for (int i = 0; i < n; i++)
                segments[i] = new CultivatedLandSegments(ReadInts());
            Console.ForegroundColor = ConsoleColor.White;
            Array.Sort(segments, new SegmentsComparer());

            List<CultivatedLandSegments> Flowerbeds = new List<CultivatedLandSegments>();
            var curSegment = segments[0];
            Flowerbeds.Add(curSegment);
            int k = 1;
            while (k < segments.Length)
            {
                var segment = segments[k];
                if (curSegment.Overlaps(segment))
                    curSegment.Merge(segment);
                else
                {
                    curSegment = segment;
                    Flowerbeds.Add(segment);
                }
                k++;
            }

            int fN = Flowerbeds.Count;
            for (int f = 0; f < fN; f++)
            {
                var flowerbed = Flowerbeds[f];
                writer.WriteLine(flowerbed.Start + " " + flowerbed.End);
            }

            reader.Close();
            writer.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static int[] ReadInts()
        {
            return reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
    }

    class CultivatedLandSegments
    {
        private int start;
        private int end;

        public CultivatedLandSegments(int start, int end)
        {
            this.start = start;
            this.end = end;
        }

        public CultivatedLandSegments(int[] startEnd)
        {
            this.start = startEnd[0];
            this.end = startEnd[1];
        }

        public int Start
        {
            get => start;
            set => start = value;
        }

        public int End
        {
            get => end;
            set => end = value;
        }

        public bool Overlaps(CultivatedLandSegments segment)
        {
            return (segment.Start >= start && segment.Start <= end)
                || (segment.End >= start && segment.End <= end);
        }

        public void Merge(CultivatedLandSegments segment)
        {
            start = Math.Min(this.Start, segment.Start);
            end = Math.Max(this.End, segment.End);
        }
    }

    class SegmentsComparer : IComparer<CultivatedLandSegments>
    {
        public int Compare(CultivatedLandSegments x, CultivatedLandSegments y)
        {
            return x.Start.CompareTo(y.Start);
        }
    }
}
