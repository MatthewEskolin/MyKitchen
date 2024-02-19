//TODO Consider moving this to a utilities assembly

using System;
using System.Collections.Generic;

namespace MyKitchen.Tests.Utilities
{
    public class Comparer<T> : Comparer, IEqualityComparer<T>
    {
        private Func<T, T, bool> comparisonFunction;

        public Comparer(Func<T, T, bool> func)
        {
            comparisonFunction = func;
        }

        public bool Equals(T x, T y)
        {
            return comparisonFunction(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }
    }

    public class Comparer
    {

        public static Comparer<U> Get<U>(Func<U, U, bool> func)
        {
            return new Comparer<U>(func);
        }
    }
}