Shader "Custom/TransformShader" {
	Properties {
		_MainTex ("Base", 2D) = "white" {}
		_Ripple ("Ripple"  , Float) = 0.0
		_Tint ("Tint Color", Color) = (1,1,1,1)
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
			float _Ripple;
			float4 _Tint;

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
				float ripple = pow(-1, (int)(i.uv.y / 0.2)) * _Ripple;

				return tex2D(_MainTex, half2(i.uv.x + sign(sin(i.uv.y*60))*_Ripple/100, i.uv.y)) * _Tint;
			}

			ENDCG
		}
	} 
	FallBack "Diffuse"
}
