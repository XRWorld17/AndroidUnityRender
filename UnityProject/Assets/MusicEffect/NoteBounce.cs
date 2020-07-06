using UnityEngine;

public class NoteBounce : MonoBehaviour
{
    public float bootIntensity = 0.5f;
    public GameObject fireworksObj;
    private Material fireworksMat;
    private Material material;
    private new AudioSource audioSource;

    private int m_NumSamples = 256;
    private float[] m_Samples;
    private float sum, rms;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        BeginListener(0);
        //audioSource.clip = Microphone.Start(null, true, 10, 44100);
        fireworksMat = fireworksObj.GetComponent<MeshRenderer>().material;
        material = GetComponent<MeshRenderer>().material;
        m_Samples = new float[m_NumSamples];
    }

    // Update is called once per frame
    private void Update()
    {
        audioSource.GetOutputData(m_Samples, 0);
        sum = m_Samples[m_NumSamples - 1] * m_Samples[m_NumSamples - 1];
        rms = Mathf.Sqrt(sum/* / m_NumSamples*/);
        float intensity = rms;
        Debug.Log(intensity);
        if (intensity > bootIntensity)
        {
            material.SetFloat("_Intensity", intensity);
        }
        else
        {
            material.SetFloat("_Intensity", 0);
        }
        fireworksMat.SetFloat("_ContinueTime", 2);
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