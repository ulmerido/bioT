using System;

namespace bioT.Interfaces
{
    public interface IEntityCollection
    {
        void Add(IEntity entity);
        IEntity RemoveMaxValue();
    }
}