using System;

public static class SystemObjectExtension
{
    public static bool IsNull<T>(this T selfObj) where T : class
    {
        return null == selfObj;
    }
    
    public static bool IsNotNull<T>(this T selfObj) where T : class
    {
        return null != selfObj;
    }
}