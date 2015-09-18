Shader "chrawl/DissolveShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Amount ("Amount", Range(0,1)) = 1.0
		_DissolveTex ("Dissolve Texture", 2D) = "white" {}
	}
	SubShader {
		Tags{"Queue"="Transparent"}
		Pass{
         	Fog { Mode Off }
     		Blend SrcAlpha OneMinusSrcAlpha
			LOD 200
			CGPROGRAM
				#pragma fragment frag
		        #pragma vertex vert
				#pragma target 3.0
				#include "UnityCG.cginc"

				struct v2f {
					float4 pos : SV_POSITION;
					float2 uv  : TEXCOORD0;
				};
				float _Amount;
				fixed4 _Color;
				sampler2D _DissolveTex;

				v2f vert( appdata_img v ) {
					v2f o;
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					o.uv = v.texcoord.xy;
					return o;
				} 

				float4 frag(v2f i) : COLOR{
					fixed4 c = _Color;
					fixed4 t = tex2D(_DissolveTex, i.uv);
					c.a = 1-((t.r - _Amount)/t.g);
					return c;
				}
			ENDCG
		}
	}
}
