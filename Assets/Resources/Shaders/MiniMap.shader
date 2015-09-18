Shader "Custom/MiniMap" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_X ("Player X", Int) = 0
		_Y ("Player Y", Int) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque"}
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows

		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		fixed4 _Color;
		float _X;
		float _Y;

		void surf (Input i, inout SurfaceOutputStandard o) {
			half2 uv = i.uv_MainTex;
			if(length(half2((uv.x - 0.5), uv.y - 0.5)) > 0.45){
				if(length(half2((uv.x - 0.5), uv.y - 0.5)) < 0.5){
					o.Albedo = half4(1,1,1,1);
					return;
				}
				clip(-1);
			}

			
			if(uv.x > 0.475 && uv.x < 0.5){
				if(uv.y > 0.475 && uv.y < 0.52){
					o.Albedo = half4(0.16, .21, .38, 1);
					return;
				}
			}

			half2 offset = half2(0.5f,0.45f);//  half2(_X/40, _Y/30);
			fixed4 c = tex2D(_MainTex, (uv - offset)*0.88*half2(1.8*0.6,0.8) + offset + half2((_X-20)/40, (_Y-15)/30));
			clip(c.a - .9);
			o.Albedo = c.rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
