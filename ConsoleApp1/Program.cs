using bioT.EntityCollections;
using bioT.Enums;
using bioT.Interfaces;
using System;

namespace bioT
{
    class Program
    {
        private class Yafe : IEntity
        {
            public int Value { get; set; }
            public Yafe(int x)
            {
                Value = x;
            }
        }

        static void Main(string[] args)
        {
            FactoryEntityCollection factory = new FactoryEntityCollection();

            foreach (eEntityCollection ec in Enum.GetValues(typeof(eEntityCollection)))
            {
                test(factory.GetEntityCollection(ec));
            }

        }

        public static void test(IEntityCollection i_Collection)
        {
            Console.WriteLine(i_Collection.ToString());

            for (int i = 0; i < 10; i++)
            {

                i_Collection.Add(new Yafe(i));
            }

            for (int i = 0; i < 10; i++)
            {
                Console.Write(i_Collection.RemoveMaxValue().Value + " ");
            }
            Console.WriteLine("");

            Console.WriteLine("_____________________");

        }
    }
}
