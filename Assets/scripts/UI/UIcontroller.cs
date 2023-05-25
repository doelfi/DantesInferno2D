using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class UIcontroller : MonoBehaviour
{
    public TMP_Text panel;
    public TMP_Text gameOver_panel;
    public TMP_Text victory_panel;
    
    void Start()
    {
        panel.text = "Lifes: " + GameStats.lifes.ToString() + " Coins: " + GameStats.coins.ToString();
    }

    public void updateUI()
    {
        panel.text = "Lifes: " + GameStats.lifes.ToString() + " Coins: " + GameStats.coins.ToString();
    }

    public void gameOverUI()
    {
        gameOver_panel.text = "Game Over"; 
    } 

    public void Victory()
    {
        gameOver_panel.text = "You win";
    }
}
