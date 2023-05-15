Shader "URPSample/BaseSample/Properties"
{
    Properties
    {
        //定义Numbers（数值）和Sliders（区间值）
        _Num1 ("Num1", Int) = 0.5
        _Num2 ("Num2", Float) = 0.5
        _Num3 ("Num3", Range (0, 1)) = 0.5
        [PowerSlider(3)]_Num4("Num4", Range(0 , 1)) = 0.5           //滑动条不均匀
        [IntRange]_Num5("Num5", Range( 0 , 10)) = 5                 //只能是整数
        [Toggle]_Num6("Num6", Int) = 1
        [Enum(UnityEngine.Rendering.CullMode)]_Float("我是Float", Float) = 1
        
        //定义Colors（颜色）和Vectors（向量）
        //颜色表示为一个四维向量，对应颜色的RGBA通道；每个分量取值为[0,1]
        //向量必须是四维向量
        _BaseColor ("_BaseColor", Color) = (1,1,1,1)
        _Vector1 ("Vector1", Vector) = (1,1,1,1)

        //定义Textures（纹理）
        //用[MainTexture]和[MainColor]这两个标签修饰，可以用C#快捷访问，具体怎么访问查文档
        _BaseMap ("BaseMap", 2D) = "" {}
        _BaseMapCube ("BaseMapCube", Cube) = "white" {}
        _BaseMap3D ("BaseMap3D", 3D) = "gray" {}

        //对于 2D Texture（2维纹理）
        //  内置默认值	(R,G,B,A)
        //  empty string	null
        //  "white"	1, 1, 1, 1
        //  "black"	0, 0, 0, 0
        //  "gray"	0.5, 0.5, 0.5, 0.5
        //  "bump"	0.5, 0.5, 1, 0.5        法线图？？
        //  "red"	1, 0, 0, 0

        /*
        [Header(text)]：会在这个标签上方生成一个文本
        [Toggle]：这个属性只接受数字类型，并且会作为一个勾选项，勾选时为1，否则为0
        [IntRange]：把参数限制在整数范围
        [PowerSlider(Value)]：power即幂，也就是指数级的缩放滑动条的疏密程度，靠近Range的左值会拥有更精确的调整范围，Value接受指数等级控制，值为1时相当于普通Range
        [Enum(List)]：枚举类型，把参数限制下拉列表的可选项中
        [HideInInspector]：不在Inspector面板显示这个属性
        [NoScaleOffset]：不显示纹理的缩放/偏移编辑器
        [Normal]：提示这是一个法线纹理
        */
    }

    SubShader
    {

        Tags
        {
            "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Tags
            {
                "LightMode"="UniversalForward"
            }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
            float4 _BaseMap_ST;
            half4 _BaseColor;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _BaseMap);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv);
                half4 finalColor = color * _BaseColor;
                return finalColor;
            }
            ENDHLSL
        }
    }
}