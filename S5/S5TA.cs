using System;

namespace S5TA
{
    class Solution
    {
        public static int Solve(Node root)
        {
            return GetMaxWithDFS(root);
        }

        private static int GetMaxWithDFS(Node node)
        {
            if (node == null)
                return int.MinValue;
            int maxLeft = GetMaxWithDFS(node.Left);
            int maxRight = GetMaxWithDFS(node.Right);
            int max = Math.Max(node.Value, Math.Max(maxLeft, maxRight));
            return max;
        }
    }
    
    // закомментируйте перед отправкой
    public class Node
    {
        public int Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }
}