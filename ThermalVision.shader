Shader "Hidden/ThermalVision"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
        
		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		float3 _MinColor;
		float3 _MidColor;
		float3 _MaxColor;

        float Luminance(float3 color)
        {
            return dot(color, float3(0.299f, 0.587f, 0.114f));
        }

        float4 Frag(VaryingsDefault i) : SV_Target
        {
			float3 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, float2(i.texcoord.x, i.texcoord.y)).rgb;
			float lum = Luminance(color);
			float ix = step(0.5,lum);
			float3 range1 = lerp(_MinColor, _MidColor, (lum - ix*0.5) / 0.5);
			float3 range2 = lerp(_MidColor, _MaxColor, (lum - ix*0.5) / 0.5);
			color = lerp(range1, range2, ix);
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