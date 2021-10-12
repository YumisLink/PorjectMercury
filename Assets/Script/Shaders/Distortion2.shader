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
          uniform float2 _Center;

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

          float Fitting(float low, float high, float input)
          {
            high -= low;
            input -= low;
            return (input / high);
          }

          float4 frag(Varyings input) : SV_Target
          {
              float2 uv = input.uv;
              float2 offset = uv - _Center;
              float2 direction = normalize(offset);
              float len = length(offset);
              float t = _Time.x * 25;
              float x = len;

              
              //hack
              float edge = step(0 + (t * 0.2), x) * step(x, (t * 0.2) + PI / 40 * (t * t * 10 + 0.1));
              float wave = 3 * (1 - (t * 80) / (1 + t * 80)) * sin(40 / (t * t * 10 + 0.1) * (x - (t * 0.2)));
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
