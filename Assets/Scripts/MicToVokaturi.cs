using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IronPython;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System.IO;
using System;

public class MicToVokaturi : MonoBehaviour
{
    static ScriptEngine pyEngine = null;
    dynamic vokaWrapper;
    AudioClip c;
    double secs;
    bool cs = false;
    // Start is called before the first frame update
    void Start()
    {
        pyEngine = Python.CreateEngine();
        setSearchPaths();


        dynamic py = pyEngine.ExecuteFile(Application.dataPath + "/Vokaturi_Python/Python/Prueba.py");
        vokaWrapper = py.vokaNetWrapper(Application.dataPath+ "/DLL/OpenVokaturi-3-0-win64.dll");

        Debug.Log("Started recording");
        secs = Time.realtimeSinceStartup;
        c = Microphone.Start(Microphone.devices[0], false, 10, 44100);
        Debug.Log("Recording from: " + Microphone.devices[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.realtimeSinceStartup - secs) > 4)
        {
            Debug.Log("finished");

            float[] data = new float[c.channels * c.samples];
            List<string> dataS = new List<string>();

            for (int i = 0; i < data.Length; i++)
                dataS.Add(data[i].ToString());

            bool success =  c.GetData(data, 0);

            double[] doubleArray = Array.ConvertAll(data, x => (double)x);

            if (!cs)
            {
                for(int i = 0; i < doubleArray.Length/4; i++)
                {
                    Debug.Log(doubleArray[i]);
                }
            }
            
            

            if (success)
            {
                Debug.Log("Data copied");
                dynamic result = vokaWrapper.vokalculate(doubleArray);

                if (result["Success"])
                {
                    Debug.Log(result["Neutral"]);
                    Debug.Log(result["Happy"]);
                    Debug.Log(result["Sad"]);
                    Debug.Log(result["Angry"]);
                    Debug.Log(result["Fear"]);
                    Debug.Log(result["Error"]);

                }
                else
                {
                    Debug.Log(result["Error"]);
                }
                
            }
            else
            {
                Debug.Log("not coolio");
            }
            c = Microphone.Start(Microphone.devices[0], false, 10, 44100);
            secs = Time.realtimeSinceStartup;
        }
    }
    private double [] TestArray (int size)
    {
        var z = new double[size];

        for(int i = 0; i < size; i++)
        {
            z[i] = Mathf.Sin(i);
        }

        return z;
    }

    private void setSearchPaths()
    {
        ICollection<string> searchPaths = pyEngine.GetSearchPaths();
        searchPaths.Add(Application.dataPath+ "/Vokaturi_Python/Python");
        searchPaths.Add(Application.dataPath+ "/Vokaturi_Python/Python/Lib");
        pyEngine.SetSearchPaths(searchPaths);
    }
}
