using System;
using System.Collections.Generic;

namespace Mocker.Core.Collections
{
    public static class MockerCollections
    {
        public static IList<int> RandomNumbers(int listSize, int minimum = 0, int maximum = 1000)
        {
            var list = new List<int>(listSize);
            var random = new Random();

            for(var i = 0; i < listSize; i++)
            {
                list.Add(random.Next(minimum, maximum));
            }

            return list;
        }

        public static IList<int> RandomNumbers(int listSize, int magnitude)
        {
            // 3 -> min: 100, max: 999
            // 10 ^ (magnitude-1) = min
            // 10 ^ (magnitude) - 1 = max
            var list = new List<int>(listSize);
            var random = new Random();
            var min = (int) Math.Pow(10, (magnitude - 1));
            var max = (int) Math.Pow(10, magnitude) - 1;

            for (var i = 0; i < listSize; i++)
            {
                list.Add(random.Next(min, max));
            }

            return list;
        }
    }
}
