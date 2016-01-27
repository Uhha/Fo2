//sampler s0;
//
//
//float4 AmbientColor = float4(0, 0, 0, 1);
//float AmbientIntensity = 0.3;
//
//float4 PixelShaderFunction(float4 position : SV_Position, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
//{
//	//return float4(1, 1, 0, 1);
//	//float4 c1 = tex2D(s0, texCoord);
//	//c1.gb = c1.r;
//	//return c1;
//	return AmbientColor * AmbientIntensity;
//}
//
//technique Technique1
//{
//	pass Pass1
//	{
//		PixelShader = compile ps_4_0_level_9_1 PixelShaderFunction();
//	}
//}

float ScreenWidth;
float ScreenHeight;

float AmbientIntensity;
float4 AmbientColor;

float4 LightPosition;
float4 LightColor;
float LightIntensity;
float LightRadius;

sampler _Sampler;

float4 CalculateAmbientLight(float4 position : SV_Position, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
	float4 TextureMap = tex2D(_Sampler, texCoord);

	return TextureMap * AmbientColor * AmbientIntensity;
}

float4 CalculatePointLight(float4 position : SV_Position, float4 color : COLOR0, float2 texCoord : TEXCOORD0) : COLOR0
{
	float4 TextureMap = tex2D(_Sampler, texCoord);

	float2 PixelPosition = float2(ScreenWidth * texCoord.x, ScreenHeight * texCoord.y);

	float2 Direction = LightPosition - PixelPosition;
	float Distance = saturate(1 / length(Direction) * LightRadius);

	return TextureMap * Distance * LightColor * LightIntensity;
}

technique Technique1
{
	pass AmbientLightPass
	{
		PixelShader = compile ps_4_0 CalculateAmbientLight();
	}
	pass PointLightPass
	{
		PixelShader = compile ps_4_0 CalculatePointLight();
	}
}