using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NoteBounce : MonoBehaviour
{
    public GameObject fireworksObj;
    private Material fireworksMat;
    private new AudioSource audioSource;
    private Material material;

    private int m_NumSamples = 256;
    private float[] m_Samples;
    private float sum, rms;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        BeginListener(0);
        //audioSource.clip = Microphone.Start(null, true, 10, 44100);
        material = GetComponent<MeshRenderer>().material;
        fireworksMat = fireworksObj.GetComponent<MeshRenderer>().material;
        m_Samples = new float[m_NumSamples];
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.GetOutputData(m_Samples, 0);
        sum = m_Samples[m_NumSamples - 1] * m_Samples[m_NumSamples - 1];
        rms = Mathf.Sqrt(sum/* / m_NumSamples*/);
        float intensity = rms;
        Debug.Log(intensity);
        if (intensity > 0.2f)
        {
            fireworksMat.SetFloat("_ContinueTime", 2);
        }
        else
        {
            fireworksMat.SetFloat("_ContinueTime", 0);
        }
        material.SetFloat("_Intensity", intensity);
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
