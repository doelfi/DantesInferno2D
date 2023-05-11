using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class UIcontroller : MonoBehaviour
{

    public TMP_Text panel;

    // FIXME: I don't know why but this does not get called on start up
    // My current fix is to call the updateUI() function from the start of the player

    // maybe you need to have a public void?? 
    void start()
    {
        panel.text = "Lifes: " + GameStats.lifes.ToString() + " Coins: " + GameStats.coins.ToString();
    }

    public void updateUI()
    {
        panel.text = "Lifes: " + GameStats.lifes.ToString() + " Coins: " + GameStats.coins.ToString();
    }

    public void Tutorial()
    {
        panel.text = "Start moving forward with the right arrow or d"; 
        // if (Input.GetKeyDown("d") || Input.GetKeyDown("right")) 
        if (Input.GetKeyDown("d")) 
        {   
            panel.text = "Nice. Get used to jumping. Press space.";
            if (Input.GetKeyDown("space"))
            {
                panel.text = "Well done. Now be careful with spikes. Jump over it or loose a life.";
                if (transform.position.x > 10) 
                {
                    panel.text = "Good job. Finish the level by touching the plastic flag.";
                }
            }
        }
    }
}
