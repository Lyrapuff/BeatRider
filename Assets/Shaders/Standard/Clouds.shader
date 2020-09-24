Shader "Unlit/Clouds"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            #include "Assets\Shaders\Includes\Snoise.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 hitPos : TEXCOORD1;
                float3 cameraPos : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            float4 _Color;
            float _Offset;

            v2f vert (appdata v)
            {
                v2f o;
                
                o.hitPos = mul(unity_ObjectToWorld, v.vertex);
                o.cameraPos = _WorldSpaceCameraPos;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float density = snoise(float3(i.hitPos.x, 0, i.hitPos.z + _Offset * 0.6) / 100);
            
                fixed4 col = 0;
            
                if (density > 0)
                {
                    col = _Color;
                }
                else
                {
                    discard;
                }
            
                return col;
            }
            ENDCG
        }
    }
}
