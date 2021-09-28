using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RenderMaskFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class RenderMaskSetting
    {
        /// <summary>
        /// 渲染时机
        /// </summary>
        public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;

        /// <summary>
        /// 遮罩Layer
        /// </summary>
        public LayerMask LayerMask;

        /// <summary>
        /// 遮罩使用的Material
        /// </summary>
        public Material Material;
    }

    class RenderMaskRenderPass : ScriptableRenderPass
    {
        //用于储存之后申请来的RT的ID
        private int m_SoildColorID = 0;
        private ShaderTagId m_ShaderTag = new ShaderTagId("UniversalForward");
        private RenderMaskSetting m_Setting;
        private FilteringSettings m_FilteringSettings;

        public RenderMaskRenderPass(RenderMaskSetting setting)
        {
            this.m_Setting = setting;
            m_FilteringSettings = new FilteringSettings(RenderQueueRange.all, this.m_Setting.LayerMask);
        }

        // This method is called before executing the render pass.
        // It can be used to configure render targets and their clear state. Also to create temporary render target textures.
        // When empty this render pass will render to the active camera render target.
        // You should never call CommandBuffer.SetRenderTarget. Instead call <c>ConfigureTarget</c> and <c>ConfigureClear</c>.
        // The render pipeline will ensure target setup and clearing happens in an performance manner.
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            //获取一个ID，这也是我们之后在Shader中用到的Buffer名
            int temp = Shader.PropertyToID("_MaskSoildColor");
            m_SoildColorID = temp;
            //一般都可以使用比较低的分辨率来做扭曲效果，因为扭曲效果本身就不太追求画质了
            RenderTextureDescriptor desc = new RenderTextureDescriptor(128, 128);
            cmd.GetTemporaryRT(temp, desc);
            //将这个RT设置为Render Target
            ConfigureTarget(temp);
            //将RT清空为黑
            ConfigureClear(ClearFlag.All, Color.black);
        }

        // Here you can implement the rendering logic.
        // Use <c>ScriptableRenderContext</c> to issue drawing commands or execute command buffers
        // https://docs.unity3d.com/ScriptReference/Rendering.ScriptableRenderContext.html
        // You don't have to call ScriptableRenderContext.submit, the render pipeline will call it at specific points in the pipeline.
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var drawMask = CreateDrawingSettings(m_ShaderTag, ref renderingData, SortingCriteria.CommonTransparent);
            drawMask.overrideMaterial = m_Setting.Material;
            drawMask.overrideMaterialPassIndex = 0;
            context.DrawRenderers(renderingData.cullResults, ref drawMask, ref m_FilteringSettings);
        }

        /// Cleanup any allocated resources that were created during the execution of this render pass.
        public override void FrameCleanup(CommandBuffer cmd)
        {
        }
    }

    public RenderMaskSetting Setting;
    private RenderMaskRenderPass m_ScriptableMaskRenderPass;

    public override void Create()
    {
        m_ScriptableMaskRenderPass = new RenderMaskRenderPass(this.Setting);
        m_ScriptableMaskRenderPass.renderPassEvent = this.Setting.Event;
    }

    // Here you can inject one or multiple render passes in the renderer.
    // This method is called when setting up the renderer once per-camera.
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(m_ScriptableMaskRenderPass);
    }
}


