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

public class S2TD
{
    public static int Solve(Node<string> head, string value)
    {
        Node<string> node = head;
        int index = 0;
        while (node != null)
        {
            if (node.Value.Equals(value))
                return index;
            node = node.Next;
            index++;
        }
        return -1;
    }
}