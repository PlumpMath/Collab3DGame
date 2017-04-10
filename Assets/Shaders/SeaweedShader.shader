Shader "Custom/SeaweedShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Speed ("Speed", Range(0,10)) = 0.5
		_Amplitude ("Amplitude", Range(0,10)) = 1.0
		_Frequency ("Frequency", Range(0,1)) = 0.3
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float _Amplitude;
			float _Speed;
			float _Frequency;
		
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
 
				float	_PhaseShift	= 1*_Time[1];
				float2	_SineWave	= _Amplitude* sin((_Frequency* o.vertex.y) + _Speed*_PhaseShift);

				o.vertex.x += _SineWave;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the textures
				fixed4 col = tex2D(_MainTex, i.uv);
				return col;
			}
			ENDCG
		}
	}
}
