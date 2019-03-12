using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance = null;
    float timer = 10.0f;
    bool finished = false;
    public TextMesh contador;
    int characters = 0;
    int[] frames;
    float[] mirada;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        frames = new int[] { 0,0,0 };
        mirada = new float[] { 0.0f, 0.0f, 0.0f};
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            finished = true;
            contador.text = " " + mirada[0] * 100 / frames[0] + "%" + " " + mirada[1]*100/frames[1]+ "%" + " " + mirada[2] * 100 / frames[2] + "%";
        }
        else
        {
            contador.text = ""+timer;
        }
     //   Debug.Log(timer);
        
    }

 

    public void frameLooking(int player)
    {
        if (!finished)
        {
            frames[player]++;
            mirada[player] += 1.0f;
        }
    }
    public void frameNotLooking(int player)
    {
        if (!finished)
        {
            frames[player]++;
        }
    }
}
