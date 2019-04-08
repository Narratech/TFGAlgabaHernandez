using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    public GameObject Preguntas;
    SaveDataManager SD;
    float timer = 15.0f;
    bool finished = false;
    bool started = true;
    public TextMesh contador;
    int characters = 0;

    //Characters
    List<float> Gazes;

    //Hands
    List<float> Lhand;
    List<float> Rhand;

    //Sound
    List<float> Sound;


    //vokaturi
    List<float> Vokaturi;
    //Preguntas
    List<float> Answers;
    void Awake()
    {
        if (instance == null)
            instance = this;
        Gazes = new List<float>();
        Lhand = new List<float>();
        Rhand = new List<float>();
        Answers = new List<float>();
        Sound = new List<float>();
        Vokaturi = new List<float>();

    }

    // Start is called before the first frame update
    void Start()
    {
        SD = SaveDataManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (started && !finished)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                finished = true;
                SD.Test();
               // float[] aux = new float[7];
                SD.SaveData("Gazes", Gazes);
                SD.SaveData("LeftHand", Lhand);
                SD.SaveData("RightHand", Rhand);
                SD.SaveData("Sound", Sound);
                SD.SaveData("Vokaturi", Vokaturi);
                //  contador.text = " " + mirada[0] * 100 / frames[0] + "%" + " " + mirada[1]*100/frames[1]+ "%" + " " + mirada[2] * 100 / frames[2] + "%";
            }
            else
            {
                contador.text = "" + timer;
            }
            //   Debug.Log(timer);
        }
    }

    //Gaze

   public void addGaze( float a, float b, float c)
    {
        if (!finished && started)
        {
            Debug.Log("Adding gaze");
            Gazes.Add(a);
            Gazes.Add(b);
            Gazes.Add(c);
        }
    }



    //Hands
    public void AddHandData(string hand,float x, float y)
    {
        Debug.Log("ADddingHand");

        if (!finished && started)
        {
            if (hand.ToLower() == "right")
            {
                Rhand.Add(x);
                Rhand.Add(y);
            }
            else
            {
                Lhand.Add(x);
                Lhand.Add(y);

            }
        }
    }


    //Sound
    public void AddSound(float x)
    {
        if (!finished && started)
        {
            Sound.Add(x);
        }
    }

    //Vokaturi
    public void AddVokaturi(float neutral,float happy,float sad,float angry,float fear)
    {
        if (!finished && started)
        {
            Debug.Log("VOkaturi added");
            Vokaturi.Add(neutral);
            Vokaturi.Add(happy);
            Vokaturi.Add(sad);
            Vokaturi.Add(angry);
            Vokaturi.Add(fear);
        }
    }

    //Preguntas
    public void AddAnswer(int x)
    {
        Answers.Add((float)x);
    }
    public void SaveAnswers()
    {
        SD.SaveData("Answers", Answers);
    }

}
