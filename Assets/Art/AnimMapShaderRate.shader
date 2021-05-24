Shader "URP/XHH/AnimMapShader"
{
    Properties
    {
        [MainTexture]_MainTex ("Albedo", 2D) = "white" { }
        _AnimMap ("AnimMap", 2D) = "white" { }
        _AnimRate ("_AnimRate", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderPipeline" = "UniversalPipeline" "RenderType" = "Opaque" }
        LOD 100
        

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode" = "UniversalForward" }
            
            ZWrite On
            ZTest On
            Cull back
            
            HLSLPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing
            #pragma instancing_options procedural:setup


            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            // #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"

            struct Attributes
            {
                float2 uv: TEXCOORD0;
                float4 positionOS: POSITION;
                uint instanceID: SV_InstanceID;
                // UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float2 uv: TEXCOORD0;
                float4 positionCS: SV_POSITION;
                float4 positionWS: TEXCOORD2;
                float f: TEXCOORD1;
                // UNITY_VERTEX_INPUT_INSTANCE_ID
                // UNITY_VERTEX_OUTPUT_STEREO
            };

            TEXTURE2D_X(_MainTex);SAMPLER(sampler_MainTex);
            sampler2D _AnimMap;float4 _AnimMap_TexelSize;//x == 1/width
            float4x4 _LocalToWorld;

            // #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            struct AnimInfo
            {
                float4x4 trs;
                float animRate1;
                float animRate2;
                float animLerp;
            };
            StructuredBuffer<AnimInfo> _AnimInfos;
            // #endif

            void setup()
            {
                
            }
            

            Varyings vert(Attributes input, uint vid: SV_VertexID)//vid对应的就是
            {
                Varyings output;
                float3 positionOS = input.positionOS;
                float4 pos;

                // #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
                uint instanceID = input.instanceID;
                AnimInfo animInfo = _AnimInfos[instanceID];
                float animMap_y1 = animInfo.animRate1;
                float animMap_y2 = animInfo.animRate2;
                float animLerp = animInfo.animLerp;
                // positionOS = mul(float3(0, 0, 0), positionOS);//要么动画不对 要么旋转不对

                
                float animMap_x = (vid + 0.5) * _AnimMap_TexelSize.x;
                
                float4 pos1 = tex2Dlod(_AnimMap, float4(animMap_x, animMap_y1, 0, 0));
                float4 pos2 = tex2Dlod(_AnimMap, float4(animMap_x, animMap_y2, 0, 0));

                pos = lerp(pos1, pos2, animLerp);
                // positionOS = mul(animInfo.trs, float4(positionOS, 1)).xyz;

                pos = mul(animInfo.trs, pos);
                // float4 center = mul(animInfo.trs, float4(0, 0, 0, 1));
                // pos = pos + center;
                // positionOS += pos;
                // positionOS = mul(float3(0, 0, 0), positionOS);
                // positionOS = center + pos;

                // #endif
                
                float4 positionWS = mul(_LocalToWorld, pos);
                positionWS /= positionWS.w;

                
                output.uv = input.uv;
                output.positionWS = positionWS;
                // output.positionCS = TransformObjectToHClip(positionOS);
                output.positionCS = mul(UNITY_MATRIX_VP, positionWS);
                return output;
            }
            
            float4 frag(Varyings input): SV_Target
            {
                float4 col = SAMPLE_TEXTURE2D_X(_MainTex, sampler_MainTex, input.uv);
                return col;
            }
            ENDHLSL
            
        }
    }
    FallBack "Hidden/Universal Render Pipeline/FallbackError"
}
