using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displaySpeedRunTImes : MonoBehaviour
{

    public TMP_Text panel;

    // Start is called before the first frame update
    void Start()
    {
        string concatedTimes = "Your Times \n";

        int i = 0;
        foreach(double time in GameStats.runTimes)
        {
            i++;
            double roundedTime = System.Math.Round(time, 3);
            concatedTimes = concatedTimes + "Level " + i.ToString() + ": " + roundedTime.ToString() + "\n";
        }

        panel.text = concatedTimes;
    }
    
}
