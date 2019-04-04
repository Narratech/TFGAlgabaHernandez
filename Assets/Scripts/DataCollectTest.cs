using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataCollectTest : MonoBehaviour
{
    public MicToVokaturi MicToVokaturi;
    public SoundLoudness soundLoudness;

    // Start is called before the first frame update
    void Start()
    {
        MicToVokaturi = gameObject.GetComponent<MicToVokaturi>() as MicToVokaturi;
        soundLoudness = gameObject.GetComponent<SoundLoudness>() as SoundLoudness;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startCollecting()
    {
        MicToVokaturi.StartCollecting();
        soundLoudness.StartCollecting();

    }
    public void stopCollecting()
    {
        MicToVokaturi.StopCollecting();
        var datav = MicToVokaturi.GetData();

        soundLoudness.StopCollecting();
        var data = soundLoudness.GetData();

        string outp = "Vokaturi: \n";
        for(int i = 0; i < datav.Length; i++)
        {
            outp += datav[i] + " ";
        }
        outp += "Loudness: ";
        for (int i = 0; i < data.Length/4; i++)
        {
            outp += data[i] + " ";
        }
        Debug.Log(outp);
    }
}
