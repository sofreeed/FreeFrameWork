using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public static class CollectionsExtension
{
    private static void Example()
    {
        IEnumerable<int> testIEnumerable = new List<int> { 1, 2, 3 };
        testIEnumerable.IsNullOrEmpty();
        testIEnumerable.IsNotNullAndEmpty();
        
        //遍历集合
        testIEnumerable.ForEach(number => Debug.Log(number));

        //遍历字典
        new Dictionary<string, string>()
            {
                { "name", "liangxie" },
                { "company", "liangxiegame" }
            }
            .ForEach(keyValue => Debug.LogFormat("key:{0},value:{1}", keyValue.Key, keyValue.Value));

        //合并字典，不支持重复key
        var dictionary1 = new Dictionary<string, string> { { "1", "2" } };
        var dictionary2 = new Dictionary<string, string> { { "3", "4" } };
        var dictionary3 = dictionary1.Merge(dictionary2);
        dictionary3.ForEach(pair => Debug.LogFormat("{0}:{1}", pair.Key, pair.Value));
        // 1:2
        // 3:4
        
        //添加字典，支持重复key，override表示是否覆盖值
        var dictionary11 = new Dictionary<string, string> { { "1", "2" } };
        var dictionary22 = new Dictionary<string, string> { { "1", "4" } };
        dictionary11.AddRange(dictionary22,true); // true means override
        dictionary11.ForEach(pair => Debug.LogFormat("{0}:{1}", pair.Key, pair.Value));
        // 1:2
        // 3:4
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> self, Action<T> action)
    {
        foreach (var item in self)
        {
            action(item);
        }

        return self;
    }

    public static List<T> ForEachReverse<T>(this List<T> selfList, Action<T> action)
    {
        for (var i = selfList.Count - 1; i >= 0; --i)
            action(selfList[i]);

        return selfList;
    }

    public static void ForEach<T>(this List<T> list, Action<int, T> action)
    {
        for (var i = 0; i < list.Count; i++)
        {
            action(i, list[i]);
        }
    }

    public static void ForEach<K, V>(this Dictionary<K, V> dict, Action<K, V> action)
    {
        var dictE = dict.GetEnumerator();

        while (dictE.MoveNext())
        {
            var current = dictE.Current;
            action(current.Key, current.Value);
        }

        dictE.Dispose();
    }
    
    /// <summary>
    /// 合并字典，注意：不支持重复的 key
    /// </summary>
    public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> dictionary,
        params Dictionary<TKey, TValue>[] dictionaries)
    {
        return dictionaries.Aggregate(dictionary,
            (current, dict) => current.Union(dict).ToDictionary(kv => kv.Key, kv => kv.Value));
    }
    
    public static void AddRange<K, V>(this Dictionary<K, V> dict, Dictionary<K, V> addInDict,
        bool isOverride = false)
    {
        var enumerator = addInDict.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            if (dict.ContainsKey(current.Key))
            {
                if (isOverride)
                    dict[current.Key] = current.Value;
                continue;
            }

            dict.Add(current.Key, current.Value);
        }

        enumerator.Dispose();
    }

    
    public static bool IsNullOrEmpty<T>(this T[] collection) => collection == null || collection.Length == 0;
    public static bool IsNullOrEmpty<T>(this IList<T> collection) => collection == null || collection.Count == 0;
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || !collection.Any();
    public static bool IsNotNullAndEmpty<T>(this T[] collection) => !IsNullOrEmpty(collection);
    public static bool IsNotNullAndEmpty<T>(this IList<T> collection) => !IsNullOrEmpty(collection);
    public static bool IsNotNullAndEmpty<T>(this IEnumerable<T> collection) => !IsNullOrEmpty(collection);
}