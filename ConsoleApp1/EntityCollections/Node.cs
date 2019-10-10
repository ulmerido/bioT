using System;
using System.Collections;
using System.Collections.Generic;

namespace bioT
{
    // Generic Node For A Binary Tree
    public class Node<T> : IDisposable, IEnumerable
    {
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public Node<T> Parent { get; set; }
        public T Data { get; set; }

        public Node()
        {
            Parent = Left = Right = null;
        }

        public Node(Node<T> left, Node<T> right, Node<T> parent, T data)
        {
            Left = left;
            Right = right;
            Parent = parent;
            Data = data;
        }

        public void Dispose()
        {
            Data = default(T);
            Parent = Left = Right = null;
        }

        // Itreration using a stack to store the unused nodes
        public IEnumerator GetEnumerator()
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            stack.Push(this);
            while (stack.Count > 0)
            {
                Node<T> res = stack.Pop();
                yield return res.Data;
                if (res.Left != null) stack.Push(res.Left);
                if (res.Right != null) stack.Push(res.Right);
            }
        }
    }

}
