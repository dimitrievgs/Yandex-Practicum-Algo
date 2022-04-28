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

            durations.Sort();
            List<Duration> durationsOut = new List<Duration>();
            GetMaxNumberOfLessons(durations, durations[0].Start, 0, durationsOut);

            writer.WriteLine(durationsOut.Count);
            for (int i = 0; i < durationsOut.Count; i++)
                writer.WriteLine(durationsOut[i].Start.ToString()
                    + ' ' + durationsOut[i].End.ToString());

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

        private static void GetMaxNumberOfLessons(List<Duration> durationsIn, Time curTime, int s,
            List<Duration> durationsOut)
        {
            //из тех, что не начались, выбираем то, которое закончится раньше всего
            if (s > durationsIn.Count - 1)
                return;
            int firstToEndIndex = -1;
            for (int i = s; i < durationsIn.Count; i++)
            {
                if (durationsIn[i].Start.CompareTo(curTime) <= 0 //ещё не началось к curTime
                    && (firstToEndIndex == -1 || durationsIn[i].End.CompareTo(durationsIn[firstToEndIndex].End) > 0)) //ищем то, которое закончится раньше других
                    firstToEndIndex = i;
            }
            if (firstToEndIndex >= 0)
            {
                durationsOut.Add(durationsIn[firstToEndIndex]);
                GetMaxNumberOfLessons(durationsIn, durationsIn[firstToEndIndex].End, firstToEndIndex + 1, durationsOut);
            }
        }

        class Duration : IComparable
        {
            public Time Start { get; set; }
            public Time End { get; set; }

            public Duration(Time start, Time end)
            {
                Start = start;
                End = end;
            }

            public int CompareTo(object obj)
            {
                int result = 0;
                if (obj == null)
                    result = 1;
                else
                {
                    Duration otherDuration = obj as Duration;
                    if (otherDuration != null)
                    {
                        if (this.Start.CompareTo(otherDuration.Start) != 0)
                            result = otherDuration.Start.CompareTo(this.Start);
                        else if (this.End.CompareTo(otherDuration.End) != 0)
                            result = otherDuration.End.CompareTo(this.End);
                    }
                    else
                        throw new ArgumentException("Object is not a Duration");
                }
                return result;
            }
        }

        class Time : IComparable
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

            public int CompareTo(object obj)
            {
                int result = 0;
                if (obj == null)
                    result = 1;
                else
                {
                    Time otherTime = obj as Time;
                    if (otherTime != null)
                    {
                        if (this.Hour != otherTime.Hour)
                            result = otherTime.Hour.CompareTo(this.Hour);
                        else if (this.Minute != otherTime.Minute)
                            result = otherTime.Minute.CompareTo(this.Minute);
                    }
                    else
                        throw new ArgumentException("Object is not a Time");
                }
                return result;
            }

            public override string ToString()
            {
                if (Minute == 0)
                    return Hour.ToString();
                else
                    return Hour + "." + Minute;
            }
        }
    }
}