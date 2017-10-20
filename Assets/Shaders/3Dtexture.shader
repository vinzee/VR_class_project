
Shader "DX11/Sample 3D Texture"
{
	Properties
	{
		_Volume("Texture", 3D) = "" {}
	}

	SubShader
	{
		Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma exclude_renderers flash gles

			#include "UnityCG.cginc"

			struct vs_input
			{
				float4 vertex : POSITION;
			};

			struct fs_input
			{
				float4 pos: SV_POSITION;
				float3 uv : TEXCOORD0;
			};

			fs_input vert(vs_input v)
			{
				fs_input o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.vertex.xyz * 0.5 + 0.5;
				return o;
			}

			sampler3D _Volume;

			float4 frag (fs_input i) : COLOR
			{
				return tex3D(_Volume, i.uv);
			}

			ENDCG
		}
	}
}