// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "PannerShader"
{
	Properties
	{
		[HideInInspector] __dirty( "", Int ) = 1
		_Diffuse("Diffuse", 2D) = "white" {}
		_Speed("Speed", Range( -2 , 2)) = 1
		_Normal("Normal", 2D) = "bump" {}
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float2 uv_Normal;
			float2 texcoord_0;
		};

		uniform sampler2D _Normal;
		uniform sampler2D _Diffuse;
		uniform float _Speed;

		void vertexDataFunc( inout appdata_full vertexData, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			o.texcoord_0.xy = vertexData.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
		}

		void surf( Input input , inout SurfaceOutputStandard output )
		{
			output.Normal = UnpackNormal( tex2D( _Normal,input.uv_Normal) );
			output.Albedo = tex2D( _Diffuse,(abs( input.texcoord_0+( _Time.y * _Speed ) * float2(1,1 )))).xyz;
			output.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=3001
62;105;1565;1017;1752.047;308.5098;1.179999;True;True
Node;AmplifyShaderEditor.SamplerNode;1;-436.95,0.5900208;Float;Property;_Diffuse;Diffuse;0;None;True;0;False;white;Auto;False;Object;-1;Auto;SAMPLER2D;;FLOAT2;0,0;FLOAT;1.0;FLOAT2;0,0;FLOAT2;0,0;FLOAT;1.0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;-74.33987,3.539996;Float;True;2;Float;ASEMaterialInspector;Standard;PannerShader;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;0;False;0;0;Opaque;0.5;True;True;0;False;Opaque;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;False;0;255;255;0;0;0;0;False;0;4;10;25;True;FLOAT3;0,0,0;FLOAT3;0,0,0;FLOAT3;0,0,0;FLOAT;0.0;FLOAT;0.0;FLOAT;0.0;FLOAT3;0,0,0;FLOAT3;0,0,0;FLOAT;0.0;OBJECT;0.0;OBJECT;0.0;OBJECT;0.0;OBJECT;0.0;FLOAT3;0,0,0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1203.271,-74.71993;Float;0;-1;FLOAT2;1,1;FLOAT2;0,0
Node;AmplifyShaderEditor.PannerNode;3;-779.1689,-74.01006;Float;1;1;FLOAT2;0,0;FLOAT;0.0
Node;AmplifyShaderEditor.TimeNode;7;-1116.029,164.4501;Float
Node;AmplifyShaderEditor.RangedFloatNode;5;-1186.171,353.0996;Float;Property;_Speed;Speed;1;1;-2;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-943.1183,205.4198;Float;FLOAT;0.0;FLOAT;0.0
Node;AmplifyShaderEditor.SamplerNode;16;-460.788,197.7097;Float;Property;_Normal;Normal;3;None;True;0;False;bump;Auto;True;Object;-1;Auto;SAMPLER2D;;FLOAT2;0,0;FLOAT;1.0;FLOAT2;0,0;FLOAT2;0,0;FLOAT;1.0
Node;AmplifyShaderEditor.RotatorNode;14;-1003.227,-244.7899;Float;FLOAT2;0,0;FLOAT2;0.5,0.5;FLOAT;0.0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1181.288,-236.5299;Float;Property;_Float0;Float 0;3;45;0;0
WireConnection;1;1;3;0
WireConnection;0;0;1;0
WireConnection;0;1;16;0
WireConnection;3;0;4;0
WireConnection;3;1;8;0
WireConnection;8;0;7;2
WireConnection;8;1;5;0
WireConnection;14;0;4;0
WireConnection;14;2;15;0
ASEEND*/
//CHKSM=0131799BFD56344073A9FCAFDCDFC3E00B822082