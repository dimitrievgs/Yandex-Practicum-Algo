using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace S7TB
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = ReadInt();
            List<Duration> durations = new List<Duration>();
            for (int i = 0; i < n; i++)
                durations.Add(ReadDuration());

            //writer.Write(maxGain);



            writer.Close();
            reader.Close();
        }

        private static int ReadInt()
        {
            return int.Parse(reader.ReadLine());
        }

        private static Duration ReadDuration()
        {
            string[] timeStamps = reader.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var duration = new Duration(new Time(timeStamps[0]), new Time(timeStamps[1]));
            return duration;
        }

        class Duration
        {
            public Time Start { get; set; }
            public Time End { get; set; }

            public Duration(Time start, Time end)
            {
                Start = start;
                End = end;
            }
        }

        class Time
        {
            public int Hour { get; set; }
            public int Minute { get; set; }

            public Time(int hour, int minute)
            {
                Hour = hour;
                Minute = minute;
            }

            public Time(string timeStamp)
            {
                string[] parts = timeStamp.Split('.');
                Hour = int.Parse(parts[0]);
                if (parts.Length > 1)
                    Minute = int.Parse(parts[1]);
            }
        }
    }
}