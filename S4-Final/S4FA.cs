/*
ID 65699123
отчёт https://contest.yandex.ru/contest/24414/run-report/65699123/
задача https://contest.yandex.ru/contest/24414/problems/A/

-- ПРИНЦИП РАБОТЫ --
По условиям задачи нужно для каждого запроса вывести 5 самых релевантных
документов, по убыванию релевантности (частоте встречаемости слов запроса
в документах) и затем по возрастанию индексов документов.
Для этого создаются специальная структура для поиска из HashMap, ключами которой
выступают уникальные слова документов, а значениями - также HashMap, где, 
в свою очередь, ключ - индекс документа, где это слово встречается, а значение - 
сколько раз это слово в этом документе появляется.
-- ДОКАЗАТЕЛЬСТВО КОРРЕКТНОСТИ --
Это позволяет в процессе считывания каждого документа попутно "посчитать" слова,
которые в него входят, и прилинковать индекс данного документа и частоту вхождения
к каждому слову.
Затем в процессе считывания каждого запроса пройтись по каждому слову запроса и
вытащить всю информацию, в каких документах и сколько раз это слово входит. Пройдясь
по всем словам запроса мы суммируем эту информацию и получаем список релевантных документов.
-- ВРЕМЕННАЯ СЛОЖНОСТЬ --
Пусть n - кол-во документов, m - кол-во запросов.
Тогда чтение всех документов и построение структуры для поиска происходит за:
O(Σn (n_doc_words * 1[HashMap])) = O(all_docs_words_without_uniqueness)
Затем считываем по очереди запросы и суммируем встречаемость всех слов 
каждого запроса в документах, сортируем релевантность:
O(Σm (m_request_words * request_word_occurences_in_docs[суммируем встречаемость] 
    + request_s_docs[перегоняем данные] + request_s_docs * log(request_s_docs)[сортировка])
-- ПРОСТРАНСТВЕННАЯ СЛОЖНОСТЬ --
Структура DocWords: 
O(all_docs_unique_words * custom_word_occurrences_in_docs[структура для поиска]
Структуры Request:
O(m * (request_word + request_occurences_in_docs_first_5))
*/

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
            // суммируем встречаемость в документах данного запроса, пройдясь по всем словам запроса
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
                DocsRelevancyCounter.Add(new DocRelevancy(el.Key, el.Value));
            // сортируем релевантность документов и берём первые 5
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
