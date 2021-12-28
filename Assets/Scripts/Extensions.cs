using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Dictionary<TKey,TValue> DeepCopy<TKey, TValue>(this Dictionary<TKey, TValue> dict)
    {
        var result = new Dictionary<TKey, TValue>();
        foreach (var key in dict.Keys)
            result[key] = dict[key];
        return result;
    }
}
