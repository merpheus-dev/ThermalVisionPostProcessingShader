Shader "Hidden/ThermalVision"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
		#define mod(x, y) (x - y * floor(x / y))
		#define fract(x)  x - floor(x)
		#define PIXELSIZE 3.0
#define Luminance(color) dot(color, float3(0.299f, 0.587f, 0.114f))
		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
        float _Blend;
		float3 _MinColor;
		float3 _MidColor;
		float3 _MaxColor;

		float scanline(float2 uv) {
			return sin(_ScreenParams.y * uv.y * 0.7 - _Time * 10.0);
		}

		float slowscan(float2 uv) {
			return sin(_ScreenParams.y * uv.y * 0.02 + _Time * 6.0);
		}

        float4 Frag(VaryingsDefault i) : SV_Target
        {
			float3 color;
			if (i.texcoord.x < (_Blend - 0.005))
			{
				color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(i.texcoord.x, i.texcoord.y)).rgb;

				color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(i.texcoord.x, i.texcoord.y)).rgb;
				float lum = Luminance(color.rgb);
				float ix = (lum < 0.5) ? 0.0 : 1.0;
				float3 range1 = lerp(_MinColor, _MidColor, (lum - ix * 0.5) / 0.5);
				float3 range2 = lerp(_MidColor, _MaxColor, (lum - ix * 0.5) / 0.5);

				color = lerp(range1, range2, ix);
			}
			else if (i.texcoord.x >= (_Blend + 0.0005))
			{
				color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(i.texcoord.x, i.texcoord.y)).rgb;
			}
            return float4(color,1.);
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}