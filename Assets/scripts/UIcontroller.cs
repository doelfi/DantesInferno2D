using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class UIcontroller : MonoBehaviour
{

    public TMP_Text panel;

    public void updateUI(int lifes, int coins)
    {
        panel.text = "Lifes: " + lifes.ToString() + " Coins: " + coins.ToString();
    }
}
