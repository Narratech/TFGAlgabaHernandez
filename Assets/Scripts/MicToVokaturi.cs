using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IronPython;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System.IO;
using UnityEngine.Audio;
using System;

public class MicToVokaturi : MonoBehaviour
{
   
    static ScriptEngine pyEngine = null;
    dynamic vokaWrapper;

    public AudioClip audioClip = null;

    double actTime;
    public int SampleRate = 44100;
    public int ClipLength = 1;


    public int selectedMic = 0;
    private string activeMic;
    private bool collect = false;
    private int window = -1;

    List<float> results;

    // Start is called before the first frame update
    void Start()
    {
        pyEngine = Python.CreateEngine();
        setSearchPaths();


        dynamic py = pyEngine.ExecuteFile(Application.dataPath + "/Vokaturi_Python/Python/VokaWrapper.py");
        vokaWrapper = py.vokaNetWrapper(Application.dataPath+ "/DLL/OpenVokaturi-3-0-win64.dll");

        activeMic = Microphone.devices[selectedMic];
        Debug.Log("Selected microphone: " + activeMic);

        window = ClipLength * SampleRate;


    }
    public void StartCollecting()
    {
        results = new List<float>();
        collect = true;

        Debug.Log("Started recording");
        audioClip = Microphone.Start(activeMic, true, 999, SampleRate);
        actTime = Time.realtimeSinceStartup;

    }

    public void StopCollecting()
    {
        collect = false;
        if (Microphone.IsRecording(activeMic)) Microphone.End(activeMic);
    }

    public float[] GetData()
    {
        return results.ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (!collect) return;
        if ((Time.realtimeSinceStartup - actTime) > ClipLength+1)
        {
            //Get the data from the microphone
            float[] data = new float[window];
            int micPosition = Microphone.GetPosition(activeMic);
            int position = Microphone.GetPosition(activeMic) - (window + 1);

            bool success =  audioClip.GetData(data, position);

            float z = data[0];
            //Parse it to double so python can use it
            double[] doubleArray = Array.ConvertAll(data, x => (double)x);

            if (success)
            {
                Debug.Log("Data copied");
                dynamic result = vokaWrapper.vokalculate(doubleArray, SampleRate);

                if (result["Success"])
                {
                    Debug.Log("Neutrality: "+result["Neutral"]);
                    Debug.Log("Happiness: " + result["Happy"]);
                    Debug.Log("Sadness: " + result["Sad"]);
                    Debug.Log("Anger: " + result["Angry"]);
                    Debug.Log("Fear: " + result["Fear"]);
                    Debug.Log("Error msg: "+ result["Error"]);

                    float neutral = (float)result["Neutral"];
                    float happy = (float)result["Happy"];
                    float sad = (float)result["Sad"];
                    float angry = (float)result["Angry"];
                    float fear = (float)result["Fear"];
                      

                    results.Add(neutral);
                    results.Add(happy);
                    results.Add(sad);
                    results.Add(angry);
                    results.Add(fear);

                }
                else
                {

                    results.Add(-1);
                    results.Add(-1);
                    results.Add(-1);
                    results.Add(-1);
                    results.Add(-1);
                    Debug.Log(result["Error"]);
                }
            }
            else
            {
                Debug.Log("Something went wrong while copying the data ");
                results.Add(-1);
                results.Add(-1);
                results.Add(-1);
                results.Add(-1);
                results.Add(-1);
            }
            actTime = Time.realtimeSinceStartup;
        }
    }

    /*
        Function used to set the search paths for python to find dependencies 
    */
    private void setSearchPaths()
    {
        ICollection<string> searchPaths = pyEngine.GetSearchPaths();
        searchPaths.Add(Application.dataPath+ "/Vokaturi_Python/Python");
        searchPaths.Add(Application.dataPath+ "/Vokaturi_Python/Python/Lib");
        pyEngine.SetSearchPaths(searchPaths);
    }
}
