using System;
using Xunit;
using Linked_List;

namespace Linked_List_Facts
{
    public class LinkedListFacts
    {
        [Fact]
        public void Count_Property_For_New_Int_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var nodeToInsert = new Node<int>(3);
            linkedList.AddFirst(nodeToInsert);
            //Then
            Assert.Equal(1, linkedList.Count);
        }

        [Fact]
        public void First_Property_For_New_Int_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var nodeToInsert = new Node<int>(3);
            linkedList.AddFirst(nodeToInsert);
            //Then
            Assert.Equal(nodeToInsert, linkedList.First);
        }

        [Fact]
        public void First_Property_For_Empty_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            //Then
            Assert.Null(linkedList.First);
        }

        [Fact]
        public void Last_Property_For_New_Int_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var nodeToInsert = new Node<int>(3);
            var anotherNode = new Node<int>(2);
            linkedList.AddFirst(nodeToInsert);
            linkedList.AddLast(anotherNode);
            //Then
            Assert.Equal(anotherNode, linkedList.Last);
        }

        [Fact]
        public void Last_Property_For_Empty_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            //Then
            Assert.Null(linkedList.Last);
        }

        [Fact]
        public void Test_Contains_Method_Should_Return_True_List_Contains_Element()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var nodeToInsert = new Node<int>(3);
            linkedList.AddFirst(nodeToInsert);
            //Then
            Assert.Contains(3, linkedList);
        }

        [Fact]
        public void Test_Contains_Method_Should_Return_False_List_Does_Not_Contains_Element()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var nodeToInsert = new Node<int>(3);
            linkedList.AddFirst(nodeToInsert);
            //Then
            Assert.DoesNotContain(100, linkedList);
        }

        [Fact]
        public void Test_AddFirst_Method_For_Int_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var nodeToInsert = new Node<int>(3);
            //When
            linkedList.AddFirst(nodeToInsert);
            //Then
            Assert.Single(linkedList);
        }

        [Fact]
        public void Test_AddFirst_Method_Value_Param()
        {
            //Given
            var linkedList = new LinkedList<int>();
            //When
            linkedList.AddFirst(3);
            linkedList.AddFirst(4);
            var enumerator = linkedList.GetEnumerator();
            //Then
            Assert.Equal(2, linkedList.Count);
            Assert.True(enumerator.MoveNext());
            Assert.True(enumerator.MoveNext());
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void Get_Enumerator_For_Empty_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            //When
            var enumerator = linkedList.GetEnumerator();
            //Then
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void Get_Enumerator_For_One_Element_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var node = new Node<int>(3);
            //When
            linkedList.AddFirst(node);
            var enumerator = linkedList.GetEnumerator();
            //Then
            Assert.True(enumerator.MoveNext());
            Assert.Equal(3, enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void Get_Enumerator_When_List_Has_2_Elements()
        {
            //Given
            var linkedList = new LinkedList<string>();
            var node = new Node<string>("Andrei");
            //When
            linkedList.AddFirst(node);
            linkedList.AddFirst(new Node<string>("AltAndrei"));
            var enumerator = linkedList.GetEnumerator();
            //Then
            Assert.True(enumerator.MoveNext());
            Assert.Equal("AltAndrei", enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal("Andrei", enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void Get_Enumerator_When_List_Has_More_Repeating_Elements()
        {
            //Given
            var linkedList = new LinkedList<string>();
            var node = new Node<string>("Andrei");
            //When
            linkedList.AddFirst(node);
            linkedList.AddFirst(new Node<string>("AltAndrei"));
            linkedList.AddFirst(new Node<string>("AltAndrei"));
            linkedList.AddFirst(new Node<string>("abc"));
            linkedList.AddFirst(new Node<string>("abc"));
            var enumerator = linkedList.GetEnumerator();
            //Then
            Assert.True(enumerator.MoveNext());
            Assert.Equal("abc", enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal("abc", enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal("AltAndrei", enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal("AltAndrei", enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal("Andrei", enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void Test_Find_Method_For_One_Element_List()
        {
            //Given
            var node = new Node<int>(5);
            var linkedList = new LinkedList<int>();
            //When
            linkedList.AddFirst(node);
            //Then
            Assert.Equal(node, linkedList.Find(5));
        }

        [Fact]
        public void Test_Find_Method_For_Empty_List()
        {
            //When
            var linkedList = new LinkedList<int>();
            //Then
            Assert.Null(linkedList.Find(2));
        }

        [Fact]
        public void Test_Find_Method_For_Null_Element()
        {
            //Given
            var linkedList = new LinkedList<object>();
            var obj = new Node<object>(null);
            //When
            linkedList.AddFirst(obj);
            //Then
            Assert.Null(linkedList.Find(2));
        }

        [Fact]
        public void Test_Find_Method_When_List_Has_More_Elements()
        {
            //Given
            var node = new Node<int>(5);
            var secondNode = new Node<int>(3);
            var thirdNode = new Node<int>(4);
            var forthNode = new Node<int>(4);
            var linkedList = new LinkedList<int>();
            //When
            linkedList.AddFirst(forthNode);
            linkedList.AddFirst(thirdNode);
            linkedList.AddFirst(secondNode);
            linkedList.AddFirst(node);
            //Then
            Assert.Equal(node, linkedList.Find(5));
            Assert.Equal(secondNode, linkedList.Find(3));
            Assert.Equal(thirdNode, linkedList.Find(4));
            Assert.Null(linkedList.Find(100));
        }

        [Fact]
        public void Test_FindLast_Method_For_Simple_List()
        {
            //Given
            var a = new Node<int>(5);
            var b = new Node<int>(3);
            var c = new Node<int>(3);
            var linkedList = new LinkedList<int>();
            //When
            linkedList.AddFirst(c);
            linkedList.AddFirst(b);
            linkedList.AddFirst(a);
            //Then
            Assert.Equal(c, linkedList.FindLast(3));
        }

        [Fact]
        public void Test_Exception_For_AddFirst_Argument_Is_Null()
        {
            //Given
            var linkedList = new LinkedList<string>();
            var node = new Node<string>(default);
            node = null;
            //When
            var exception = Assert.Throws<ArgumentNullException>(() => linkedList.AddFirst(node));
            //Then
            Assert.Equal("node", exception.ParamName);
            Assert.Empty(linkedList);
        }

        [Fact]
        public void Test_Exception_For_AddFirst_Argument_Already_Belongs_To_Another_List()
        {
            //Given
            var node = new Node<string>("Andrei");
            var originalList = new LinkedList<string>();
            originalList.AddFirst(node);
            var listToAdd = new LinkedList<string>();
            //When
            var exception = Assert.Throws<InvalidOperationException>(() => listToAdd.AddFirst(node));
            //Then
            Assert.Equal("Node already belongs to another list!\n", exception.Message);
            Assert.Empty(listToAdd);
        }

        [Fact]
        public void Test_Exception_For_AddFirst_Argument_Already_Belongs_To_Another_List_First_Property()
        {
            //Given
            var originalList = new LinkedList<string> { "1", "2" };
            var node = originalList.First;
            var listToAdd = new LinkedList<string>();
            //When
            var exception = Assert.Throws<InvalidOperationException>(() => listToAdd.AddFirst(node));
            //Then
            Assert.Equal("Node already belongs to another list!\n", exception.Message);
            Assert.Empty(listToAdd);
        }

        [Fact]
        public void Test_FindLast_Method_For_Empty_List()
        {
            //When
            var linkedList = new LinkedList<int>();
            //Then
            Assert.Null(linkedList.FindLast(2));
        }

        [Fact]
        public void Test_FindLast_Method_For_One_Element_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var a = new Node<int>(3);
            //When
            linkedList.AddFirst(a);
            //Then
            Assert.Equal(a, linkedList.FindLast(3));
        }

        [Fact]
        public void Test_FindLast_Method_When_List_Has_More_Elements()
        {
            //Given
            var node = new Node<int>(5);
            var secondNode = new Node<int>(3);
            var thirdNode = new Node<int>(4);
            var forthNode = new Node<int>(4);
            var linkedList = new LinkedList<int>();
            //When
            linkedList.AddFirst(forthNode);
            linkedList.AddFirst(thirdNode);
            linkedList.AddFirst(secondNode);
            linkedList.AddFirst(node);
            //Then
            Assert.Equal(node, linkedList.FindLast(5));
            Assert.Equal(forthNode, linkedList.FindLast(4));
            Assert.Equal(secondNode, linkedList.FindLast(3));
            Assert.Equal(node, linkedList.Find(5));
        }

        [Fact]
        public void Test_FindLast_Method_For_Null_Element()
        {
            //Given
            var linkedList = new LinkedList<object>();
            var obj = new Node<object>(default);
            obj = null;
            //When
            var exception = Assert.Throws<ArgumentNullException>(() => linkedList.AddFirst(obj));
            //Then
            Assert.Equal("node", exception.ParamName);
        }

        [Fact]
        public void Test_AddLast_Method_For_Empty_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var nodeToInsert = new Node<int>(3);
            //When
            linkedList.AddLast(nodeToInsert);
            //Then
            Assert.Single(linkedList);
            Assert.Equal(nodeToInsert, linkedList.Find(3));
        }

        [Fact]
        public void Test_AddLast_Method_For_Int_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var a = new Node<int>(1);
            var b = new Node<int>(2);
            var c = new Node<int>(3);
            var d = new Node<int>(4);
            //When
            linkedList.AddLast(a);
            linkedList.AddLast(b);
            linkedList.AddLast(c);
            linkedList.AddLast(d);
            //Then
            Assert.Equal(4, linkedList.Count);
            Assert.Equal(d, linkedList.Find(4));
            Assert.Equal(c, linkedList.Find(3));
            Assert.Equal(b, linkedList.Find(2));
            Assert.Equal(a, linkedList.Find(1));
        }

        [Fact]
        public void Test_Add_Method_Value_param()
        {
            //Given
            var linkedList = new LinkedList<string>();
            //When
            linkedList.Add("a");
            linkedList.Add("b");
            linkedList.Add("b");
            var enumerator = linkedList.GetEnumerator();
            //Then
            Assert.Equal(3, linkedList.Count);
            Assert.True(enumerator.MoveNext());
            Assert.Equal("a", enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal("b", enumerator.Current);
            Assert.True(enumerator.MoveNext());
            Assert.Equal("b", enumerator.Current);
            Assert.False(enumerator.MoveNext());
        }

        [Fact]
        public void Test_AddBefore_Method_For_Int_List()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            var nodeToInsert = new Node<int>(100);
            linkedList.AddLast(nodeToInsert);
            //When
            var newNode = new Node<int>(100);
            linkedList.AddBefore(nodeToInsert, newNode);
            //Then
            Assert.Equal(5, linkedList.Count);
            Assert.Equal(nodeToInsert, linkedList.FindLast(100));
            Assert.Equal(newNode, linkedList.Find(100));
        }

        [Fact]
        public void Test_AddBefore_Method_For_Item_Second_Overloading()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            var nodeToInsert = new Node<int>(100);
            linkedList.AddLast(nodeToInsert);
            //When
            linkedList.AddBefore(nodeToInsert, 100);
            //Then
            Assert.Equal(5, linkedList.Count);
            Assert.Equal(nodeToInsert, linkedList.FindLast(100));
            Assert.NotEqual(nodeToInsert, linkedList.Find(100));
        }

        [Fact]
        public void Test_Exception_For_AddBefore_NewNode_Is_Null()
        {
            //Given
            var a = new Node<string>("Eusebiu");
            var linkedList = new LinkedList<string>();
            linkedList.AddLast(a);
            linkedList.AddFirst("a");
            var node = new Node<string>(default);
            node = null;
            //When
            var exception = Assert.Throws<ArgumentNullException>(() => linkedList.AddBefore(a, node));
            //Then
            Assert.Equal("node", exception.ParamName);
            Assert.Equal(2, linkedList.Count);
        }

        [Fact]
        public void Test_Exception_For_AddBefore_ListNode_Is_Null()
        {
            //Given
            var a = new Node<string>("Eusebiu");
            var linkedList = new LinkedList<string>();
            linkedList.AddLast(a);
            linkedList.AddFirst("a");
            var node = new Node<string>(default);
            node = null;
            //When
            var exception = Assert.Throws<ArgumentNullException>(() => linkedList.AddBefore(node, a));
            //Then
            Assert.Equal("node", exception.ParamName);
            Assert.Equal(2, linkedList.Count);
        }

        [Fact]
        public void Test_Exception_For_AddBefore_List_Node_Is_Not_in_List()
        {
            //Given
            var a = new Node<string>("Eusebiu");
            var linkedList = new LinkedList<string>();
            linkedList.AddFirst("a");
            var node = new Node<string>("Andrei");
            //When
            var exception = Assert.Throws<InvalidOperationException>(() => linkedList.AddBefore(node, a));
            //Then
            Assert.Equal("List does not contain given node!\n", exception.Message);
            Assert.Single(linkedList);
        }

        [Fact]
        public void Test_AddAfter_Method_For_Int_List()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            var nodeToInsert = new Node<int>(100);
            linkedList.AddFirst(nodeToInsert);
            //When
            var newNode = new Node<int>(100);
            linkedList.AddAfter(nodeToInsert, newNode);
            //Then
            Assert.Equal(5, linkedList.Count);
            Assert.Equal(newNode, linkedList.FindLast(100));
            Assert.Equal(nodeToInsert, linkedList.Find(100));
        }

        [Fact]
        public void Test_AddAfter_Method_For_Item_Second_Overloading()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            var nodeToInsert = new Node<int>(100);
            linkedList.AddFirst(nodeToInsert);
            //When
            linkedList.AddAfter(nodeToInsert, 100);
            //Then
            Assert.Equal(5, linkedList.Count);
            Assert.Equal(nodeToInsert, linkedList.Find(100));
            Assert.NotEqual(nodeToInsert, linkedList.FindLast(100));
        }

        [Fact]
        public void Test_CopyTo_Method_Array_Has_Enough_Capacity()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            var array = new int[linkedList.Count];
            //When
            linkedList.CopyTo(array, 0);
            //Then
            Assert.Equal(3, linkedList.Count);
            Assert.Equal(1, array[0]);
            Assert.Equal(2, array[1]);
            Assert.Equal(3, array[2]);
        }

        [Fact]
        public void Test_CopyTo_Method_Uncopied_Part_Has_Remained_Unchanged()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            var array = new int[10];
            //When
            linkedList.CopyTo(array, 2);
            //Then
            Assert.Equal(10, array.Length);
            Assert.Equal(0, array[0]);
            Assert.Equal(0, array[1]);
            Assert.Equal(1, array[2]);
            Assert.Equal(3, array[4]);
            Assert.Equal(0, array[5]);
            Assert.Equal(0, array[9]);
        }

        [Fact]
        public void Test_Exception_For_CopyTo_Method_Array_Is_Null()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            int[] array = null;
            //When
            var exception = Assert.Throws<ArgumentNullException>(() => linkedList.CopyTo(array, 2));
            //Then
            Assert.Equal("array", exception.ParamName);
        }

        [Fact]
        public void Test_Exception_For_CopyTo_Method_ArrayIndex_Is_Smaller_Then_0()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            int[] array = new int[5];
            //When
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => linkedList.CopyTo(array, -4));
            //Then
            Assert.Equal("arrayIndex", exception.ParamName);
        }

        [Fact]
        public void Test_Exception_CopyTo_Method_Not_Enough_Available_Space()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            var array = new int[5];
            //When
            var exception = Assert.Throws<ArgumentException>(() => linkedList.CopyTo(array, 3));
            //Then
            Assert.Equal("array", exception.ParamName);
        }

        [Fact]
        public void Test_RemoveMethod_should_correctly_Remove_Item()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            //Then
            Assert.True(linkedList.Remove(1));
            Assert.Equal(2, linkedList.Count);
            Assert.Equal(3, linkedList.Last.Data);
            Assert.Equal(2, linkedList.First.Data);
        }

        [Fact]
        public void Test_RemoveMethod_should_correctly_Remove_Item_One_Element_List()
        {
            //Given
            var linkedList = new LinkedList<int> { 1 };
            //Then
            Assert.True(linkedList.Remove(1));
            Assert.Empty(linkedList);
        }

        [Fact]
        public void Test_RemoveMethod_should_correctly_Remove_Item_Second_Overload()
        {
            //Given
            var linkedList = new LinkedList<int>();
            var node = new Node<int>(1);
            //When
            linkedList.AddLast(node);
            //Then
            Assert.True(linkedList.Remove(node));
            Assert.Empty(linkedList);
            Assert.Null(linkedList.Find(1));
        }

        [Fact]
        public void Test_RemoveMethod_should_return_False()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            //Then
            Assert.False(linkedList.Remove(100));
            Assert.Equal(3, linkedList.Count);
        }

        [Fact]
        public void Test_Exception_For_Remove_Node_Is_Not_In_List()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            var node = new Node<int>(100);
            //When
            var exception = Assert.Throws<InvalidOperationException>(() => linkedList.Remove(node));
            //Then
            Assert.Equal("List does not contain given node!\n", exception.Message);
        }

        [Fact]
        public void Test_RemoveFirst_Method_should_correctly_Remove_Item()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            //When
            linkedList.RemoveFirst();
            //Then
            Assert.Equal(2, linkedList.Count);
            Assert.Equal(3, linkedList.Last.Data);
            Assert.Equal(2, linkedList.First.Data);
        }

        [Fact]
        public void Test_RemoveLast_Method_should_correctly_Remove_Item()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            //When
            linkedList.RemoveLast();
            //Then
            Assert.Equal(2, linkedList.Count);
            Assert.Equal(2, linkedList.Last.Data);
            Assert.Equal(1, linkedList.First.Data);
        }

        [Fact]
        public void Test_Exception_For_RemoveLast_Method_List_Is_Empty()
        {
            //Given
            var linkedList = new LinkedList<int>();
            //When
            var exception = Assert.Throws<InvalidOperationException>(() => linkedList.RemoveLast());
            //Then
            Assert.Empty(linkedList);
            Assert.Equal("List is empty!\n", exception.Message);
        }

        [Fact]
        public void Test_ReadOnly_Property_List_Is_NOT_Readonly()
        {
            //Given
            var linkedList = new LinkedList<int>();
            //Then
            Assert.False(linkedList.IsReadOnly);
        }

        [Fact]
        public void Test_ReadOnly_Property_List_IS_Readonly()
        {
            //Given
            var linkedList = new LinkedList<int>();
            //When
            var ro = linkedList.AsReadOnly();
            //Then
            Assert.True(ro.IsReadOnly);
            Assert.False(linkedList.IsReadOnly);
        }

        [Fact]
        public void Test_Exception_When_List_Is_ReadOnly()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3 };
            //When
            var ro = linkedList.AsReadOnly();
            var exception = Assert.Throws<NotSupportedException>(() => ro.AddFirst(100));
            //Then
            Assert.Equal(3, ro.Count);
            Assert.Equal(3, linkedList.Last.Data);
            Assert.Equal(1, linkedList.First.Data);
            Assert.Equal("List is readonly!\n", exception.Message);
        }

        [Fact]
        public void Test_Exception_When_List_Is_ReadOnly_Count_Is_1()
        {
            //Given
            var linkedList = new LinkedList<int> { 1 };
            //When
            var ro = linkedList.AsReadOnly();
            var exception = Assert.Throws<NotSupportedException>(() => ro.AddFirst(100));
            //Then
            Assert.Single(ro);
            Assert.Equal(1, linkedList.First.Data);
            Assert.Equal("List is readonly!\n", exception.Message);
        }

        [Fact]
        public void Test_Exception_When_List_Is_ReadOnly_Empty_List()
        {
            //Given
            var linkedList = new LinkedList<int>();
            //When
            var ro = linkedList.AsReadOnly();
            var exception = Assert.Throws<NotSupportedException>(() => ro.AddFirst(100));
            //Then
            Assert.Empty(ro);
            Assert.Equal("List is readonly!\n", exception.Message);
        }

        [Fact]
        public void Test_Exception_When_List_Is_ReadOnly_References_Do_Not_Interfere()
        {
            //Given
            var linkedList = new LinkedList<int> { 1, 2, 3, 4, 5 };
            //When
            var ro = linkedList.AsReadOnly();
            linkedList.First.Data = 3;
            //Then
            Assert.Equal(1, ro.First.Data);
        }
    }
}
