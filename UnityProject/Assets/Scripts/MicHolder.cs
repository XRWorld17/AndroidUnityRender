using UnityEngine;

public class MicHolder : MonoBehaviour
{
    public float bootIntensity = 0.02f;
    private Material material;
    private new AudioSource audioSource;

    public ParticleSystem m_ParticleSystem;

    private int m_NumSamples = 256;
    private float[] m_Samples;
    private float sum, rms;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        BeginListener(0);
        //audioSource.clip = Microphone.Start(null, true, 10, 44100);
        m_Samples = new float[m_NumSamples];
    }

    // Update is called once per frame
    private void Update()
    {
        var emission = m_ParticleSystem.emission;
        audioSource.GetOutputData(m_Samples, 0);
        sum = m_Samples[m_NumSamples - 1] * m_Samples[m_NumSamples - 1];
        rms = Mathf.Sqrt(sum/* / m_NumSamples*/);
        float intensity = rms;
        Debug.Log(intensity);
        if (intensity > bootIntensity)
        {
            emission.rateOverTime = 10f * (1 + intensity * 1000);
        }
        else
        {
            emission.rateOverTime = 10f;
        }
    }

    public void BeginListener(int index)
    {
        int min = 0;
        int max = 0;

        Microphone.GetDeviceCaps(Microphone.devices[index], out min, out max);

        audioSource.clip = Microphone.Start(Microphone.devices[index], true, 2, max);

        while (!(Microphone.GetPosition(Microphone.devices[index]) > 1))
        {
            // Wait until the recording has started
        }

        audioSource.loop = true;
        audioSource.Play();
    }
}