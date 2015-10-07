Shader "Custom/GoldShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Gold ("Gold", Int) = 0
		_DisplayGold ("DisplayGold", Int) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		float _Gold;
		float _DisplayGold;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;


			if((_Gold-_DisplayGold)/24-IN.uv_MainTex.y < 0){
				if(_Gold - _DisplayGold < 0){
					c.gb *= 0.2;
				}else{
					c.rb *= 0.2;
				}
			}

			o.Alpha = c.a;
			o.Albedo = c.rgb;
			clip(_Gold/24-IN.uv_MainTex.y);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
