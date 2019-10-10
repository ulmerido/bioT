using System;
using System.Collections;
using System.Collections.Generic;
using bioT.Interfaces;
namespace bioT.EntityCollections
{   
    //  Add O(1)
    //  RemoveMax O(n)

    public class EntityCollectionA : IEntityCollection, IEnumerable
    {

        protected LinkedList<IEntity> m_Collection = new LinkedList<IEntity>();

        public void Add(IEntity entity)
        {
            m_Collection.AddLast(entity);
        }

        public IEnumerator GetEnumerator()
        {
           foreach( IEntity data in m_Collection)
            {
                yield return data;
            }
        }

        // Itrate on collection to find the max value, removes it and returns the removed data 
        public IEntity RemoveMaxValue()
        {
            int maxVal = Int32.MinValue;
            IEntity toRemove = null;

            foreach(IEntity entity in m_Collection)
            {
                if (entity.Value >= maxVal)
                {
                    maxVal = entity.Value;
                    toRemove = entity;
                }
            }

            m_Collection.Remove(toRemove);
            return toRemove;
        }
    }
}
