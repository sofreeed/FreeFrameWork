Shader "URPSample/BaseSample/Variant"
{
    // The _BaseMap variable is visible in the Material's Inspector, as a field
    // called Base Map.
    Properties
    {
        [MainTexture] _BaseMap("Base Map", 2D) = "white"

        //Toggle定义选中框，绑定有两种方式：写变体名字和不写变体名字
        //如果写变体名，那么需要定义相同的变体名字；如果不写，那么需要定义变体名字为：变量名(转大写)+"_ON"
        //都用Float定义，选中和不选中分别为1和0
        [Toggle] _Invert ("Invert color?", Float) = 0
        [Toggle(ENABLE_FANCY)] _Fancy ("Fancy?", Float) = 0


        //Enum定义下拉列表，可以使用内置变量，也可以自定义（第5个）
        //一般用于内置设置项，如果是自己定义，最多不超过7个值
        //Enum没有和变体相关，使用的是【属性名】
        [Enum(UnityEngine.Rendering.BlendMode)] _SrcBlend ("Src Blend Mode", Float) = 1
        [Enum(UnityEngine.Rendering.BlendMode)] _DstBlend ("Dst Blend Mode", Float) = 1
        [Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("ZTest", Float) = 0
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull Mode", Float) = 1
        [Enum(Off, 0, On, 1)] _ZWrite ("ZWrite", Float) = 0


        //KeywordEnum是和变体相关的下拉列表，需要定义变体名字：变量名(转大写)+枚举名
        //和"#pragma multi_compile"配置使用？为什么？
        [KeywordEnum(None1, Add1, Multiply1)] Overlay ("Overlay mode", Float) = 0

        //不好使
        //[PowerSlider(3.0)] _Shininess ("Shininess", Range (0.01, 1)) = 0.08
    }

    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Blend [_SrcBlend] [_DstBlend]
            ZWrite [_ZWrite]
            ZTest [_ZTest]
            Cull [_Cull]

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            #pragma shader_feature _INVERT_ON
            #pragma shader_feature ENABLE_FANCY

            #pragma multi_compile   _ OVERLAY_MULTIPLY1 OVERLAY_ADD1  OVERLAY_NONE1         

            #pragma multi_compile _ MY_multi_1
            #pragma multi_compile _ MY_multi_2

            //float _Shininess;

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
                //half4 color = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv);
                half4 color = half4(0, 0, 0, 1);

                //#if _INVERT_ON
                //color = half4(1, 0, 0, 1);
                //return color;
                //#endif

                //#if ENABLE_FANCY
                //color = half4(0, 1, 0, 1);
                //return color;
                //#endif

                half r = 0;
                half g = 0;
                half b = 0;

                //#if AA
                //r = 1;
                //#else
                //g = 1;
                //#endif

                //#if defined (MY_multi_1)
			    //r = 1;//输出绿色
                //#endif
                //#if defined (MY_multi_2)
			    //g = 1;//输出蓝色
                //#endif

                #if  defined(OVERLAY_NONE1)
                r = 1;
                #endif
                
                #if defined(OVERLAY_ADD1)
                g = 1;
                #endif
                
                #if defined(OVERLAY_MULTIPLY1)
                b = 1;
                #endif

                //#if _OVERLAY_NONE
                //color = half4(1, 1, 0, 1);
                //#elif _OVERLAY_ADD
                //color = half4(0, 1, 1, 1);
                //#elif _OVERLAY_MULTIPLY
                //color = half4(1, 0.5, 1, 1);
                //#endif

                color = half4(r, g, b, 1);
                return color;
            }
            ENDHLSL
        }
    }
}