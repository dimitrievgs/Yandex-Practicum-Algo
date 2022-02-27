using System;
using System.Collections.Generic;
using System.Text;

namespace S4FA
{
    class Solution
    {
        public static void Main(string[] args)
        {
            int n = ReadInt();
            DocWords docWords = ReadDocs(n);
            int m = ReadInt();
            List<Request> requests = ReadRequests(m, docWords);
            PrintDocsRelevancyForRequests(requests);
        }

        private static int ReadInt()
        {
            return int.Parse(Console.ReadLine());
        }

        private static DocWords ReadDocs(int docsNumber)
        {
            var docWords = new DocWords();
            for (int i = 0; i < docsNumber; i++)
            {
                string[] words = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                    docWords.CountWord(word, i + 1); //Нумерация с 1
            }
            return docWords;
        }

        private static List<Request> ReadRequests(int requestsNumber, DocWords docWords)
        {
            List<Request> requests = new List<Request>();
            for (int j = 0; j < requestsNumber; j++)
            {
                string[] requestWords = Console.ReadLine()
                    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Request request = new Request(requestWords, docWords);
                requests.Add(request);
            }
            return requests;
        }

        /// <summary>
        /// Выводим отчёт по каждому запросу
        /// </summary>
        private static void PrintDocsRelevancyForRequests(List<Request> requests)
        {
            foreach (var request in requests)
            {
                StringBuilder docRelevancyReportString = new StringBuilder();
                for (int k = 0; k < request.DocsRelevancyCounter.Count; k++)
                {
                    if (k > 0)
                        docRelevancyReportString.Append(" ");
                    docRelevancyReportString.Append(request.DocsRelevancyCounter[k].DocIndex);
                }
                Console.WriteLine(docRelevancyReportString);
            }
        }
    }

    public class DocWords
    {
        private Dictionary<string, Dictionary<int, int>> searchStructure;

        public DocWords()
        {
            searchStructure = new Dictionary<string, Dictionary<int, int>>();
        }

        public void CountWord(string word, int docIndex)
        {
            if (!searchStructure.ContainsKey(word))
                searchStructure.Add(word, new Dictionary<int, int> { { docIndex, 1 } });
            else
            {
                if (!searchStructure[word].ContainsKey(docIndex))
                    searchStructure[word].Add(docIndex, 1);
                else
                    searchStructure[word][docIndex] += 1;
            }
        }

        public Dictionary<int, int> GetOccurrencesInDocuments(string word)
        {
            if (searchStructure.ContainsKey(word))
                return searchStructure[word];
            else 
                return null;
        }
    }

    public class Request
    {
        private HashSet<string> words;

        public HashSet<string> Words
        {
            get => words;
        }

        private List<DocRelevancy> docsRelevancyCounter;

        /// <summary>
        /// Doc Index, Relevancy Counter
        /// </summary>
        public List<DocRelevancy> DocsRelevancyCounter
        {
            get => docsRelevancyCounter;
            set => docsRelevancyCounter = value;
        }

        public Request(string[] requestWords, DocWords docWords)
        {
            words = new HashSet<string>();
            foreach (var word in requestWords)
                words.Add(word);

            Dictionary<int, int> requestWordsInDocs = new Dictionary<int, int>();
            foreach (string requestWord in Words)
            {
                var docsOccurences = docWords.GetOccurrencesInDocuments(requestWord);
                if (docsOccurences != null)
                {
                    foreach (var el in docsOccurences)
                    {
                        if (!requestWordsInDocs.ContainsKey(el.Key))
                            requestWordsInDocs.Add(el.Key, el.Value);
                        else
                            requestWordsInDocs[el.Key] += el.Value;
                    }
                }
            }
            DocsRelevancyCounter = new List<DocRelevancy>();
            foreach (var el in requestWordsInDocs)
            {
                DocsRelevancyCounter.Add(new DocRelevancy(el.Key, el.Value));
            }
            DocsRelevancyCounter.Sort();
            int maxRange = Math.Min(5, DocsRelevancyCounter.Count);
            DocsRelevancyCounter = DocsRelevancyCounter.GetRange(0, maxRange);
        }
    }

    public class DocRelevancy : IComparable
    {
        private int docIndex;

        /// <summary>
        /// Нумерация с 1
        /// </summary>
        public int DocIndex
        {
            get => docIndex;
            set => docIndex = value;
        }

        private int relevancyCounter;

        public int RelevancyCounter
        {
            get => relevancyCounter;
            set => relevancyCounter = value;
        }

        public DocRelevancy(int docIndex, int relevancyCounter)
        {
            this.docIndex = docIndex;
            this.relevancyCounter = relevancyCounter;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            DocRelevancy otherDocRelevancy = obj as DocRelevancy;
            if (otherDocRelevancy != null)
            {
                if (this.RelevancyCounter != otherDocRelevancy.RelevancyCounter)
                    return otherDocRelevancy.RelevancyCounter.CompareTo(this.RelevancyCounter);
                else
                    return this.docIndex.CompareTo(otherDocRelevancy.docIndex);
            }
            throw new ArgumentException("Object is not a DocRelevancy");
        }
    }
}
