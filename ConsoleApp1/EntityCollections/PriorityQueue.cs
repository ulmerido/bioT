using System;
using System.Collections;

namespace bioT.EntityCollections
{
    /* 
     * PriorityQueue via Binary Tree; Max Heap (nodes, not array)
     * Deafult compare: Higher value = Higher priority
     * Ctor: User can use this data stracture with T(T is Generic) where T is IComperable, or any T and send a compare Functions
     * The compare function Func<T, T, int> gets T1 and T2 and returns:
           negative  if T1 < T2 
           0         if T1 == T2
           positive  if T1 > T2

        Example:
            var pq1 = new PriorityQueue<int>();                                                       // Works!  max heap
            var pq2 = new PriorityQueue<int>( (x,y)=> {return y-x;})                                  // Works!  min Heap
            var pq3 = new PriorityQueue<SomeClassNotIComperabel>()                                    // Dont Work! Exception("Can't compare types");  
            var pq3 = new PriorityQueue<SomeClassNotIComperabel>(someCompareFunctionForThisClass)     // Works! Pirority is dictated by the function the user sent    
     */

    public class PriorityQueue<T> :IEnumerable
    {
        protected readonly Node<T> r_Root = new Node<T>();
        protected Func<T, T, int>  f_Comperator;
        private const string       k_ErrorCompareMsg = "Can't compare types";

        public uint Count { get; protected set; } = 0;

        public PriorityQueue(Func<T, T, int> i_Comperator = null)
        {
            if (i_Comperator == null)
            {
                if (!typeof(IComparable).IsAssignableFrom(typeof(T)))
                {
                    throw new Exception(k_ErrorCompareMsg);
                }
                else
                {
                    f_Comperator = compare;
                }
            }
            else
            {
                f_Comperator = i_Comperator;
            }
        }

        public void Add(T i_Data)
        {
            Count++;
            if (Count == 1) // only 1 node
            {
                r_Root.Data = i_Data;
                return;
            }

            Node<T> lastNode = getLastNode(true); //find the node we need to a child for him
            Node<T> nodeToAdd = new Node<T>(null, null, lastNode, i_Data); // create the node for adding
            if (lastNode.Left == null)             
            {
                lastNode.Left = nodeToAdd;
                bubbleUp(lastNode.Left);
            }
            else
            {
                lastNode.Right = nodeToAdd;
                bubbleUp(lastNode.Right);
            }
        }

        public T Peek()
        {
            return r_Root.Data;
        }

        //swap the removed data(data from Root)  with the last node added, removes the last node. bubbel down the new data in the root to the correct place.
        public T Pop()
        {
            if (Count == 0) // empty queue
                return default(T);

            T res = r_Root.Data;
            Node<T> lastNode = Count == 1 ? r_Root : getLastNode(); // last node added is root or find it with the function
            Count--; 

            swapData(lastNode, r_Root); 
            removeNode(lastNode);

            bubbleDown();

            return res;
        }

        // move the root data to the correct place
        private void bubbleDown() 
        {
            Node<T> curr = r_Root;
            Node<T> maxChild = maxPriority(curr.Left, curr.Right);
            while (!isLeaf(curr) && !isPrier(curr.Data, maxChild.Data))
            {
              
                swapData(curr, maxChild);
                curr = maxChild;
                maxChild = maxPriority(curr.Left, curr.Right);
            }
        }

        // move the leaf data to the correct place
        private void bubbleUp(Node<T> i_Curr)
        {
            while (i_Curr.Parent != null && f_Comperator(i_Curr.Data, i_Curr.Parent.Data) > 0)
            {
                swapData(i_Curr, i_Curr.Parent);
                i_Curr = i_Curr.Parent;
            }
        }

        private void removeNode(Node<T> i_Node)
        {
            if (i_Node.Parent?.Right == i_Node)
                i_Node.Parent.Right = null;

            if (i_Node.Parent?.Left == i_Node)
                i_Node.Parent.Left = null;

            i_Node.Dispose();

        }

        private void swapData(Node<T> a, Node<T> b)
        {
            T temp = a.Data;
            a.Data = b.Data;
            b.Data = temp;
        }

        // Find last node that was added by using the current Count
        // Algoritm: let X be the Current Count
        //           let bx be the binary representation of X
        //           start from the root
        //           ignore the MSB of bx. for each bit from MSB to LSB (left to right) in bx
        //           if the bit is 0 go to left node, else go to right node
        //           Example: X  = 11 (decimal)
        //                    bx = 1011 (binary)
        //                     MSB ignored so were left with 011
        //                               0 - left-node. 
        //                               1 - right-node. 
        //                               1 - right-node. 
        //                     summary: root->left->right->right = last node added
        //                     To find the node that we should add to him a child call: getLastNode(false)

        private Node<T> getLastNode(bool getNodeForAdding =false)
        {
            int condition = 0;
            if (getNodeForAdding) condition = 1;
            Node<T> curr = r_Root;
            string binary = Convert.ToString(this.Count, 2);

            for (int i = 1; i < binary.Length - condition; i++)
            {
                curr = binary[i] == '0' ? curr.Left : curr.Right;
            }

            return curr;
        }
  
        protected virtual Node<T> maxPriority(Node<T> a, Node<T> b)
        {
            if (a == null)
                return b;

            if (b == null)
                return a;

            return isPrier(a.Data, b.Data) ? a : b;
        }

        private bool isLeaf(Node<T> node)
        {
            bool res = node.Left == null && node.Right == null;
            return res;
        }

        private bool isPrier(T a, T b)
        {
            return f_Comperator(a, b) > 0;
        }

        private int compare(T a, T b)
        {
            return ((IComparable)a).CompareTo(b);
        }

        public virtual IEnumerator GetEnumerator()
        {
            foreach(T data in r_Root)
            {
                yield return data; 
            }
        }
    }
}
