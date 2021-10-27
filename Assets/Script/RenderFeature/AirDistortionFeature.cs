using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AirDistortionFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;
        public Material AirDistortionMaterial = null;
        public Vector2 Center = new Vector2(0.5f, 0.5f);
        public float Distance = 1;
        public float Power = 5;
        public float Amplitude = 1;
        public float WaveLength = 1;
        public float Speed = 1;
        public float Near = 0.125f;
        public float Far = 0.5f;
    }

    class AirDistortionRenderPass : ScriptableRenderPass
    {
        public Material blitMaterial = null;
        private RenderTargetIdentifier source { get; set; }
        private RenderTargetHandle destination { get; set; }

        RenderTargetHandle m_TemporaryColorTexture;
        RenderTargetHandle _cameraTexture;
        string m_ProfilerTag;

        public AirDistortionRenderPass(RenderPassEvent renderPassEvent, Material blitMaterial)
        {
            this.renderPassEvent = renderPassEvent;
            this.blitMaterial = blitMaterial;
            m_ProfilerTag = "AirDistortion";
            m_TemporaryColorTexture.Init("_TemporaryColorTexture");
            _cameraTexture.Init("_CameraColorTexture");
        }

        public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
        {
            this.source = source;
            this.destination = destination;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(m_ProfilerTag);

            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;

            cmd.GetTemporaryRT(m_TemporaryColorTexture.id, opaqueDesc);
            Blit(cmd, source, m_TemporaryColorTexture.Identifier(), blitMaterial);
            Blit(cmd, m_TemporaryColorTexture.Identifier(), source);
            cmd.ReleaseTemporaryRT(m_TemporaryColorTexture.id);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
        }
    }

    public Settings Setting;

    AirDistortionRenderPass _airDistortionRenderPass;

    public static readonly int CameraTexture = Shader.PropertyToID("_CameraColorTexture");
    private static readonly int Center = Shader.PropertyToID("_Center");
    private static readonly int Distance = Shader.PropertyToID("_Distance");
    private static readonly int Power = Shader.PropertyToID("_Power");
    private static readonly int Amplitude = Shader.PropertyToID("_Amplitude");
    private static readonly int WaveLength = Shader.PropertyToID("_WaveLength");
    private static readonly int Speed = Shader.PropertyToID("_Speed");
    private static readonly int OffsetNear = Shader.PropertyToID("_OffsetNear");
    private static readonly int OffsetFar = Shader.PropertyToID("_OffsetFar");

    public override void Create()
    {
        Setting.AirDistortionMaterial.SetVector(Center, Setting.Center);
        Setting.AirDistortionMaterial.SetFloat(Distance, Setting.Distance);
        Setting.AirDistortionMaterial.SetFloat(Power, Setting.Power);
        Setting.AirDistortionMaterial.SetFloat(Amplitude, Setting.Amplitude);
        Setting.AirDistortionMaterial.SetFloat(WaveLength, Setting.WaveLength);
        Setting.AirDistortionMaterial.SetFloat(Speed, Setting.Speed);
        Setting.AirDistortionMaterial.SetFloat(OffsetNear, Setting.Near);
        Setting.AirDistortionMaterial.SetFloat(OffsetFar, Setting.Far);
        _airDistortionRenderPass = new AirDistortionRenderPass(Setting.Event, Setting.AirDistortionMaterial);
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var src = renderer.cameraColorTarget;
        var dest = RenderTargetHandle.CameraTarget;

        _airDistortionRenderPass.Setup(src, dest);
        renderer.EnqueuePass(_airDistortionRenderPass);
    }
}


