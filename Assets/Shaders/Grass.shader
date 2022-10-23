Shader "Custom/Grass"
{
	Properties
	{
	    _DentTex("Dent Texture", 2D) = "black" {}
		_Displacement("Displacement", Range(0, 1.0)) = 0.3
		_GrassTex("Grass Texture", 2D) = "white" {}
		_MudTex("Mud Texture", 2D) = "white" {}
		_BumpTex("Bump Texture", 2D) = "bump"{}
		_BumpAmount("Bump Amount", Range(0,10)) = 1
	}

		SubShader
		{
			CGPROGRAM
			#pragma surface surf Lambert vertex:vert 

			struct Input
			{
				float2 uv_MudTex;
				float2 uv_GrassTex;
				float2 uv_DentTex;
				float2 uv_BumpTex;
			};

			struct appdata
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 texcoord : TEXCOORD0;
				float4 tangent : TANGENT;
			};

			sampler2D _DentTex;
			float _Displacement;

			void vert(inout appdata v)
			{
				float d = tex2Dlod(_DentTex, float4(v.texcoord.xy, 0, 0)).r * _Displacement;
				v.vertex.xyz -= v.normal * d;
			}

			sampler2D _MudTex;
			sampler2D _GrassTex;
			sampler2D _BumpTex;
			half _BumpAmount;

			void surf(Input IN, inout SurfaceOutput o)
			{
				half dentAmount = tex2D(_DentTex, float4(IN.uv_DentTex, 0, 0)).r;
				fixed4 c = lerp(tex2D(_GrassTex, IN.uv_GrassTex), tex2D(_MudTex, IN.uv_MudTex), dentAmount * 0.5);
				o.Albedo = c.rgb;
				o.Normal = UnpackNormal(tex2D(_BumpTex, IN.uv_BumpTex));
				o.Normal *= float3(_BumpAmount, _BumpAmount, 1);
			}
			ENDCG
		}
		Fallback "Diffuse"
}