using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PRCS : MonoBehaviour
{
    string[] questions;
    int position = 0;
    int[] answers;

    int active = 0;
    float lastA = 0;

    float time;

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
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            addAnswer(active);
        }

        if (time > 0) time -= Time.deltaTime;
        else
        {
            if (aux > 0.3f)
            {
                active++;
                time += 0.3f;
                if (active == 6) active = 0;
            }
            else if (aux < -0.3f)
                active--;
            time += 0.3f;


            if (active == -1) active = 5;

        }
    


    
        DrawData();
    }
    public void DrawData()
    {
        Debug.Log(questions[position]);
        Debug.Log(active);

    
    }

    public void addAnswer(int number)
    {
        answers[position] = number;
        position++;

        if(position >= 12)
        {
            finish();
        }

    }
    void finish()
    {
        Debug.Log("Finished questiosns");
    }

}
