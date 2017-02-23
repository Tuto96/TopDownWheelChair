Shader "Custom/UIWheel" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex("Color (RGB) Alpha (A)", 2D) = "white"
		_FalloffFactor("Falloff Factor", Float) = 2.0
		_BaseAlpha("Base Alpha", Float) = 0.8
	}
	SubShader {
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Lambert alpha

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;


		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};

		fixed4 _Color;

		float _FalloffFactor;
		float _BaseAlpha;

		void surf (Input IN, inout SurfaceOutput o) {
			float dist = log(distance(_WorldSpaceCameraPos, IN.worldPos))*_FalloffFactor;// (_FalloffFactor / 100);
			// Albedo comes from a texture tinted by color
			half4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = _BaseAlpha -saturate(dist);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
