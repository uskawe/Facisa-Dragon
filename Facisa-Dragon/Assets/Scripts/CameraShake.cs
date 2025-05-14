using UnityEngine;
using Cinemachine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCam;
    private CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        if (virtualCam != null)
        {
            noise = virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }

    public void Shake(float intensity, float duration)
    {
        if (noise == null) return;

        noise.m_AmplitudeGain = intensity;
        StartCoroutine(ResetShake(duration));
    }

    private IEnumerator ResetShake(float duration)
    {
        yield return new WaitForSeconds(duration);
        if (noise != null)
            noise.m_AmplitudeGain = 0f;
    }
}
