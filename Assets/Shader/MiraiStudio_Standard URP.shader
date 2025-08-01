Shader "MiraiStudio/Standard URP" {
	Properties {
		_MainTex ("Main Texture", 2D) = "white" {}
		_Color ("Color", Vector) = (1,1,1,1)
		_Brightness ("Brightness", Range(0, 25)) = 1
		_Contrast ("Contrast", Range(0, 25)) = 1
		_Saturation ("Saturation", Range(0, 2)) = 1
		[Toggle] _IgnoreMainTexAlpha ("Ignore Main Texture Alpha", Float) = 0
		[HeaderGroup(Diffuse)] [Toggle(DIFFUSE)] _Diffuse ("Enable Diffuse", Float) = 1
		_DiffuseWrap ("Diffuse Softness", Range(-1, 1)) = 0
		_DiffuseBrightness ("Diffuse Brightness", Range(0, 25)) = 1
		_DiffuseContrast ("Diffuse Contrast", Range(0.01, 25)) = 1
		[Toggle(DIFFUSE_PER_PIXEL)] _DiffusePerPixel ("Per-Pixel Point Lights", Float) = 0
		[HeaderGroup(Specular)] [Toggle(SPECULAR)] _Specular ("Enable Specular", Float) = 0
		_SpecularColor ("Specular Color", Vector) = (1,1,1,1)
		_SpecularBrightness ("Specular Brightness", Range(0, 25)) = 1
		[PowerSlider(10)] _SpecularSmoothness ("Specular Smoothness", Range(1, 500)) = 20
		_SpecularTex ("Specular Map", 2D) = "white" {}
		[KeywordEnum(UV1, UV2)] _SpecularUV ("Specular UV", Float) = 0
		[Toggle(SPECULAR_HAIR)] _SpecularHair ("Specular Hair", Float) = 0
		[HeaderGroup(Rim Light)] [Toggle(RIM)] _Rim ("Enable Rim Light", Float) = 0
		[HDR] _RimColor ("Rim Color", Vector) = (1,1,1,1)
		_RimAmount ("Rim Amount", Range(0, 25)) = 1
		_RimContrast ("Rim Contrast", Range(0, 50)) = 5
		[KeywordEnum(Alpha Blend, Additive, Multiply, Multiply Lighten, Transparency)] _RimBlend ("Rim Blend Mode", Float) = 1
		[Toggle] _RimInverse ("Rim Invert", Float) = 0
		_RimDirection ("Rim Direction", Vector) = (0,0,0,0)
		[HeaderGroup(Reflection)] [Toggle(REFLECTION)] _Reflection ("Enable Reflection", Float) = 0
		[NoScaleOffset] _ReflectionTex ("Reflection Cubemap", Cube) = "" {}
		_ReflectionAmount ("Reflection Amount", Range(0, 1)) = 1
		[HDR] _ReflectionColor ("Reflection Color", Vector) = (1,1,1,1)
		[Toggle(REFLECTION_RIM)] _ReflectionRim ("Mask With Rim", Float) = 0
		[HeaderGroup(Normal Map)] [Normal] _NormalTex ("Normal Map", 2D) = "bump" {}
		_NormalStrength ("Normal Strength", Range(0, 5)) = 1
		[KeywordEnum(UV1, UV2)] _NormalUV ("Normal UV", Float) = 0
		[HeaderGroup(Occlusion Map)] _LightmapTex ("Occlusion Map", 2D) = "white" {}
		_LightmapColor ("Occlusion Color", Vector) = (1,1,1,1)
		_LightmapBrightness ("Occlusion Brightness", Range(0, 25)) = 1
		[KeywordEnum(UV1, UV2)] _LightmapUV ("Occlusion UV", Float) = 0
		[HeaderGroup(Emissive)] [HDR] _Emissive ("Emissive Color", Vector) = (0,0,0,0)
		_EmissiveTex ("Emissive Map", 2D) = "white" {}
		[HeaderGroup(MatCap)] _MatCapTex ("MatCap Texture", 2D) = "black" {}
		_MatCapColor ("MatCap Color", Vector) = (1,1,1,1)
		_MatCapBrightness ("MatCap Brightness", Range(0, 25)) = 1
		_MatCapContrast ("MatCap Contrast", Range(0, 25)) = 1
		[KeywordEnum(Multiply, Multiply Lighten)] _MatCapBlend ("MatCap Blend Mode", Float) = 0
		[Toggle(MATCAP_PERSPECTIVE)] _MatCapPerspective ("Perspective Correction", Float) = 1
		[Toggle(MATCAP_STATIC)] _MatCapStatic ("Use Static Rotation", Float) = 0
		_MatCapRot ("MatCap Static Rotation", Vector) = (0,0,0,0)
		[HeaderGroup(Vertex Colors)] [Toggle(VERTEX_COLORS)] _VertexColors ("Enable Vertex Colors", Float) = 0
		_VertexColorsAmount ("Vertex Colors Amount", Range(0, 1)) = 1
		_VertexColorsContrast ("Vertex Colors Contrast", Range(0, 25)) = 1
		[HeaderGroup(Detail Map)] _DetailTex ("Detail Map", 2D) = "white" {}
		_DetailColor ("Detail Color", Vector) = (1,1,1,1)
		_DetailBrightness ("Detail Brightness", Range(0, 25)) = 1
		_DetailContrast ("Detail Contrast", Range(0, 25)) = 1
		[KeywordEnum(Alpha Blend, Additive, Multiply, Multiply Lighten)] _DetailBlend ("Detail Blend Mode", Float) = 2
		[KeywordEnum(UV1, UV2)] _DetailUV ("Detail UV", Float) = 0
		[Toggle(DETAIL_LIGHTING)] _DetailLighting ("Apply To Lighting", Float) = 0
		[Toggle(DETAIL_VERTEX_COLORS)] _DetailVertexColors ("Mask With Vertex Color (A)", Float) = 0
		[HeaderGroup(Layer 1)] _Layer1Tex ("Layer Texture", 2D) = "white" {}
		_Layer1Color ("Layer Color", Vector) = (1,1,1,1)
		_Layer1Brightness ("Layer Brightness", Range(0, 25)) = 1
		_Layer1Alpha ("Layer Alpha", Range(0, 25)) = 1
		[KeywordEnum(Alpha Blend, Additive, Multiply, Multiply Lighten)] _Layer1Blend ("Layer Blend Mode", Float) = 0
		[Toggle] _Layer1VertexColor ("Mask With Vertex Color (R)", Float) = 0
		[HeaderGroup(Layer 2)] _Layer2Tex ("Layer Texture", 2D) = "white" {}
		_Layer2Color ("Layer Color", Vector) = (1,1,1,1)
		_Layer2Brightness ("Layer Brightness", Range(0, 25)) = 1
		_Layer2Alpha ("Layer Alpha", Range(0, 25)) = 1
		[KeywordEnum(Alpha Blend, Additive, Multiply, Multiply Lighten)] _Layer2Blend ("Layer Blend Mode", Float) = 0
		[Toggle] _Layer2VertexColor ("Mask With Vertex Color (G)", Float) = 0
		[HeaderGroup(Layer 3)] _Layer3Tex ("Layer Texture", 2D) = "white" {}
		_Layer3Color ("Layer Color", Vector) = (1,1,1,1)
		_Layer3Brightness ("Layer Brightness", Range(0, 25)) = 1
		_Layer3Alpha ("Layer Alpha", Range(0, 25)) = 1
		[KeywordEnum(Alpha Blend, Additive, Multiply, Multiply Lighten)] _Layer3Blend ("Layer Blend Mode", Float) = 0
		[Toggle] _Layer3VertexColor ("Mask With Vertex Color (B)", Float) = 0
		[HeaderGroup(Transparency Mask)] _TransparencyMaskTex ("Transparency Mask", 2D) = "white" {}
		_TransparencyMaskAmount ("Mask Amount", Range(0, 25)) = 1
		_TransparencyMaskContrast ("Mask Contrast", Range(0, 25)) = 1
		[HeaderGroup(Height Based ColorsS)] [Toggle(HEIGHT_COLORS)] _HeightColors ("Enable Height Based Colors", Float) = 0
		_HeightColorsColor ("Color", Vector) = (1,1,1,1)
		_HeightColorsAlpha ("Alpha", Range(0, 25)) = 1
		_HeightColorsHeight ("Height", Range(-100, 100)) = 0
		_HeightColorsEdgeThickness ("Edge Thickness", Range(0.001, 100)) = 1
		_HeightColorsThickness ("Thickness", Range(0, 100)) = 0
		[Enum(World, 0, Local, 1)] _HeightColorsSpace ("Coordinate Space", Float) = 0
		[KeywordEnum(Alpha Blend, Additive, Lit)] _HeightColorsBlend ("Height Colors Blend Mode", Float) = 0
		_HeightColorsTex ("Height Colors Texture", 2D) = "white" {}
		[HeaderGroup(Shadow Overlay)] _ShadowOverlayTex ("Shadow Overlay Tex", 2D) = "white" {}
		_ShadowOverlayBrightness ("Shadow Brightness", Range(0, 2)) = 1
		_ShadowOverlaySpeedU ("Shadow Speed U", Range(-5, 5)) = 0.1
		_ShadowOverlaySpeedV ("Shadow Speed V", Range(-5, 5)) = 0.03
		_ShadowOverlaySwayAmount ("Shadow Sway Amount", Range(0, 0.01)) = 0.01
		[KeywordEnum(Scroll, Sway)] _ShadowOverlayAnimation ("Animation Type", Float) = 0
		[HeaderGroup(Plant Sway)] [Toggle(PLANT_SWAY)] _Plant ("Enable Plant Sway", Float) = 0
		_PlantSwayAmount ("Sway Amount", Range(0, 10)) = 0.15
		_PlantSwaySpeed ("Sway Speed", Range(0, 10)) = 1
		_PlantPhaseVariation ("Phase Variation", Range(0, 100)) = 0.3
		_PlantBaseHeight ("Base Height", Range(-100, 100)) = 0
		[KeywordEnum(Plant, Leaf, Vertex Color Alpha)] _PlantType ("Plant Type", Float) = 0
		[HeaderGroup(Outline)] [Toggle(OUTLINE)] _Outline ("Enable Outline", Float) = 0
		_OutlineWidth ("Outline Width", Range(0, 0.1)) = 0.002
		_OutlineColor ("Outline Color", Vector) = (0,0,0,1)
		[HeaderGroup(Anime)] [Toggle(ANIME)] _Anime ("Enable Anime", Float) = 0
		[HDR] _AnimeColor1 ("Color 1", Vector) = (1,1,1,1)
		_AnimeThreshold1 ("Luminance Threshold 1", Range(0, 3)) = 0.45
		[HDR] _AnimeColor2 ("Color 2", Vector) = (1.35,1.35,1.35,1)
		_AnimeThreshold2 ("Luminance Threshold 2", Range(0, 3)) = 0.85
		[HDR] _AnimeColor3 ("Color 3", Vector) = (2,2,2,1)
		_AnimeSoftness ("Softness", Range(0, 0.25)) = 0.01
		[HeaderGroup(Environment And Shadows)] _AmbientBrightness ("Ambient Brightness", Range(0, 25)) = 1
		[Toggle(FOG)] _Fog ("Enable Fog", Float) = 1
		[Header(Shadows)] [Toggle(SHADOWS_ENABLED)] _ShadowsEnabled ("Enable Shadows", Float) = 1
		[HDR] _ShadowColor ("Shadow Color", Vector) = (0.3,0.3,0.3,1)
		[HeaderGroup(Culling And Blending)] [Enum(Opaque, 0, Transparent, 1, Transparent Additive, 2, Transparent Additive Alpha, 3, Opaque Cutout, 4)] _Preset ("Culling And Blend Preset", Float) = 0
		[Header(Culling)] [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Culling", Float) = 2
		[Enum(Off, 0, On, 1)] _ZWrite ("Z Write", Float) = 1
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest ("Z Test", Float) = 4
		_ZOffset ("Depth Offset", Range(-5, 5)) = 0
		[Toggle(CUTOUT)] _Cutout ("Cutout Transparency", Float) = 0
		[Header(Blending)] [Enum(UnityEngine.Rendering.BlendMode)] _SourceBlend ("Source Blend", Float) = 5
		[Enum(UnityEngine.Rendering.BlendMode)] _DestBlend ("Dest Blend", Float) = 10
		[Enum(UnityEngine.Rendering.BlendOp)] _BlendOp ("Blend Mode", Float) = 0
		[HeaderGroup(Rendering)] [Toggle(FLAT)] _Flat ("Enable Flat Shading", Float) = 0
		[HeaderGroup(Shader Stripping (advanced))] [KeywordEnum(All, Disabled, Enabled Only)] _OptShadow ("Shadows", Float) = 0
		[KeywordEnum(All, Disabled)] _OptPointLights ("Point Lights", Float) = 0
		[KeywordEnum(All, Disabled, Enabled Only)] _OptFog ("Fog", Float) = 0
		[KeywordEnum(All, Disabled, Enabled Only)] _OptLightmapping ("Lightmapping", Float) = 0
		[KeywordEnum(All, Disabled)] _OptFallback ("Fallback for OpenGL ES 2", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}