#region

using System;
using System.Linq;

#endregion

namespace Dolphin.Object
{
    public static class ObjectComparer
    {
        public static bool Compare(object obj, object another,
            Func<IComparisonOptions, IComparisonOptions> options)
        {
            var propNames = obj.GetType().GetProperties().Select(x => x.Name);
            var opts = options(new ComparisonOptions(propNames));

            if (ReferenceEquals(obj, another)) return true;
            if (obj == null || another == null) return false;
            if (obj.GetType() != another.GetType()) return false;

            if (!obj.GetType().IsClass) return obj.Equals(another);

            var result = true;
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                //Recursion
                if (!objValue.DeepCompare(anotherValue)) result = false;
            }

            return result;
        }

        public static void Compare<T>(object obj, object toCompare,
            Func<IComparisonOptions, IComparisonOptions> options)
        {
            var propNames = typeof(T).GetProperties().Select(x => x.Name);
            var opts = options(new ComparisonOptions(propNames));
        }

        public static bool DeepCompare(this object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if (obj == null || another == null) return false;
            //Compare two object's class, return false if they are difference
            if (obj.GetType() != another.GetType()) return false;

            var result = true;
            //Get all properties of obj
            //And compare each other
            foreach (var property in obj.GetType().GetProperties())
            {
                var objValue = property.GetValue(obj);
                var anotherValue = property.GetValue(another);
                if (!objValue.Equals(anotherValue)) result = false;
            }

            return result;
        }
    }
}