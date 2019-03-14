using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.SpeechToText.v1;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.DataTypes;

using IBM.Watson.DeveloperCloud.Services.ToneAnalyzer.v3;
using IBM.Watson.DeveloperCloud.Connection;

public class IBMApiTest : MonoBehaviour
{
    private string TA_username = "manherna@ucm.es";
    private string stt_username = "manherna@ucm.es";
    private string TA_password = "m4nh3rn4";
    private string stt_password = "m4nh3rn4";


    private string _conversationVersionDate = "";
    private string convo_workspaceID;

    ToneAnalyzer _toneAnalyzer;
    SpeechToText _speechToText;

    AudioClip clip;
    public string selectedMic = "";

    // Start is called before the first frame update
    void Start()
    {
        LogSystem.InstallDefaultReactors();

        Credentials credentials_stt = new Credentials(stt_username, stt_password,
            "https://stream.watsonplatform.net/speech-to-text/api");
        Credentials credentials_TA = new Credentials(TA_username, TA_password,
            "https://stream.watsonplatform.net/tone-analyzer/api");
        _speechToText = new SpeechToText(credentials_stt);
        _speechToText.StreamMultipart = true;
        _toneAnalyzer = new ToneAnalyzer(credentials_TA);


        foreach (string mic in Microphone.devices)
            Debug.Log(mic);



        if (selectedMic == "") selectedMic = Microphone.devices[0];
        clip = Microphone.Start(selectedMic, true, 10, 44100);


    }

    // Update is called once per frame
    void Update()
    {



    }
}
