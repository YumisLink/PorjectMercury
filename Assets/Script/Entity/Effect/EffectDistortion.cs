using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EffectDistortion : MonoBehaviour
{
    [System.Serializable]
    public class Settings
    {
        public float Distance = 0.5f;
        public float Power = 5;
        public float Amplitude = 5;
        public float WaveLength = 0.5f;
        public float Speed = 10;
        public float Near = 0.25f;
        public float Far = 0.4f;
    }

    private static readonly int Center = Shader.PropertyToID("_Center");
    private static readonly int Distance = Shader.PropertyToID("_Distance");
    private static readonly int Power = Shader.PropertyToID("_Power");
    private static readonly int Amplitude = Shader.PropertyToID("_Amplitude");
    private static readonly int WaveLength = Shader.PropertyToID("_WaveLength");
    private static readonly int Speed = Shader.PropertyToID("_Speed");
    private static readonly int OffsetNear = Shader.PropertyToID("_OffsetNear");
    private static readonly int OffsetFar = Shader.PropertyToID("_OffsetFar");

    public ForwardRendererData RendererData;
    public Settings Setting;
    public float PlaySpeed = 5;

    private Coroutine _effCo;
    private AirDistortionFeature _distortionFeature;
    private Material Mat;

    private void Start()
    {
        _distortionFeature = RendererData.rendererFeatures.OfType<AirDistortionFeature>().First();
        Mat = _distortionFeature.Setting.AirDistortionMaterial;
        _distortionFeature.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Play(transform);
        }
    }

    public void Play(Transform target)
    {
        if (_effCo != null)
        {
            StopCoroutine(_effCo);
            _effCo = null;
        }
        var mat = Mat;
        mat.SetVector(Center, Camera.main.WorldToViewportPoint(target.position));
        mat.SetFloat(Distance, Setting.Distance);
        mat.SetFloat(Power, Setting.Power);
        mat.SetFloat(Amplitude, Setting.Amplitude);
        mat.SetFloat(WaveLength, Setting.WaveLength);
        mat.SetFloat(Speed, Setting.Speed);
        mat.SetFloat(OffsetNear, Setting.Near);
        mat.SetFloat(OffsetFar, Setting.Far);
        _effCo = StartCoroutine(PlayEffect(target, Setting.Near, Setting.Far));
    }

    private IEnumerator PlayEffect(Transform target, float near, float far)
    {
        _distortionFeature.SetActive(true);
        //float dis = far - near;
        while (near <= PlaySpeed)
        {
            var center = Camera.main.WorldToViewportPoint(target.position);
            var mat = Mat;
            mat.SetVector(Center, center);
            mat.SetFloat(OffsetNear, near);
            mat.SetFloat(OffsetFar, far);
            near += PlaySpeed * Time.deltaTime;
            far += PlaySpeed * Time.deltaTime;
            yield return null;
        }
        _distortionFeature.SetActive(false);
        _effCo = null;
    }

    private void OnDestroy()
    {
        _distortionFeature.SetActive(false);
    }
}
