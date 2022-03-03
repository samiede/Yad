Shader "Unlit/CardUIText"
{
    Properties
        {
        _MainTex("Base",   2D) = ""{}
        _Color("Color",  Color) = (1, 1, 1, 1)
        _Cutoff("Cutoff", Float) = 0.5
        }

    CGINCLUDE

    #include "UnityCG.cginc"

    struct v2f
    {
        float4 position : SV_POSITION;
        float2 texcoord : TEXCOORD0;
        float4 color : COLOR;
    };

    sampler2D _MainTex;
    float4 _MainTex_ST;
    float4 _Color;
    float _Cutoff;

    v2f vert(appdata_full v)
    {
        v2f o;
        o.position = UnityObjectToClipPos(v.vertex);
        o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
        o.color = v.color;
        return o;
    }

    float4 frag(v2f i) : COLOR
    {
        float4 c = tex2D(_MainTex, i.texcoord);
        clip(c.a - _Cutoff);
        return _Color * i.color;
    }

    ENDCG

    SubShader
        {
        Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
        }
    FallBack "Diffuse"
}