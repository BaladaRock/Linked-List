
namespace Linked_List
{
    public class Node<T>
    {
        public Node(T data)
        {
            Data = data;
            Previous = null;
            Next = null;
        }

        public T Data { get; set; }
        public Node<T> Previous { get; set; }
        public Node<T> Next { get; set; }

        public void LinkTo(Node<T> prev = null, Node<T> next = null)
        {
            if (prev != null)
                Previous = prev;
            if (next != null)
                Next = next;
        }
    }
}
