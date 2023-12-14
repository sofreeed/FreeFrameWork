
//一般情况下UpperCamelCase
//私有变量+_
//常量ALL_UPPER

//接口：IUpperCamelCase
//泛型：TUpperCamelCase
//类：UpperCamelCase
public class CodeStyle
{
    //静态(公有)字段：UpperCamelCase
    public static string StrTest1 = "aaa";
    //静态(私有)字段：_UpperCamelCase
    private static string _StrTest2 = "aaa";

    //静态只读(公有)字段：UpperCamelCase
    public static readonly string StrTest7 = "aaa";
    //静态只读(私有)字段：_UpperCamelCase
    private static readonly string _StrTest8 = "aaa";

    //常量(公有)字段：ALL_UPPER
    public const string STR_TEST3 = "aaa";
    //常量(私有)字段：_ALL_UPPER
    private const string _STR_TEST4 = "aaa";

    //实例(公有)字段：UpperCamelCase
    public string StrTest5 = "aaa";
    //实例(私有)字段：_lowerCamelCase
    private string _strTest6 = "aaa";

    //属性：UpperCamelCase
    public int Name { set; get; }
    private int Name1 { set; get; }
    
    //方法：UpperCamelCase
    //参数：lowerCamelCase
    public string Method(string str)
    {
        //局部变量(常量)：lowerCamelCase
        const string localStr = "a";
        var localInt = 1;
        localInt++;
        return localStr + str + localInt.ToString();
    }

    public void Dispose()
    {
        StrTest1 = "123";
        _StrTest2 = "123";
        //StrTest3 = "123";
        //StrTest4 = "123";
        StrTest5 = "123";
        _strTest6 = "123";
        var aaa = StrTest1 + _StrTest2 + STR_TEST3 + _STR_TEST4 + StrTest5 + _strTest6 + StrTest7 + _StrTest8;
    }
}