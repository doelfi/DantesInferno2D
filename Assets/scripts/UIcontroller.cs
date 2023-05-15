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
}
