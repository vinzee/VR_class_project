	// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'


Shader "DX11/3D Texture Raymarching/Head/LowDetail"
{
	Properties
	{
		_Volume("Texture", 3D) = "" {}
		_ColorTex("Texture", 2D) = "" {}
		_NoiseTex("Texture", 2D) = "" {}
		_Radius("Radius", Float) = 2.0
		_BuckyCentre("Bucky Centre", Vector) = (0,0,0,0)
		_BoxWidth("Box Width", Float) = 5.01
		_BoxLength("Box Length", Float) = 5.01
		_BoxHeight("Box Height", Float) = 5.01
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Volume"
		}
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		Pass
		{
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			//#pragma exclude_renderers flash gles

			#include "UnityCG.cginc"

			#define STEPS 100


			#define STEP_SIZE .05


			sampler3D _Volume;
			sampler2D _ColorTex;
			sampler2D _NoiseTex;

			float _Radius;
			float4 _BuckyCentre;

			float _BoxWidth;
			float _BoxLength;
			float _BoxHeight;


			struct vertInput
			{
				float4 vertex : POSITION;
			};

			struct fragInput
			{
				float4 pos		: SV_POSITION;		// Position in screen space
				float3 worldPos : TEXCOORD1;		// position in the world
				float3 uv		: TEXCOORD0;		// texture lookup coord
			};

			// determines if a particular point is inside the box or not
			bool insideBox(float3 p)
			{
				if (p.x > _BuckyCentre.x + _BoxWidth / 2)
					return false;
				if (p.x < _BuckyCentre.x - _BoxWidth / 2)
					return false;
				if (p.y > _BuckyCentre.y + _BoxLength / 2)
					return false;
				if (p.y < _BuckyCentre.y - _BoxLength / 2)
					return false;
				if (p.z > _BuckyCentre.z + _BoxHeight / 2)
					return false;
				if (p.z < _BuckyCentre.z - _BoxHeight / 2)
					return false;

				return true;
			}


			//return lerp(fixed4(1, 0, 0, 1), fixed4(0, 1, 0, 1), step(_Radius, distance(position, _BuckyCentre)) );
			//return fixed4(frac(distance(position, _BuckyCentre.xyz)), 0,0, 1);



			fragInput vert(vertInput input)
			{
				fragInput output;
				output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
				output.worldPos = mul(unity_ObjectToWorld, input.vertex).xyz;
				output.uv = input.vertex.xyz + 0.5f;// *0.5f + 0.5f;
				return output;
			}


			fixed4 frag(fragInput inputVert) : SV_Target
			{
				fixed4 outputColor = fixed4(0,0,0,0);
				fixed4 lookupColor = fixed4(0, 0, 0, 0);
				fixed4 lookupDensity = fixed4(0, 0, 0, 0);
				float randomVal = 0.0f;

				float3 worldPosition = inputVert.worldPos;											// get the position of the vertex
				float3 rayDirection = normalize(inputVert.worldPos - _WorldSpaceCameraPos);		// calculate the ray direction using the eye and the target position


				float3 objectSpace, rayUV;

				// calculate position in texture space
				objectSpace = mul(unity_WorldToObject, worldPosition);
				rayUV = objectSpace + 0.5f;// *0.5 + 0.5f;

				// randomize starting step location
				randomVal = tex2D(_NoiseTex, inputVert.pos.xy/1024).x;
				//worldPosition += (rayDirection * STEP_SIZE * randomVal);

				//return fixed4(randomVal, randomVal, randomVal, randomVal);

				// march along the ray
				for (int i = 0; i < STEPS; i++)
				{
					// if we are outside the box...
					if (!insideBox(worldPosition))
						break;

					else
					{
						// otherwise, update the position along the viewing ray
						worldPosition += rayDirection * STEP_SIZE;

						// calculate position in texture space
						objectSpace = mul(unity_WorldToObject, worldPosition);
						rayUV = objectSpace + 0.5f;// *0.5 + 0.5f;

						lookupDensity = tex3D(_Volume, rayUV);
						lookupColor = tex2D(_ColorTex, lookupDensity.aa);

						// and increment the color with the new step
						//outputColor = outputColor + 0.2f * (1.0 - outputColor.a) * lookupColor;
						outputColor = outputColor + 0.2f * (1.0 - outputColor.a) * tex3D(_Volume, rayUV);
					}
				}

				//float3 outputLookupTest = float3(16, 16, 16);
				//outputColor = tex3D(_Volume, inputVert.uv);
				return outputColor;

				//return tex3D(_Volume, worldPosition);

				//return tex3D(_Volume, inputVert.worldPos);				// return a texture lookup

				//return fixed4(worldPosition/10, 1);
				//return tex3D(_Volume, i.uv);
			}
		ENDCG
		}
	}
}