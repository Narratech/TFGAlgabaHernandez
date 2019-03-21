using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IronPython;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;


public class MicToVokaturi : MonoBehaviour
{
    static ScriptEngine pyEngine = null;
    dynamic vokaWrapper;
    AudioClip c;
    double secs;
    // Start is called before the first frame update
    void Start()
    {
        pyEngine = Python.CreateEngine();
        setSearchPaths();


        dynamic py = pyEngine.ExecuteFile(Application.dataPath + "/Vokaturi_Python/Python/Prueba.py");
        vokaWrapper = py.vokaNetWrapper(Application.dataPath+ "/DLL/OpenVokaturi-3-0-win32.dll");

        Debug.Log("Started recording");
        secs = Time.realtimeSinceStartup;
        AudioClip c = Microphone.Start(Microphone.devices[0], false, 10, 44100);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Time.realtimeSinceStartup - secs) > 11)
        {
            Debug.Log("finished");

            float[] data = new float[c.channels * c.samples];
            bool cool = c.GetData(data, 0);

            if (cool)
            {
                Debug.Log("COolio");
                dynamic result = vokaWrapper.vokalculate(data);
                Debug.Log(result["Neutral"]);
                Debug.Log(result["Happy"]);
                Debug.Log(result["Sad"]);
                Debug.Log(result["Angry"]);
                Debug.Log(result["Fear"]);
                
            }
            else
            {
                Debug.Log("not coolio");
            }
        }
    }

    private void setSearchPaths()
    {
        ICollection<string> searchPaths = pyEngine.GetSearchPaths();
        searchPaths.Add(Application.dataPath+ "/Vokaturi_Python/Python");
        searchPaths.Add(Application.dataPath+ "/Vokaturi_Python/Python/Lib");
        pyEngine.SetSearchPaths(searchPaths);
    }
}
