using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinaryTree
{
    /// <summary>
    /// Generic binary tree
    /// </summary>
    /// <typeparam name="T">Type of the elements which this tree contains</typeparam>
    public class BinaryTree<T> : ISet<T>
    {
        /// <summary>
        /// Pointer to the top node
        /// </summary>
        private Node head;

        /// <summary>
        /// Get the priority between two nodes
        /// </summary>
        private Func<T, T, bool> compare;

        /// <summary>
        /// Get the function to compare elements;
        /// Return true, if the first element has higher priority
        /// </summary>
        /// <param name="func">Needed function</param>
        public BinaryTree(Func<T, T, bool> func)
        {
            compare = func;
        }

        /// <summary>
        /// The size of the tree
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Check whether we can only read the tree
        /// </summary>
        public bool IsReadOnly { get; set; } = false;

        /// <summary>
        /// Add node in the tree using compare function to define the priority
        /// </summary>
        /// <param name="value">The element to add to the set</param>
        /// <returns>true if the element is added; false if the element is already in the set</returns>
        public bool Add(T value)
        {
            if (IsReadOnly)
            {
                throw new ReadOnlyException();
            }

            if (Count == 0)
            {
                head = new Node(null, null, null, value);
                Count++;
                return true;
            }

            var temp = head;
            while (true)
            {
                if (Equals(temp.Value, value))
                {
                    return false;
                }
                else if (compare(value, temp.Value))
                {
                    if (temp.LeftChild == null)
                    {
                        temp.LeftChild = new Node(null, null, temp, value);
                        Count++;
                        return true;
                    }

                    temp = temp.LeftChild;
                }
                else
                {
                    if (temp.RightChild == null)
                    {
                        temp.RightChild = new Node(null, null, temp, value);
                        Count++;
                        return true;
                    }

                    temp = temp.RightChild;
                }
            }
        }

        void ICollection<T>.Add(T value)
        {
            Add(value);
        }

        /// <summary>
        /// Remove all nodes from the tree
        /// </summary>
        public void Clear()
        {
            if (IsReadOnly)
            {
                throw new ReadOnlyException();
            }

            head = null;
            Count = 0;
        }

        /// <summary>
        /// Check whether the tree contains this value
        /// </summary>
        /// <param name="value">Value to be checked</param>
        /// <returns>True, if the tree contains this value</returns>
        public bool Contains(T value) => FindNode(value) != null;

        /// <summary>
        /// Copy all elements to the given array starting from given index
        /// </summary>
        /// <param name="array">Given array</param>
        /// <param name="index">Given index</param>
        public void CopyTo(T[] array, int index)
        {
            if (index < 0 || index >= array.Length)
            {
                throw new WrongIndexException();
            }

            if (array.Length - (index + Count) < 0)
            {
                throw new IndexOutOfRangeException();
            }

            foreach (var element in this)
            {
                array[index] = element;
                index++;
            }
        }

        /// <summary>
        /// Remove from the tree all the elements given in set
        /// </summary>
        /// <param name="set">Set of elements that are to be removed from the tree</param>
        public void ExceptWith(IEnumerable<T> set)
        {
            foreach (var element in set)
            {
                RemoveNode(FindNode(element));
            }
        }

        /// <summary>
        /// Get the enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(head);
        }

        /// <summary>
        /// Get enumerator for the container
        /// </summary>
        /// <returns>Enumerator</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements 
        /// that are also in a specified collection
        /// </summary>
        /// <param name="set">The collection to compare to the tree</param>
        public void IntersectWith(IEnumerable<T> set)
        {
            Intersect(set, head);
        }

        /// <summary>
        /// Determines whether the tree is a proper (strict) subset of a specified collection
        /// </summary>
        /// <param name="set">The collection to compare to the tree</param>
        /// <returns>true if the tree is a proper subset of other; otherwise, false</returns>
        public bool IsProperSubsetOf(IEnumerable<T> set) => DefineBelonging(this, set, true);

        /// <summary>
        /// Determines whether a specified collection is a proper (strict) subset of the tree
        /// </summary>
        /// <param name="set">The collection to compare to the tree</param>
        /// <returns>true if set is a proper subset of the tree; otherwise, false</returns>
        public bool IsProperSupersetOf(IEnumerable<T> set) => DefineBelonging(set, this, true);

        /// <summary>
        /// Determines whether the tree is a subset of a specified collection
        /// </summary>
        /// <param name="set">he collection to compare to the tree</param>
        /// <returns>true if the tree is a subset of other; otherwise, false</returns>
        public bool IsSubsetOf(IEnumerable<T> set) => DefineBelonging(this, set, false);

        /// <summary>
        /// Determines whether a specified collection is a subset of the tree
        /// </summary>
        /// <param name="set">he collection to compare to the tree</param>
        /// <returns>true if set is a subset of the tree; otherwise, false</returns>
        public bool IsSupersetOf(IEnumerable<T> set) => DefineBelonging(set, this, false);

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection
        /// </summary>
        /// <param name="set">The collection to compare to the tree</param>
        /// <returns>true if the tree set and other share at least one common element; otherwise, false</returns>
        public bool Overlaps(IEnumerable<T> set)
        {
            foreach (var element in this)
            {
                if (set.Contains(element))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Remove node from the tree
        /// </summary>
        /// <param name="value">Element to be removed</param>
        /// <returns>Whether the element was removed</returns>
        public bool Remove(T value)
        {
            return RemoveNode(FindNode(value));
        }

        /// <summary>
        /// Determines whether the tree and the specified collection contain the same elements
        /// </summary>
        /// <param name="set">The collection to compare to the tree</param>
        /// <returns>true if the tree is equal to set; otherwise, false</returns>
        public bool SetEquals(IEnumerable<T> set)
        {
            if (!Equals(set.Count(), Count))
            {
                return false;
            }

            foreach (var element in set)
            {
                if (!Contains(element))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Modifies the tree so that it contains only elements that are present
        /// either in the tree or in the specified collection, but not both
        /// </summary>
        /// <param name="set">The collection to compare to the current set</param>
        public void SymmetricExceptWith(IEnumerable<T> set)
        {
            foreach (var element in set)
            {
                var node = FindNode(element);
                if (node != null)
                {
                    RemoveNode(node);
                }
            }
        }

        /// <summary>
        /// Modifies the tree so that it contains all elements 
        /// that are present in the tree, in the specified collection, or in both
        /// </summary>
        /// <param name="set">The collection to compare to the tree</param>
        public void UnionWith(IEnumerable<T> set)
        {
            foreach (var element in set)
            {
                if (!Contains(element))
                {
                    Add(element);
                }
            }
        }

        /// <summary>
        /// Finds the node by it`s value, returns the pointer to the wanted node
        /// </summary>
        /// <param name="value">Given value</param>
        /// <returns>Pointer to the wanted node</returns>
        private Node FindNode(T value)
        {
            var temp = head;
            while (temp != null && !Equals(temp.Value, value))
            {
                temp = compare(value, temp.Value) ? temp.LeftChild : temp.RightChild;
            }

            return temp;
        }

        /// <summary>
        /// Modifies the current set so that it contains only elements 
        /// that are also in a specified collection
        /// </summary>
        /// <param name="set">Specified collection</param>
        /// <param name="temp">Current pointer</param>
        private void Intersect(IEnumerable<T> set, Node temp)
        {
            if (IsReadOnly)
            {
                throw new ReadOnlyException();
            }

            if (Count == 0)
            {
                return;
            }

            if (temp.LeftChild != null)
            {
                Intersect(set, temp.LeftChild);
            }

            while (!set.Contains(temp.Value))
            {
                RemoveNode(temp);
                if (temp == null)
                {
                    return;
                }
            }

            if (temp.RightChild != null)
            {
                Intersect(set, temp.RightChild);
            }
        }

        /// <summary>
        /// Define whether the first set is a (proper) subset of the second set
        /// </summary>
        /// <param name="subset">Set that is thought to be a subset</param>
        /// <param name="set">Set that is thought to be a superset</param>
        /// <param name="strict">Check whether belonging is strict</param>
        /// <returns>Whether this belonging is right</returns>
        private bool DefineBelonging(IEnumerable<T> subset, IEnumerable<T> set, bool strict)
        {
            if (strict)
            {
                if (set.Count() <= subset.Count())
                {
                    return false;
                }
            }

            if (!strict)
            {
                if (set.Count() < subset.Count())
                {
                    return false;
                }
            }

            foreach (var element in subset)
            {
                if (!set.Contains(element))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Removes node by the pointer
        /// </summary>
        /// <param name="node">The pointer to the node that is to be deleted</param>
        private bool RemoveNode(Node node)
        {
            if (IsReadOnly || Count == 0 || node == null)
            {
                return false;
            }

            var temp = node;
            
            if (temp.LeftChild != null)
            {
                temp = temp.LeftChild;
                while (temp.RightChild != null)
                {
                    temp = temp.RightChild;
                }
                node.Value = temp.Value;
                RemoveNode(temp);
            }
            else if (temp.RightChild != null)
            {
                temp = temp.RightChild;
                while (temp.LeftChild != null)
                {
                    temp = temp.LeftChild;
                }
                node.Value = temp.Value;
                RemoveNode(temp);
            }
            else
            {
                if (temp.Parent == null)
                {
                    head = null;
                }
                else if (Equals(temp.Parent.LeftChild, temp))
                {
                    temp.Parent.LeftChild = null;
                }
                else
                {
                    temp.Parent.RightChild = null;
                }

                Count--;
            }

            return true;
        }

        /// <summary>
        /// Class that describes the element of the BinaryTree
        /// </summary>
        private class Node
        {
            /// <summary>
            /// Get and set the pointer to the left child
            /// </summary>
            public Node LeftChild { get; set; }

            /// <summary>
            /// Get and set the pointer to the right child
            /// </summary>
            public Node RightChild { get; set; }

            /// <summary>
            /// Get and set the pointer to the parent
            /// </summary>
            public Node Parent { get; set; }

            /// <summary>
            /// Get and set the value of the element
            /// </summary>
            public T Value { get; set; }

            /// <summary>
            /// Set parametrs during creation of the element
            /// </summary>
            /// <param name="leftChild">Pointer to the left child</param>
            /// <param name="rightChild">Pointer to the right child</param>
            /// <param name="parent">Pointer to the parent</param>
            /// <param name="value">Value of the node</param>
            public Node(Node leftChild, Node rightChild, Node parent, T value)
            {
                LeftChild = leftChild;
                RightChild = rightChild;
                Parent = parent;
                Value = value;
            }
        }

        /// <summary>
        /// The class that supports a simple iteration over a generic collection
        /// </summary>
        private class Enumerator : IEnumerator<T>
        {
            private Node head;
            private Node current;

            /// <summary>
            /// Gets the reference to the head of the list
            /// </summary>
            /// <param name="head">The first element of the list</param>
            public Enumerator(Node head)
            {
                this.head = head;
            }

            /// <summary>
            /// Get the value of the current node
            /// </summary>
            public T Current { get => current.Value; }
            object IEnumerator.Current { get => Current; }

            public void Dispose() { }

            /// <summary>
            /// Moves the reference to the current position to the next position
            /// </summary>
            /// <returns>Whether the current element isn`t the last</returns>
            public bool MoveNext()
            {
                if (head == null)
                {
                    return false;
                }

                void DeepLeft()
                {
                    while (current.LeftChild != null)
                    {
                        current = current.LeftChild;
                    }
                }

                if (current == null)
                {
                    current = head;
                    DeepLeft();
                    return true;
                }

                if (current.RightChild != null)
                {
                    current = current.RightChild;
                    DeepLeft();
                    return true;
                }

                if (current.Parent != null && Equals(current, current.Parent.LeftChild))
                {
                    current = current.Parent;
                    return true;
                }

                while (current.Parent != null && Equals(current, current.Parent.RightChild))
                {
                    current = current.Parent;
                }
                if (current.Parent == null)
                {
                    return false;
                }
                current = current.Parent;
                return true;
            }

            /// <summary>
            /// Set the enumerator to its initial position
            /// </summary>
            public void Reset()
            {
                current = null;
            }
        }
    }
}
