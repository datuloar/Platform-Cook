﻿Shader "Unlit/VisibleThroughWalls"
{
    Properties
    {
	    _Color ("Visible color", Color) = (1,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent" "Queue"="Transparent" }
        LOD 100
    	
    	Blend SrcAlpha OneMinusSrcAlpha

    	Stencil 
    	{
    		Ref 2
            Comp NotEqual
			Pass Replace
        }
    	
		Pass
		{
			Cull Off
			ZWrite Off
			ZTest Always

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
			};

			float4 _Color;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				return _Color;
			}
			ENDCG
		}
    }
}