using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4FB_CCR
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            var (n, inputLines) = ReadFromTextFile();
            //var (n, inputLines) = ReadFromConsole();
            HashMap hashMap = new HashMap();

            List<string> badOperations = new List<string>();
            List<double> elTime = new List<double>();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 120000; i++) //int i = 0; i < n; i++
            {
                DateTime t1 = DateTime.Now;
                string operation = inputLines[i];
                ParseCommand(hashMap, operation, sb);
                double elapsedTime = (DateTime.Now - t1).TotalMilliseconds;
                elTime.Add(elapsedTime);
                if (elapsedTime > 5d)
                    badOperations.Add(operation);
                if (i == 16941)
                    ;
            }
            /* List<string> commands = Test.GenerateTestData(n);
            for (int i = 0; i < n; i++)
            {
                ParseCommand(hashMap, commands[i]);
            }*/

            WriteDoubleListToFile(elTime);
            WriteDoubleListToFile(hashMap, 100000);

            writer.WriteLine(sb.ToString());

            writer.Close();
            reader.Close();
        }

        private static (int, List<string>) ReadFromTextFile()
        {
            string text = System.IO.File.ReadAllText(@"..\net5.0\S4-Final\S4FB-20");
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

        private static void WriteDoubleListToFile(List<double> elTime)
        {
            using (TextWriter tw = new StreamWriter(@"..\net5.0\S4-Final\S4FB-20-time.txt"))
            {
                foreach (var v in elTime)
                    tw.WriteLine(v.ToString("F5", CultureInfo.InvariantCulture));
            }
        }

        private static void WriteDoubleListToFile(HashMap hashMap, int lastIndex)
        {
            using (TextWriter tw = new StreamWriter(@"..\net5.0\S4-Final\S4FB-20-cell-filling.txt"))
            {
                for (int i = 0; i < hashMap.array.Length; i++)
                {
                    var list = hashMap.array[i];
                    int count = (list != null) ? list.Count : 0;
                    tw.WriteLine(count.ToString("F5", CultureInfo.InvariantCulture));
                }
            }
        }

        private static void WriteBadOperations(List<string> badOperations)
        {
            using (TextWriter tw = new StreamWriter(@"..\net5.0\S4-Final\S4FB-20-bad-operations.txt"))
            {
                foreach (var op in badOperations)
                    tw.WriteLine(op);
            }
        }

        private static void ParseCommand(HashMap hashMap, string commandLine, StringBuilder sb)
        {
            string[] commandParts = commandLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string command = commandParts[0];
            uint key, value;
            switch (command)
            {
                case "put":
                    key = uint.Parse(commandParts[1]);
                    value = uint.Parse(commandParts[2]);
                    hashMap.Put(key, value);
                    break;
                case "get":
                    key = uint.Parse(commandParts[1]);
                    try
                    {
                        value = hashMap.Get(key);
                        sb.Append(value);
                        sb.Append("\r\n");
                    }
                    catch (InvalidOperationException e)
                    {
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
                    catch (InvalidOperationException e)
                    {
                        sb.Append(e.Message);
                        sb.Append("\r\n");
                    }
                    break;
            }
        }
    }

    class HashMap
    {
        /// <summary>
        /// 0.75 - предельный адекватный фактор заполнения, при бОльших значениях 
        /// ожидаемое количество операций при поиске (например) резко возрастает
        /// </summary>
        private double loadFactor = 0.4;
        private uint backingArraySize;
        public LinkedList<KeyValue>[] array; //private //коллизии разрешаются методом цепочек
        private string keyNotFoundMessage = "None";

        public HashMap(int keysCapacity = 100_000)
        {
            backingArraySize = (uint)(keysCapacity / loadFactor); //чтобы получить предельный адекватный фактор заполнения
            backingArraySize = GetNearestLargerPrimeNumber(backingArraySize); //13337
            array = new LinkedList<KeyValue>[backingArraySize];
        }

        private uint GetNearestLargerPrimeNumber(uint n)
        {
            uint i = n;
            while (!IsPrime(i))
                i++;
            return i;
        }

        private bool IsPrime(uint n)
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

        private int GetArrayIndex(uint key)
        {
            return (int)(key % backingArraySize); // в качестве хеш-функции выбирается просто само значение ключа
        }

        private int GetArrayIndex2(uint key)
        {
            key = ((key >> 16) ^ key) * 0x45d9f3b;
            key = ((key >> 16) ^ key) * 0x45d9f3b;
            key = (key >> 16) ^ key;
            return (int)(key % backingArraySize);
        }

        public void Put(uint key, uint value)
        {
            int index = GetArrayIndex(key);
            LinkedList<KeyValue> chain = array[index];
            if (chain == null)
                chain = (array[index] = new LinkedList<KeyValue>());
            var node = FindInList(chain, key);
            if (node != null)
                node.Value.Value = value; //перезаписываем значение
            else
                chain.AddLast(new KeyValue(key, value));
        }

        public uint Get(uint key)
        {
            int index = GetArrayIndex(key);
            LinkedList<KeyValue> chain = array[index];
            if (chain == null)
                throw new InvalidOperationException(keyNotFoundMessage);
            var node = FindInList(chain, key);
            if (node == null)
                throw new InvalidOperationException(keyNotFoundMessage);
            return node.Value.Value;
        }

        public uint Delete(uint key)
        {
            int index = GetArrayIndex(key);
            LinkedList<KeyValue> chain = array[index];
            if (chain == null)
                throw new InvalidOperationException(keyNotFoundMessage);
            var node = FindInList(chain, key);
            if (node == null)
                throw new InvalidOperationException(keyNotFoundMessage);
            uint value = node.Value.Value;
            chain.Remove(node.Value); //~~
            return value;
        }

        private LinkedListNode<KeyValue> FindInList(LinkedList<KeyValue> list, uint key)
        {
            var node = list.First;
            while (node != null)
            {
                if (node.Value.Key == key)
                    return node;
                node = node.Next;
            }
            return null;
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

            public KeyValue(uint key, uint value)
            {
                this.key = key;
                this.iValue = value;
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
