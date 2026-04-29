Shader "UI/WaterRipple"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        // 水波参数
        _RippleCenter ("Ripple Center", Vector) = (0.5,0.5,0,0) // 波纹中心（UV坐标）
        _RippleRadius ("Ripple Radius", Float) = 0.0 // 波纹半径
        _RippleStrength ("Ripple Strength", Float) = 0.1 // 波纹强度
        _RippleFalloff ("Ripple Falloff", Float) = 5.0 // 波纹衰减速度
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
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            struct appdata_t
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
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;
            float4 _RippleCenter;
            float _RippleRadius;
            float _RippleStrength;
            float _RippleFalloff;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 计算当前像素到波纹中心的距离
                float2 dir = i.uv - _RippleCenter.xy;
                float dist = length(dir);

                // 计算波纹影响范围（半径内才有波纹）
                float ripple = smoothstep(_RippleRadius, _RippleRadius - 0.1, dist);
                // 波纹衰减（离中心越远，强度越低）
                ripple *= exp(-dist * _RippleFalloff);

                // 偏移UV模拟水波扭曲
                float2 offset = dir * ripple * _RippleStrength;
                fixed4 col = tex2D(_MainTex, i.uv + offset) * i.color;

                // 波纹边缘加亮（模拟水的反光）
                col.rgb += ripple * 0.2;

                return col;
            }
            ENDCG
        }
    }
}