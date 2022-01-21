/*
// закомментируйте перед отправкой

public class Node<TValue>
{
   public TValue Value { get; private set; }
   public Node<TValue> Next { get; set; }

   public Node(TValue value, Node<TValue> next)
   {
       Value = value;
       Next = next;
   }
}
*/

public class Solution<TValue>
{
    public static Node<TValue> Solve(Node<TValue> head, int idx) //Node<TValue>, int
    {
        Node<TValue> node = head;
        int index = 0;
        if (idx == 0)
        {
            head = head.Next;
        }
        else
        {
            while (index < idx - 1)
            {
                node = node.Next;
                index++;
            }
            node.Next = node.Next.Next;
        }
        return head;
    }
}
