using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    SaveDataManager SD;
    float timer = 10.0f;
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



    //vokaturi
    void Awake()
    {
        if (instance == null)
            instance = this;
       
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
               
                SD.SaveData("Gazes", Gazes.ToArray());
                SD.SaveData("LeftHand", Lhand.ToArray());
                SD.SaveData("RightHand", Lhand.ToArray());
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
            Gazes.Add(a);
            Gazes.Add(b);
            Gazes.Add(c);
        }
    }



    //Hands
    public void AddHandData(string hand,float x, float y)
    {

        if (!finished && started)
        {
            if (hand.ToLower() == "rigth")
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


    //Vokaturi


}
