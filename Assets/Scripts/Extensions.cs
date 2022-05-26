using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

public static class Extensions
{
    private static System.Random rand = new System.Random();
    public static Dictionary<TKey, TValue> DeepCopy<TKey, TValue>(this Dictionary<TKey, TValue> dict)
    {
        var result = new Dictionary<TKey, TValue>();
        foreach (var key in dict.Keys)
            result[key] = dict[key];
        return result;
    }

    public static string Format(this string s, params object[] args)
    {
        return string.Format(s, args);
    }

    public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s);

    public static Type FindTypeByName(this Assembly assembly, string name)
    {
        return assembly.GetTypes().FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.Ordinal));
    }

    public static Type FindTypeByFullName(this Assembly assembly, string name)
    {
        return assembly.GetTypes().FirstOrDefault(t => string.Equals(t.FullName, name, StringComparison.Ordinal));
    }

    public static T Chain<T>(this Nullable<T> n, params Nullable<T>[] args) where T : struct
    {
        if (n.HasValue)
            return n.Value;
        foreach (var t in args)
            if (t.HasValue)
                return t.Value;
        return default;
    }

    public static T RandomElement<T>(this IEnumerable<T> arr)
    {
        return arr.ElementAt(rand.Next(0, arr.Count()));
    }
}
