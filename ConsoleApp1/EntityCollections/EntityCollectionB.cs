using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bioT.Enums;
using bioT.Interfaces;
namespace bioT.EntityCollections
{

    //  Add O(log(n))
    //  RemoveMax O(log(n))

    public class EntityCollectionB : IEntityCollection, IEnumerable
    {
        protected readonly PriorityQueue<IEntity> m_Collection;

        public EntityCollectionB()
        {
            m_Collection = new PriorityQueue<IEntity>((a, b) => { return a.Value.CompareTo(b.Value); });
        }

        public void Add(IEntity entity)
        {
            m_Collection.Add(entity);
        }

        public IEnumerator GetEnumerator()
        {
            foreach(IEntity data in m_Collection)
            {
                yield return data;
            }
        }

        public IEntity RemoveMaxValue()
        {
            return m_Collection.Pop();
        }
    }
}
