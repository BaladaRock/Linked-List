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
            sentinel = new Node<T>(default);
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
            ThrowReadOnly();
            ThrowNull(listNode);
            ThrowInvalidOperation(listNode);

            if (!IsListEmpty(listNode))
            {
                sentinel.Next.Previous = listNode;
                listNode.Next = sentinel.Next;
                sentinel.Next = listNode;
            }
            Count++;
        }

        public void AddFirst(T value)
        {
            var newNode = new Node<T>(value);
            AddFirst(newNode);
        }

        public void AddLast(Node<T> listNode)
        {
            ThrowReadOnly();
            ThrowNull(listNode);
            ThrowInvalidOperation(listNode);

            if (!IsListEmpty(listNode))
            {
                listNode.LinkTo(prev: sentinel.Previous, next: sentinel);
                sentinel.Previous.Next = listNode;
                sentinel.Previous = listNode;
            }
            Count++;
        }

        public void Add(T item)
        {
            var newNode = new Node<T>(item);
            AddLast(newNode);
        }

        private void AddForEmptyList(Node<T> listNode)
        {
            if (Count == 0)
                LinkToSentinel(listNode);
        }

        private void LinkToSentinel(Node<T> listNode)
        {
            sentinel.LinkTo(prev: listNode, next: listNode);
            listNode.LinkTo(prev: sentinel, next: sentinel);
        }

        private bool IsListEmpty(Node<T> listNode)
        {
            AddForEmptyList(listNode);
            return Count == 0;
        }

        public void AddBefore(Node<T> listNode, Node<T> newListNode)
        {
            ExceptionsForAddingMethods(listNode, newListNode);

            Node<T> markerNode = FindNode(listNode);
            markerNode.Previous.Next = newListNode;
            newListNode.LinkTo(prev: markerNode.Previous, next: markerNode);
            markerNode.Previous = newListNode;
            Count++;
        }

        private bool CheckIfNodeIsInList(Node<T> listNode)
        {
            return FindNode(listNode) != null;
        }

        public void AddBefore(Node<T> listNode, T item)
        {
            Node<T> newNode = new Node<T>(item);
            AddBefore(listNode, newNode);
        }

        public void AddAfter(Node<T> listNode, Node<T> newListNode)
        {
            ExceptionsForAddingMethods(listNode, newListNode);

            Node<T> markerNode = FindNode(listNode);
            markerNode.Next.Previous = newListNode;
            newListNode.LinkTo(prev: markerNode, next: markerNode.Next);
            markerNode.Next = newListNode;
            Count++;
        }

        public void AddAfter(Node<T> listNode, T item)
        {
            Node<T> newNode = new Node<T>(item);
            AddAfter(listNode, newNode);
        }

        private void ExceptionsForAddingMethods(Node<T> listNode, Node<T> newListNode)
        {
            ThrowReadOnly();
            ThrowNull(listNode);
            ThrowNull(newListNode);
            ThrowInvalidOperation(newListNode);
            ThrowNodeIsNotInList(listNode);
        }

        private Node<T> FindNode(Node<T> listNode)
        {
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

            Node<T> foundNode = FindNode(node);
            RemoveItem(foundNode);
            return foundNode != null;
        }

        public bool Remove(T item)
        {
            return Find(item) != null && Remove(Find(item));
        }

        private void RemoveItem(Node<T> foundNode)
        {
            if (foundNode != null)
            {
                foundNode.Previous.Next = foundNode.Next;
                foundNode.Next.Previous = foundNode.Previous;
                foundNode.LinkTo();
                Count--;
            }
        }

        public void RemoveFirst()
        {
            ThrowReadOnly();
            ThrowListIsEmpty();

            First.Next.Previous = sentinel;
            sentinel.Next = First.Next;
            First.LinkTo();
            Count--;
        }

        public void RemoveLast()
        {
            ThrowReadOnly();
            ThrowListIsEmpty();

            Last.Previous.Next = sentinel;
            sentinel.Previous = Last.Previous;
            Last.LinkTo();
            Count--;
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
            if (node.Next != null || node.Previous != null)
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
