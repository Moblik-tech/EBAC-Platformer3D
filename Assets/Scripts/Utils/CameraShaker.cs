using UnityEngine;
using Cinemachine;
using Moblik.Core.Singleton;

public class CameraShaker : Singleton<CameraShaker>
{
    public CinemachineVirtualCamera virtualCamera;
    private float _shakeDuration;
    private CinemachineBasicMultiChannelPerlin c;

    [Header("Shake Values")]
    public float amplitude = 3f;
    public float frequency = 3f;
    public float time = 0.3f;

    protected override void Awake()
    {
        base.Awake();
        c = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    [NaughtyAttributes.Button]
    public void Shake()
    {
        ShakeCamera(amplitude, frequency, time);
    }

    public void ShakeCamera(float amplitude, float frequency, float time)
    {
        c.m_AmplitudeGain = amplitude;
        c.m_FrequencyGain = frequency;

        _shakeDuration = time;
    }

    private void Update()
    {
        if (_shakeDuration > 0)
        {
            _shakeDuration -= Time.deltaTime;
        }
        else
        {
            c.m_AmplitudeGain = 0f;
            c.m_FrequencyGain = 0f;
        }
    }
}