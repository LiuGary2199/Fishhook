Shader "Unlit/WaterAlphaMove_Fixed"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _WaterTex("WaterTex",2D) = "white" {}
        _NoiesTex("噪声图",2D) = "white" {}
        _Amplitude("振幅",Range(0,0.1))=0.01
        _Cycle("循环",Range(0,29))=10
        _VibrationRange("振动范围",Range(0,10))=0.2
        _AmplitudeWater("水波振幅",Range(0,0.1))=0.01
        _CycleWater("水循环",Range(0,29))=10
        _VibrationRangeWater("VibrationRangeWater",Range(0,10))=0.2
        _uvScaleX("控制水波纹的缩放X",float) = 1
        _uvScaleY("控制水波纹的缩放Y",float) = 1
        _LightAlpha("LightAlpha",float)= 1
        _LightPower("LightPower",float)= 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        LOD 100
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

            // 这是 Unity UI 官方标准结构体，任何通道下都永远正确
            struct appdata_ui
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                //float2 uv1 : TEXCOORD2;
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //float2 uv1 : TEXCOORD2;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _WaterTex;
            sampler2D _NoiesTex;
            float4 _WaterTex_ST;
            float _Cycle;
            float _Amplitude;
            float _VibrationRange;
            float _LightAlpha;
            float _LightPower;
            float _VibrationRangeWater;
            float _CycleWater;
            float _AmplitudeWater;
            float _uvScaleX;
            float _uvScaleY;

            v2f vert (appdata_ui v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //o.uv1 = TRANSFORM_TEX(v.uv1, _WaterTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 uv1 = i.uv;
                float2 uv2 = i.uv;
                uv2+=_Time.x;
                uv1.x*=_uvScaleX;
                uv1.y*=_uvScaleY;
                float dis = distance(i.uv,float2(0.5f,1));
                float f = saturate(1-dis/_VibrationRange);
                i.uv += f*_Amplitude*sin(dis*3.14*_Cycle+_Time.y);
                uv1+= f*_Amplitude*sin(dis*3.14*_Cycle+_Time.y);
                float disWater = distance(uv1,float2(0.5f,1));
                float fWater = saturate(1-disWater/_VibrationRangeWater);
                uv1 += fWater *_AmplitudeWater*sin(disWater*3.14*_CycleWater+_Time.y);
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 NoiesCol = tex2D(_NoiesTex, uv2);
                fixed4 Watercol = tex2D(_WaterTex,uv1);
                Watercol.rgb *= _LightPower;
                Watercol.rgb *= col.rgb;
                Watercol.a *= saturate(1-uv1.y+_LightAlpha)*NoiesCol.r;
                Watercol.rgb *= NoiesCol.r*Watercol.a;
                col += Watercol;
                return  col;
            }
            ENDCG
        }
    }
}