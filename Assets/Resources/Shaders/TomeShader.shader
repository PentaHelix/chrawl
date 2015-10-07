Shader "chrawl/Tome" {
    Properties {
    	_MainTex ("UV Map", 2D) = "white" {}
    	_RuneTex1 ("Rune 1", 2D) = "white" {} 
    	_RuneTex2 ("Rune 2", 2D) = "white" {} 
    	_RuneTex3 ("Rune 3", 2D) = "white" {} 
    	_RuneTex4 ("Rune 4", 2D) = "white" {} 
    }
    SubShader {
      	Tags { "RenderType" = "Opaque" }
      	CGPROGRAM
      	#pragma surface surf Lambert
        #pragma target 3.0
     
      	struct Input {
   			  float2 uv_MainTex;      
      	};
      	sampler2D _MainTex;      
      	sampler2D _RuneTex1;
      	sampler2D _RuneTex2;
      	sampler2D _RuneTex3;
      	sampler2D _RuneTex4;
     
      	void surf (Input i, inout SurfaceOutput o) {
      		if(i.uv_MainTex.x > 0.038 && i.uv_MainTex.x < 0.138){
      			if(i.uv_MainTex.y > 0.2877 && i.uv_MainTex.y < 0.42){
      				float2 uv_Rune = float2((i.uv_MainTex.x-0.038)/0.1, (i.uv_MainTex.y-0.2877)/0.133);
      				if(tex2D(_RuneTex1, uv_Rune).a == 1){
	      				o.Albedo = tex2D(_RuneTex1, uv_Rune).rgb;
	      				return;
      				}
      			}
      		}

      		if(i.uv_MainTex.x > 0.176 && i.uv_MainTex.x < 0.276){
      			if(i.uv_MainTex.y > 0.2877 && i.uv_MainTex.y < 0.42){
      				float2 uv_Rune = float2((i.uv_MainTex.x-0.176)/0.1, (i.uv_MainTex.y-0.2877)/0.133);
      				if(tex2D(_RuneTex2, uv_Rune).a == 1){
	      				o.Albedo = tex2D(_RuneTex2, uv_Rune).rgb;
	      				return;
      				}
      			}
      		}

      		if(i.uv_MainTex.x > 0.038 && i.uv_MainTex.x < 0.138){
      			if(i.uv_MainTex.y > 0.077 && i.uv_MainTex.y < 0.21){
      				float2 uv_Rune = float2((i.uv_MainTex.x-0.038)/0.1, (i.uv_MainTex.y-0.077)/0.133);
      				if(tex2D(_RuneTex3, uv_Rune).a == 1){
	      				o.Albedo = tex2D(_RuneTex3, uv_Rune).rgb;
	      				return;
      				}
      			}
      		}

      		if(i.uv_MainTex.x > 0.176 && i.uv_MainTex.x < 0.276){
      			if(i.uv_MainTex.y > 0.077 && i.uv_MainTex.y < 0.21){
      				float2 uv_Rune = float2((i.uv_MainTex.x-0.176)/0.1, (i.uv_MainTex.y-0.077)/0.133);
      				if(tex2D(_RuneTex4, uv_Rune).a == 1){
	      				o.Albedo = tex2D(_RuneTex4, uv_Rune).rgb;
	      				return;
      				}
      			}
      		}

        	o.Albedo = tex2D(_MainTex, i.uv_MainTex).rgb;

      	}
      	ENDCG
    }
    Fallback "Diffuse"
}