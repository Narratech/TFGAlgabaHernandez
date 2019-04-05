using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeManager : MonoBehaviour
{
    DataManager DT;
    int[] frames;
    float[] mirada;
    public float refreshtime = 1;

    // Start is called before the first frame update
    void Start()
    {
        DT = DataManager.instance;
        frames = new int[] { 0, 0, 0 };
        mirada = new float[] { 0.0f, 0.0f, 0.0f };
        StartCoroutine("WriteData");

    }

    IEnumerator WriteData()
    {
        Debug.Log("Hola1");

        for (int i = 0; i < 600; i++)//puentesito
        {




            DT.addGaze(mirada[0] * 100 / frames[0], mirada[1] * 100 / frames[1], mirada[2] * 100 / frames[2]);
            //reseting to take new percentajes
            frames[0] = frames[1] = frames[2] = 0;
            mirada[0] = mirada[1] = mirada[2] = 0;
            //Debug.Log(" ID : " + id + " " + "Velocity X " + velocityx + " Velocity Y " + velocityy);
            yield return new WaitForSeconds(refreshtime);

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void frameLooking(int player)
    {
     
            frames[player]++;
            mirada[player] += 1.0f;
        
    }

    public void frameNotLooking(int player)
    {
            frames[player]++;
        
    }
}
