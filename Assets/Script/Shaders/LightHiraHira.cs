using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightHiraHira : MonoBehaviour
{
    public Light2D Li;
    public float Delta;

    private float _delta;

    private void Start()
    {
        _delta = Delta;
    }

    private void Update()
    {
        var li = Li.intensity;
        if (Li.intensity > 5)
        {
            _delta = -Delta;
        }
        else if (Li.intensity < 1)
        {
            _delta = Delta;
        }
        li += _delta * Time.deltaTime;
        Li.intensity = li;
    }
}
