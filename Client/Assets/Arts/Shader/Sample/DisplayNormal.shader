Shader "URPSample/DisplayNormal"
{
   Properties
    { }

    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

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
                float4 positionOS   : POSITION;
                // 声明包含每个顶点的法线矢量的
                // 变量。
                half3 normal        : NORMAL;
            };

            struct Varyings
            {
                float4 positionHCS  : SV_POSITION;
                half3 normal        : TEXCOORD0;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                // 使用 TransformObjectToWorldNormal 函数将法线
                // 从对象空间变换到世界空间。此函数来自 Core.hlsl 中引用的
                // SpaceTransforms.hlsl 文件。
                OUT.normal = TransformObjectToWorldNormal(IN.normal);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 color = 0;
                // IN.normal 是一个 3D 矢量。每个矢量分量都位于 -1..1
                // 范围内。要将所有矢量元素（包括负值）都显示为
                // 颜色，请将每个值压缩到 0..1 范围内。
                color.rgb = IN.normal * 0.5 + 0.5;
                return color;
            }
            ENDHLSL
        }
    }
}
