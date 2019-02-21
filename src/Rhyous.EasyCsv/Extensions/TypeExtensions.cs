using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Rhyous.EasyCsv.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// A method to get a list of primitive properties from an object to use as headers for a csv file.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>Returns a list of primitive properties to use as headers.</returns>
        public static IEnumerable<string> GetHeaders(this Type type)
        {
            if (type == null)
                return null;
            return type.GetProperties().Where(p=>p.PropertyType.IsPrimitive || p.PropertyType == typeof(string)).Select(p => p.Name);
        }

        public static T GetPropertyValue<T>(this object o, string propertyName, T defaultValue = default(T))
        {
            var propertyInfo = o.GetType().GetPropertyInfo(propertyName)?.GetValue(o);
            if (propertyInfo == null)
                return defaultValue;
            return (T)propertyInfo;
        }

        public static object GetPropertyValue(this object o, string propertyName, object defaultValue = null)
        {
            return o.GetType().GetPropertyInfo(propertyName)?.GetValue(o) ?? defaultValue;
        }

        public static PropertyInfo GetPropertyInfo(this Type t, string propertyName, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
        {
            var props = t.GetProperties();
            return props.FirstOrDefault(propInfo => propInfo.Name.Equals(propertyName, comparison));
        }
    }
}
