/* 
 * Pico 3D Shader File
 * =====================================================================
 * FileName: default.fx
 * Location: ./Shaders/
 * Project: Pico 3D
 * ---------------------------------------------------------------------
 * Created: 2/10/2016 3:56:34 PM
 * ---------------------------------------------------------------------
 * This document is distributed under GNU General Public License.
 * Copyright © David Kutnar 2016 - All rights reserved.
 * =====================================================================
 */

float4x4 Transform;
float4x4 CameraView;
float4x4 CameraProjection;

float AmbientIntensity = 0.25f;
float4 AmbientColor = float4( 1.0f, 1.0f, 1.0f, 1.0f );

float DiffuseIntensity = 1.0f;
float4 DiffuseColor = float4( 1.0f, 1.0f, 1.0f, 1.0f );
float3 DiffuseDirection = float3( -1.0f, -1.0f, -1.0f );

struct VS_In
{
    float4 Position : POSITION0;
	float3 Normal : NORMAL0;
};

struct VS_Out
{
    float4 Position : POSITION0;
	float3 Normal : TEXCOORD0;
};

struct PS_Out
{
	float4 Color : COLOR0;
};

VS_Out VS_Default( VS_In input )
{
	float4 world_position = mul( input.Position, Transform );
    float4 view_position = mul( world_position, CameraView );

    VS_Out output = ( VS_Out )0;

    output.Position = mul( view_position, CameraProjection );
	output.Normal = normalize( mul( input.Normal, Transform ) );

    return output;
}

PS_Out PS_Default( VS_Out input )
{
	float4 normal = float4( input.Normal, 1.0f );
	float4 diffuse = saturate( dot( -DiffuseDirection, normal ) );

	PS_Out output;

	output.Color = ( AmbientColor * AmbientIntensity + DiffuseColor * DiffuseIntensity * diffuse );

    return output;
}

technique Render_Default
{
    pass Default
    {
        VertexShader = compile vs_2_0 VS_Default( );
        PixelShader = compile ps_2_0 PS_Default( );
    }
}