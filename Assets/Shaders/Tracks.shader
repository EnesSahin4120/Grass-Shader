Shader "Custom/Tracks"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _CurrentTrackCoord("Current Track UV Coordinate", Vector) = (0,0,0,0)
        _Size("Size", Range(1, 20)) = 1
        _Power("Power", Range(0.1, 5)) = 1
    }
    SubShader
    {
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
       
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 _CurrentTrackCoord;
            half _Size;
            half _Power;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float draw = pow(saturate(1 - distance(i.uv, _CurrentTrackCoord.xy)),750 / _Size);
                fixed4 drawCol = draw * _Power;
                return saturate(col + drawCol);
            }
            ENDCG
        }
    }
}
