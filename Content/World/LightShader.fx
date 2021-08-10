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

sampler2D TextureSampler = sampler_state {
	Texture = (ModelTex);
	MinFilter = Linear;
	MagFilter = Linear;
	AddressU = Clamp;
	AddressV = Clamp;
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
	/*	//Texture zeichnen

	float4 VertexTexCol = tex2D(TextureSampler, input.TexCoords);

	//Diffuse

	float4 normal = input.Normal;
	float4 DiffuseCol = saturate(dot(normal, DiffuseLightDirection));



	float4 Diffuse = DiffuseCol * DiffuseIntensity * DiffuseColor;

	//Specular

	float3 light = normalize(SpecularLightDirection);
	normal = normalize(input.Normal);
	float3 v = normalize(mul(normalize(ViewVector), World));  // normalize(ViewVector);
	float Diff = saturate(dot(light, normal));

	float3 r = normalize(2 * Diff * normal - light);
	float dotProduct = abs(dot(r, v));

	float4 Specular = SpecularIntensity * SpecularColor * max(1, pow(dotProduct, Shiny));
*/
//* length(DiffuseCol) wenn was schief läuft, das am Ende von 99 einsetzen xD

/*Fuer Einstellungen vom ersten Screenshot ersetze die Zeilen 99 & 107 mit:
	float4 SpecularIntensity * SpecularColor * max(pow(dotProduct, Shiny), 3.5f);
	float4 res = VertexTexCol * Diffuse + Specular;
*/



//Ergebnisse
//WICHTIG es gibt zwei Möglichkeiten Hell/Dunkel -> Einfach auskommentieren 

//float4 resDunkel = VertexTexCol * Diffuse + Specular;

	float4 light = float4(0, 1, 0, 0);
	
	float4 normal = input.Normal;

	float ang = dot(light, normal);

	return saturate(float4(ang*Color.r, ang * Color.g, ang * Color.b, Color.a));


	//float4 resHell = VertexTexCol + Diffuse * Diff + Specular;

				//laut der Formel das "richtige" ist aber super hell und wird nicht dunkel :/

	//return saturate(float4(resHell.r, resHell.g, resHell.b, 1.0f));

}

technique AmbientDiffSpec
{
	pass P0
	{
		VertexShader = compile VS_SHADERMODEL VertexShaderFunction();
		PixelShader = compile  PS_SHADERMODEL PixelShaderFunction();
	}
};