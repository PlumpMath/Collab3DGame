//SHADER NOT COMPLETE
Shader "Custom/OceanShader" {

	//Properties are defined in the "CGPROGRAM"
	Properties {
		_Color ("Color", Color) = (1,1,1,1)

		// 2D Image Texture
		_MainTex ("Albedo (RGB)", 2D) = "white" {}

		// Standard Material Parameters
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		// **START**: Shader Programming Begins Here

		CGPROGRAM

		// Sourcing the fullForwardShadows Model
		#pragma surface surf Standard fullforwardshadows

		// Sourcing the target 3.0 lighting model
		#pragma target 3.0
		
		//Main Image
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			//float2 uv2_MainTex; Would get us the second UV Map
		};

		//--------------These are from the standard material interface variables -------------
		
		float _Glossiness;
		float _Metallic;
		fixed4 _Color;

		//--------------End of Material Interface -------------

		//Surface Shader function, Input IN and Output O
		void surf (Input IN, inout SurfaceOutputStandard o) {

			//--------------Sine Wave Shader ---------------

			float _Frequency	= 3;

			// Independent Variable
			float	_IV			= IN.uv_MainTex.x;

			float	_Amplitude	= 3;
			float	_PhaseShift	= _Time[1];
			float2	_SineWave	= float2(0, _Amplitude* sin((_Frequency*_IV) + _PhaseShift));

			//NVidea Texture LookUp (Will ASK TA For details), Parameters are 
			//	1) Sampler to lookup
			//	2) Coordinates to perform the lookup
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex + _SineWave) * _Color;

			//---------------End of Sine Wave Shader ------------


			//--------------These are from the standard material interface -------------

			// Albedo comes from color texture without lighting
			// For us it is the pixel color from the texture map
			o.Albedo = c.rgb;

			// These are standard material attributes, from the slider
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;

			//----------------End of Material Interface -------------------
		}

		// **END**: Shader Programming Ends Here
		ENDCG
	}
	FallBack "Diffuse"
}
