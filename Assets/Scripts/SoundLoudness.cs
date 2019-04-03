using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoudness : MonoBehaviour
{

    public static float step = 0.5f;
    public static int DeviceNumber = 0;
    public static int SampleRate = 44100;


    AudioClip _clipRecord = null;
    int _sampleWindow = 128;
    private string _device;
    private List<float> results;
    private bool collect = false;
    private bool _isInitialized;

    float actTime;

    //mic initialization
    void InitMic()
    {
        if (_device == null) _device = Microphone.devices[DeviceNumber];
        if(gameObject.GetComponent<MicToVokaturi>().audioClip == null)_clipRecord = Microphone.Start(_device, true, 999, SampleRate);
        else
        {
            _clipRecord = gameObject.GetComponent<MicToVokaturi>().audioClip;
        }
    }

    void StopMicrophone()
    {
       if(Microphone.IsRecording(_device)) Microphone.End(_device);
    }

    public void StartCollecting()
    {
        InitMic();
        results = new List<float>();
        collect = true;
        actTime = Time.realtimeSinceStartup;
    }
    public void StopCollecting()
    {
        collect = false;
        StopMicrophone();
        
    }
    public float [] GetData()
    {
        return results.ToArray();

    }


    //get data from microphone into audioclip
    float LevelMax()
    {
        float levelMax = -100;
        float[] waveData = new float[_sampleWindow];
        int micPosition = Microphone.GetPosition(_device) - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        _clipRecord.GetData(waveData, micPosition);
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

    // start mic when scene starts
    void OnEnable()
    {
        _isInitialized = true;
    }

    //stop mic when loading a new level or quit application
    void OnDisable()
    {
        StopMicrophone();
    }

    void OnDestroy()
    {
        StopMicrophone();
    }
}
