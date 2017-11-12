using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwanturaLib
{
    public static class Extensions
    {
        public static T TakeRandom<T>(this IEnumerable<T> collection, Random rand)
        {
            return collection.ElementAt(rand.Next(0, collection.Count()));
        }
    }
}
