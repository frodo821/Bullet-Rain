Shader "Custom/scroll" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("2D Texture", 2D) = "white" {}
	_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
		Tags{ "RenderType" = "Transparent" }
		LOD 200
		CGPROGRAM

#pragma surface surf Standard alpha:fade
#pragma enable_d3d11_debug_symbols
#pragma target 3.0
		sampler2D _MainTex;
	struct Input {
		float2 uv_MainTex;
	};
	half _Glossiness;
	half _Metallic;
	fixed4 _Color;
	void surf(Input IN, inout SurfaceOutputStandard o) {

		fixed2 scrolledUV = IN.uv_MainTex;
		scrolledUV.x += _Time * 2;
		fixed4 c = tex2D(_MainTex, scrolledUV) * _Color;
		o.Albedo = c.rgb;

		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
		o.Alpha = c.a;
	}
	ENDCG
	}
		FallBack "Diffuse"
}
