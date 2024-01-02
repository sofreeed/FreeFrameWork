using System.Collections.Generic;
using UnityEngine;

public static class UnityEngineOthersExtension
{
    public static T Choose<T>(this T[] args)
    {
        return args[UnityEngine.Random.Range(0, args.Length)];
    }
    
    public static T GetRandomItem<T>(this List<T> list)
    {
        return list[UnityEngine.Random.Range(0, list.Count)];
    }
    
    public static T GetAndRemoveRandomItem<T>(this List<T> list)
    {
        var randomIndex = UnityEngine.Random.Range(0, list.Count);
        var randomItem = list[randomIndex];
        list.RemoveAt(randomIndex);
        return randomItem;
    }
    
    public static SpriteRenderer Alpha(this SpriteRenderer self, float alpha)
    {
        var color = self.color;
        color.a = alpha;
        self.color = color;
        return self;
    }
    
    public static Color HtmlStringToColor(this string htmlString)
    {
        var parseSucceed = ColorUtility.TryParseHtmlString(htmlString, out var retColor);
        return parseSucceed ? retColor : Color.black;
    }
}
