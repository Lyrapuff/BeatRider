Shader "Unlit/CPURoad"
{
    Properties
    {
        _RoadChunk ("Chunk", 2D) = "white" {}
        _Color ("Color", Color) = (0,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            float _Offset;
            float _Max;
            float _Min;
            
            float _RoadChunk[1024];
            
            float invLerp(float from, float to, float value)
            {
                return (value - from) / (to - from);
            }
            
            float remap(float value, float origFrom, float origTo, float targetFrom, float targetTo)
            {
                float rel = invLerp(origFrom, origTo, value);
                return lerp(targetFrom, targetTo, rel);
            }
            
            v2f vert (appdata v)
            {
                v2f o;

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                
                int index = ceil(worldPos.z + _Offset);
                
                float height = _RoadChunk[index];
                
                v.vertex.y = remap(height, 0, 1, -600, 600);
                
                o.vertex = UnityObjectToClipPos(v.vertex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
}
