Shader "Unlit/CPURoad"
{
    Properties
    {
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
            
            uniform float4 _Points[1000];
            float _Count;
            float _Length;
            
            float getPoint(float p0, float p1, float p2, float p3, float t)
            {
                float oneMinusT = 1 - t;
                
                return
                    oneMinusT * oneMinusT * oneMinusT * p0 +
                    3 * oneMinusT * oneMinusT * t * p1 +
                    3 * oneMinusT * t * t * p2 +
                    t * t * t * p3;
            }
            
            v2f vert (appdata v)
            {
                v2f o;

                float3 worldPos = mul(unity_ObjectToWorld, v.vertex);
                
                float position = worldPos.z + _Offset;
                
                int j;
                float4 p0 = float4(0, 0, 0, 0);
                float4 p1 = float4(0, 0, 0, 0);
                
                for (j = 0; j < _Count - 1; j++)
                {
                    if (position < p1.x)
                    {
                        break;
                    }
                
                    p0 = _Points[j];
                    p1 = _Points[j + 1];
                }
                
                float i0 = (float) j / _Count;
                float i1 = (float) (j + 1) / _Count;
                float time = (p1.x - position) / (p1.x - p0.x);
                
                float t = lerp(i1, i0, time);
                
                int i = 0;
                
                if (t > 1)
                {
                    t = 1;
                    i = _Count - 4;
                }
                else
                {
                    t = t * (_Count - 1) / 3;
                    i = (int) t;
                    t = t - i;
                    i = i * 3;
                }
                
                float height = 0;
                
                if (i < _Count - 3)
                {
                    height = getPoint(_Points[i].y, _Points[i + 1].y, _Points[i + 2].y, _Points[i + 3].y, t);
                }
                else
                {
                    height = getPoint(_Points[_Count - 4].y, _Points[_Count - 3].y, _Points[_Count - 2].y, _Points[_Count - 1].y, t);
                }
                
                v.vertex.y = height;
                
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
