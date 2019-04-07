using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneManager : MonoBehaviour
{
    private static AudioClip _audioClip;
    private static string _device;

    public static AudioClip AudioClip { get => _audioClip; set => _audioClip = value; }
    public static int SAMPLERATE = 88200;
    public int deviceNumber = 0;
 
    private void OnEnable()
    {
        InitMic();
        Debug.Log("Recording from: " + _device);
    }
    private void OnDisable()
    {
        StopMic();
    }
    private void OnDestroy()
    {
        StopMic();
    }
    void InitMic()
    {
        if (_device == null) _device = Microphone.devices[deviceNumber];
       _audioClip = Microphone.Start(_device, true, 999, SAMPLERATE);
    }
    void StopMic()
    {
        Microphone.End(_device);
    }
    public static int GetMicrophonePosition()
    {
        if (_device == null) return -1;
        return Microphone.GetPosition(_device);
    }
}
