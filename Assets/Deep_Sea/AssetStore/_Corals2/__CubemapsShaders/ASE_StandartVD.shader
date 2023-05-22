// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ASE/ASE_StandartVD"
{
	Properties
	{
		_RedMask("RedMask", 2D) = "white" {}
		_RedTexture("RedTexture", 2D) = "white" {}
		_RedMultiplayer("RedMultiplayer", Float) = 1
		_RedOpacity("RedOpacity", Float) = 1
		_BlueMask("BlueMask", 2D) = "white" {}
		_BlueTexture("BlueTexture", 2D) = "white" {}
		_BlueMultiplayer("BlueMultiplayer", Float) = 1
		_BlueOpacity("BlueOpacity", Float) = 1
		_Albedo("Albedo", 2D) = "white" {}
		_CubemapColor("CubemapColor", Color) = (0,0,0,1)
		_AlbedoColor("AlbedoColor", Color) = (1,1,1,1)
		_Cubmap("Cubmap", CUBE) = "white" {}
		_CubemapBlured("CubemapBlured", CUBE) = "white" {}
		_EmissionMap("EmissionMap", 2D) = "white" {}
		_EmissionColor("EmissionColor", Color) = (0,0,0,0)
		_EmissionMultiplayer("EmissionMultiplayer", Float) = 0
		_VertexColorStrenth("VertexColorStrenth", Float) = 1
		_RedNormals("RedNormals", 2D) = "bump" {}
		_NormalMap("NormalMap", 2D) = "bump" {}
		[Toggle]_SmoothFromMapSwitch("SmoothFromMapSwitch", Float) = 1
		[Toggle]_EmissionSwitch("EmissionSwitch", Float) = 0
		[Toggle]_SmoothRough("Smooth/Rough", Float) = 0
		_SmoothnessMap("SmoothnessMap", 2D) = "white" {}
		_MetallicMap("MetallicMap", 2D) = "white" {}
		_Snoothness("Snoothness", Float) = 1
		_Metallic("Metallic", Float) = 1
		_NormalMapDepth("NormalMapDepth", Float) = 1
		_MetalicBrightnes("MetalicBrightnes", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityStandardUtils.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
			float3 worldRefl;
			INTERNAL_DATA
		};

		uniform float _NormalMapDepth;
		uniform sampler2D _NormalMap;
		uniform float4 _NormalMap_ST;
		uniform sampler2D _RedNormals;
		uniform float4 _RedNormals_ST;
		uniform float _RedOpacity;
		uniform sampler2D _RedMask;
		uniform float4 _RedMask_ST;
		uniform float _VertexColorStrenth;
		uniform sampler2D _BlueTexture;
		uniform float4 _BlueTexture_ST;
		uniform float _BlueMultiplayer;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float4 _AlbedoColor;
		uniform samplerCUBE _CubemapBlured;
		uniform samplerCUBE _Cubmap;
		uniform float _SmoothRough;
		uniform float _SmoothFromMapSwitch;
		uniform sampler2D _MetallicMap;
		uniform float4 _MetallicMap_ST;
		uniform sampler2D _SmoothnessMap;
		uniform float4 _SmoothnessMap_ST;
		uniform float _Snoothness;
		uniform float4 _CubemapColor;
		uniform float _Metallic;
		uniform float _MetalicBrightnes;
		uniform sampler2D _RedTexture;
		uniform float4 _RedTexture_ST;
		uniform float _RedMultiplayer;
		uniform sampler2D _BlueMask;
		uniform float4 _BlueMask_ST;
		uniform float _BlueOpacity;
		uniform float4 _EmissionColor;
		uniform float _EmissionMultiplayer;
		uniform float _EmissionSwitch;
		uniform sampler2D _EmissionMap;
		uniform float4 _EmissionMap_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_NormalMap = i.uv_texcoord * _NormalMap_ST.xy + _NormalMap_ST.zw;
			float3 tex2DNode13 = UnpackScaleNormal( tex2D( _NormalMap, uv_NormalMap ), _NormalMapDepth );
			float2 uv_RedNormals = i.uv_texcoord * _RedNormals_ST.xy + _RedNormals_ST.zw;
			float2 uv_RedMask = i.uv_texcoord * _RedMask_ST.xy + _RedMask_ST.zw;
			float clampResult124 = clamp( ( i.vertexColor.r + ( i.vertexColor.r * tex2D( _RedMask, uv_RedMask ).r ) ) , 0.0 , 1.0 );
			float temp_output_126_0 = ( _RedOpacity * pow( clampResult124 , _VertexColorStrenth ) );
			float3 lerpResult144 = lerp( tex2DNode13 , BlendNormals( tex2DNode13 , UnpackNormal( tex2D( _RedNormals, uv_RedNormals ) ) ) , temp_output_126_0);
			o.Normal = lerpResult144;
			float2 uv_BlueTexture = i.uv_texcoord * _BlueTexture_ST.xy + _BlueTexture_ST.zw;
			float4 temp_output_137_0 = ( tex2D( _BlueTexture, uv_BlueTexture ) * _BlueMultiplayer );
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float4 temp_output_3_0 = ( tex2D( _Albedo, uv_Albedo ) * _AlbedoColor );
			float3 newWorldReflection69 = WorldReflectionVector( i , tex2DNode13 );
			float2 uv_MetallicMap = i.uv_texcoord * _MetallicMap_ST.xy + _MetallicMap_ST.zw;
			float4 tex2DNode5 = tex2D( _MetallicMap, uv_MetallicMap );
			float2 uv_SmoothnessMap = i.uv_texcoord * _SmoothnessMap_ST.xy + _SmoothnessMap_ST.zw;
			float3 linearToGamma103 = LinearToGammaSpace( tex2D( _SmoothnessMap, uv_SmoothnessMap ).rgb );
			float temp_output_10_0 = ( lerp(lerp(tex2DNode5.a,linearToGamma103.z,_SmoothFromMapSwitch),( 1.0 - lerp(tex2DNode5.a,linearToGamma103.z,_SmoothFromMapSwitch) ),_SmoothRough) * _Snoothness );
			float4 lerpResult87 = lerp( texCUBE( _CubemapBlured, newWorldReflection69 ) , texCUBE( _Cubmap, newWorldReflection69 ) , temp_output_10_0);
			float4 temp_output_71_0 = ( lerpResult87 * _CubemapColor );
			float3 linearToGamma105 = LinearToGammaSpace( tex2DNode5.rgb );
			float lerpResult93 = lerp( linearToGamma105.x , 1.0 , _MetalicBrightnes);
			float temp_output_7_0 = ( _Metallic * lerpResult93 );
			float4 lerpResult90 = lerp( ( temp_output_71_0 * saturate( temp_output_10_0 ) ) , ( temp_output_71_0 * temp_output_3_0 ) , temp_output_7_0);
			float2 uv_RedTexture = i.uv_texcoord * _RedTexture_ST.xy + _RedTexture_ST.zw;
			float4 lerpResult114 = lerp( ( temp_output_3_0 + lerpResult90 ) , ( tex2D( _RedTexture, uv_RedTexture ) * _RedMultiplayer ) , temp_output_126_0);
			float4 blendOpSrc145 = temp_output_137_0;
			float4 blendOpDest145 = lerpResult114;
			float2 uv_BlueMask = i.uv_texcoord * _BlueMask_ST.xy + _BlueMask_ST.zw;
			float clampResult132 = clamp( ( i.vertexColor.b + ( tex2D( _BlueMask, uv_BlueMask ).r * i.vertexColor.b ) ) , 0.0 , 1.0 );
			float temp_output_134_0 = ( pow( clampResult132 , _VertexColorStrenth ) * _BlueOpacity );
			float4 lerpBlendMode145 = lerp(blendOpDest145,(( blendOpDest145 > 0.5 ) ? ( 1.0 - 2.0 * ( 1.0 - blendOpDest145 ) * ( 1.0 - blendOpSrc145 ) ) : ( 2.0 * blendOpDest145 * blendOpSrc145 ) ),temp_output_134_0);
			o.Albedo = ( saturate( lerpBlendMode145 )).rgb;
			float2 uv_EmissionMap = i.uv_texcoord * _EmissionMap_ST.xy + _EmissionMap_ST.zw;
			float4 tex2DNode33 = tex2D( _EmissionMap, uv_EmissionMap );
			o.Emission = ( _EmissionColor * ( _EmissionMultiplayer * lerp(tex2DNode33,( temp_output_3_0 * tex2DNode33.a ),_EmissionSwitch) ) ).rgb;
			o.Metallic = temp_output_7_0;
			o.Smoothness = temp_output_10_0;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float4 tSpace0 : TEXCOORD2;
				float4 tSpace1 : TEXCOORD3;
				float4 tSpace2 : TEXCOORD4;
				half4 color : COLOR0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.color = v.color;
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldRefl = -worldViewDir;
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				surfIN.vertexColor = IN.color;
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=17000
282;252.6667;1718;1100;372.3777;1314.917;1.90014;True;False
Node;AmplifyShaderEditor.SamplerNode;6;-1961.9,260.7534;Float;True;Property;_SmoothnessMap;SmoothnessMap;22;0;Create;True;0;0;False;0;None;ecd150409cb7e4346b8965f927a90a95;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LinearToGammaNode;103;-1611.315,288.9816;Float;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;5;-1775.516,-38.90791;Float;True;Property;_MetallicMap;MetallicMap;23;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;104;-1383.809,277.0616;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.ToggleSwitchNode;2;-1130.743,191.3664;Float;False;Property;_SmoothFromMapSwitch;SmoothFromMapSwitch;19;0;Create;True;0;0;False;0;1;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;66;-1536.795,-552.9112;Float;False;Property;_NormalMapDepth;NormalMapDepth;26;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;12;-750.9462,311.9731;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;435.9743,346.0193;Float;True;Property;_NormalMap;NormalMap;18;0;Create;True;0;0;False;0;None;7c405fbbd0105034595f03fdc5e7c836;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ToggleSwitchNode;11;-515.8754,163.1064;Float;False;Property;_SmoothRough;Smooth/Rough;21;0;Create;True;0;0;False;0;0;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldReflectionVector;69;-897.6703,-872.7013;Float;False;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;9;-461.2012,333.9779;Float;False;Property;_Snoothness;Snoothness;24;0;Create;True;0;0;False;0;1;0.66;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;68;-413.7061,-1445.651;Float;True;Property;_Cubmap;Cubmap;11;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Cube;6;0;SAMPLER2D;0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;1;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-196.8749,160.8066;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;107;874.6304,-1462.761;Float;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;86;-428.6443,-1101.225;Float;True;Property;_CubemapBlured;CubemapBlured;12;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Cube;6;0;SAMPLER2D;0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;1;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;115;820.2333,-1758.987;Float;True;Property;_RedMask;RedMask;0;0;Create;True;0;0;False;0;None;804fea49e5f02b7439007c59ad23c6f2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LinearToGammaNode;105;-1487.305,24.36759;Float;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;4;-469.1862,-579.3098;Float;False;Property;_AlbedoColor;AlbedoColor;10;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;125;1545.633,-1154.487;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-1387.871,-249.1329;Float;False;Property;_MetalicBrightnes;MetalicBrightnes;27;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;70;-80.62634,-1197.825;Float;False;Property;_CubemapColor;CubemapColor;9;0;Create;True;0;0;False;0;0,0,0,1;0,0,0,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.BreakToComponentsNode;106;-1207.845,1.661224;Float;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;97;-1389.108,-135.8561;Float;False;Constant;_Float0;Float 0;21;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;129;1216.324,-691.377;Float;True;Property;_BlueMask;BlueMask;4;0;Create;True;0;0;False;0;None;804fea49e5f02b7439007c59ad23c6f2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;87;-32.05244,-1379.678;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;1;-483.455,-819.0295;Float;True;Property;_Albedo;Albedo;8;0;Create;True;0;0;False;0;None;7e1eced1a9e53274d8d875859ba39bcc;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;130;1560.223,-459.6972;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-644.9746,-14.00033;Float;False;Property;_Metallic;Metallic;25;0;Create;True;0;0;False;0;1;0.27;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;93;-879.5681,-29.05231;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;118;1787.433,-1242.886;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;89;-4.434421,10.70831;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;193.4309,-1369.369;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-14.28024,-832.3592;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;202.1891,-764.209;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;33;-483.3045,-288.8951;Float;True;Property;_EmissionMap;EmissionMap;13;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;131;1833.532,-461.507;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;1635.33,-876.5028;Float;False;Property;_VertexColorStrenth;VertexColorStrenth;16;0;Create;True;0;0;False;0;1;7.19;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;124;1988.933,-1216.887;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-293.0748,8.706559;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;380.866,-975.7206;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;117;1990.233,-964.6873;Float;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;90;555.6287,-822.0814;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;141;971.9734,-837.9867;Float;False;Property;_RedMultiplayer;RedMultiplayer;2;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-4.894272,-515.6068;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;132;1998.242,-477.7973;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;113;876.5543,-1220.136;Float;True;Property;_RedTexture;RedTexture;1;0;Create;True;0;0;False;0;None;53606c36d7f4f9947b2742118eee6a77;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;127;1661.053,-703.0063;Float;False;Property;_RedOpacity;RedOpacity;3;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ToggleSwitchNode;32;319.2034,-471.3975;Float;False;Property;_EmissionSwitch;EmissionSwitch;20;0;Create;True;0;0;False;0;0;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;142;514.6033,613.8568;Float;True;Property;_RedNormals;RedNormals;17;0;Create;True;0;0;False;0;None;None;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;38;278.4175,-630.2753;Float;False;Property;_EmissionMultiplayer;EmissionMultiplayer;15;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;140;1167.453,-897.7175;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;74;891.655,-985.1011;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.PowerNode;133;2251.643,-492.2761;Float;True;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;128;944.8245,-485.0371;Float;True;Property;_BlueTexture;BlueTexture;5;0;Create;True;0;0;False;0;None;00f3826d9439a484f8b9eb0c1c73c741;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;138;1009.983,-275.0761;Float;False;Property;_BlueMultiplayer;BlueMultiplayer;6;0;Create;True;0;0;False;0;1;2.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;135;2029.013,-162.8565;Float;False;Property;_BlueOpacity;BlueOpacity;7;0;Create;True;0;0;False;0;1;0.35;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;126;1922.633,-672.1864;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;709.4679,-581.6841;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;134;2343.952,-224.3965;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;36;476.2137,-1228.58;Float;False;Property;_EmissionColor;EmissionColor;14;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;114;1378.845,-971.3478;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendNormalsNode;143;822.6033,514.8568;Float;False;0;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;137;1350.549,-251.7266;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.BlendOpsNode;145;1670.273,-60.82452;Float;False;Overlay;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;867.0258,-713.5732;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;136;1717.693,-258.7864;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;144;1199.038,496.7181;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2110.239,80.47925;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;ASE/ASE_StandartVD;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;0;4;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;1;False;-1;1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;103;0;6;0
WireConnection;104;0;103;0
WireConnection;2;0;5;4
WireConnection;2;1;104;2
WireConnection;12;0;2;0
WireConnection;13;5;66;0
WireConnection;11;0;2;0
WireConnection;11;1;12;0
WireConnection;69;0;13;0
WireConnection;68;1;69;0
WireConnection;10;0;11;0
WireConnection;10;1;9;0
WireConnection;86;1;69;0
WireConnection;105;0;5;0
WireConnection;125;0;107;1
WireConnection;125;1;115;1
WireConnection;106;0;105;0
WireConnection;87;0;86;0
WireConnection;87;1;68;0
WireConnection;87;2;10;0
WireConnection;130;0;129;1
WireConnection;130;1;107;3
WireConnection;93;0;106;0
WireConnection;93;1;97;0
WireConnection;93;2;95;0
WireConnection;118;0;107;1
WireConnection;118;1;125;0
WireConnection;89;0;10;0
WireConnection;71;0;87;0
WireConnection;71;1;70;0
WireConnection;3;0;1;0
WireConnection;3;1;4;0
WireConnection;75;0;71;0
WireConnection;75;1;89;0
WireConnection;131;0;107;3
WireConnection;131;1;130;0
WireConnection;124;0;118;0
WireConnection;7;0;8;0
WireConnection;7;1;93;0
WireConnection;92;0;71;0
WireConnection;92;1;3;0
WireConnection;117;0;124;0
WireConnection;117;1;112;0
WireConnection;90;0;75;0
WireConnection;90;1;92;0
WireConnection;90;2;7;0
WireConnection;34;0;3;0
WireConnection;34;1;33;4
WireConnection;132;0;131;0
WireConnection;32;0;33;0
WireConnection;32;1;34;0
WireConnection;140;0;113;0
WireConnection;140;1;141;0
WireConnection;74;0;3;0
WireConnection;74;1;90;0
WireConnection;133;0;132;0
WireConnection;133;1;112;0
WireConnection;126;0;127;0
WireConnection;126;1;117;0
WireConnection;39;0;38;0
WireConnection;39;1;32;0
WireConnection;134;0;133;0
WireConnection;134;1;135;0
WireConnection;114;0;74;0
WireConnection;114;1;140;0
WireConnection;114;2;126;0
WireConnection;143;0;13;0
WireConnection;143;1;142;0
WireConnection;137;0;128;0
WireConnection;137;1;138;0
WireConnection;145;0;137;0
WireConnection;145;1;114;0
WireConnection;145;2;134;0
WireConnection;37;0;36;0
WireConnection;37;1;39;0
WireConnection;136;0;114;0
WireConnection;136;1;137;0
WireConnection;136;2;134;0
WireConnection;144;0;13;0
WireConnection;144;1;143;0
WireConnection;144;2;126;0
WireConnection;0;0;145;0
WireConnection;0;1;144;0
WireConnection;0;2;37;0
WireConnection;0;3;7;0
WireConnection;0;4;10;0
ASEEND*/
//CHKSM=DE94CA9552BF9CCAFE532D3124E9F1EB6DB1A268