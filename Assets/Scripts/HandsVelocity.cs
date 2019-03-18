using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsVelocity : MonoBehaviour
{
    float velocityx;
    float velocityy;
    float positionx;
    float lastx;
    float lasty;
    float positiony;

    //Tiempo de refresco de toma 1 == 1s
    public float refreshtime = 1;

    public int id = 0;

    // Start is called before the first frame update
    void Start()
    {
        velocityy = velocityx = positionx = positiony = lastx = lasty= 0;
        Debug.Log("Hola");
        StartCoroutine("WriteData");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator WriteData()
    {
        Debug.Log("Hola1");

        for (int i = 0; i< 60; i++)//puentesito
        {


            positionx = transform.position.x;
            positiony = transform.position.y;

            velocityx = (positionx - lastx) / refreshtime;
            velocityy = (positiony - lasty) / refreshtime;


            Debug.Log(" ID : " + id + " " + "Velocity X " + velocityx + " Velocity Y " + velocityy);
            lastx = positionx;
            lasty = positiony;
            yield return new WaitForSeconds(refreshtime);

        }
    }
}
