using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class MicTestScript : MonoBehaviour
{
    // Start is called before the first frame update

    AudioSource audioSrc;
    public GameObject CounterPanel;
    string _micDevice;
    public float testTime = 2;
    public static float[] sampleTaken;

    float maxAmplSilence;
    bool silenceChecked;

    void Start()
    {
        silenceChecked = false;
        maxAmplSilence = 1.0f;
        audioSrc = GetComponent<AudioSource>();
        foreach(var mic in Microphone.devices)
        {
            Debug.Log("Microfono: " + mic);
        }

        _micDevice = Microphone.devices[0].ToString();
        Debug.Log("Recording from: " + _micDevice);
        Debug.Log("Mic Started");
        sampleTaken = new float[64];




    }
    public void minuteTest()
    {
        if (!silenceChecked) Debug.LogError("Silence Test was not taken.");
        Debug.Log("Starting minute test");
        StartCoroutine(SpeakCheckCoroutine());
    }
    public void calculateSilence()
    {
        Debug.Log("Measuring silence");
        StartCoroutine(SoundCoroutine());
        silenceChecked = true;
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    void getSpectrumAudio()
    {
        audioSrc.GetSpectrumData(sampleTaken, 0, FFTWindow.BlackmanHarris);
    }

    float getAverageValueFromSpectrum()
    {
        getSpectrumAudio();

        float acum = 0.0f;
        foreach (var val in sampleTaken)
        {
            acum += val;
        }
        acum /= sampleTaken.Length;
        return acum;

    }

    private IEnumerator SoundCoroutine()
    {
        CounterPanel.SetActive(true);
        CounterPanel.GetComponentInChildren<Text>().text = ((int)testTime).ToString();
        audioSrc.clip = Microphone.Start(_micDevice, true, 1, 44100);
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSrc.loop = true;
        audioSrc.Play();

        float startTime = Time.realtimeSinceStartup;
        float max = getAverageValueFromSpectrum();
        while(Time.realtimeSinceStartup - startTime < testTime)
        {
            if (getAverageValueFromSpectrum() > max) max = getAverageValueFromSpectrum();

            float countdownState = (testTime - (Time.realtimeSinceStartup - startTime))+1;
            CounterPanel.GetComponentInChildren<Text>().text = ((int)countdownState).ToString();
            yield return new WaitForSecondsRealtime(0.1f);
        }

        maxAmplSilence = max;
        Debug.Log("Max was found to be: " + max.ToString());
        Microphone.End(_micDevice);
        audioSrc.Stop();
        CounterPanel.SetActive(false);
        yield return null;
    }
    
    private IEnumerator SpeakCheckCoroutine()
    {
        CounterPanel.SetActive(true);
        CounterPanel.GetComponentInChildren<Text>().text = ((int)testTime).ToString();
        audioSrc.clip = Microphone.Start(_micDevice, true, 1, 44100);
        while (!(Microphone.GetPosition(null) > 0)) { }
        audioSrc.loop = true;
        audioSrc.Play();



        float startTime = Time.realtimeSinceStartup;
        float timeNotSpoken = 0.0f;
        while (Time.realtimeSinceStartup - startTime < 15)
        {


            if (getAverageValueFromSpectrum() < maxAmplSilence) timeNotSpoken += 0.1f;

            float countdownState = (15 - (Time.realtimeSinceStartup - startTime)) + 1;
            CounterPanel.GetComponentInChildren<Text>().text = ((int)countdownState).ToString();
            yield return new WaitForSecondsRealtime(0.1f);
        }


        Debug.Log("You spoke for " + (15 - timeNotSpoken).ToString() + "seconds");
        Microphone.End(_micDevice);
        audioSrc.Stop();
        CounterPanel.SetActive(false);



    }






}
