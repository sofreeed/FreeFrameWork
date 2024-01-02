using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using UnityEngine;

public static class SystemReflectionExtension
{
    class A
    {

        public int Number;
        private bool Complete => Number > 100;
        public void SayComplete()
        {
            Debug.Log(Complete);
        }
        
        private void Say()
        {
            Debug.Log("I'm A!");
        }

        private int Add(int a, int b)
        {
            return a + b;
        }
    }

    public static void Example()
    {
        new A().ReflectionCallPrivateMethod("Say");
        string res = new A().ReflectionCallPrivateMethod("Add",1,2).ToString();
        object obj = new A().ReflectionCallPrivateMethod<object>("Add",1,2);
        
        var aType = typeof(A);
        Debug.Log(aType.HasAttribute(typeof(DisplayNameAttribute)));
        Debug.Log(aType.HasAttribute<DisplayNameAttribute>());
        
        Debug.Log(aType.GetAttribute(typeof(DisplayNameAttribute)));
        Debug.Log(aType.GetAttribute<DisplayNameAttribute>());
    }

    public static object ReflectionCallPrivateMethod<T>(this T self, string methodName, params object[] args)
    {
        var type = typeof(T);
        var methodInfo = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);

        return methodInfo?.Invoke(self, args);
    }
    
    public static TReturnType ReflectionCallPrivateMethod<T, TReturnType>(this T self, string methodName,
        params object[] args)
    {
        return (TReturnType)self.ReflectionCallPrivateMethod(methodName, args);
    }
    
    /// <summary>
    /// 同时 也支持 MethodInfo、PropertyInfo、FieldInfo
    /// </summary>
    public static bool HasAttribute<T>(this Type type, bool inherit = false) where T : Attribute
    {
        return type.GetCustomAttributes(typeof(T), inherit).Any();
    }
    
    public static bool HasAttribute(this Type type, Type attributeType, bool inherit = false)
    {
        return type.GetCustomAttributes(attributeType, inherit).Any();
    }
    
    public static bool HasAttribute<T>(this PropertyInfo prop, bool inherit = false) where T : Attribute
    {
        return prop.GetCustomAttributes(typeof(T), inherit).Any();
    }
    
    public static bool HasAttribute(this PropertyInfo prop, Type attributeType, bool inherit = false)
    {
        return prop.GetCustomAttributes(attributeType, inherit).Any();
    }
    
    public static bool HasAttribute<T>(this FieldInfo field, bool inherit = false) where T : Attribute
    {
        return field.GetCustomAttributes(typeof(T), inherit).Any();
    }
    
    public static bool HasAttribute(this FieldInfo field, Type attributeType, bool inherit)
    {
        return field.GetCustomAttributes(attributeType, inherit).Any();
    }
    
    public static bool HasAttribute<T>(this MethodInfo method, bool inherit = false) where T : Attribute
    {
        return method.GetCustomAttributes(typeof(T), inherit).Any();
    }
    
    public static bool HasAttribute(this MethodInfo method, Type attributeType, bool inherit = false)
    {
        return method.GetCustomAttributes(attributeType, inherit).Any();
    }
    
    /// <summary>
    /// 同时 也支持 MethodInfo、PropertyInfo、FieldInfo
    /// </summary>
    public static T GetAttribute<T>(this Type type, bool inherit = false) where T : Attribute
    {
        return type.GetCustomAttributes<T>(inherit).FirstOrDefault();
    }
    
    public static object GetAttribute(this Type type, Type attributeType, bool inherit = false)
    {
        return type.GetCustomAttributes(attributeType, inherit).FirstOrDefault();
    }
    
    public static T GetAttribute<T>(this MethodInfo method, bool inherit = false) where T : Attribute
    {
        return method.GetCustomAttributes<T>(inherit).FirstOrDefault();
    }
    
    public static object GetAttribute(this MethodInfo method, Type attributeType, bool inherit = false)
    {
        return method.GetCustomAttributes(attributeType, inherit).FirstOrDefault();
    }
    
    public static T GetAttribute<T>(this FieldInfo field, bool inherit = false) where T : Attribute
    {
        return field.GetCustomAttributes<T>(inherit).FirstOrDefault();
    }
    
    public static object GetAttribute(this FieldInfo field, Type attributeType, bool inherit = false)
    {
        return field.GetCustomAttributes(attributeType, inherit).FirstOrDefault();
    }
    
    public static T GetAttribute<T>(this PropertyInfo prop, bool inherit = false) where T : Attribute
    {
        return prop.GetCustomAttributes<T>(inherit).FirstOrDefault();
    }
    
    public static object GetAttribute(this PropertyInfo prop, Type attributeType, bool inherit = false)
    {
        return prop.GetCustomAttributes(attributeType, inherit).FirstOrDefault();
    }
}