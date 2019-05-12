using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PRCS : MonoBehaviour
{
    public DataManager DT;
    string[] questions;
    int position = 0;
    int[] answers;

    int active = 1;
    bool was_pressed = false;
    bool finished = false;

    float time;

    public TextMesh Answer;
    public TextMesh Question;
    public TextMesh Numero;
    public TextMesh UpArrow;
    public TextMesh DownArrow;

    public AudioClip ArrowClick, LastQuestionClick, NextQuestionClick;



    // Start is called before the first frame update
    void Start()
    {
        questions = new string[12];
        answers = new int[12];
        questions[0] = "Cuando hablo delante de un auditorio, los pensamientos se me confunden y mezclan.";
        questions[1] = "No tengo miedo de estar enfrente del público";
        questions[2] = "Aunque estoy nervioso/a justo antes de ponerme de pie, \n pronto olvido mis temores y disfruto de la experiencia";
        questions[3] = "Afronto con completa confianza la perspectiva de dar una charla";
        questions[4] = "Creo que estoy en completa posesión de mí mismo/a mientras hablo";
        questions[5] = "Aunque hablo con fluidez con mis amigos/as, no encuentro\n palabras para expresarme en la tarima";
        questions[6] = "Me siento relajado/a y a gusto mientras hablo";
        questions[7] = "Siempre que me es posible, evito hablar en público";
        questions[8] = "Mi mente está clara cuando me encuentro delante de un auditorio";
        questions[9] = "Mi postura parece forzada y poco natural";
        questions[10] = "Tengo miedo y estoy tenso/a todo el tiempo que \n estoy hablando delante de un grupo de gente";
        questions[11] = "Me siento aterrorizado/a ante la idea de \n hablar delante de un grupo de personas";

    }

    // Update is called once per frame
    void Update()
    {

        var aux = Input.GetAxis("Oculus_GearVR_RThumbstickY");

        Debug.Log("Input : " + Input.GetAxis("Oculus_GearVR_RThumbstickY").ToString());
        Debug.Log("Aux: " + aux.ToString());

        if (!finished)
        {

            if (Input.GetButtonDown("Fire1"))
            {
                if (position < questions.Length) answers[position] = active;
                position++;

                if (position >= questions.Length)
                {
                    finish();
                }

                playSound(NextQuestionClick);
            }
            else if (Input.GetButtonDown("Fire2") && (position > 0))
            {
                position--;
                playSound(LastQuestionClick);
            }

            if (aux > 0.8f && !was_pressed && active < 6)
            {
                active = active + 1;
                UpArrow.color = Color.black;
                was_pressed = true;
                playSound(ArrowClick);
            }
            else if (aux < -0.8f && !was_pressed && active > 1)
            {
                active = active - 1;
                was_pressed = true;
                DownArrow.color = Color.black;
                playSound(ArrowClick);
            }
            if (aux > -0.1 && aux < 0.1)
            {
                UpArrow.color = Color.white;
                DownArrow.color = Color.white;
                was_pressed = false;
            }
        }
        DrawData();

       
    }
    void playSound (AudioClip clip)
    {
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
    }
    public void DrawData()
    {
        if(finished)
        {
            DownArrow.text = UpArrow.text = "";
            Answer.text = "";
            Numero.text = "";
            Question.text = "Ha acabado el test. ¡Gracias por participar!";
            Question.color = Color.yellow;
        }

        else if (position < questions.Length)
        {
            Answer.text = active.ToString();
            Question.text = questions[position];
            Numero.text = "Pregunta " + (position + 1);
        }
        


       //  Debug.Log(active);
    }


    void finish()
    {
        foreach (var value in answers)
        {
            DT.AddAnswer(value);
        }
        DT.SaveAnswers();
        Debug.Log("Finished questiosns");
        finished = true;
    }

}
