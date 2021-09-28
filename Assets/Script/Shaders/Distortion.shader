Shader "MercuryFeature/Distortion"
{
  SubShader
  {
      Pass
      {
          HLSLPROGRAM
          #pragma vertex vert
          #pragma fragment frag

          #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

          #define PI2 6.2831853

          TEXTURE2D(_CameraColorTexture);
          SAMPLER(sampler_CameraColorTexture);
          TEXTURE2D(_MaskSoildColor);
          SAMPLER(sampler_MaskSoildColor);
          uniform float2 _Center;
          uniform float _Distance;
          uniform float _Power;
          uniform float _Amplitude;
          uniform float _WaveLength;
          uniform float _Speed;
          uniform float _OffsetNear;
          uniform float _OffsetFar;

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
              float2 offset = uv - _Center;
              float2 direction = normalize(offset);
              float len = length(offset);

              float edge = step(_OffsetNear, len) * step(len, _OffsetFar);
              
              //计算振幅，简单模拟距离中心越远振幅越小
              float2 amp = (direction * _Amplitude) * pow(saturate(1.0 - len / _Distance), _Power);
              //sin((2π/L)t+φ)
              //初始相位 + 根据时间算相位偏移
              float phase = (len / _WaveLength * PI2) + ((_Time.x * _Speed * PI2) / _WaveLength);
              float sinPhase = sin(phase);
              //float2 wave = amp * sinPhase;
              float2 wave = amp * sinPhase * edge;

              float4 factor = SAMPLE_TEXTURE2D(_MaskSoildColor, sampler_MaskSoildColor, input.uv);

              //float2 resultUv = wave * factor.r + uv;
              float2 resultUv = wave + uv;
              float4 result = SAMPLE_TEXTURE2D(_CameraColorTexture, sampler_CameraColorTexture, resultUv);
              return result;
          }
          ENDHLSL
      }
  }
  FallBack off
}
