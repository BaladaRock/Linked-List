using System;
using System.Collections;
using System.Collections.Generic;

namespace Linked_List
{
    public class LinkedList<T> : ICollection<T>
    {
        private readonly Node<T> sentinel;

        public LinkedList()
        {
            Count = 0;
            sentinel = new Node<T>(default)
            {
                List = this
            };
            sentinel.LinkTo(sentinel, sentinel);
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public Node<T> First
        {
            get
            {
                return Count == 0 ? null : sentinel.Next;
            }
        }

        public Node<T> Last
        {
            get
            {
                return Count == 0 ? null : sentinel.Previous;
            }
        }

        public void AddFirst(Node<T> listNode)
        {
            AddBefore(sentinel.Next, listNode);
        }

        public void AddFirst(T value)
        {
            AddBefore(sentinel.Next, new Node<T>(value));
        }

        public void AddLast(Node<T> listNode)
        {
            AddBefore(sentinel, listNode);
        }

        public void Add(T item)
        {
            AddBefore(sentinel, new Node<T>(item));
        }

        public void AddBefore(Node<T> listNode, Node<T> newListNode)
        {
            ExceptionsForAddingMethods(listNode, newListNode);

            listNode.Previous.Next = newListNode;
            newListNode.LinkTo(prev: listNode.Previous, next: listNode);
            listNode.Previous = newListNode;
            newListNode.List = this;
            Count++;
        }

        public void AddBefore(Node<T> listNode, T item)
        {
            AddBefore(listNode, new Node<T>(item));
        }

        public void AddAfter(Node<T> listNode, Node<T> newListNode)
        {
            AddBefore(listNode.Next, newListNode);
        }

        public void AddAfter(Node<T> listNode, T item)
        {
            Node<T> newNode = new Node<T>(item);
            AddBefore(listNode.Next, newNode);
        }

        private void ExceptionsForAddingMethods(Node<T> listNode, Node<T> newListNode)
        {
            ThrowReadOnly();
            ThrowNull(listNode);
            ThrowNodeIsNotInList(listNode);
            ThrowNull(newListNode);
            ThrowInvalidOperation(newListNode);
        }

        private bool CheckIfNodeIsInList(Node<T> listNode)
        {
            return listNode.List == this;
        }

        private Node<T> FindNode(Node<T> listNode)
        {
            if (listNode == sentinel)
                return sentinel;

            foreach (var node in GetNodesFromStart())
            {
                if (listNode == node)
                    return node;
            }
            return null;
        }

        public Node<T> Find(T element)
        {
            var enumerable = GetNodesFromStart();
            return SearchLinkedList(element, enumerable);
        }

        public Node<T> FindLast(T element)
        {
            var enumerable = GetNodesFromEnd();
            return SearchLinkedList(element, enumerable);
        }

        private Node<T> SearchLinkedList(T element, IEnumerable<Node<T>> nodeToFind)
        {
            foreach (var node in nodeToFind)
            {
                if (CheckForNull(node, element)
                    || node.Data?.Equals(element) == true)
                {
                    return node;
                }
            }

            return null;
        }

        public void Clear()
        {
            ThrowReadOnly();
            Count = 0;
        }

        public bool Contains(T item)
        {
            return Find(item) != null;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ExceptionsForCopyingProcess(array, arrayIndex);

            var enumerator = GetEnumerator();
            for (int i = arrayIndex; i < Count + arrayIndex; i++)
            {
                enumerator.MoveNext();
                array[i] = enumerator.Current;
            }
        }

        public bool Remove(Node<T> node)
        {
            ThrowReadOnly();
            ThrowNull(node);
            ThrowNodeIsNotInList(node);
            return RemoveItem(FindNode(node));
        }

        public bool Remove(T item)
        {
            return RemoveItem(Find(item));
        }

        private bool RemoveItem(Node<T> node)
        {
            if (node == null)
                return false;
            node.Previous.Next = node.Next;
            node.Next.Previous = node.Previous;
            node.LinkTo();
            Count--;
            return true;
        }

        public void RemoveFirst()
        {
            ThrowReadOnly();
            ThrowListIsEmpty();
            RemoveItem(First);
        }

        public void RemoveLast()
        {
            ThrowReadOnly();
            ThrowListIsEmpty();
            RemoveItem(Last);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> i = sentinel.Next; i != sentinel; i = i.Next)
                yield return i.Data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public LinkedList<T> AsReadOnly()
        {
            var newList = new LinkedList<T>();
            var enumerator = GetEnumerator();
            while (enumerator.MoveNext())
                newList.Add(enumerator.Current);

            newList.IsReadOnly = true;
            return newList;
        }

        private IEnumerable<Node<T>> GetNodesFromStart()
        {
            for (Node<T> i = sentinel.Next; i != sentinel; i = i.Next)
                yield return i;
        }

        private IEnumerable<Node<T>> GetNodesFromEnd()
        {
            for (Node<T> i = sentinel.Previous; i != sentinel; i = i.Previous)
                yield return i;
        }

        private static bool CheckForNull(Node<T> node, T value)
        {
            return node.Data == null && value == null;
        }

        private void ThrowNull(Node<T> node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
        }

        private void ThrowInvalidOperation(Node<T> node)
        {
            if (!CheckIfNodeIsInList(node) && (node.Next != null || node.Previous != null))
                throw new InvalidOperationException(message: "Node already belongs to another list!\n");
        }

        private void ThrowNodeIsNotInList(Node<T> node)
        {
            if (!CheckIfNodeIsInList(node))
                throw new InvalidOperationException(message: "List does not contain given node!\n");
        }

        private void ExceptionsForCopyingProcess(T[] array, int arrayIndex)
        {
            ThrowArrayIsNull(array);
            ThrowIndexException(arrayIndex);
            ThrowArgumentException(array, arrayIndex);
        }

        private void ThrowArgumentException(T[] array, int arrayIndex)
        {
            if (array.Length < Count + arrayIndex)
                throw new ArgumentException(message: "Copying proccess cannot be initialised\n", paramName: nameof(array));
        }

        private void ThrowIndexException(int arrayIndex)
        {
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(paramName: nameof(arrayIndex), message: "Give a valid index!\n");
        }

        private void ThrowArrayIsNull(T[] array)
        {
            if (array == null)
                throw new ArgumentNullException(paramName: nameof(array));
        }

        private void ThrowListIsEmpty()
        {
            if (Count == 0)
                throw new InvalidOperationException(message: "List is empty!\n");
        }

        private void ThrowReadOnly()
        {
            if (IsReadOnly)
                throw new NotSupportedException(message: "List is readonly!\n");
        }
    }
}
