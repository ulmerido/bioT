using System.Collections;
using System.Collections.Generic;
using bioT.Interfaces;
namespace bioT.EntityCollections
{
    //  Add O(n)
    //  RemoveMax O(1)

    public class EntityCollectionC : IEntityCollection, IEnumerable
    {
        protected readonly LinkedList<IEntity> m_Collection = new LinkedList<IEntity>();

        // Itirate on the ordered Hight-Low LinkedList and inserts the new entity to the correct place
        public void Add(IEntity i_Entity)
        {
            LinkedListNode<IEntity> node = m_Collection.First;
            while (node != null)
            {
                if (i_Entity.Value > node.Value.Value)
                {
                    m_Collection.AddBefore(node, i_Entity);
                    return;
                }
            }

            m_Collection.AddLast(i_Entity);
        }

        public IEnumerator GetEnumerator()
        {
            foreach (IEntity data in m_Collection)
            {
                yield return data;
            }
        }

        public IEntity RemoveMaxValue()
        {
            IEntity res = m_Collection.First.Value;
            m_Collection.RemoveFirst();
            return res;
        }
    }
}
