// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unity Shaders Book/Chatper7/Single Texture" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Main Tex", 2D) = "white" {}
		_Specular("Specular", Color) = (1, 1, 1, 1)
		_Gloss ("Gloss", Range(8.0, 256)) = 20
	}
	SubShader {
		Pass {
			// declar tag for assemnly by linght;
			Tags  {"LightModel"="Forward"}
			// declar cg code area;
			CGPROGRAM
				// declar cg function with vertex and fragment;
				#pragma vertex vert
				#pragma fragment frag

				//require 'lighting..cginc'
				#include "Lighting.cginc"

				// link properties
				fixed4 _Color;
				sampler2D _MainTex;
				float4 _MainTex_ST;
				fixed4 _Specular;
				float _Gloss;

				struct a2v {
					float4 vertex : POSITION;	// get unityu vertex position;
					float3 normal :NORMAL;	// get unity model normal;
					float4 texcoord : TEXCOORD0;	// get unity model frist texture;
				};

				struct v2f {
					float4 pos : SV_POSITION;	//
					float3 worldNormal : TEXCOORD0;
					float3 worldPos : TEXCOORD1;	// 
					float2 uv : TEXCOORD2;	// texture position;
				};

				v2f vert(a2v  v) {
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.worldNormal = UnityObjectToWorldNormal(v.normal);	// convert model normal to world normal;
					//o.worldPos = UnityObjectToWorldPos(v.vertex).xyz;	// coonvert Model position to world position;
					o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
					o.uv = v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
					// or just call the built-in function;
					//o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					return o;
				}

				fixed4 frag(v2f i) :SV_Target {
					fixed3 worldNormal = normalize(i.worldNormal);
					fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
					fixed3 albdo =  tex2D(_MainTex, i.uv).rgb * _Color.rgb;
					fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albdo;
					fixed3 diffuse = _LightColor0.rgb * albdo * max(0, dot(worldNormal, worldLightDir));
					fixed3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
					fixed3 halfDir = normalize(worldLightDir + viewDir);
					fixed3 specular = _LightColor0.rgb * _Specular.rgb * pow(max(0, dot(worldNormal, halfDir)), _Gloss);
					return fixed4(ambient + diffuse + specular, 1.0);
				}


			ENDCG
		}
	}
	FallBack "Specular"
}
