using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class UIcontroller : MonoBehaviour
{

    public TMP_Text panel;

    public void updateUI()
    {
        panel.text = "Lifes: " + GameStats.lifes.ToString() + " Coins: " + GameStats.coins.ToString();
    }
}
