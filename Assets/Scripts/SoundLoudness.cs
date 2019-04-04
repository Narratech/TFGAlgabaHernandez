using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoudness : MonoBehaviour
{

    public static float step = 0.5f;


    int _sampleWindow = 128;
    private List<float> results;
    private bool collect = false;

    float actTime;

    //mic initialization

    public void StartCollecting()
    {
        results = new List<float>();
        collect = true;
        actTime = Time.realtimeSinceStartup;
    }
    public void StopCollecting()
    {
        collect = false;
        
    }
    public float [] GetData()
    {
        return results.ToArray();
    }
    float LevelMax()
    {
        float levelMax = -100;
        float[] waveData = new float[_sampleWindow];
        int micPosition = MicrophoneManager.GetMicrophonePosition() - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        MicrophoneManager.AudioClip.GetData(waveData, micPosition);
        // Getting a peak on the last 128 samples
        for (int i = 0; i < _sampleWindow; i++)
        {
            float wavePeak = waveData[i] * waveData[i];
            if (levelMax < wavePeak)
            {
                levelMax = wavePeak;
            }
        }
        return levelMax;
    }

    void Update()
    {
        if (!collect) return;
        float aux = -100f;
        if (Time.realtimeSinceStartup - actTime > step)
        {
            aux = LevelMax();
            results.Add(aux);
        }
    }
}
