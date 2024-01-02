using UnityEngine;

public static class UnityEngineVectorExtension
{
    private static void Example()
    {
        
    }
    
    public static Vector2 ToVector2(this Vector3 self)
    {
        return new Vector2(self.x, self.y);
    }

    public static Vector3 ToVector3(this Vector2 self, float z = 0)
    {
        return new Vector3(self.x, self.y, z);
    }
    
    public static Vector3 X(this Vector3 self,float x)
    {
        self.x = x;
        return self;
    }
        
    public static Vector3 Y(this Vector3 self,float y)
    {
        self.y = y;
        return self;
    }
        
    public static Vector3 Z(this Vector3 self,float z)
    {
        self.z = z;
        return self;
    }
        
        
    public static Vector2 X(this Vector2 self,float x)
    {
        self.x = x;
        return self;
    }
        
    public static Vector2 Y(this Vector2 self,float y)
    {
        self.y = y;
        return self;
    }
}