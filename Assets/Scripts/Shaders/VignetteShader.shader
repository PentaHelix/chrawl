Shader "Custom/VignetteShader" {
	Properties {
		_MainTex ("Base", 2D) = "white" {}
		_VignettePower ("VignettePower", Range(0.0,6.0)) = 5.5
	}

	SubShader {
		Pass{
			Tags { "RenderType"="Opaque" }
			LOD 200
			
			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma fragment frag
			#pragma vertex vert
			#pragma target 3.0

			sampler2D _MainTex;
			float _Amount;
			float _R;
			float _G;
			float _B;

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv  : TEXCOORD0;
			};

			v2f vert( appdata_img v ) {
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord.xy;
				return o;
			} 

			float4 frag(v2f i) : COLOR{
				float4 renderTex = tex2D(_MainTex, i.uv);
       			float2 dist = (i.uv - 0.5f) * 1.25f;
        		dist.x = 1 - dot(dist, dist) * _Amount;
        		renderTex *= dist.x;
        		return renderTex;
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
