using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4FB
{
    class Solution
    {
        public static TextReader reader;
        public static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            int n = int.Parse(reader.ReadLine());
            HashMap hashMap = new HashMap();
                        
            for (int i = 0; i < n; i++)
            {
                ParseCommand(hashMap, reader.ReadLine());
            }
            
            /*List<string> commands = Test.GenerateTestData(n);
            for (int i = 0; i < n; i++)
            {
                ParseCommand(hashMap, commands[i]);
            }*/

            writer.Close();
            reader.Close();
        }

        private static void ParseCommand(HashMap hashMap, string commandLine)
        {
            string[] commandParts = commandLine.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            string command = commandParts[0];
            uint key, value;
            switch (command)
            {
                case "put":
                    key = Convert.ToUInt32(commandParts[1]);
                    value = Convert.ToUInt32(commandParts[2]);
                    hashMap.Put(key, value);
                    break;
                case "get":
                    key = Convert.ToUInt32(commandParts[1]);
                    try
                    {
                        value = hashMap.Get(key);
                        writer.WriteLine(value);
                    }
                    catch (InvalidOperationException e)
                    {
                        writer.WriteLine(e.Message);
                    }
                    break;
                case "delete":
                    key = Convert.ToUInt32(commandParts[1]);
                    try
                    {
                        value = hashMap.Delete(key);
                        writer.WriteLine(value);
                    }
                    catch (InvalidOperationException e)
                    {
                        writer.WriteLine(e.Message);
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
        private KeyValue[] array; //коллизии разрешаются методом цепочек
        private string keyNotFoundMessage = "None";

        private int c1 = 137, c2 = 9973;

        public HashMap(int keysCapacity = 100_000)
        {
            backingArraySize = (uint)(keysCapacity / loadFactor); //чтобы получить предельный адекватный фактор заполнения
            backingArraySize = GetNearestLargerPrimeNumber(backingArraySize); //13337
            array = new KeyValue[backingArraySize];
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

        private int GetArrayIndex(uint key, int step)
        {
            return (int)((key + c1 * step + c2 * (int)Math.Pow(step, 2)) % backingArraySize);
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
                            throw new InvalidOperationException("There are empty slots, but can't reach them");
                    }
                    throw new InvalidOperationException("No empty slots");
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
                    throw new InvalidOperationException(keyNotFoundMessage);
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
                    throw new InvalidOperationException(keyNotFoundMessage);
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
                    throw new InvalidOperationException(keyNotFoundMessage);
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
                    throw new InvalidOperationException(keyNotFoundMessage);
                }
            }
        }

        private class KeyValue
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
