using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S2TH
{
    public class Solution
    {
        private static TextReader reader;
        private static TextWriter writer;

        public static void Main(string[] args)
        {
            reader = new StreamReader(Console.OpenStandardInput());
            writer = new StreamWriter(Console.OpenStandardOutput());

            char[] brackets = reader.ReadLine().ToCharArray();
            writer.WriteLine(Check(brackets));

            reader.Close();
            writer.Close();
        }

        private static bool Check(char[] brackets)
        {
            int charsNr = brackets.Length;
            Stack<Bracket> stack = new Stack<Bracket>();
            for (int i = 0; i < charsNr; i++)
            {
                Bracket bracket = new Bracket(brackets[i]);
                Bracket topBracket = stack.peek();
                if (topBracket == null)
                    stack.push(bracket);
                else if (bracket.Value == topBracket.Value)
                {
                    if (bracket.Left != topBracket.Left && topBracket.Left == true)
                    {
                        stack.pop();
                    }
                    else
                    {
                        stack.push(bracket);
                    }
                }
                else
                {
                    stack.push(bracket);
                }
            }
            return stack.isEmpty();
        }
    }

    public class Bracket
    {
        public char Value;
        public bool Left;

        public Bracket(char value, bool left)
        {
            Value = value;
            Left = left;
        }

        public Bracket(char value)
        {
            switch (value)
            {
                case '{':
                case '[':
                case '(':
                    Value = value;
                    Left = true;
                    break;
                case '}':
                    Value = '{';
                    Left = false;
                    break;
                case ']':
                    Value = '[';
                    Left = false;
                    break;
                case ')':
                    Value = '(';
                    Left = false;
                    break;
            }
        }
    }

    public class Stack<TValue>
    {
        Node<TValue> head;

        public Stack()
        {
            head = null;
        }

        public void push(TValue value)
        {
            Node<TValue> node = new Node<TValue>(value, head);
            head = node;
        }

        public void pop()
        {
            if (head != null)
                head = head.Next;
        }

        public TValue peek()
        {
            return (head != null) ? head.Value : default(TValue);
        }

        public bool isEmpty()
        {
            return head == null;
        }
    }

    public class Node<TValue>
    {
        public TValue Value;
        public Node<TValue> Next;

        public Node(TValue value, Node<TValue> next)
        {
            Value = value;
            Next = next;
        }
    }
}