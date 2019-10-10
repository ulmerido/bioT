using bioT.Enums;
using bioT.Interfaces;
using System;
namespace bioT.EntityCollections
{
    // a Factory to create an instance of an IEntityCollection
    public class FactoryEntityCollection
    {
        public IEntityCollection GetEntityCollection(eEntityCollection eType)
        {
            switch (eType)
            {
                case eEntityCollection.typeA:
                    return new EntityCollectionA();
                case eEntityCollection.typeB:
                    return new EntityCollectionB();
                case eEntityCollection.typeC:
                    return new EntityCollectionC();
                default:
                    throw new NotSupportedException();
            }

        }
    }
}