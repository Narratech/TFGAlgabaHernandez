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

    AudioClip audioClip;

    double secs;
    int CLIPLEN = 10;
    int SAMPLERATE = 44100;

    public int selectedMic = 0;
    private string activeMic;

    // Start is called before the first frame update
    void Start()
    {
        pyEngine = Python.CreateEngine();
        setSearchPaths();


        dynamic py = pyEngine.ExecuteFile(Application.dataPath + "/Vokaturi_Python/Python/VokaWrapper.py");
        vokaWrapper = py.vokaNetWrapper(Application.dataPath+ "/DLL/OpenVokaturi-3-0-win64.dll");

        activeMic = Microphone.devices[selectedMic];
        Debug.Log("Started recording");
        secs = Time.realtimeSinceStartup;

        audioClip = Microphone.Start(activeMic, false, CLIPLEN, SAMPLERATE);
        Debug.Log("Recording from: " + activeMic);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.realtimeSinceStartup - secs) > CLIPLEN)
        {
            Debug.Log("Lap");
            Microphone.End(activeMic);

            //Get the data from the microphone
            float[] data = new float[audioClip.channels * audioClip.samples];
            bool success =  audioClip.GetData(data, 0);

            //Parse it to double so python can use it
            double[] doubleArray = Array.ConvertAll(data, x => (double)x);


            if (success)
            {
                Debug.Log("Data copied");
                dynamic result = vokaWrapper.vokalculate(doubleArray, SAMPLERATE);

                if (result["Success"])
                {
                    Debug.Log("Neutrality: "+result["Neutral"]);
                    Debug.Log("Happiness: " + result["Happy"]);
                    Debug.Log("Sadness: " + result["Sad"]);
                    Debug.Log("Anger: " + result["Angry"]);
                    Debug.Log("Fear: " + result["Fear"]);
                    Debug.Log("Error msg: "+ result["Error"]);



                }
                else
                {
                    Debug.Log(result["Error"]);
                }
            }
            else
            {
                Debug.Log("Something went wrong while copying the data ");
            }
            audioClip = Microphone.Start(activeMic, false, CLIPLEN, SAMPLERATE);
            secs = Time.realtimeSinceStartup;
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
