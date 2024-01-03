using System.Collections.Generic;
using UnityEngine;

public static class UnityEngineMathfExtension
{
    private static void Example()
    {
        
    }
    
    public static float Lerp(this float self, float a, float b)
    {
        return Mathf.Lerp(a, b, self);
    }
    
    public static float Abs(this float self)
    {
        return Mathf.Abs(self);
    }

    public static float Abs(this int self)
    {
        return Mathf.Abs(self);
    }
    
    public static float Sign(this float self)
    {
        return Mathf.Sign(self);
    }

    public static float Sign(this int self)
    {
        return Mathf.Sign(self);
    }
    
    public static float Cos(this float self)
    {
        return Mathf.Cos(self);
    }

    public static float Cos(this int self)
    {
        return Mathf.Cos(self);
    }
    
    public static float Sin(this float self)
    {
        return Mathf.Sin(self);
    }

    public static float Sin(this int self)
    {
        return Mathf.Sin(self);
    }
    
    public static float CosAngle(this float self)
    {
        return Mathf.Cos(self * Mathf.Deg2Rad);
    }

    public static float CosAngle(this int self)
    {
        return Mathf.Cos(self * Mathf.Deg2Rad);
    }
    
    public static float SinAngle(this float self)
    {
        return Mathf.Sin(self * Mathf.Deg2Rad);
    }

    public static float SinAngle(this int self)
    {
        return Mathf.Sin(self * Mathf.Deg2Rad);
    }
    
    public static float Deg2Rad(this float self)
    {
        return self * Mathf.Deg2Rad;
    }

    public static float Deg2Rad(this int self)
    {
        return self * Mathf.Deg2Rad;
    }
    
    public static float Rad2Deg(this float self)
    {
        return self * Mathf.Rad2Deg;
    }

    public static float Rad2Deg(this int self)
    {
        return self * Mathf.Rad2Deg;
    }
}