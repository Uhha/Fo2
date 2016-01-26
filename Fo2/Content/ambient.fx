sampler s0;


float4 AmbientColor = float4(0, 0, 0, 1);
float AmbientIntensity = 0.3;

float4 PixelShaderFunction(float4 position : SV_Position, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
	//return float4(1, 1, 0, 1);
	//float4 c1 = tex2D(s0, texCoord);
	//c1.gb = c1.r;
	//return c1;
	return AmbientColor * AmbientIntensity;
}

technique Technique1
{
	pass Pass1
	{
		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
	}
}