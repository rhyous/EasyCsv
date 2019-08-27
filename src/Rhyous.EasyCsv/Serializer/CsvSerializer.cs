using Rhyous.EasyCsv.Extensions;
using Rhyous.StringLibrary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Rhyous.EasyCsv
{
    public class CsvSerializer
    {
        public string Serialize(object o, string delimn = ",")
        {
            IEnumerable<string> headers;
            if (o.GetType().IsGenericType && o is IEnumerable<object> enumerable)
            {
                string csv = string.Empty;
                var first = enumerable.FirstOrDefault();
                if (first == null)
                    return csv;
                headers = first.GetType().GetProperties().Select(p => p.Name);
                foreach (var item in enumerable)
                {
                    csv += item.ToCsvRow(headers) + Environment.NewLine;
                }
                return csv;
            }
            headers = o.GetType().GetProperties().Select(p => p.Name);
            return o.ToCsvRow(headers);
        }

        public T Deserialize<T>(string csvString)
            where T : new()
        {
            return Deserialize<T>(csvString.ToStream());
        }

        public T Deserialize<T>(Stream csvStream)
            where T : new()
        {
            var csv = new Csv(csvStream);
            var typeOfT = typeof(T);

            if (typeOfT.IsGenericType && typeof(IEnumerable<object>).IsAssignableFrom(typeOfT))
            {
                var itemType = typeOfT.GetGenericArguments().First();
                var mi = GetType().GetMethod("DeserializeList", BindingFlags.NonPublic|BindingFlags.Instance);
                var method = mi.MakeGenericMethod(itemType);
                var result = method.Invoke(this, new[] { csv });
                return (T)result;
            }

            return DeserializeSingleObject<T>(csv);
        }

        private T DeserializeSingleObject<T>(Csv csv) 
            where T : new()
        {
            var list = new List<T>();
            foreach (var csvRow in csv.Rows)
            {
                var tObj = new T();
                foreach (var header in csv.Headers)
                {
                    var propInfo = typeof(T).GetPropertyInfo(header, StringComparison.OrdinalIgnoreCase);
                    var value = csvRow[header];
                    propInfo.SetValue(tObj, value.ToType(propInfo.PropertyType));
                }
                list.Add(tObj);
            }
            // T is not a list
            return (T)list.First();
        }

        private List<T> DeserializeList<T>(Csv csv)
            where T : new()
        {
            var list = new List<T>();
            foreach (var csvRow in csv.Rows)
            {
                var tObj = new T();
                foreach (var header in csv.Headers)
                {
                    var propInfo = typeof(T).GetPropertyInfo(header, StringComparison.OrdinalIgnoreCase);
                    var value = csvRow[header];
                    propInfo.SetValue(tObj, value.ToType(propInfo.PropertyType));
                }
                list.Add(tObj);
            }
            // T is not a list
            return list;
        }

        public static object ConvertList(List<object> value, Type type)
        {
            var containedType = type.GenericTypeArguments.First();
            return value.Select(item => Convert.ChangeType(item, containedType)).ToList();
        }

    }
}
