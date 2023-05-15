using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialUI : MonoBehaviour
{
    public TMP_Text panel;

    void Start()
    {
        Tutorial("Start moving with the arrows and jump with space"); 
    }

    public void Tutorial(string message)
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
