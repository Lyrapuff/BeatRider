Shader "Unlit/Shine"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShineAmount ("Shine amount", float) = 1
        _ShineSpeed ("Shine speed", float) = 1
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }
 
        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
                float4 worldPosition : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ShineAmount;
            float _ShineSpeed;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPosition = v.vertex;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                #ifdef UNITY_HALF_TEXEL_OFFSET
                OUT.vertex.xy += (_ScreenParams.zw-1.0)*float2(-1,1);
                #endif
                
                float shine = abs(sin(_Time[0] * _ShineSpeed)) * _ShineAmount;
                
                o.color = v.color + fixed4(shine, shine, shine, 0);
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half4 color = (tex2D(_MainTex, i.uv) + _TextureSampleAdd) * i.color;
                
                color.a *= UnityGet2DClipping(i.worldPosition.xy, _ClipRect);
                       
                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - 0.001);
                #endif
               
                return color;
            }
            ENDCG
        }
    }
}
