Shader "MercuryFeature/SuperDistortion"
{
  SubShader
  {
      Pass
      {
          HLSLPROGRAM
          #pragma vertex vert
          #pragma fragment frag

          #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

          TEXTURE2D(_CameraColorTexture);
          SAMPLER(sampler_CameraColorTexture);

          struct Attributes
          {
              float4 positionOS : POSITION;
              float2 uv : TEXCOORD0;
          };

          struct Varyings
          {
              float4 vertex : SV_POSITION;
              float2 uv : TEXCOORD0;
          };

          Varyings vert(Attributes input)
          {
              Varyings output;

              VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
              output.vertex = vertexInput.positionCS;
              output.uv = input.uv;

              return output;
          }

          float4 frag(Varyings input) : SV_Target
          {
              float2 uv = input.uv;
              float2 offset = uv - float2(0.5, 0.5);
              float2 direction = normalize(offset);
              float len = length(offset);
              float t = _Time.x;
              float x = len;

              float edge = step(0 + (t * t * 0.3 + t), x) * step(x, (t * t * 0.3 + t) + PI / 5 * (t + 0.5));
              float wave = 3 / (pow(t + 3, 5) * 0.05) * sin(5 / (t + 0.5) * (x - (t * t * 0.3 + t)));
              float mixin = edge * wave;

              float2 resultUv = mixin + uv;
              float4 result = SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, resultUv);
              return result;
          }
          ENDHLSL
      }
  }
  FallBack off
}
