using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public TMP_Text panel;

    void Start()
    {
        string start_text = "Start moving with the arrows and jump with space";
        Tutorial(ref start_text); 
    }

    public void Tutorial(ref string message)
    {
        // if (Input.GetKeyDown("d") || Input.GetKeyDown("right")) 
        panel.text = message;
    }
            /* 
            panel.text = "Nice. Get used to jumping. Press space.";
            if (Input.GetKeyDown("space"))
            {
                panel.text = "Well done. Now be careful with spikes. Jump over it or loose a life.";
                if (transform.position.x > 10) 
                    // panel.text = "Good job. Finish the level by touching the plastic flag.";
        */
}
