Shader "Custom/GridShader" {
	Properties {
		_MainTex ("Base", 2D) = "white" {}
	}

	SubShader {
		Pass{
            ZTest Always Cull Off ZWrite Off
			CGPROGRAM
	        #pragma fragment frag
	        #pragma vertex vert_img
			#pragma target 3.0
			#include "UnityCG.cginc"

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv  : TEXCOORD0;
			};

			sampler2D _MainTex;

			float4 frag(v2f i) : COLOR{
				if(i.uv.x % (1/160.0f) < 0.0008){
					return half4(1,0,0,0);
				}
				if(i.uv.y % (1/50.0f) < 0.0016){
					return half4(1,0,0,0);
				}
				return tex2D(_MainTex,i.uv);
			}
			ENDCG
		}
	}
	FallBack off
}
