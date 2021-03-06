﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SoundLoudness : MonoBehaviour
{

    public static float step = 0.05f;


    int _sampleWindow = 1024;
    private List<float> results;
    private bool collect = false;

    float actTime;

    //mic initialization

    void Start()
    {
        results = new List<float>();
        collect = false;
        actTime = Time.realtimeSinceStartup;
        startCollecting();
    }
    void startCollecting()
    {
        collect = true;
    }
    void OnDisable()
    {
        collect = false;
        
    }
    public float [] GetData()
    {
        return results.ToArray();
    }
    /// <summary>
    /// Obtiene el valor medio cuadrático de 1024 muestras obtenidas en el instante.
    /// Con ese valor, calcula el valor en decibelios de la muestra. 
    /// </summary>
    /// <returns>Valor en decibelios del micrófono.</returns>
    float getLoudness()
    {

        float[] waveData = new float[_sampleWindow];
        int micPosition = MicrophoneManager.GetMicrophonePosition() - (_sampleWindow + 1); // null means the first microphone
        if (micPosition < 0) return 0;
        MicrophoneManager.AudioClip.GetData(waveData, micPosition);

        //Normalize(waveData);
       
        //Root Mean Square value calculation
        float rmsvalue = 0.0f;
        for (int i = 0; i < _sampleWindow; i++)
        {
           rmsvalue += waveData[i] * waveData[i];
        }
        rmsvalue = Mathf.Sqrt(rmsvalue / _sampleWindow);

        float decibels = 20 * Mathf.Log10(rmsvalue / 0.01f);

        return rmsvalue;
    }

    //Normalización multiplicado por 1/max del array
    void Normalize(float [] arr)
    {
        //Normalización de los datos
        float max = arr.Max();

        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = arr[i] * (1 / max);
        }
    }

    void Update()
    {
        if (!collect) return;
        float aux = -100f;
        if (Time.realtimeSinceStartup - actTime > step)
        {
            aux = getLoudness();
            results.Add(aux);
           // Debug.Log(aux);
            if(DataManager.instance != null)DataManager.instance.AddSound(aux);
            actTime = Time.realtimeSinceStartup;
        }
    }
}
