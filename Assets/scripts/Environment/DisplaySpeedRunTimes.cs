using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DisplaySpeedRunTimes : MonoBehaviour
{
    // Displays the times that were spend in each level at the end of the game
    // (either if Player dies or if the game is succesfully finished).
    public TMP_Text panel;

    void Start()
    {
        // Counting trough the runTimes and adding the times to a string that is displayed
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

    public void OnMainPress()
    {
        // Loads the Scene where the times are displayed.
        SceneManager.LoadScene(0);
    }
    
}
