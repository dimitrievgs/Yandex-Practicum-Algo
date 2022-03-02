using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4FB_OA_E
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            string testName = "T20M";
            var (n, inputLines) = ReadFromTextFile(testName);
            //var (n, inputLines) = ReadFromConsole();
            HashMap hashMap = new HashMap();

            List<double> elTime = new List<double>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 120000; i++) //int i = 0; i < n; i++
            {
                DateTime t1 = DateTime.Now;
                bool exceptionTriggered = ParseCommand(hashMap, inputLines[i], sb); //reader.ReadLine()
                double elapsedTime = (DateTime.Now - t1).TotalMilliseconds;
                if (i == 57738)
                    ;
                if (exceptionTriggered == true) //elapsedTime > 10d && 
                    ;
                elTime.Add(elapsedTime);
            }

            writer.WriteLine(sb.ToString());

            /*List<string> commands = Test.GenerateTestData(n);
            for (int i = 0; i < n; i++)
            {
                ParseCommand(hashMap, commands[i]);
            }*/

            WriteDoubleListToFile(elTime, testName);
            WriteDoubleListToFile(hashMap, testName);

            WriteToFile(sb, testName);
            writer.WriteLine(sb.ToString());

            writer.Close();
            reader.Close();
        }

        private static (int, List<string>) ReadFromTextFile(string testName)
        {
            string text = System.IO.File.ReadAllText(@$"..\net5.0\S4-Final\{testName}");
            List<string> inputLines = text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            int n = int.Parse(inputLines[0]);
            inputLines = inputLines.GetRange(1, n);
            return (n, inputLines);
        }

        private static (int, List<string>) ReadFromConsole()
        {
            int n = int.Parse(reader.ReadLine());
            List<string> inputLines = new List<string>(n);
            for (int i = 0; i < n; i++)
            {
                inputLines.Add(reader.ReadLine()); //reader.ReadLine()
            }
            return (n, inputLines);
        }

        private static void WriteDoubleListToFile(List<double> elTime, string testName)
        {
            using (TextWriter tw = new StreamWriter(@$"..\net5.0\S4-Final\{testName}-time.txt"))
            {
                foreach (var v in elTime)
                    tw.WriteLine(v.ToString("F5", CultureInfo.InvariantCulture));
            }
        }

        private static void WriteDoubleListToFile(HashMap hashMap, string testName)
        {
            using (TextWriter tw = new StreamWriter(@$"..\net5.0\S4-Final\{testName}-cf.txt"))
            {
                for (int i = 0; i < hashMap.array.Count; i++)
                {
                    int cell = Convert.ToInt32(hashMap.array[i] != null && hashMap.array[i].Deleted == false);
                    tw.WriteLine(cell.ToString("F0", CultureInfo.InvariantCulture));
                }
            }
        }

        private static void WriteToFile(StringBuilder sb, string testName)
        {
            using (TextWriter tw = new StreamWriter(@$"..\net5.0\S4-Final\{testName}-result.txt"))
            {
                tw.WriteLine(sb.ToString());
            }
        }

        private static void WriteBadOperations(List<string> badOperations, string testName)
        {
            using (TextWriter tw = new StreamWriter(@$"..\net5.0\S4-Final\{testName}-bad-operations.txt"))
            {
                foreach (var op in badOperations)
                    tw.WriteLine(op);
            }
        }

        private static bool ParseCommand(HashMap hashMap, string commandLine, StringBuilder sb)
        {
            bool exceptionTriggered = false;
            string[] commandParts = commandLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string command = commandParts[0];
            uint key, value;
            switch (command)
            {
                case "put":
                    key = uint.Parse(commandParts[1]);
                    value = uint.Parse(commandParts[2]);
                    hashMap.Put(key, value);
                    sb.Append("\r\n");
                    break;
                case "get":
                    key = uint.Parse(commandParts[1]);
                    try
                    {
                        value = hashMap.Get(key);
                        sb.Append(value);
                        sb.Append("\r\n");
                    }
                    catch (Exception e)
                    {
                        exceptionTriggered = true;
                        sb.Append(e.Message);
                        sb.Append("\r\n");
                    }
                    break;
                case "delete":
                    key = uint.Parse(commandParts[1]);
                    try
                    {
                        value = hashMap.Delete(key);
                        sb.Append(value);
                        sb.Append("\r\n");
                    }
                    catch (Exception e)
                    {
                        exceptionTriggered = true;
                        sb.Append(e.Message);
                        sb.Append("\r\n");
                    }
                    break;
            }
            return exceptionTriggered;
        }
    }

    class HashMap
    {
        /// <summary>
        /// 0.75 - предельный адекватный фактор заполнения, при бОльших значениях 
        /// ожидаемое количество операций при поиске (например) резко возрастает
        /// </summary>
        private double loadFactor = 0.5;
        private uint backingArraySize;
        public List<KeyValue> array; //коллизии разрешаются методом цепочек
        private string keyNotFoundMessage = "None";

        private int c1 = (int)GetNearestLargerPrimeNumber(20_000), c2 = (int)GetNearestLargerPrimeNumber(100_000); //c1 = 137, c2 = 9973;
        //private int cL = 7;

        public HashMap(int keysCapacity = 100_000)
        {
            backingArraySize = (uint)(keysCapacity / loadFactor); //чтобы получить предельный адекватный фактор заполнения
            backingArraySize = GetNearestLargerPrimeNumber(backingArraySize); //13337
            //array = new List<KeyValue>((int)backingArraySize);
            array = new List<KeyValue>(new KeyValue[backingArraySize]);
        }

        private static uint GetNearestLargerPrimeNumber(uint n)
        {
            uint i = n;
            while (!IsPrime(i))
                i++;
            return i;
        }

        private static bool IsPrime(uint n)
        {
            if (n == 1)
                return false;
            int i = 2;
            while (i * i <= n)
            {
                if ((n % i) == 0)
                    return false;
                i = i + 1;
            }
            return true;
        }

        private int GetArrayIndex(uint key, int step)
        {
            return (int)((key + c1 * step + c2 * step * step) % backingArraySize);
            //return (int)((key + cL * step) % backingArraySize);
        }

        public void Put(uint key, uint value)
        {
            int step = 0;
            int startIndex = GetArrayIndex(key, step);
            int index = startIndex;
            while (true)
            {
                if (array[index] == null || array[index].Deleted) // эта ячейка пустая
                {
                    array[index] = new KeyValue(key, value);
                    return;
                }
                else if (array[index].Key == key) //перезаписываем значение
                {
                    array[index].Value = value;
                    return;
                }
                else // коллизия
                {
                    step++;
                    index = GetArrayIndex(key, step);
                }
                if (index == startIndex) //пришли снова в стартовый индекс
                {
                    for (int i = 0; i < backingArraySize; i++)
                    {
                        if (array[i] == null || array[i].Deleted)
                            throw new Exception("There are empty slots, but can't reach them");
                    }
                    throw new Exception("No empty slots");
                }
            }
        }

        public uint Get(uint key)
        {
            int step = 0;
            int startIndex = GetArrayIndex(key, step);
            int index = startIndex;
            while (true)
            {
                if (array[index] == null) // не смогли найти
                {
                    throw new Exception(keyNotFoundMessage);
                }
                else if (array[index].Key == key)
                {
                    return array[index].Value;
                }
                else // коллизия
                {
                    step++;
                    index = GetArrayIndex(key, step);
                }
                if (index == startIndex) //пришли снова в стартовый индес
                {
                    throw new Exception(keyNotFoundMessage);
                }
            }
        }

        public uint Delete(uint key)
        {
            int step = 0;
            int startIndex = GetArrayIndex(key, step);
            int index = startIndex;
            while (true)
            {
                if (array[index] == null) // не смогли найти
                {
                    throw new Exception(keyNotFoundMessage);
                }
                else if (array[index].Key == key)
                {
                    uint value = array[index].Value;
                    array[index] = new KeyValue(true);
                    return value;
                }
                else // коллизия
                {
                    step++;
                    index = GetArrayIndex(key, step);
                }
                if (index == startIndex) //пришли снова в стартовый индес
                {
                    throw new Exception(keyNotFoundMessage);
                }
            }
        }

        public class KeyValue
        {
            private uint key;

            public uint Key
            {
                get => key;
            }

            private uint iValue;

            public uint Value
            {
                get => iValue;
                set => iValue = value;
            }

            private bool deleted;

            public bool Deleted
            {
                get => deleted;
            }

            public KeyValue(uint key, uint value)
            {
                this.key = key;
                this.iValue = value;
                this.deleted = false;
            }

            public KeyValue(bool deleted)
            {
                this.key = uint.MaxValue;
                this.iValue = uint.MaxValue;
                this.deleted = deleted;
            }
        }
    }

    class Test
    {
        private static Random random = new Random();
        private const int maxNumber = 1_000_000_000;

        public static List<string> GenerateTestData(int n)
        {
            //only put
            List<string> array = new List<string>(n);
            for (int i = 0; i < n; i++)
            {
                int key = random.Next(0, maxNumber);
                int value = random.Next(0, maxNumber);
                array.Add($"put {key} {value}");
            }
            return array;
        }
    }
}
