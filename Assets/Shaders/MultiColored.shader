Shader "Custom/MultiColored" {
	Properties{}

		SubShader{
		Tags{ "Queue" = "Geometry" "RenderType" = "Opaque" }

		////////////////////////////////////////////////////////
		//Surface Shader  - RED                               //
		////////////////////////////////////////////////////////

		CGPROGRAM

#pragma target 3.0
#pragma surface surf BlinnPhong

			struct Input {
			float4 color : COLOR;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = float3(1, 0, 1);
		}

		ENDCG


			////////////////////////////////////////////////////////
			//Surface Shader  - GREEN                             //
			////////////////////////////////////////////////////////
			CGPROGRAM

#pragma target 3.0
#pragma surface surf BlinnPhong

			struct Input {
			float4 color : COLOR;
		};



		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = float3(IN.color.r, 1, IN.color.b);
		}

		ENDCG

			//The result is yellow!
	}

		Fallback "Diffuse"
}