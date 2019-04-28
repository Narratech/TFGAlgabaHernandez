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
    float lastA = 0;

    float time;

    public TextMesh Answer;
    public TextMesh Question;
    public TextMesh Numero;



    // Start is called before the first frame update
    void Start()
    {
        questions = new string[12];
        answers = new int[12];
        questions[0] = "Cuando hablo delante de un auditorio, los pensamientos se me confunden y mezclan.";
        questions[1] = "No tengo miedo de estar enfrente del público";
        questions[2] = "Aunque estoy nervioso / a justo antes de ponerme de pie, pronto olvido mis temores y disfruto de la experiencia";
        questions[3] = "Afronto con completa confianza la perspectiva de dar una charla";
        questions[4] = "Creo que estoy en completa posesión de mí mismo / a mientras hablo";
        questions[5] = "Aunque hablo con fluidez con mis amigos /as, no encuentro palabras para expresarme en la tarima";
        questions[6] = "Me siento relajado / a y a gusto mientras hablo";
        questions[7] = "Siempre que me es posible, evito hablar en público";
        questions[8] = "Mi mente está clara cuando me encuentro delante de un auditorio";
        questions[9] = "Mi postura parece forzada y poco natural";
        questions[10] = "Tengo miedo y estoy tenso / a todo el tiempo que estoy hablando delante de un grupo de gente";
        questions[11] = "Me siento aterrorizado / a ante la idea de hablar delante de un grupo de personas";

    }

    // Update is called once per frame
    void Update()
    {
        float aux = Input.GetAxis("Horizontal");
       

        if (time > 0) time -= Time.deltaTime;
        else
        {
            if(Input.GetAxis("Vertical")< -0.1f || Input.GetKey(KeyCode.KeypadEnter))
            {
                time += 0.3f;
                addAnswer(active);
            }
            else if (aux > 0.3f)
            {
                active++;
                time += 0.3f;
                if (active == 7) active = 1;
            }
            else if (aux < -0.3f)
                active--;
            time += 0.3f;


            if (active == 0) active = 6;

        }
    


    
        DrawData();
    }
    public void DrawData()
    {
        Answer.text = active.ToString();
        Question.text = questions[position];
        Numero.text = "Pregunta " + (position +1) ;


       //  Debug.Log(active);
    }

    public void addAnswer(int number)
    {
        
        answers[position] = number;
        position++;
        DT.AddAnswer(number);

        if(position >= 12)
        {
            finish();
        }

    }
    void finish()
    {
        DT.SaveAnswers();
        Debug.Log("Finished questiosns");
    }

}
