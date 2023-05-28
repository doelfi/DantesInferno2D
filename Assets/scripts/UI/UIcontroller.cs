using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using TMPro;

public class UIcontroller : MonoBehaviour
{
    // Displays the stats (lives and coins) and game ending on the screen.

    public TMP_Text panel;
    public TMP_Text gameOver_panel;
    public TMP_Text victory_panel;
    
    void Start()
    {
        panel.text = "Lives: " + GameStats.lifes.ToString() + " Coins: " + GameStats.coins.ToString();
    }

    public void updateUI()
    {
        // Updates the UI if the Player loses or gains a life/ coins
        panel.text = "Lives: " + GameStats.lifes.ToString() + " Coins: " + GameStats.coins.ToString();
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
