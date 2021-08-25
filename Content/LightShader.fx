#if OPENGL
#define SV_POSITION POSITION
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// Variablen

float4x4 World;
float4x4 View;
float4x4 Projection;
texture tex1, tex2, tex3;
int maxTexCount;
int currentTex;
float4 Color = float4 (0.5f, 0.5f, 0.5f, 1.0f);


/*// Texturen
texture ModelTex;

//Diffuse 

float4 DiffuseColor = float4 (0.5f, 0.5f, 0.5f, 1.0f);
float DiffuseIntensity = 1.0f;
float3 DiffuseLightDirection = (1.0f, 1.0f, 1.0f);

// Specular 

float Shiny = 64.0f;
float4 SpecularColor = float4 (1.0f, 1.0f, 1.0f, 1.0f);
float SpecularIntensity = 0.5f;

float3 SpecularLightDirection = float3 (1.0f, 0.0f, 0.0f);
float3 ViewVector;

*/

sampler Texture1Sampler = sampler_state {
	Texture = <tex1>;

};

sampler Texture2Sampler = sampler_state {
	Texture = <tex2>;

};

sampler Texture3Sampler = sampler_state {
	Texture = <tex3>;

};


struct VertexShaderInput
{
	float4 Position : POSITION0;
	float2 TexCoords : TEXCOORD0;
	float4 Normal : NORMAL0;

};

struct VertexShaderOutput
{
	float4 Position : POSITION0;
	float2 TexCoords : TEXCOORD0;
	float4 Normal : TEXCOORD1;
	float4 Color : COLOR0;

};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
	VertexShaderOutput output;

	float4 worldPosition = mul(input.Position, World);
	float4 viewPosition = mul(worldPosition, View);

	output.Position = mul(viewPosition, Projection);
	output.TexCoords = input.TexCoords;
	output.Normal = input.Normal;

	output.Color = float4 (0.5f, 0.5f, 0.5f, 1.0f);

	return output;


}



float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{

	float4 color1 = tex2D(Texture1Sampler, input.TexCoords);
	float4 color2;
	float4 color3;

	if (maxTexCount > 0)
	{
		color1 = tex2D(Texture1Sampler, input.TexCoords);
	}
	if (maxTexCount > 1) 
	{
		color2 = tex2D(Texture2Sampler, input.TexCoords);
	}
	if (maxTexCount > 2)
	{
		color3 = tex2D(Texture3Sampler, input.TexCoords);
	}




	float4 light = float4(0, 1, 1, 0);
	
	float4 normal = input.Normal;

	float ang = dot(light, normal);



	if (maxTexCount > 1 && currentTex == 1)
	{
		color1 = color2;
	}
	if (maxTexCount > 2)
	{
		color1 = color3;

	}

	return saturate(float4(ang*color1.r, ang * color1.g, ang * color1.b, color1.a));



}

technique AmbientDiffSpec
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL VertexShaderFunction();
		PixelShader = compile  PS_SHADERMODEL PixelShaderFunction();
	}
};