using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SuperDistortionFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;
        public Material SuperDistortionMaterial = null;
    }

    class SuperDistortionRenderPass : ScriptableRenderPass
    {
        public Material blitMaterial = null;
        private RenderTargetIdentifier source { get; set; }
        private RenderTargetHandle destination { get; set; }

        RenderTargetHandle m_TemporaryColorTexture;
        RenderTargetHandle _cameraTexture;
        string m_ProfilerTag;

        public SuperDistortionRenderPass(RenderPassEvent renderPassEvent, Material blitMaterial)
        {
            this.renderPassEvent = renderPassEvent;
            this.blitMaterial = blitMaterial;
            m_ProfilerTag = "SuperDistortion";
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

    SuperDistortionRenderPass _airDistortionRenderPass;

    public override void Create()
    {
        _airDistortionRenderPass = new SuperDistortionRenderPass(Setting.Event, Setting.SuperDistortionMaterial);
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
