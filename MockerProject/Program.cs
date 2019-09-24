using System;
using System.Collections.Generic;
using Mocker.Core;
using Mocker.Core.Collections;

namespace MockerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            var listOfNamesToPickFrom = new List<string>
            {
                "John Smith",
                "Adrien Goff",
                "Dan Reese",
                "Brittany Trevino",
                "Samuel Black",
                "James Buck",
                "Amara Brown",
                "Steven Lenn",
                "Larry Hughes"
            };


           var objs = Mocker.Core.Mocker.ForEntity<TestObject>()
                .Prop(x => x.Name).ReturnsRandom(listOfNamesToPickFrom)
                .Prop(x => x.AccountNumber).ReturnsRandom(MockerCollections.RandomNumbers(100, 8))
                .Prop(x => x.Age).ReturnsRandom(MockerCollections.RandomNumbers(25,18,36))
                .Make(10);

            foreach(var o in objs)
            {
                Console.WriteLine(o.Name);
                Console.WriteLine(o.Age);
                Console.WriteLine(o.AccountNumber);
                Console.WriteLine("----------");
            }
        }
    }

    public class TestObject
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public int AccountNumber { get; set; }
    }
}
