using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro : MonoBehaviour
{
    public string[] frases;
    int contador = 0;
    public TextMesh text;
    DataManager DT;

    // Start is called before the first frame update
    void Start()
    {
        text.text = frases[0];
        DT = DataManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            contador++;

            if (contador >= frases.Length)
            {
                Finish();
            }
            if(contador < frases.Length)
                text.text = frases[contador];
        }
    }
    void Finish()
    {
        DT.setStart(true);
       gameObject.SetActive(false);
        Debug.Log("Finished introduction");
    }
}
